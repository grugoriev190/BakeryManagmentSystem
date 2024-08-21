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

namespace BakeryManagmentSystem.All_Products_Windows
{
	/// <summary>
	/// Interaction logic for DeleteProductFromDBWindow.xaml
	/// </summary>
	public partial class DeleteProductFromDBWindow : Window
	{
		public DeleteProductFromDBWindow()
		{
			InitializeComponent();
		}

		private void deleteProductFromDBBtn_Click(object sender, RoutedEventArgs e)
		{
			string productName = deleteProductFromDBTextBox.Text;

			if (string.IsNullOrWhiteSpace(productName))
			{
				MessageBox.Show("Please enter valid data.");
				return;
			}
			string query = "DELETE FROM Products WHERE ProductName = @ProductName";
			DbHelper.DeleteProductFromDatabase(query, productName);
			this.Close();
		}

		
	}
}
