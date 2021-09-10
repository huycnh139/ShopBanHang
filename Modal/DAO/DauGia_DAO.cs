using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class DauGia_DAO
    {
        Context_ db = null;
        public DauGia_DAO()
        {
            db = new Context_();
        }

        public int get_count()
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.DauGias.Count();
        }
        public int get_count_run()
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.DauGias.Count(x=>x.status_=="Đang áp dụng");
        }
        public int get_count_stop()
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.DauGias.Count(x => x.status_ == "Kết Thúc");
        }

        public List<DauGia> get_daugia(string status)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.DauGias.Where(x=>x.status_==status).ToList();
        }
        
        public DauGia update_daugia_result(int id_daugia,string result)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var res = db.DauGias.FirstOrDefault(x => x.id_daugia == id_daugia);
            if (res != null)
            {
                res.result = result;
                var ok=db.SaveChanges();
                if (ok>0) return res;
                return null;
            }
            else
            {
                return null;
            }
        }
        public DauGia update_daugia_status(int id_daugia,int date)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var res = db.DauGias.FirstOrDefault(x => x.id_daugia == id_daugia);
            if (res != null)
            {
                if (date == 0)
                {
                    res.status_ = "Kết Thúc";

                    var ok = db.SaveChanges();
                    if (ok > 0) return res;
                    return null;
                }
                else return null;
            }
            else
            {
                return null;
            }
        }

        public List<DauGia> get_daugia_use(string status_,string status_use)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.DauGias.Where(x =>x.status_==status_&& x.status_use == status_use).ToList();
        }
        
        public List<DauGia> get_daugia_use_sanpham(string status_, string status_use,int id_sanpham)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.DauGias.Where(x => x.status_ == status_ && x.status_use == status_use&&x.id_sanpham==id_sanpham).ToList();
        }

        public DauGia update_daugia_use(int id_daugia, int date)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var res = db.DauGias.FirstOrDefault(x => x.id_daugia == id_daugia);
            if (res != null)
            {
                if (date == 0)
                {
                    res.status_ = "Kết Thúc";

                    var ok = db.SaveChanges();
                    if (ok > 0) return res;
                    return null;
                }
                else return null;
            }
            else
            {
                return null;
            }
        }

        public int update_daugia_status_(int id_daugia)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var res = db.DauGias.FirstOrDefault(x => x.id_daugia == id_daugia);
            if (res != null)
            {
                res.status_use = "Đã Sử Dụng";
                db.SaveChanges();

                return 1;
            }
            return 0;
        }


        public List<DauGia> get_DauGia_all()
        {
            return db.DauGias.ToList();
        }
        public string get_DauGia_id(int? id)
        {
            var res = db.DauGias.FirstOrDefault(x => x.id_daugia == id && x.status_ == "Mới Thêm");
            if (res != null)
            {
                db.DauGias.Remove(res);
                db.SaveChanges();
                return "Xoá Thành Công";
            }
            else
            {

                var res1 = db.DauGias.FirstOrDefault(x => x.id_daugia == id);
                return "Không thể xoá vì đang trong tình trang" + res1.status_;
            }

        }
        public int setdaugia(DauGia dg)
        {
            db.DauGias.Add(dg);
            db.SaveChanges();
            return 0;
        }
        public DauGia get_DauGia_id_(int? id)
        {
            return db.DauGias.FirstOrDefault(x => x.id_daugia == id);
        }
        public DauGia update_DauGia_id_(int? id)
        {
            var res = db.DauGias.FirstOrDefault(x => x.id_daugia == id);
            res.status_ = "Đang áp dụng";
            db.SaveChanges();
            return res;
        }
        public void update_DauGia()
        {
            var res = db.DauGias.Where(x => x.status_ == "Mới Thêm").ToList();
            var timeNow = DateTime.Now;
            foreach(var item in res)
            {
                if (item.time_start!=null)
                {
                    DateTime dateTime = item.time_start ?? new DateTime(2012, 12, 25, 10, 30, 50);
                    if ((timeNow - dateTime).Milliseconds >=0)
                    {
                        item.status_ = "Đang áp dụng";
                        db.SaveChanges();
                    }
                }
                
               
            }

        }
    }
}
