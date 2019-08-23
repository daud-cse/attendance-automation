using deepp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.erp
{
  public  class DataSetService
    {
      PNSMSContext _contex = new PNSMSContext();
      public DataSet GetDataSetObject(string CommandText)
      {
        
          SqlConnection con = new SqlConnection();
          SqlCommand cmd = new SqlCommand();
          con.ConnectionString = _contex.Database.Connection.ConnectionString;
          cmd.Connection = con;
          cmd.CommandType = CommandType.Text;
          cmd.CommandText = CommandText;
          DataSet ds = new DataSet();
          System.Data.SqlClient.SqlDataAdapter ad = new SqlDataAdapter();
          ad.SelectCommand = cmd;
          ad.Fill(ds);
          con.Close();
          con.Dispose();
                
          return ds;
      }
      //public SqlParameterCollection SetSqlParameter()
      //{
      //    cmd.Parameters.Clear();
      //    return cmd.Parameters;
      //}
    }
}
