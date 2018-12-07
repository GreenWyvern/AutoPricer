using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserProfile : System.Web.UI.Page
{
    string connectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        connectionString = ConfigurationManager.ConnectionStrings["database"].ToString();
        fill();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Home.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand comm = new SqlCommand("INSERT INTO CreditCard (Email,Address,Phone) VALUES (@Email,@Address,@Phone) ", conn);
        comm.Parameters.AddWithValue("@Email", tbEmail.Text);
        comm.Parameters.AddWithValue("@Address", tbAddress.Text);
        comm.Parameters.AddWithValue("@Phone", tbPhone.Text);

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception)
        {
        }
    }
    
    
    protected void fill()
    {
		SqlConnection conn = new SqlConnection(connectionString);
		SqlCommand comm = new SqlCommand("SELECT Email,Address,Phone FROM [User] WHERE Username=@username", conn);
        comm.Parameters.AddWithValue("@User", System.Web.HttpContext.Current.Session["user"]);
		try
		{
			conn.Open();
			string account = "";

			SqlDataReader reader = comm.ExecuteReader();

			//add to email reader.Read().ToString().Trim();
			//add to address reader.Read().ToString().Trim();
			//add to phone reader.Read().ToString().Trim();

			reader.Close();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
    }
}
