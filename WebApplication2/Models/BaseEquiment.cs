using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class BaseEquipment
    {
        public string Name { get; set; }
        public int EqCount { get; set; }
        public int No { get; set; }
        public DateTime EntryDate { get; set; }

        public List<BaseEquipment> ListEquipment { get; set; }
        public BaseEquipment() { 
        ListEquipment = new List<BaseEquipment>();
        }

        public static List<BaseEquipment> ListEquipmentData()
                                                                                                                                                                                                                                                           {
            List<BaseEquipment> plsData = new List<BaseEquipment>();
          
            for(int i=1; i<=50; i++)
            {
                BaseEquipment equipmentObj = new BaseEquipment();
                equipmentObj.Name = "Laptop_"+ i.ToString();
                equipmentObj.No = i;
                equipmentObj.EqCount = 5+i;
                equipmentObj.EntryDate = DateTime.Now.Date;
                plsData.Add(equipmentObj);
            }
          

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
    }
}