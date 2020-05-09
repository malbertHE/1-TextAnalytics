using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace MAF.Entropy.WPF
{
    /// <summary>
    /// Interaction logic for Items.xaml
    /// </summary>
    public partial class Items : Page
    {

        /// <summary>A tétel lap konstruktora, aminek meg kell adni, a megjelenítendő tétel adatokat.</summary>
        /// <param name="pData">A megjelenítendő tétel adatok.</param>
        public Items(XElement pData)
        {
            InitializeComponent();
            IEnumerable<XElement> xe = pData.Elements("ItemList");
            StatisticsGrid.DataContext = xe;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).SetBase();
        }
    }
}
