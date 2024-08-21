using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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

namespace BakeryManagmentSystem.All_Products_Windows
{
	/// <summary>
	/// Interaction logic for AddProductToDBWindow.xaml
	/// </summary>
	public partial class AddProductToDBWindow : Window
	{
		public AddProductToDBWindow()
		{
			InitializeComponent();
		}

		private void addProductToDBBtn_Click(object sender, RoutedEventArgs e)
		{
			string productName = addProductToDBTextBox.Text;

			if (string.IsNullOrWhiteSpace(productName))
			{
				MessageBox.Show("Please enter valid data.");
				return;
			}

			DbHelper.AddProductToDatabase(productName);
			this.Close();
		}

		
	}
}
