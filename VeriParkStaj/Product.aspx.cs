using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace VeriParkStaj
{
    public partial class Product : Page
    {
        string chart = "";
        string productName = "";
        SqlConnection db;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    createContents();
                    calculateChart();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.GetType().FullName);
                    Console.WriteLine(ex.Message);
                    exceptions.Text += ex.GetType().FullName + "<br/>";
                    exceptions.Text += ex.Message;
                }
            }
        }
        void createContents()
        {
            string productID = Request.QueryString.Get("ID");

            int dummy;
            //To prevent SQL injection, input check
            if (!int.TryParse(productID, out dummy))
            {
                Response.Redirect("~/Default.aspx");
            }

            //database connection and get the information
            string query = "SELECT p.NAME AS productName, p.PRICE AS productPrice, c.NAME AS categoryName, p.Details AS pDetails, p.Sold AS pSold" +
                    " From [dbo].[Products] AS p, [dbo].[Categories] AS c" +
                    " WHERE p.ID = " + productID + " AND c.ID = p.CID";
            db = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFileName=|DataDirectory|internship.mdf;Integrated Security=SSPI;User Instance=True");
            db.Open();
            SqlDataReader dbRead = new SqlCommand(query, db).ExecuteReader();

            //output of product's information
            if (dbRead.Read())
            {
                productName = dbRead["productName"].ToString();
                Page.Title = productName;
                Sold.Text = dbRead["pSold"].ToString();
                pName.Text = "<br />" + productName;
                price.Text = dbRead["productPrice"].ToString();
                category.Text = dbRead["categoryName"].ToString();
                details.Text = dbRead["pDetails"].ToString();
                dbRead.Close();
            }
            else
                Response.Redirect("~/Default.aspx");

        }
        void calculateChart()
        {
            //get the number from the database
            string query = "SELECT COUNT FROM [dbo].[Chart] WHERE NAME = '" + productName + "'";
            SqlDataReader dbRead = new SqlCommand(query, db).ExecuteReader();
            if (dbRead.Read())
            {
                chart = dbRead["COUNT"].ToString();
            }
            else
                chart = "0";
            dbRead.Close();
            string chartrequest = Request.QueryString.Get("chart");
            int chartnum;
            if (int.TryParse(chartrequest, out chartnum))      //if there is an addition/removing request for chart
            {
                chartnum = Convert.ToInt32(chart) + Convert.ToInt32(chartrequest);
                query = "UPDATE [dbo].[Chart] SET COUNT = " + chartnum.ToString() + " WHERE NAME = '" + productName + "'";
                if (chartnum <= 0)
                {
                    query = "DELETE FROM [dbo].[Chart] WHERE NAME ='" + productName + "'";
                    chartnum = 0;
                }
                else if (chart == "0")
                    query = "INSERT INTO [dbo].[Chart] (NAME, COUNT, PRICE) VALUES ('"+productName+"', " + chartrequest + ", " +price.Text +")";
                int row = new SqlCommand(query, db).ExecuteNonQuery();
                if (row == 1)
                {
                    chart = chartnum.ToString();
                }
            }
            chartLabel.Text = chart; //output value
            db.Close();
        }
        protected void add_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string productID = Request.QueryString.Get("ID");

                //create a query to change number of items in the chart
                Response.Redirect(Request.Url.AbsolutePath + "?ID=" + productID + "&chart=" + number.Text);
            }
        }
        protected void remove_Click(object sender, EventArgs e)
        {
            //when there is no item then you don't have to send remove request
            if (Page.IsValid && Convert.ToInt32(chartLabel.Text) > 0) 
            {
                string productID = Request.QueryString.Get("ID");

                //create a query to change number of items in the chart
                Response.Redirect(Request.Url.AbsolutePath + "?ID=" + productID + "&chart=-" + number.Text);
            }

        }
    }
}