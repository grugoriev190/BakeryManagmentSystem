using System;
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
using System.Windows.Shapes;

namespace BakeryManagmentSystem
{
	/// <summary>
	/// Interaction logic for DeleteProductWindow.xaml
	/// </summary>
	public partial class DeleteProductWindow : Window
	{
		public DeleteProductWindow()
		{
			InitializeComponent();
		}

		private void deleteProductFromStorageBtn_Click(object sender, RoutedEventArgs e)
		{
			string productName = deleteFromStorageTextBox.Text;

			if (string.IsNullOrWhiteSpace(productName))
			{
				MessageBox.Show("Please enter valid data.");
				return;
			}
			string query = "DELETE FROM ProductsStorage WHERE ProductName = @productName";
			DbHelper.DeleteProductFromDatabase(query, productName);
			this.Close();
		}
    }
}
