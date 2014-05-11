using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace VeriParkStaj
{
    public partial class _Default : Page
    {
        SqlConnection db;
        void loadDropDownList()
        {
            // if the list of category is created before
            // then it clear all list
            while (categoryList.Items.Count > 0)
                categoryList.Items.RemoveAt(0);

            //it reads the category names and put it to the list
            SqlDataReader dbReader = new SqlCommand("SELECT NAME FROM [dbo].[Categories]", db).ExecuteReader();
            while (dbReader.Read())
                categoryList.Items.Add(dbReader["Name"].ToString());
            dbReader.Close();
        }

        void createTable()
        {
            string categoryName = Request.QueryString.Get("cname");
            string additionQuery = "";

            // if the querystring is in the list of categories
            // then it modify the sql query to get products from a specific category
            // Also add category to header
            if (categoryName != null && categoryList.Items.FindByText(categoryName) != null)
            {
                additionQuery = " AND c.NAME = '" + categoryName + "'";
                categoryHeader.Text = "<br />" + categoryName;
            }

            //SQL query to get get products' name, category and price
            string query = "SELECT p.NAME AS productName, p.PRICE AS productPrice, c.NAME AS categoryName, p.ID AS productID" +
                " From [dbo].[Products] AS p, [dbo].[Categories] AS c" +
                " WHERE c.ID = p.CID" + additionQuery +
                " ORDER BY productName ASC;";

            //Binding the query result to Repeater Control
            //products is the repeater control
            products.DataSource = new SqlCommand(query, db).ExecuteReader();
            products.DataBind();
            db.Close();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    db = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFileName=|DataDirectory|internship.mdf;Integrated Security=SSPI;User Instance=True");
                    string success = Request.QueryString.Get("buy");

                    //it is only send success message to inform that items are bought
                    if (success != null && success == "TRUE")
                    {
                        string myscript = "alert ('You bought the products.');";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", myscript, true);
                    }

                    db.Open();
                    loadDropDownList();
                    createTable();
                }
            }
            catch (Exception ex)
            {
                exceptions.Text += ex.GetType().FullName + "<br/>";
                exceptions.Text += ex.Message;
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }

        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string myUrl = "~/Default.aspx";
                myUrl += "?cname=" + categoryList.SelectedItem.Text;
                Response.Redirect(myUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                exceptions.Text += ex.GetType().FullName + "<br/>";
                exceptions.Text += ex.Message;
            }

        }

    }
}