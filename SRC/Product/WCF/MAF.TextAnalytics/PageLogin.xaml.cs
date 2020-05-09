using System;
using System.Windows;
using System.Windows.Controls;

namespace MAF.TextAnalytics
{
	/// <summary>
	/// Interaction logic for Page1.xaml
	/// </summary>
	public partial class PageLogin : Page
	{
		public PageLogin(Action<string, string> pLoginOKAction, Action pLoginCancel, Action pRegistrationAction)
		{
			InitializeComponent();
			loginOKAction = pLoginOKAction;
			registrationAction = pRegistrationAction;
            loginCancel = pLoginCancel;
        }

		Action<string, string> loginOKAction;
		Action registrationAction;
        Action loginCancel;

        private void Button_Click(object sender, RoutedEventArgs e)
		{
            loginCancel();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			loginOKAction(tbLoginName.Text, pbPass.Password);
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			registrationAction();
		}
	}
}
