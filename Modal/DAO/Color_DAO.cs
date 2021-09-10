using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class Color_DAO
    {
        Context_ db = null;
        public Color_DAO()
        {
            db = new Context_();
        }

        public List<Color> get_color_size_id(int id_size)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.Colors.Where(x => x.id_size == id_size).ToList();
        }
        public List<Color> get_color_size_id_color(int id_size,string color)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.Colors.Where(x => x.id_size == id_size&&x.color1==color).ToList();
        }

        #region 13-01
        public List<Color> get_color_all()
        {
            return db.Colors.ToList();
        }
        public Color get_color_idColor(int? id)
        {
            // db.Configuration.ProxyCreationEnabled = false;
            return db.Colors.FirstOrDefault(x => x.id_color == id);
        }
        public int set_color(Color color)
        {
            db.Colors.Add(color);
            db.SaveChanges();
            return 0;
        }
        public int checkupdate_color(Color color)
        {
            var res = db.Colors.FirstOrDefault(x => x.color1 == color.color1 && x.id_size == color.id_size);
            if (res != null)
            {
                res.soluong += color.soluong;
                db.SaveChanges();
            }
            else
            {
                db.Colors.Add(color);
                db.SaveChanges();
            }
            return 0;
        }
        public int remove_color(Color s)
        {
            db.Colors.Remove(s);
            db.SaveChanges();
            return 0;
        }
        #endregion
        public Color get_color_id(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Colors.FirstOrDefault(x => x.id_color == id);
        }
        public int update_color(Color s)
        {
             var res=db.Colors.FirstOrDefault(x => x.id_color == s.id_color);
            if (res != null)
            {
                res.soluong = s.soluong;
                db.SaveChanges();
            }
            return 0;
        }
        public IQueryable<Color> get_reSearch_product(string sTuKhoa)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Colors.Where(n => n.Size.SanPham.tensp.Contains(sTuKhoa)).OrderBy(n => n.soluong);
        }
    }
}
