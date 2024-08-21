using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace BakeryManagmentSystem.Recipes_Window
{
	/// <summary>
	/// Interaction logic for AddRecipeWindow.xaml
	/// </summary>
	public partial class AddRecipeWindow : Window
	{
		public AddRecipeWindow()
		{
			InitializeComponent();
			IngredientsDataGrid.ItemsSource = new List<Ingredient>();
		}

		private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
		{
			string recipeName = RecipeNameTextBox.Text;
			List<Ingredient> ingredients = new List<Ingredient>();

			foreach (var item in IngredientsDataGrid.Items)
			{
				if (item is Ingredient ingredient && !string.IsNullOrWhiteSpace(ingredient.ProductName) && ingredient.Quantity > 0)
				{
					ingredients.Add(ingredient);
				}
			}

			if (string.IsNullOrWhiteSpace(recipeName) || ingredients.Count == 0)
			{
				MessageBox.Show("Please enter a valid recipe name and at least one ingredient with a quantity greater than 0.");
				return;
			}

			DbHelper.AddRecipeToDatabase(recipeName, ingredients);
		}
	}
}
