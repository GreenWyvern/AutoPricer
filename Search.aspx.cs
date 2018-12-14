using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

    public partial class Search : System.Web.UI.Page
    {
        private string statement;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
        Session["Search"] = selectStatement();
        //Response.Redirect("~/SearchResult.aspx");
        tbModel.Text = (string)Session["Search"];
        }

        private string selectStatement()
        {
            statement = "SELECT * FROM [Car] ";
            //if there is a where condition
            if (tbModel.Text != "" || tbMake.Text != "" || tbMilage.Text != "" || tbAge.Text != "" || tbEngine.Text != "" || tbCondition.Text != "")
            {
                statement += "WHERE ";
                //model
                if (tbModel.Text != "")
                {
                    statement += "Model LIKE \'" + tbModel.Text + "\' AND ";
                }
                //make
                if (tbMake.Text != "")
                {
                    statement += "Make LIKE \'" + tbMake.Text + "\' AND ";
                }
                //milage min
                if (tbMilage.Text != "")
                {
                    statement += "Milage LIKE \'" + tbMilage.Text + "\' AND ";
                }
                //age 
                if (tbAge.Text != "")
                {
                    statement += "Age LIKE \'" + tbAge.Text + "\' AND ";
                }
                //engine
                if (tbEngine.Text != "")
                {
                    statement += "Make LIKE \'" + tbEngine.Text + "\' AND ";
                }
                //condition
                if (tbCondition.Text != "")
                {
                    statement += "Make LIKE \'" + tbCondition.Text + "\' AND ";
                }
                statement.Substring(0,statement.Length - 4);
            statement += "JOIN Listing ON Listing.CarID = Car.CarId";
                //statement += ";";
            }
            return statement;
        }
    }
