using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Register : System.Web.UI.Page
{
    int confirmNumber;

    SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AutoDB.mdf;Integrated Security=True");
    protected void Page_Load(object sender, EventArgs e)
    {
        Random random = new Random();
 
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        SqlCommand comm = new SqlCommand("SELECT Username FROM [User] WHERE Username=@username", conn);
        comm.Parameters.AddWithValue("@username", tbUsername.Text);
        try
        {
            conn.Open();
            string user = "";
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                user = reader[0].ToString().Trim();
            }
            reader.Close();

            if (user.Equals(""))//if account not taken
            {
                SqlCommand comm2 = new SqlCommand(@"INSERT INTO [User](Username,Password,Email,Address,PhoneNumber,AccountType) Values('" + tbUsername.Text + "', '" + tbPassword.Text + "', '" + tbEmail.Text + "', '" + tbAddress.Text + "', '" + tbPhone.Text + "'," + "'unconfirmed')", conn);
                try
                {
                    comm2.ExecuteNonQuery();
                    insertAuthentication();
                    Response.Redirect("~/Login.aspx");
                }
                catch (Exception ex)
                {
                    labelWarning.Text = ex.Message + "failed to insert to database!";
                }
            }
            else
            {
                labelWarning.Text = "Username taken";
            }
            conn.Close();
        }
        catch (Exception ea)
        {
            labelWarning.Text = "failed to connect to database";
        }
    }


    private void insertAuthentication()
    {
        SqlCommand comm2 = new SqlCommand(@"INSERT INTO [Authentication](Username,Code) Values('" + tbUsername.Text + "', " + 0 + ")", conn);
        try
        {
            comm2.ExecuteNonQuery();
            //Response.Redirect("~/Login.aspx");
        }
        catch (Exception ex)
        {
            labelWarning.Text = ex.Message + "failed to insert to database!";
        }
    }


}
