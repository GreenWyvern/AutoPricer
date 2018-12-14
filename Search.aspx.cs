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
            if (tbModel.Text != null || tbMake.Text != null || tbMilageMin.Text != null || tbMilageMax.Text != null || tbAgeMin.Text != null || tbAgeMax.Text != null || tbEngine.Text != null || tbCondition.Text != null )
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
                if (tbMilageMin.Text != null)
                {
                    statement += "Milage >=" + tbMilageMin.Text + "\' AND ";
                }
                //milage max
                if (tbMilageMin.Text != null)
                {
                    statement += "Milage <=" + tbMilageMax.Text + "\' AND ";
                }
                //age min
                if (tbAgeMin.Text != null)
                {
                    statement += "Age >=" + tbAgeMin.Text + "\' AND ";
                }
                //age max
                if (tbAgeMax.Text != null)
                {
                    statement += "Age <=" + tbAgeMax.Text + "\' AND ";
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
				/*
				if (tbPriceMin.Text != null)
                {
                    statement += "Price >='" + tbPriceMin.Text + "\' AND ";
                }
				if (tbPriceMax.Text != null)
                {
                    statement += "Price <='" + tbPriceMax.Text + "\' AND ";
                }*/
                statement.Substring(statement.Length - 4);
                statement += ";";
            }
            return statement;
        }
    }
