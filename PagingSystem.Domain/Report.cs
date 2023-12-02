using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

public class Reports
{
    public List<int> ReportID { get; set; }
    public List<string> Operator { get; set; }
    public List<string> Localisation { get; set; }

    public Reports()
    {
        ReportID = new List<int>();
        Operator = new List<string>();
        Localisation = new List<string>();
        ReadReports();
    }

    public void ReadReports()
    {
        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        connection.Open();
        string query = "Select ZgloszenieID, ZgloszenieOperator, ZgloszenieLokalizacja from dbo.Zgloszenia";
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Operator.Add((string)reader["ZgloszenieOperator"]);
            Localisation.Add((string)reader["ZgloszenieLokalizacja"]);
            ReportID.Add((int)reader["ZgloszenieID"]);
        }
    }

    public void DeleteReport(int ZgloszenieID)
    {
        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        connection.Open();
        string query = "Delete from dbo.Zgloszenia Where ZgloszenieID = @ZgloszenieID";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ZgloszenieID", ZgloszenieID);
        command.ExecuteNonQuery();
    }

    public int GetReportsCount()
    {
        return ReportID.Count;
    }
}
