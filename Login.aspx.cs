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
    bool codeSent = false;
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
            if (requireConfirmAccount(tbUsername.Text))
            {
                labelWarning.Text = "Authentication required.";

                rowCode.Visible = true;

            }
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

                SqlCommand commCode = new SqlCommand("SELECT Code FROM Authentication WHERE Username=@username", conn);

                commCode.Parameters.AddWithValue("@Username", tbUsername.Text);
                SqlDataReader readCode = commCode.ExecuteReader();
                if (!readCode.HasRows)
                {
                    readCode.Close();
                    insertAuthentication();
                    if (generateNewAuthenticationCode(tbUsername.Text))
                    {
                        if (sendAuthenticationCode(tbUsername.Text))
                        {
                            labelWarning.Text = "Confirmation code sent, please check email to activate your account.";
                        }
                    }
                    labelWarning.Text = "Confirmation code sent, please check email to activate your account.";
                    return;
                }
               
                readCode.Close();
                readCode = commCode.ExecuteReader();
                readCode.Read();
                int code = readCode.GetInt32(0);
                if (code == 0)
                {
                    readCode.Close();
                    if (generateNewAuthenticationCode(tbUsername.Text))
                    {
                        if (sendAuthenticationCode(tbUsername.Text))
                        {
                            labelWarning.Text = "Confirmation code sent, please check email to activate your account.";
                        }
                    }
                    else
                    {
                        labelWarning.Text = "Unable to activate your account, please contact admin.";
                    }
                }
                else
                {
                    SqlCommand comm2 = new SqlCommand("SELECT Code FROM Authentication WHERE Username=@username", conn);
                    comm2.Parameters.AddWithValue("@username", tbUsername.Text);
                    readCode.Close();
                    readCode = comm2.ExecuteReader();
                    readCode.Read();
                    int newCode = readCode.GetInt32(0);

                    int inputCode = 0;
                    int.TryParse(tbCode.Text, out inputCode);

                    if(newCode == 0)
                    {
                        labelWarning.Text = "Authentication required. Check your email.";
                    }

                    if (inputCode == newCode)
                    {
                        userConfirm();
                        Response.Redirect("~/UserProfile.aspx");
                    }
                    else
                    {
                        labelWarning.Text = "Incorrect Code!";
                    }
                }

                
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



    private bool UserExist(string username)
    {
        SqlCommand comm2 = new SqlCommand("SELECT Username FROM [User] WHERE Username=@username", conn);
        comm2.Parameters.AddWithValue("@username", username);
        if (comm2.ExecuteNonQuery() > 0)
        {
            return true;
        }
        return false;
    }

    private void userConfirm()//set the user account type to regular
        {
            
            SqlCommand comm = new SqlCommand("UPDATE [User] SET AccountType = 'Regular' WHERE Username =@username ", conn);
            comm.Parameters.AddWithValue("@username", tbUsername.Text);
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            labelWarning.Text = ex.Message; 
            }
        }

    private bool requireConfirmAccount(string username)
    {
        SqlCommand comm = new SqlCommand("SELECT AccountType FROM [User] WHERE Username=@username", conn);
        comm.Parameters.AddWithValue("@username", username);
        try
        {
            SqlDataReader reader = comm.ExecuteReader();
            reader.Read();
            string type = reader.GetString(0).Trim().ToLower();
            if (type.Equals("unconfirmed"))
            {
                reader.Close();
                return true;
            }
            else
            {
                reader.Close();
                return false;
            }
        }
        catch (Exception ex)
        {
            labelWarning.Text = ex.Message;
        }
        return false;
    }


    /*







    /*



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
        }*/

    private bool generateNewAuthenticationCode(string username)
        {
            try
            {
                int newCode = new Random().Next(1000, 10000);
                DateTime expiryTime = DateTime.Now.AddYears(5);

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
            SqlDataReader reader = null;
            try
            {
                SqlCommand getCodeComm = new SqlCommand("Select Code From Authentication WHERE Username=@username", conn);
                getCodeComm.Parameters.AddWithValue("@username", username);
                SqlCommand getEmailComm = new SqlCommand("Select Email From [User] WHERE Username=@username", conn);
                getEmailComm.Parameters.AddWithValue("@username", username);

            reader = getCodeComm.ExecuteReader();

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
            finally
            {
            if (reader != null)
            {
                reader.Close();
            }
               
            }
            return false;
        }

    private void insertAuthentication()
    {
        SqlCommand comm2 = new SqlCommand(@"INSERT INTO [Authentication](Username, Code) Values('" + tbUsername.Text + "', " + 0 + ")", conn);
        try
        {
            comm2.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            labelWarning.Text = ex.Message + "failed to insert to database!";
        }
    }
}
