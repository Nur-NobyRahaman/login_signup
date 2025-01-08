using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class BaseCustomer
    {
       public int CustomerId { get; set; }
       public string CustomerName { get; set; }
       public string CustomerMobile { get; set; }

        public static List<BaseCustomer> ListCustomer()
        {
            List<BaseCustomer> plsCustomer = new List<BaseCustomer>();

            //DataTable dataTable = new DataTable();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOST_LstCustomer";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            //cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;


            SqlDataReader mrd = cmd.ExecuteReader();
            if (mrd.HasRows)
            {
                while (mrd.Read())
                {
                    BaseCustomer obj = new BaseCustomer();
                    obj.CustomerId = Convert.ToInt32(mrd["CustomerId"].ToString());
                    obj.CustomerName = mrd["CustomerName"].ToString();
                    obj.CustomerMobile = mrd["CustomerMobile"].ToString();
                    plsCustomer.Add(obj);

                }
            }
            cmd.Dispose();
            connection.Close();   
            return plsCustomer;
        }

        // get ListCustomerEquipment  
        public static DataTable ListCustomerEquipment()
        {
          

            DataTable dataTable = new DataTable();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOST_LstCustomerEquipAssignment";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            //cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);


         
            cmd.Dispose();
            connection.Close();
            // link u sintex

            //var pData = (from p in dataTable.AsEnumerable()
                        
            //             select new
            //             {
            //                 CustomerId = p["CustomerId"].ToString(),
            //                 CustomerName = Convert.ToInt32(p["CustomerName"].ToString()),
            //                 CustomerMobile = Convert.ToInt32(p["CustomerMobile"].ToString()),
            //                 EquipCount = Convert.ToInt32(p["EquipCount"].ToString()),









            //             }).ToList();



            return dataTable;
        }

        // insert customer 
        public int SaveCustomer()
        {
            // connect sql database
            string connecting = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connecting);
            connection.Open();

            SqlCommand cmd =new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOST_InsrtCustomer";
            cmd.Parameters.Clear();

            cmd.Parameters.Add(new SqlParameter("@CustomerName",this.CustomerName));
            cmd.Parameters.Add(new SqlParameter("@CustomerMobile",this.CustomerMobile));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            return result;
        }

        public int EquipCustomerAssign(int CustomerID, int EquipmentID, int EquipCount) {

            String connecting = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connecting);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "spOST_InstEquipAssignment";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@CustomerID", CustomerID));
            cmd.Parameters.Add(new SqlParameter("@EquipmentID", EquipmentID));
            cmd.Parameters.Add(new SqlParameter("@EquipCount", EquipCount));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();

            return result;
        }

    }
}