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
        Response.Redirect("~/SearchResult.aspx");
        }

        private string selectStatement()
        {
            statement = "SELECT * FROM [Car] ";
            //if there is a where condition
            if (tbModel.Text != null || tbMake.Text != null || tbMilage.Text != null || tbAge.Text != null || tbEngine.Text != null || tbCondition.Text != null )
            {
                statement += "WHERE ";
                //model
                if (tbModel.Text != null)
                {
                    statement += "Model LIKE \'" + tbModel.Text + "\' AND ";
                }
                //make
                if (tbMake.Text != null)
                {
                    statement += "Make LIKE \'" + tbMake.Text + "\' AND ";
                }
                //milage min
                if (tbMilage.Text != null)
                {
                    statement += "Milage LIKE \'" + tbMilage.Text + "\' AND ";
                }
                //age 
                if (tbAge.Text != null)
                {
                    statement += "Age LIKE \'" + tbAge.Text + "\' AND ";
                }
                //engine
                if (tbEngine.Text != null)
                {
                    statement += "Make LIKE \'" + tbEngine.Text + "\' AND ";
                }
                //condition
                if (tbCondition.Text != null)
                {
                    statement += "Make LIKE \'" + tbCondition.Text + "\' AND ";
                }
                statement.Substring(0,statement.Length - 4);
                statement += ";";
            }
            return statement;
        }
    }
