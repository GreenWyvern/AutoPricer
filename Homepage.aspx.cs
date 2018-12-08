using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Homepage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void goToPages(object sender, EventArgs e)
    {

        string account = Session["accountType"].ToString();

        switch (Select1.SelectedIndex)
        {
            case 1:
                if (account == "regular" || account == "admin")
                {
                    Response.Redirect("~/UserProfile.aspx");//user profile
                }
                else
                {
                    test.Text = "Please login first !";
                }
                break;
            case 2:
                if (account == "regular" || account == "admin")
                {
                    Response.Redirect("~/Listing_Description.aspx");//add listings
                }
                else
                {
                    test.Text = "Please login first !";
                }
                break;
            case 3:
                if (account == "admin")
                {
                    Response.Redirect("~/UserList.aspx");//web management (delete users)
                }
                else
                {
                    test.Text = "need admin account !";
                }
                break;
        }

    }

}
