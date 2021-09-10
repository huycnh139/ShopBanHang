using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pingping.Models
{
    public class PickOrder_Model
    {
        public int id_province { get; set; }
        public string name_province { get; set; }
        public int id_dictrict { get; set; }
        public string name_dictrict { get; set; }
        public int id_ward { get; set; }
        public string name_ward { get; set; }
        public string name_address { get; set; }
        public int id_service { get; set; }
        public string token { get; set; }
        public float length { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        public float weight { get; set; }
        public float feeship { get; set; }
    }
}