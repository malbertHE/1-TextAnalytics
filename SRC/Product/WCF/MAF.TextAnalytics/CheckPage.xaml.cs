using System;
using System.Windows.Controls;

namespace MAF.TextAnalytics
{
    /// <summary>
    /// Interaction logic for CheckPage.xaml
    /// </summary>
    public partial class CheckPage : Page
    {
        public CheckPage(Action<string> pCheckOK, Action pCheckCancel)
        {
            InitializeComponent();
            checkOK = pCheckOK;
            checkCancel = pCheckCancel;
        }

        Action<string> checkOK;
        Action checkCancel;

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
			checkOK(tbFile.Text);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            checkCancel();
        }
    }
}
