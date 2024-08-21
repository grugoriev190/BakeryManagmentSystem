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

namespace BakeryManagmentSystem.Recipes_Window
{
	/// <summary>
	/// Interaction logic for DeleteRecipeWindow.xaml
	/// </summary>
	public partial class DeleteRecipeWindow : Window
	{
		public DeleteRecipeWindow()
		{
			InitializeComponent();
			LoadRecipes();
		}

		private void LoadRecipes()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["DataBaseConection"].ConnectionString;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				string query = "SELECT Id, RecipeName FROM Recipes";
				using (SqlCommand cmd = new SqlCommand(query, connection))
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					List<Recipe> recipes = new List<Recipe>();
					while (reader.Read())
					{
						recipes.Add(new Recipe
						{
							Id = reader.GetInt32(0),
							RecipeName = reader.GetString(1)
						});
					}
					RecipeComboBox.ItemsSource = recipes;
				}
			}
		}

		private void DeleteRecipeButton_Click(object sender, RoutedEventArgs e)
		{
			if (RecipeComboBox.SelectedItem is Recipe selectedRecipe)
			{
				DbHelper.DeleteRecipeFromDatabase(selectedRecipe.Id);
				LoadRecipes();
			}
			else
			{
				MessageBox.Show("Please select a recipe to delete.");
			}
		}

	}
}
