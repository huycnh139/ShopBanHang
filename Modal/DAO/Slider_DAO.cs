using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class Slider_DAO
    {
        Context_ db = null;
        public Slider_DAO()
        {
            db = new Context_();
        }
        public List<Slider> slider_get_all()
        {
            return db.Slider.ToList();
        }
        public Slider get_slider_id(int id)
        {
            var res = db.Slider.FirstOrDefault(x => x.id_slider == id);
            if (res != null)
            {
                return res;
            }
            return null;
        }
        public int set_slider(Slider slider)
        {
            var res = db.Slider.FirstOrDefault(x => x.id_slider == slider.id_slider);
            if (res != null)
            {
                res.image = slider.image;
                res.title = slider.title;
                res.description = slider.description;
                res.sale = slider.sale;
                db.SaveChanges();
                return 1;
            }

            return 0;
        }
    }
}
