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
        Response.Redirect("~/Homepage.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand comm = new SqlCommand("INSERT INTO User (Email,Address,PhoneNumber) VALUES (@Email,@Address,@Phone) ", conn);
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
		SqlCommand comm = new SqlCommand("SELECT Email, Address, PhoneNumber FROM [User] WHERE Username=@username", conn);
        comm.Parameters.AddWithValue("@username", System.Web.HttpContext.Current.Session["user"]);
		try
		{
			conn.Open();

			SqlDataReader reader = comm.ExecuteReader();
            reader.Read();
            tbEmail.Text = reader.GetString(0);
            tbAddress.Text = reader.GetString(1);
            tbPhone.Text = reader.GetString(2);
            reader.Close();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
    }
}
