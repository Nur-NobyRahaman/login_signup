using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class BaseAccount
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool VerifyLogin()
        {
           
            DataTable dataTable = new DataTable();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOst_lstMember";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;


            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            cmd.Dispose();
            connection.Close();
            // link u sintex
           


            if (dataTable.Rows.Count  > 0)
            {
                var pData = (from p in dataTable.AsEnumerable()
                             where p.Field<string>("Name") == this.UserName && p.Field<string>("Password") == this.Password
                             select new
                             {
                                 UserName = p.Field<string>("Name"),
                                 Role = p.Field<string>("ServiceType")

                             }).ToList();
                foreach (var obj in pData) { 
                    this.UserName = obj.UserName;
                    this.Role = obj.Role;
                }
                return true;
            }
            return false;

            
        }
    }

   
}