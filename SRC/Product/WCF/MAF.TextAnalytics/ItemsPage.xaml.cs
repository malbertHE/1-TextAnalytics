using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Xml.Linq;

namespace MAF.TextAnalytics
{
	/// <summary>
	/// Interaction logic for ItemsPage.xaml
	/// </summary>
	public partial class ItemsPage : Page
	{
		public ItemsPage(XElement pData, Action pOKAction)
		{
			InitializeComponent();
			Refresh(pData);
			okAction = pOKAction;
		}

		public void Refresh(XElement pData)
		{
			IEnumerable<XElement> xe = pData.Elements("ItemList");
			StatisticsGrid.DataContext = xe;
		}

		Action okAction;

		void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			okAction();
		}
	}
}
