using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.AspNet.FriendlyUrls;


namespace Product_WF
{
    public partial class _Default : Page
    {
        string connectionString = @"Data Source=DESKTOP-JJSV9PF\SQLEXPRESS;Initial Catalog=ProductDemo;Integrated Security=True;Encrypt=False";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void InsertProduct(object sender, EventArgs e)
        {
            try
            {
                int Product_ID = int.Parse(pIDtbx.Text);
                string ItemName = Pnametbx.Text;
                string Specification = SpecTbx.Text;
                int Unit = int.Parse(DropDownList.SelectedValue);
                string Status = string.Empty;
                if (running.Checked)
                {
                    Status = "Running";
                }
                else if (unused.Checked)
                {
                    Status = "Unused";
                }
                else
                {
                    Status = null;
                }
                DateTime CreationDate = DateTime.TryParse(DateTxt.Text, out var result) ? result : DateTime.Now;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCmd = new SqlCommand("spAddProduct", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@pId", Product_ID);
                    sqlCmd.Parameters.AddWithValue("@pName", ItemName);
                    sqlCmd.Parameters.AddWithValue("@Spec", Specification);
                    sqlCmd.Parameters.AddWithValue("@quantity", Unit);
                    sqlCmd.Parameters.AddWithValue("@status", Status);
                    sqlCmd.Parameters.AddWithValue("@Cdate", CreationDate);
                    sqlCmd.ExecuteNonQuery();
                    connection.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Product created Successfully');", true);

                }
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void DeletProduct(object sender, EventArgs e)
        {

        }

        protected void Search_Product(object sender, EventArgs e)
        {
            if (!int.TryParse(pIDtbx.Text, out int Product_ID) || Product_ID == 0)
            {
                ErrorMessageLabel.Text = "Invalid Product ID. Please enter a valid non-zero integer.";
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if Product_ID is NULL in the database
                if (Product_ID != 0)
                {
                    SqlCommand sqlCmd = new SqlCommand("spGetProductById", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@productID", Product_ID);

                    using (SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Pnametbx.Text = reader.GetString(1).ToString();
                            SpecTbx.Text = reader.GetString(2).ToString();
                            DropDownList.SelectedValue = reader[3].ToString();
                            string Status = string.Empty;

                            if (reader[4] != DBNull.Value) // Check for DBNull before converting to string
                            {
                                Status = reader.GetString(4); // Assuming the status is a string in the database
                            }

                            DateTxt.Text = reader.IsDBNull(5) ? string.Empty : reader.GetDateTime(5).ToString("dd-MM-yyyy");
                        }
                        else
                        {
                            ErrorMessageLabel.Text = "Invalid Product Id or no data found.";
                        }
                    }
                }
                else
                {
                    ErrorMessageLabel.Text = "Invalid Product ID. Product ID cannot be NULL.";
                }
            }
        }


    }
}