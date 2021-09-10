using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class Coupon_DAO
    {
        Context_ db = null;
        public Coupon_DAO()
        {
            db = new Context_();
        }

        public Coupon get_coupon_id(int id_sanpham,string status_)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.Coupons.FirstOrDefault(x => x.id_sanpham == id_sanpham&&x.status_==status_);
        }

        public Coupon get_coupon_MaCoupon(string Ma_Coupon)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.Coupons.FirstOrDefault(x =>x.Ma_Coupon == Ma_Coupon);
        }

        public int update_coupon(string mcp)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var res = db.Coupons.FirstOrDefault(x => x.Ma_Coupon == mcp);
            if (res != null)
            {
                res.status_ = "Đã Sử Dụng";
                db.SaveChanges();

                return 1;
            }
            return 0;
        }
    }
}
