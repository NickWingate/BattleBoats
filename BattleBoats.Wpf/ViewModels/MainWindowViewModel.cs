using BattleBoats.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
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
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };
        }
        private Window _window;

        // Thickness that user can resize the window
        public int ResizeBorder { get; set; } = 3;
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);

        // Outer margin(transparent, for drop shadow)
        private int _outerMarginSize = 10;
        public int OuterMarginSize
        {
            get
            {
                // if window is maximized we dont want outer margin(for drop shadow)
                return _window.WindowState == WindowState.Maximized ? 0 : _outerMarginSize;
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
                return _window.WindowState == WindowState.Maximized ? 0 : _windowRadius;
            }
            set { _windowRadius = value; }
        }
        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);

        // Height of title bar
        public int TitleHeight { get; set; } = 30;
        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);
    }
}
