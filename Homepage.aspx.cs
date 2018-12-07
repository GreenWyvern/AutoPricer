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
        if (Session["accountType"] != null) { 
        
            switch (Select1.SelectedIndex)
            {
                case 1:
                    Response.Redirect("~/UserProfile.aspx");//user profile
                    break;
                case 2:
                    Response.Redirect("~/Listing_Description.aspx");//add listings
                    break;
                case 3:
                    Response.Redirect("~/UserList.aspx");//web management (delete users)
                    break;
            }

        }
        else
        {
            test.Text = "Please login first !";
        }
    }

}
