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
        confirmNumber = random.Next(0, 1000000);
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

            if (user.Equals("") && Convert.ToInt32(tbConfirm.Text) == confirmNumber)//if account not taken
            {
                SqlCommand comm2 = new SqlCommand(@"INSERT INTO [User](Username,Password,Email,Address,PhoneNumber,AccountType) Values('" + tbUsername.Text + "', '" + tbPassword.Text + "', '" + tbEmail.Text + "', '" + tbAddress.Text + "', '" + tbPhone.Text + "'," + "'regular')", conn);
                SqlCommand comm3 = new SqlCommand(@"INSERT INTO Authentication(Username,Code,ExpiryTime) Values('" + tbUsername.Text + "'," + 0 + "," + DateTime.Now + ")", conn);
                try
                {
                    comm2.ExecuteNonQuery();
                    comm3.ExecuteNonQuery();
                    Response.Redirect("~/EmailConfirmation.aspx");
                }
                catch (Exception ex)
                {
                    labelWarning.Text = ex.Message + "failed to insert to database!";
                }
            }
            else
            {
                labelWarning.Text = "Username taken or incorrect email confirmation number";
            }
            conn.Close();
        }
        catch (Exception ea)
        {
            labelWarning.Text = "failed to connect to database";
        }
    }



    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        SmtpClient client = new SmtpClient();
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;
        client.Host = "smtp.gmail.com";
        client.Port = 587;

        // setup Smtp authentication
        System.Net.NetworkCredential credentials =
            new System.Net.NetworkCredential("TeamNKproject@gmail.com", "teamnk1!");
        client.UseDefaultCredentials = false;
        client.Credentials = credentials;
        client.EnableSsl = true;

        MailMessage msg = new MailMessage();
        msg.From = new MailAddress("TeamNKproject@gmail.com");
        msg.To.Add(new MailAddress(tbEmail.Text));

        msg.Subject = "This is a test Email subject";
        msg.IsBodyHtml = true;
        msg.Body = string.Format("<html><head></head><body>your confirmation number is : {0}</body>", confirmNumber);

        try
        {
            client.Send(msg);
        }
        catch (Exception ex)
        {

        }
    }
}
