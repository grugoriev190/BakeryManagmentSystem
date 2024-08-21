using BakeryManagmentSystem.All_Products_Windows;
using BakeryManagmentSystem.Recipes_Window;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BakeryManagmentSystem
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		
		public MainWindow()
		{
			InitializeComponent();
			reset_grids();
		}

		private void reset_grids()
		{
			ProductionAtStorageGrid.Visibility = Visibility.Hidden;
			OrdersGrid.Visibility = Visibility.Hidden;
			RecipesGrid.Visibility = Visibility.Hidden;
			StatisticGrid.Visibility = Visibility.Hidden;
			AllProductionGrid.Visibility = Visibility.Hidden;
		}

		private DataTable LoadDataFromDatabase(string query)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["DataBaseConection"].ConnectionString;
			DataTable dataTable = new DataTable();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
					dataAdapter.Fill(dataTable);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: " + ex.Message);
				}
			}

			return dataTable;
		}

		private void LoadAllProducts()
		{
			string query = "SELECT * FROM Products";
			DataTable dataTable = LoadDataFromDatabase(query);
			AllProductsDataGrid.ItemsSource = dataTable.DefaultView;
		}

		private void LoadDataStorage()
		{
			string query = "SELECT * FROM ProductsStorage";
			DataTable dataTable = LoadDataFromDatabase(query);
			ProductsInStorageDataGrid.ItemsSource = dataTable.DefaultView;
		}

		private void LoadRecipes()
		{
			string query = "SELECT * FROM Recipes";
			DataTable dataTable = LoadDataFromDatabase(query);
			RecipesDataGrid.ItemsSource = dataTable.DefaultView;
		}


		#region Main window
		private void productionAtStorageBtn_Click(object sender, RoutedEventArgs e)
		{
			reset_grids();	
			ProductionAtStorageGrid.Visibility = Visibility.Visible;
			LoadDataStorage();
		}

		private void allProductionBtn_Click(object sender, RoutedEventArgs e)
		{
			reset_grids();
			AllProductionGrid.Visibility = Visibility.Visible;
			LoadAllProducts();

		}

		private void showOrdersBtn_Click(object sender, RoutedEventArgs e)
		{
			reset_grids();
			OrdersGrid.Visibility = Visibility.Visible;
		}

		private void recipesBtn_Click(object sender, RoutedEventArgs e)
		{
			reset_grids();
			RecipesGrid.Visibility = Visibility.Visible;
			LoadRecipes();
		}

		private void statisticBtn_Click(object sender, RoutedEventArgs e)
		{
			reset_grids();
			StatisticGrid.Visibility = Visibility.Visible;
		}
		#endregion

		#region Order window
		private void changeOrderBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void addOrderBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void orderReadyBtn_Click(object sender, RoutedEventArgs e)
		{

		}
		#endregion

		#region Storage window
		private void addProductToStorage_Click(object sender, RoutedEventArgs e)
		{
			AddProductWindow addProductWindow = new AddProductWindow();
			addProductWindow.Show();
		}

		private void changeProductInfoAtStorageBtn_Click(object sender, RoutedEventArgs e)
		{
			ChangeProductWindow changeProductWindow = new ChangeProductWindow();
			changeProductWindow.Show();
		}

		private void deleteProductFromStorageBtn_Click(object sender, RoutedEventArgs e)
		{
			DeleteProductWindow deleteProductWindow = new DeleteProductWindow();
			deleteProductWindow.Show();
		}
		#endregion

		#region Statistic window

		#endregion

		#region Recipes Window

		private void addRecipeBtn_Click(object sender, RoutedEventArgs e)
		{
			AddRecipeWindow addRecipeWindow = new AddRecipeWindow();
			addRecipeWindow.Show();
		}
		private void changeRecipeBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void deleteRecipeBtn_Click(object sender, RoutedEventArgs e)
		{
			DeleteRecipeWindow deleteRecipeWindow = new DeleteRecipeWindow();
			deleteRecipeWindow.Show();
		}
		#endregion

		#region All Products
		private void addProductBtn_Click(object sender, RoutedEventArgs e)
		{
			AddProductToDBWindow addProductWindow = new AddProductToDBWindow();
			addProductWindow.Show();
		}

		private void deleteProductBtn_Click(object sender, RoutedEventArgs e)
		{
			DeleteProductFromDBWindow delProductWindow = new DeleteProductFromDBWindow();
			delProductWindow.Show();
		}
		#endregion

		private void refreshProductDBBtn_Click(object sender, RoutedEventArgs e)
		{
			LoadAllProducts();
			LoadDataStorage();
		}

	}
}
