using System;
//using System.Drawing.Drawing2D;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MAF.TextAnalytics
{
	/// <summary>
	/// Interaction logic for InfoPage.xaml
	/// </summary>
	public partial class InfoPage : Page
	{
		public InfoPage(Action pInfoOk)
		{
			InitializeComponent();
			infoOK = pInfoOk;
		}

		internal void SetBackground()
		{//https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/painting-with-solid-colors-and-gradients-overview
			RadialGradientBrush myRadialGradientBrush = new RadialGradientBrush();
			myRadialGradientBrush.GradientOrigin = new Point(0.5, 0.5);
			myRadialGradientBrush.Center = new Point(0.5, 0.5);
			myRadialGradientBrush.RadiusX = 0.5;
			myRadialGradientBrush.RadiusY = 0.5;
			myRadialGradientBrush.GradientStops.Add(
				new GradientStop(Colors.Blue, 0.0));
			myRadialGradientBrush.GradientStops.Add(
				new GradientStop(Colors.Green, 0.20));
			myRadialGradientBrush.GradientStops.Add(
				new GradientStop(Colors.Red, 0.40));
			myRadialGradientBrush.GradientStops.Add(
				new GradientStop(Colors.Orange, 0.60));
			myRadialGradientBrush.GradientStops.Add(
				new GradientStop(Colors.Yellow, 0.80));
			myRadialGradientBrush.GradientStops.Add(
				new GradientStop(Colors.White, 1.0));

			InfoMainGrid.Background = myRadialGradientBrush;

			tbInfo.Foreground = Brushes.White;
			tbInfo.FontSize = 18;
			tbInfo.FontStretch = FontStretches.UltraExpanded;
			tbInfo.FontWeight = FontWeights.UltraBold;
		}

		Action infoOK;

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			infoOK();
		}
	}
}
