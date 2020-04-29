using System;
using System.Windows;
using System.Windows.Controls;


namespace MAF.TextAnalytics
{
    /// <summary>
    /// Interaction logic for UserRegistrationPage.xaml
    /// </summary>
    public partial class UserRegistrationPage : Page
    {
        public UserRegistrationPage(Action<string, string, string, string> pRegistrationOKAction, Action pRegistrationCancel)
        {
            InitializeComponent();
            registrationOKAction = pRegistrationOKAction;
            registrationCancel = pRegistrationCancel;
        }

        Action<string, string, string, string> registrationOKAction;
        Action registrationCancel;

        void Button_Click(object sender, RoutedEventArgs e)
        {
            registrationCancel();
        }

        void Button_Click_1(object sender, RoutedEventArgs e)
        {
            registrationOKAction(tbLoginName.Text, tbName.Text, pbPass.Password, pbPass2.Password);
        }
    }
}
