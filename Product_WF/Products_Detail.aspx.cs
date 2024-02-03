using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Product_WF
{
    public partial class Products_Detail : System.Web.UI.Page
    {
        string connectionString = @"Data Source=DESKTOP-JJSV9PF\SQLEXPRESS;Initial Catalog=ProductDemo;Integrated Security=True;Encrypt=False;Max Pool Size=100";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReturnDetails(sender, e);
            }
                
        }

        protected void ReturnDetails(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("spGetAllProductDetails", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure; // Assuming spGetAllProductDetails is a stored procedure

                        using (SqlDataAdapter sd = new SqlDataAdapter(cmd))
                        {
                            sd.Fill(dataTable);
                        }
                    }
                }

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    AllProductGrid.DataSource = dataTable;
                    AllProductGrid.DataBind();
                }
                else
                {
                    // Display a message or handle the case where the DataTable is null or empty
                    //ErrorMessageLabel.Text = "No product details found.";
                }
            }
            catch (Exception ex)
            {
                // Log the exception or display an error message
                // ex.Message);
            }
        }



        protected void Return(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}