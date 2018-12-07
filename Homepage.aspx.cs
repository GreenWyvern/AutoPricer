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
                case 0:
                    Response.Redirect("~/UserProfile.aspx");//user profile
                    break;
                case 1:
                    Response.Redirect("~/Listing_Results.aspx");//listings
                    break;
                case 2:
                    Response.Redirect("~/UserList.aspx");//web management (delete users)
                    break;
                case 3:
                    Response.Redirect("~/FAQ.aspx");//FAQ
                    break;
            }
        }
    }

}
