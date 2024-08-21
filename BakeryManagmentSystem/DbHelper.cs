using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManagmentSystem
{
	internal class DbHelper
	{
		static string connectionString = ConfigurationManager.ConnectionStrings["DataBaseConection"].ConnectionString;
		public static void DeleteProductFromDatabase(string query, string productName)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@productName", productName);

						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected == 0)
						{
							MessageBox.Show("No product found with the specified name.");
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: " + ex.Message);
				}
			}
		}

		public static void AddProductToDatabase(string productName)
		{
			string query = "INSERT INTO Products (ProductName) VALUES (@productName)";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@productName", productName);

						command.ExecuteNonQuery();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: " + ex.Message);
				}
			}
		}


		public static void AddProductToStorage(string productName, decimal productWeight, decimal productPrice)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlTransaction transaction = connection.BeginTransaction();

				try
				{
					string procedureQuery = "EXEC AddOrUpdateProduct @ProductName, @Weight, @PricePerKg";
					using (SqlCommand cmd = new SqlCommand(procedureQuery, connection, transaction))
					{
						cmd.Parameters.AddWithValue("@ProductName", productName);
						cmd.Parameters.AddWithValue("@Weight", productWeight);
						cmd.Parameters.AddWithValue("@PricePerKg", productPrice);
						cmd.ExecuteNonQuery();
					}

					transaction.Commit();
					MessageBox.Show("Product added successfully!");
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					MessageBox.Show($"Error adding product: {ex.Message}");
				}
			}
		}


		public static void UpdateProductInfo(string productName, decimal newProductWeight, decimal newProductPrice)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlTransaction transaction = connection.BeginTransaction();

				try
				{
					string procedureQuery = "EXEC UpdateProduct @ProductName, @NewWeight, @NewPrice";
					using (SqlCommand cmd = new SqlCommand(procedureQuery, connection, transaction))
					{
						cmd.Parameters.AddWithValue("@ProductName", productName);
						cmd.Parameters.AddWithValue("@NewWeight", newProductWeight);
						cmd.Parameters.AddWithValue("@NewPrice", newProductPrice);
						cmd.ExecuteNonQuery();
					}

					transaction.Commit();
					MessageBox.Show("Product info updated successfully!");
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					MessageBox.Show($"Error updating product info: {ex.Message}");
				}
			}
		}


		public static void AddRecipeToDatabase(string recipeName, List<Ingredient> ingredients)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlTransaction transaction = connection.BeginTransaction();

				try
				{
					string recipeQuery = "INSERT INTO Recipes (RecipeName) VALUES (@RecipeName); SELECT SCOPE_IDENTITY();";
					int recipeId;
					using (SqlCommand cmd = new SqlCommand(recipeQuery, connection, transaction))
					{
						cmd.Parameters.AddWithValue("@RecipeName", recipeName);
						recipeId = Convert.ToInt32(cmd.ExecuteScalar());
					}

					foreach (var ingredient in ingredients)
					{
						string ingredientQuery = @"
                            INSERT INTO RecipeIngredients (RecipeId, ProductId, Quantity)
                            VALUES (@RecipeId, (SELECT Id FROM Products WHERE ProductName = @ProductName), @Quantity)";
						using (SqlCommand cmd = new SqlCommand(ingredientQuery, connection, transaction))
						{
							cmd.Parameters.AddWithValue("@RecipeId", recipeId);
							cmd.Parameters.AddWithValue("@ProductName", ingredient.ProductName);
							cmd.Parameters.AddWithValue("@Quantity", ingredient.Quantity);
							cmd.ExecuteNonQuery();
						}
					}

					transaction.Commit();
					MessageBox.Show("Recipe added successfully!");
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					MessageBox.Show($"Error adding recipe: {ex.Message}");
				}
			}
		}


		public static void DeleteRecipeFromDatabase(int recipeId)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlTransaction transaction = connection.BeginTransaction();

				try
				{
					string deleteIngredientsQuery = "DELETE FROM RecipeIngredients WHERE RecipeId = @RecipeId";
					using (SqlCommand cmd = new SqlCommand(deleteIngredientsQuery, connection, transaction))
					{
						cmd.Parameters.AddWithValue("@RecipeId", recipeId);
						cmd.ExecuteNonQuery();
					}

					string deleteRecipeQuery = "DELETE FROM Recipes WHERE Id = @RecipeId";
					using (SqlCommand cmd = new SqlCommand(deleteRecipeQuery, connection, transaction))
					{
						cmd.Parameters.AddWithValue("@RecipeId", recipeId);
						cmd.ExecuteNonQuery();
					}

					transaction.Commit();
					MessageBox.Show("Recipe deleted successfully!");
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					MessageBox.Show($"Error deleting recipe: {ex.Message}");
				}
			}
		}
	}
}

	public class Recipe
	{
		public int Id { get; set; }
		public string RecipeName { get; set; }
	}

	public class Ingredient
	{
		public string ProductName { get; set; }
		public decimal Quantity { get; set; }
	}

