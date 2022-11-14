using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/*
 * Author: Adrian
 * 
 */
public partial class Admin_CreateLoc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    //list view commands
    protected void LocationListView_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        //delete ice button
        if (e.CommandName == "DeleteIce")
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                try
                {
                    //retrieve location id from current row
                    int locationId = int.Parse(((Label)e.Item.FindControl("lblLocationId")).Text);
                    int usageCount = 0;

                    //define sql connection
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

                    //check number of times location is used
                    SqlCommand chkLocUsage = new SqlCommand("SELECT COUNT(*) AS COUNT FROM IceTimes WHERE LocationID=@LocationID", con);

                    //assign location id for sql usage
                    SqlParameter sqlLocationId = new SqlParameter("@LocationID", SqlDbType.Int);
                    sqlLocationId.Value = locationId;
                    chkLocUsage.Parameters.Add(sqlLocationId);

                    con.Open();

                    //run select statement
                    SqlDataReader reader = chkLocUsage.ExecuteReader();

                    //call read to access data
                    while (reader.Read())
                    {
                        //retrieve results of select statement
                        usageCount = (int)reader["COUNT"];
                    }

                    reader.Close();

                    //check count for ice times that use the location
                    if (usageCount > 0)
                    {
                        lblResult.Text = "This location cannot be deleted. <br /> Number of ice times using this location: " + usageCount;
                    }
                    else
                    {
                        //create new location id parameter for delete statement
                        SqlParameter sqlDelLocationId = new SqlParameter("@LocationID", SqlDbType.Int);
                        sqlDelLocationId.Value = locationId;

                        SqlCommand delLocation = new SqlCommand("DELETE FROM Locations WHERE LocationId = @LocationId", con);
                        delLocation.Parameters.Add(sqlDelLocationId);
                        delLocation.ExecuteNonQuery();

                        lblResult.Text = "The location has been deleted.";

                        LocationListView.DataBind();
                    }

                    con.Close();
                }
                catch (SqlException ex)
                {
                    lblResult.Text = ex.Message;
                }

            }
        }
        //if button other than delete is clicked, to clear the label
        else
        {
            lblResult.Text = "";
        }
    }
}