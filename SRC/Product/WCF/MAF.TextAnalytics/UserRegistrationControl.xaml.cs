﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextAnalytics
{
	/// <summary>
	/// Interaction logic for UserRegistrationControl.xaml
	/// </summary>
	public partial class UserRegistrationControl : UserControl
	{
		public UserRegistrationControl()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Visibility = Visibility.Hidden;
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{

		}
	}
}
