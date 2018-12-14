using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



    public partial class Login : System.Web.UI.Page
    {

    SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AutoDB.mdf;Integrated Security=True");
    protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");
        }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        SqlCommand comm = new SqlCommand("SELECT AccountType FROM [User] WHERE Username=@username AND Password=@password ", conn);
        comm.Parameters.AddWithValue("@username", tbUsername.Text);
        comm.Parameters.AddWithValue("@password", tbPassword.Text);
        try
        {
            conn.Open();
            string account = "";

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {

                account = reader[0].ToString().Trim();
            }
            reader.Close();


            if (account.Equals("admin"))
            {
                System.Web.HttpContext.Current.Session["user"] = tbUsername.Text;
                System.Web.HttpContext.Current.Session["accountType"] = "admin";
                Response.Redirect("~/Admin.aspx");

            }
            else if (account.Equals("regular"))
            {
                System.Web.HttpContext.Current.Session["user"] = tbUsername.Text;
                System.Web.HttpContext.Current.Session["accountType"] = "regular";
                Response.Redirect("~/UserProfile.aspx");
            }
            else if (account.Equals("unconfirmed"))
            {
                rowCode.Visible= true;
                /*
                 look for authentication    
                if(Autgebtucation found){
                    if(code correct)
                    {
                        userConfirm();
                        Response.Redirect("~/UserProfile.aspx");
                    } 
                    else
                    {
                        alert user
                    }
                  } 
                else(){
                    insertAuthentication();
                }

                 */

                /*
                SqlCommand comm2 = new SqlCommand("SELECT Code FROM Authentication WHERE Username=@username", conn);
                    comm2.Parameters.AddWithValue("@username", tbUsername.Text);

                    SqlDataReader reader2 = comm2.ExecuteReader();
                    while (reader2.Read())
                    {
                        int code = reader2.GetInt32(0);

                        int inputCode = 0;
                        int.TryParse(tbCode.Text, out inputCode);

                        if (code == inputCode)
                        {
                            reader2.Close();
                            
                            if (generateNewAuthenticationCode(tbUsername.Text))
                            {
                                userConfirm();
                                Response.Redirect("~/UserProfile.aspx");
                            }                   
                        }
                        else
                        {
                            labelWarning.Text = "Incorrect Code!";
                        }
                        return;
                    }
                    reader2.Close();
                    */
            }
            else
            {
                labelWarning.Text = "account not found";
            }
            
        }
        catch (Exception ex)
        {
            labelWarning.Text = ex.Message;
        }
        finally
        {
            conn.Close();
        }
    }

    private void userConfirm()//set the user account type to regular
        {
            
            SqlCommand comm = new SqlCommand("UPDATE user SET AccountType = Regular WHERE Username =@username ", conn);
            comm.Parameters.AddWithValue("@username", tbUsername.Text);
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

    private void insertAuthentication()
    {
        SqlCommand comm2 = new SqlCommand(@"INSERT INTO [Authentication](Id,Code) Values('" + tbUsername.Text + "', '" + tbPassword.Text + "', '" + tbEmail.Text + "', '" + tbAddress.Text + "', '" + tbPhone.Text + "'," + "'unconfirmed')", conn);
        try
        {
            comm2.ExecuteNonQuery();
            Response.Redirect("~/Login.aspx");
        }
        catch (Exception ex)
        {
            labelWarning.Text = ex.Message + "failed to insert to database!";
        }
    }














    private bool requireAuthentication(string username)
    {
        SqlCommand comm = new SqlCommand("SELECT * FROM Authentication WHERE Username=@username", conn);
        comm.Parameters.AddWithValue("@username", tbUsername.Text);
        try
        {
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                DateTime expiryTime = reader.GetDateTime(2);
                DateTime now = DateTime.Now;

                // If the authentication code expiry 5 minutes ago then user must do authentication
                if (now >= expiryTime)
                {
                    reader.Close();
                    bool sent = sendAuthenticationCode(tbUsername.Text);
                    labelWarning.Text = "2 Way Authentication Required!\n" + (sent ? "Authentication Code Sent. " : "") + "Please check your email. This might take a while.";
                    return true;
                }
            }
            reader.Close();
        }
        catch(Exception ex)
        {
            labelWarning.Text = ex.Message;
        }
        return false;
    }

    private bool generateNewAuthenticationCode(string username)
    {
        try
        {
            int newCode = new Random().Next(1000, 10000);
            DateTime expiryTime = DateTime.Now.AddMinutes(5);

            SqlCommand comm = new SqlCommand("UPDATE Authentication SET Code=@code, ExpiryTime=@expiryTime WHERE Username=@username", conn);
            comm.Parameters.AddWithValue("@username", tbUsername.Text);
            comm.Parameters.AddWithValue("@code", newCode);
            comm.Parameters.AddWithValue("@expiryTime", expiryTime);

            if (comm.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            labelWarning.Text = ex.Message;
        }
        return false;
    }

    private bool sendAuthenticationCode(string username)
    {
        try
        {
            SqlCommand getCodeComm = new SqlCommand("Select Code From Authentication WHERE Username=@username", conn);
            getCodeComm.Parameters.AddWithValue("@username", username);
            SqlCommand getEmailComm = new SqlCommand("Select Email From [User] WHERE Username=@username", conn);
            getEmailComm.Parameters.AddWithValue("@username", username);

            SqlDataReader reader = getCodeComm.ExecuteReader();

            reader.Read();
            int code = reader.GetInt32(0);

            reader.Close();

            reader = getEmailComm.ExecuteReader();
            reader.Read();

            string emailAddress = reader.GetString(0);

            reader.Close();

            string message = "Hello <b>" + username + "</b>,\n<br>Here is your authentication code: <b>" + code + "</b>";

            labelWarning.Text += code;
            labelWarning.Text += emailAddress;

            EmailSender.Send(emailAddress, "Auto Price Authentication Code", message);
            return true;
        }
        catch (Exception ex)
        {
            labelWarning.Text = ex.Message;
        }
        return false;
    }
}
