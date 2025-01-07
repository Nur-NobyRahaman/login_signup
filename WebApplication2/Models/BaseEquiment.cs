using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WebApplication2.Models
{
    [Serializable]
    public class BaseEquipment
    {
        [DataMember]
        public int EquipmentId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int EqCount { get; set; }
        [DataMember]
        //public int No { get; set; }
        public DateTime EntryDate { get; set; }

        public List<BaseEquipment> ListEquipment { get; set; }
        public BaseEquipment() { 
        ListEquipment = new List<BaseEquipment>();
        }

        public static List<BaseEquipment> ListEquipmentData()
                                                                                                                                                                                                                                                           {
            List<BaseEquipment> plsData = new List<BaseEquipment>();

            //DataTable dataTable = new DataTable();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOST_LstEquipment";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            //cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;


            SqlDataReader mrd = cmd.ExecuteReader();
            if (mrd.HasRows)
            {
                while (mrd.Read()) { 
                BaseEquipment obj = new BaseEquipment();
                    obj.EquipmentId = Convert.ToInt32(mrd["EquimentId"].ToString());
                    obj.Name = mrd["EquipmentName"].ToString();
                    obj.EqCount= Convert.ToInt32(mrd["Quantity"].ToString());
                    obj.EntryDate = Convert.ToDateTime(mrd["EntryDate"].ToString());
                    plsData.Add(obj);

                }
                }
            cmd.Dispose();
            connection.Close();
            // link u sintex
            //var pData = (from p in dataTable.AsEnumerable() 
            //             where p.Field<string>("Name")==this.UserName && p.Field<string>("Password")==this.Password
            //             select new
            //             {
            //                 UserName=p.Field<string>("Name")

            //             } ).SingleOrDefault();


           
            //for (int i=1; i<=50; i++)
            //{
            //    BaseEquipment equipmentObj = new BaseEquipment();
            //    equipmentObj.Name = "Laptop_"+ i.ToString();
            //    equipmentObj.No = i;
            //    equipmentObj.EqCount = 5+i;
            //    equipmentObj.EntryDate = DateTime.Now.Date;
            //    plsData.Add(equipmentObj);
            //}
          

            //equipmentObj = new BaseEquipment();
            //equipmentObj.Name = "Mouse";
            //equipmentObj.EqCount = "50";
            //equipmentObj.EntryDate = DateTime.Now.Date;
            //plsData.Add(equipmentObj);

            //equipmentObj = new BaseEquipment();
            //equipmentObj.Name = "Keyboard";
            //equipmentObj.EqCount = "25";
            //equipmentObj.EntryDate = DateTime.Now.Date;
            //plsData.Add(equipmentObj);
            return plsData;
        }


        public  int  saveEquipment()
        {
          
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOST_InsrtEquipment";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@Name", this.Name));
            cmd.Parameters.Add(new SqlParameter("@EqCount", this.EqCount));
            cmd.Parameters.Add(new SqlParameter("@EntryDate", this.EntryDate));
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;


            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            return result;
        }
    }
}