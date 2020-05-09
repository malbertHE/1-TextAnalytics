using System.Windows;
using System.Windows.Controls;

namespace MAF.Entropy.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            f = (Frame)Content;
        }

        internal void SetBase()
        {
            Content = f;
        }

        readonly Frame f;

    }
}
