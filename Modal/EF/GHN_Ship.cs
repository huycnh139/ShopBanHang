namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GHN_Ship
    {
        [Key]
        public int id_ship { get; set; }

        public int? id_hoadon { get; set; }

        public int? payment_type_id { get; set; }

        [StringLength(500)]
        public string note { get; set; }

        [StringLength(30)]
        public string required_note { get; set; }

        [StringLength(11)]
        public string return_phone { get; set; }

        [StringLength(200)]
        public string return_address { get; set; }

        public int? return_district_id { get; set; }

        public int? return_ward_code { get; set; }

        [StringLength(150)]
        public string return_name_ward { get; set; }

        [StringLength(150)]
        public string return_name_district { get; set; }

        [StringLength(200)]
        public string client_order_code { get; set; }

        [StringLength(200)]
        public string to_name { get; set; }

        [StringLength(11)]
        public string to_phone { get; set; }

        [StringLength(200)]
        public string to_address { get; set; }

        public int? to_ward_code { get; set; }

        public int? to_district_id { get; set; }

        [StringLength(150)]
        public string to_name_ward { get; set; }

        public string to_name_province { get; set; }

        [StringLength(150)]
        public string to_name_district { get; set; }

        public double? cod_amount { get; set; }

        public double? weight { get; set; }

        public double? length { get; set; }

        public double? width { get; set; }

        public double? height { get; set; }

        public int? pick_station_id { get; set; }

        public double? insurance_value { get; set; }

        public int? service_id { get; set; }

        [StringLength(500)]
        public string content { get; set; }

        public double? feeship { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}
