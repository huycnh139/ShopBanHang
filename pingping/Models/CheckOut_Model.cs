using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pingping.Models
{
    public class CheckOut_Model
    {
        public int  id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public int id_size { get; set; }
        public string color { get; set; }
    }
}