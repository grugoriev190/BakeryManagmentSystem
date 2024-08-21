using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
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
	/// Interaction logic for AddProductWindow.xaml
	/// </summary>
	public partial class AddProductWindow : Window
	{
		public AddProductWindow()
		{
			InitializeComponent();
		}

		private void AddProductButton_Click(object sender, RoutedEventArgs e)
		{
			string productName = ProductNameTextBox.Text;
			if (decimal.TryParse(ProductWeightTextBox.Text, out decimal productWeight) && decimal.TryParse(ProductPriceTextBox.Text, out decimal productPrice))
			{
				DbHelper.AddProductToStorage(productName, productWeight, productPrice);
			}
			else
			{
				MessageBox.Show("Please enter valid weight and price.");
			}
		}

		
	}
}