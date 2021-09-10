using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class LuongTruyCap_DAO
    {
        Context_ db = null;
        public LuongTruyCap_DAO()
        {
            db = new Context_();
        }

        public int update_truycap_soluong(string namepage,string loaikhachhang) //giá trị namepage phải đúng
        {
            db.Configuration.ProxyCreationEnabled = false;

            var res_ = db.LuongTruyCaps.FirstOrDefault(x => x.namepage == namepage);
            DateTime now = DateTime.Now;
            if(loaikhachhang=="Khách Vãn Lai")
            {
                if (res_ != null)
                {
                    res_.soluong_vl += 1;
                    res_.time_update = now;
                    db.SaveChanges();
                    return res_.soluong_vl;
                }
                else
                {
                    var create = new LuongTruyCap()
                    {
                        namepage = namepage,
                        soluong_vl = 1,
                        time_update = now
                    };
                    var result = db.LuongTruyCaps.Add(create);
                    db.SaveChanges();
                    if (result != null) return create.soluong_vl;
                }
            }
            else
            {
                if (res_ != null)
                {
                    res_.soluong_kh += 1;
                    res_.time_update = now;
                    db.SaveChanges();
                    return res_.soluong_kh;
                }
                else
                {
                    var create = new LuongTruyCap()
                    {
                        namepage = namepage,
                        soluong_kh = 1,
                        time_update = now
                    };
                    var result = db.LuongTruyCaps.Add(create);
                    db.SaveChanges();
                    if (result != null) return create.soluong_kh;
                }
            }
            return 0;
        }

        public List<LuongTruyCap> get_luongtruycap()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.LuongTruyCaps.ToList();
        }
        //public List<Color> get_color_size_id(int id_size)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;

        //    return db.Colors.Where(x => x.id_size == id_size).ToList();
        //}
    }
}
