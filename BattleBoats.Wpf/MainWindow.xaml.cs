using BattleBoats.Wpf.ViewModels;
using MvvmCross.Platforms.Wpf.Views;

namespace BattleBoats.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel(this);
        }
    }
}
