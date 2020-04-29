using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace MAF.TextAnalytics
{
	/// <summary>
	/// Interaction logic for DataPage.xaml
	/// </summary>
	public partial class DataPage : Page
	{
		public DataPage(string pFile, Action pNewCheck, Action<XElement> pItems)
		{
			InitializeComponent();
			file = pFile;
			newCheck = pNewCheck;
			items = pItems;
		}

		string file;
		Action newCheck;
		Action<XElement> items;

		void Page_Loaded(object sender, RoutedEventArgs e)
		{
			XElement result = XElement.Load(file);
			StatisticsGrid.DataContext = result;
		}

		private void StatisticsGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (StatisticsGrid.SelectedIndex == -1)
				return;

			XElement dr = (XElement)StatisticsGrid.SelectedItem;

			items(dr);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			newCheck();
		}
	}
}
