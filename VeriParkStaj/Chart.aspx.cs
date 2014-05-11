using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace VeriParkStaj
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //check the query
                    SqlConnection db = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFileName=|DataDirectory|internship.mdf;Integrated Security=SSPI;User Instance=True");
                    string productName = Request.QueryString.Get("name");
                    string query;
                    db.Open();

                    //if there is a query to remove item from chart
                    //little protection from SQL injection
                    if (productName != null && productName.IndexOf('\'') == -1)
                    {
                        query = "DELETE FROM [dbo].[Chart] WHERE NAME ='" + productName + "'";
                        new SqlCommand(query, db).ExecuteNonQuery();
                    }
                    //check the chart is empty
                    query = "SELECT COUNT(NAME) FROM [dbo].[Chart]";
                    int rows = Convert.ToInt32(new SqlCommand(query, db).ExecuteScalar().ToString());
                    if (rows == 0)
                        Response.Redirect("~/Default.aspx");

                    //if there is a query to buy items
                    string buy = Request.QueryString.Get("buy");
                    if (buy == "TRUE")
                    {
                        //update sold column first
                        query = "UPDATE [dbo].[Products] " +
                            "SET Sold = p.SOLD + c.COUNT " +
                            "FROM [dbo].[Products] AS p, [dbo].[Chart] AS c " +
                            "WHERE c.NAME = p.NAME";
                        new SqlCommand(query, db).ExecuteNonQuery();
                        //Then clear chart
                        query = "DELETE FROM [dbo].[Chart]";
                        new SqlCommand(query, db).ExecuteNonQuery();

                        Response.Redirect("~/Default.aspx?buy=TRUE");
                    }
                    //Get chart from database and bind it to repeater
                    query = "SELECT NAME AS name, COUNT as count, PRICE as price FROM [dbo].[Chart]";
                    chart.DataSource = new SqlCommand(query, db).ExecuteReader();
                    chart.DataBind();
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                exceptions.Text += ex.GetType().FullName + "<br/>";
                exceptions.Text += ex.Message;
            }
        }

        protected void apply_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath + "?buy=TRUE");
        }
    }
}