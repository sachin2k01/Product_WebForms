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
            if(!Page.IsPostBack)
            {
                Product_Details(sender,e);
            }

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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Product created Successfully');window.location='Default.aspx';", true);

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
            if (!int.TryParse(pIDtbx.Text, out int Product_ID) || Product_ID == 0)
            {
                ErrorMessageLabel.Text = "Invalid Product ID. Please enter a valid non-zero integer.";
                return;
            }
            using (SqlConnection sqlConnection=new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("spDeleteProduct", sqlConnection);
                sqlCmd.CommandType= CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@productID", Product_ID);
                sqlCmd.ExecuteNonQuery ();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Product Deleted Successfully');window.location='Default.aspx';", true);
            }

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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Invalid Product ID'); window.location='Default.aspx';", true);
                        return;
                    }
                }
            }
        }

        protected void Product_Details(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("spGetAllProductDetails", connection);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                sd.Fill(dataTable);
                DataGrid.DataSource = dataTable;
                DataGrid.DataBind();

            }
        }



        protected void All_Product_Details(object sender, EventArgs e)
        {
            Response.Redirect("Products_Detail.aspx");
        }

        protected void Update_Product(object sender, EventArgs e)
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
                SqlCommand sqlCmd = new SqlCommand("spUpdateProduct", connection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@prodId", Product_ID);
                sqlCmd.Parameters.AddWithValue("@pName", ItemName);
                sqlCmd.Parameters.AddWithValue("@Spec", Specification);
                sqlCmd.Parameters.AddWithValue("@quantity", (object)Unit ?? DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@status", Status);
                sqlCmd.Parameters.AddWithValue("@Cdate", CreationDate);
                sqlCmd.ExecuteNonQuery();
                connection.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Product created Successfully');window.location='Default.aspx';", true);

            }
        }
    }
}