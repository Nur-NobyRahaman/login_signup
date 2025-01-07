﻿using System;
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

    }
}