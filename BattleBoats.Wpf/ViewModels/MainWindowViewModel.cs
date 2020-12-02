using BattleBoats.Wpf.ViewModels.Base;
using BattleBoats.Wpf.WindowHelpers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace BattleBoats.Wpf.ViewModels

{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel(Window window)
        {
            _window = window;
            window.StateChanged += (sender, e) =>
            {
                WindowResized();
            };

            MinimizeCommand = new RelayCommand(() => _window.WindowState = WindowState.Minimized);
            // ^= is xor, so either 0 or 2 which is normal or maximized
            MaximizeCommand = new RelayCommand(() => _window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => _window.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(_window, GetMousePosition()));

            // Fix window resize issue
            var resizer = new WindowResizer(_window);

            // Listen out for dock changes
            resizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                _dockPosition = dock;

                // Fire off resize events
                WindowResized();
            };
        }

        private void WindowResized()
        {
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }

        private Window _window;

        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand MenuCommand { get; set; }


        // Thickness that user can resize the window
        public int ResizeBorder { get; set; } = 6;
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);

        // Outer margin(transparent, for drop shadow)
        private int _outerMarginSize = 10;
        public int OuterMarginSize
        {
            get
            {
                // if window is maximized we dont want outer margin(for drop shadow)
                return Borderless ? 0 : _outerMarginSize;
            }
            set { _outerMarginSize = value; }
        }
        public Thickness OuterMarginSizeThickness => new Thickness(OuterMarginSize);

        // Radius of rounded corners
        private int _windowRadius = 10;
        public int WindowRadius
        {
            get
            {
                // if window is maximized we dont want radius
                return Borderless ? 0 : _windowRadius;
            }
            set { _windowRadius = value; }
        }
        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);

        public bool Borderless { get { return (_window.WindowState == WindowState.Maximized || _dockPosition != WindowDockPosition.Undocked); } }

        // Height of title bar
        public int TitleHeight { get; set; } = 50;
        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);
        private WindowDockPosition _dockPosition = WindowDockPosition.Undocked;
        private Point GetMousePosition()
        {
            Point position = Mouse.GetPosition(_window);
            return new Point(position.X + _window.Left, position.Y + _window.Top);
        }

    }
}
