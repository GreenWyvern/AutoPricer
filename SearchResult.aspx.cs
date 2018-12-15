using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SearchResult : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AutoDB.mdf;Integrated Security=True");
    //private int listingId;
    //private string userName;
    //private int carId;
    //private string suggestedPrice;
    //private string listingStatus;
    public string tmp;
    private string connectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        connectionString = ConfigurationManager.ConnectionStrings["database"].ToString();
        if (!IsPostBack)
            filldata();
    }

    private void filldata()
    {
        tmp = (string)Session["Search"];
        //SqlConnection conn = new SqlConnection(connectionString);
        //SqlDataAdapter da = new SqlDataAdapter(, conn);
        SqlDataAdapter da = new SqlDataAdapter("Select * from car where model = \'corolla\';", conn);
        SqlDataSource1.SelectCommand = tmp;
        DataSet ds = new DataSet();
        da.Fill(ds);
        DataList1.DataBind();
    }

    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {

        if (e.CommandName == "Edit")
        {
            DataList1.EditItemIndex = e.Item.ItemIndex;
            filldata();
        }
        if (e.CommandName == "Cancel")
        {
            DataList1.EditItemIndex = -1;
            filldata();
        }
        else if (e.CommandName == "Update")
        {
            string id = ((Label)e.Item.FindControl("ListingIDLabel")).Text;
            string sg = ((TextBox)e.Item.FindControl("TextBox1")).Text;
            string ls = ((TextBox)e.Item.FindControl("TextBox2")).Text;
            SqlCommand cmd = new SqlCommand("update Listing set SuggestedPrice='" + sg + "',ListingStatus='" + ls + "' where ListingID=" + id, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            DataList1.EditItemIndex = -1;
            filldata();
        }
    }
}
