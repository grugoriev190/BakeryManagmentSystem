using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
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
using System.Windows.Shapes;

namespace BakeryManagmentSystem
{
	/// <summary>
	/// Interaction logic for ChangeProductWindow.xaml
	/// </summary>
	public partial class ChangeProductWindow : Window
	{
		public ChangeProductWindow()
		{
			InitializeComponent();
		}

		private void ChangeProductButton_Click(object sender, RoutedEventArgs e)
		{
			string productName = ProductNameTextBox.Text;
			if (decimal.TryParse(ChangedProductWeightTextBox.Text, out decimal newProductWeight) &&
				decimal.TryParse(ChangedProductPriceTextBox.Text, out decimal newProductPrice))
			{
				DbHelper.UpdateProductInfo(productName, newProductWeight, newProductPrice);
			}
			else
			{
				MessageBox.Show("Please enter valid weight and price.");
			}
		}
	}
}
