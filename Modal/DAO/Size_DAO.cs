using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class Size_DAO
    {
        Context_ db = null;
        public Size_DAO()
        {
            db = new Context_();
        }

        public List<Size> get_size_idsanpham(int? id_sanpham)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.Sizes.Where(x => x.id_sanpham == id_sanpham).ToList();
        }
        public dynamic get_size(int id_size)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var res = db.Sizes.Count(x => x.id_size == id_size);
          
            if (res != 0) {

                var result = db.Sizes.FirstOrDefault(x => x.id_size == id_size);
                return result; }
            return 0;
        }

        public Size get_size_(int? id_size)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var res = db.Sizes.Count(x => x.id_size == id_size);

            if (res != 0)
            {

                var result = db.Sizes.FirstOrDefault(x => x.id_size == id_size);
                return result;
            }
            return null;
        }

        #region 13-01
        public Size get_size_name(string name, int? id_sanpham)
        {
            //db.Configuration.ProxyCreationEnabled = false;
            return db.Sizes.FirstOrDefault(x => x.size == name && x.id_sanpham == id_sanpham);
        }
        public Size set_size_(string name, int? id_sanpham, int soluong)
        {
            Size s = new Size();
            s.size = name;
            s.id_sanpham = id_sanpham;
            s.soluong = soluong;
            var res = db.Sizes.Add(s);
            db.SaveChanges();
            return res;
        }
        public int update_size(Size size)
        {
            var res=db.Sizes.FirstOrDefault(x => x.id_size == size.id_size);
            if (res != null)
            {
                res.soluong = size.soluong;
                db.SaveChanges();
                return 1;
            }
            return 0;
        }
        public int remove_size(Size s)
        {
            db.Sizes.Remove(s);
            db.SaveChanges();
            return 0;
        }
        #endregion

        public Size get_size_id(int? id_size)
        {
            return db.Sizes.FirstOrDefault(x => x.id_size == id_size);
        }

        public Size get_size_id_js(int? id_size)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Sizes.FirstOrDefault(x => x.id_size == id_size);
        }
    }
}
