using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class SearchResult : System.Web.UI.Page
{
    private DataSet dataSet;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindGrid(Session["Search"].ToString());
            /*
            //get the query string(filter)
            string model = Request.QueryString["model"];
            string make = Request.QueryString["make"];
            string mileage = Request.QueryString["mileage"];
            string condition = Request.QueryString["condition"];
            if (model != null && make != null && mileage != null && condition != null)
            {
                BindGrid("SELECT * FROM Car WHERE model=\'" + model
                    + "\' AND make =\'" + make +
                    "\'" + " AND mileage =\'" + mileage +
                    "\'" + " AND condition =\'" + condition);
            }
            */
        }
    }

    private void BindGrid(string cmd = "SELECT * FROM Car")
    {
        // Define data objects
        SqlConnection conn;
        dataSet = new DataSet();
        SqlDataAdapter adapter;
        // Read the DataSet from the ViewState if available
        if (ViewState["CarsDataSet"] == null)
        {
            // Read the connection string from Web.config
            string connectionString =
                ConfigurationManager.ConnectionStrings[
                "database"].ConnectionString;
            // Initialize connection
            conn = new SqlConnection(connectionString);
            // Create adapter
            adapter = new SqlDataAdapter(
                cmd,
                conn);
            // Fill the DataSet
            adapter.Fill(dataSet, "Cars");
            // Store the DataSet in view state
            ViewState["CarsDataSet"] = dataSet;
        }
        else
        {
            dataSet = (DataSet)ViewState["CarsDataSet"];
        }
        // Prepare the sort expression using the gridSortDirection and
        // gridSortExpression properties
        string sortExpression;
        if (gridSortDirection == SortDirection.Ascending)
        {
            sortExpression = gridSortExpression + " ASC";
        }
        else
        {
            sortExpression = gridSortExpression + " DESC";
        }
        // Sort the data
        dataSet.Tables["Cars"].DefaultView.Sort = sortExpression;
        // Bind the grid to the DataSet
        carsGrid.DataSource = dataSet.Tables["Cars"].DefaultView;
        carsGrid.DataBind();
    }

    protected void cardsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // Retrieve the new page index
        int newPageIndex = e.NewPageIndex;
        // Set the new page index of the GridView
        carsGrid.PageIndex = newPageIndex;
        // Bind the grid to its data source again to update its
        // contents
        BindGrid();
    }
    protected void carsGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        // Retrieve the name of the clicked column (sort expression)
        string sortExpression = e.SortExpression;
        // Decide and save the new sort direction
        if (sortExpression == gridSortExpression)
        {
            if (gridSortDirection == SortDirection.Ascending)
            {
                gridSortDirection = SortDirection.Descending;
            }
            else
            {
                gridSortDirection = SortDirection.Ascending;
            }
        }
        else
        {
            gridSortDirection = SortDirection.Ascending;
        }
        // Save the new sort expression
        gridSortExpression = sortExpression;
        // Rebind the grid to its data source
        BindGrid();
    }

    private SortDirection gridSortDirection
    {
        get
        {
            // Initial state is Ascending
            if (ViewState["GridSortDirection"] == null)
            {
                ViewState["GridSortDirection"] = SortDirection.Ascending;
            }
            // Return the state
            return (SortDirection)ViewState["GridSortDirection"];
        }
        set
        {
            ViewState["GridSortDirection"] = value;
        }
    }

    private string gridSortExpression
    {
        get
        {
            if (ViewState["GridSortExpression"] == null)
            {
                ViewState["GridSortExpression"] = "Model";
            }
            // Return the sort expression
            return (string)ViewState["GridSortExpression"];
        }
        set
        {
            ViewState["GridSortExpression"] = value;
        }
    }

    protected void carsGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Obtain the index of the selected row

    }

    protected void Name_Click(object sender, EventArgs e)
    {
        //Get the clicked row
        GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;

        string id = carsGrid.Rows[clickedRow.RowIndex].Cells[0].Text;

        Response.Redirect("CarDetails.aspx?id=" + id);
    }
}