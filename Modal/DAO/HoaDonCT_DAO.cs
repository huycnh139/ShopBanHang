using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class HoaDonCT_DAO
    {
        Context_ db = null;
        public HoaDonCT_DAO()
        {
            db = new Context_();
        }

        public HoaDonCT createHoaDonCT(int id_hoadon,int id_sanpham,float dongia,DateTime thoigian,int soluong,int id_size,string color)
        {
            db.Configuration.ProxyCreationEnabled = false;
            HoaDonCT result = new HoaDonCT();

            //xử lý sp trùng nhau trong hoadonct
            var res_ = db.HoaDonCTs.FirstOrDefault(x => x.id_hoadon == id_hoadon && x.id_sanpham == id_sanpham);
            if (res_ != null)
            {
                var dao_size = new Size_DAO();
                var res_size = dao_size.get_size_(id_size);
                if (res_size != null) //tồn tại sp có size đó
                {
                    //var dao_color = new Color_DAO();
                    //var res_color = dao_color.get_color_size_id_color(res_size,color);

                    var checkhdct_color = db.HoaDonCTs.Count(x => x.color == color && id_size == res_size.id_size);
                    if (checkhdct_color > 0)
                    {
                        res_.thoigian = thoigian;
                        res_.soluong += soluong;
                        db.SaveChanges();
                    }
                    else
                    {
                        var modal_To_EF = new HoaDonCT()
                        {
                            id_hoadon = id_hoadon,
                            id_sanpham = id_sanpham,
                            dongia = dongia,
                            thoigian = thoigian,
                            soluong = soluong,
                            trangthai = "Chưa Thanh Toán",
                            id_size = id_size,
                            color = color
                        };

                        result = db.HoaDonCTs.Add(modal_To_EF);
                        db.SaveChanges();
                        if (result != null) return result;
                    }

                }
                else //tạo mới hdct
                {
                    var modal_To_EF = new HoaDonCT()
                    {
                        id_hoadon = id_hoadon,
                        id_sanpham = id_sanpham,
                        dongia = dongia,
                        thoigian = thoigian,
                        soluong = soluong,
                        trangthai = "Chưa Thanh Toán",
                        id_size = id_size,
                        color = color
                    };

                    result = db.HoaDonCTs.Add(modal_To_EF);
                    db.SaveChanges();
                    if (result != null) return result;
                }
            }
            else
            {
                var modal_To_EF = new HoaDonCT()
                {
                    id_hoadon = id_hoadon,
                    id_sanpham = id_sanpham,
                    dongia = dongia,
                    thoigian = thoigian,
                    soluong = soluong,
                    trangthai = "Chưa Thanh Toán",
                    id_size = id_size,
                    color = color
                };

                result = db.HoaDonCTs.Add(modal_To_EF);
                db.SaveChanges();
                if (result != null) return result;
            }
            return null;
        }

        public List<HoaDonCT> get_hoadonct(int id_hoadon)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.HoaDonCTs.Where(x => x.id_hoadon == id_hoadon).ToList();
        }
        public int get_hoadonct_id(int id_hdct)
        {
            db.Configuration.ProxyCreationEnabled = false;

            int id =db.HoaDonCTs.FirstOrDefault(x => x.id_hoadonct == id_hdct).id_hoadonct;
            if (id > 0) return id;
            return 0;
        }

        public int update_hdct(int id_hdct,int id_size,string color)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var res_ = db.HoaDonCTs.FirstOrDefault(x => x.id_hoadonct == id_hdct);
            if (res_ != null)
            {
                res_.id_size = id_size;
                res_.color = color;
                db.SaveChanges();
                return res_.id_hoadonct;
            }
            return 0;
        }

        public double? update_hdct_tonggia(int? id_hdct,int id_hoadon, int soluong)
        {
            if (id_hdct == null)
            {
                return 0;
            }
            else
            {
                double? sum = 0;
                var hdct = db.HoaDonCTs.Where(x=>x.id_hoadon==id_hoadon).ToList();
                foreach (var c in hdct)
                {
                    sum += c.soluong * c.dongia;
                }

                var hd = db.HoaDons.Find(id_hoadon);
                if (hd != null)
                {
                    hd.tonggia = sum;
                    db.SaveChanges();
                    return sum;
                }
            }
            return -1;
        }

        #region 18-05
        public List<HoaDonCT> get_hdct_all()
        {
            return db.HoaDonCTs.ToList();
        }
        public int remove_hdct_idsanpham(int? type)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var res = db.HoaDonCTs.FirstOrDefault(x => x.id_sanpham == type && x.trangthai == "Chưa Thanh Toán");
            if (res != null)
            {
                db.HoaDonCTs.Remove(res);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }
        public List<HoaDonCT> get_hdct_hoadon(int id)
        {
            return db.HoaDonCTs.Where(x => x.id_hoadon == id).ToList();
        }
        #endregion

        #region CRUD
        public double? deleted_hdct(int? id_hdct,int id_hoadon)
        {
            if (id_hdct == null)
            {
                return 0;
            }
            else
            {
                HoaDonCT hdct = db.HoaDonCTs.Find(id_hdct);
                db.HoaDonCTs.Remove(hdct);
                db.SaveChanges();
                if (hdct != null)
                {
                    double? sum = 0; int q = 0;
                    var soluong_hdct = db.HoaDonCTs.Where(x => x.id_hoadon == id_hoadon).ToList();
                    foreach(var c in soluong_hdct)
                    {
                        sum += c.soluong * c.dongia;
                        q++;
                    }
                    var hd_soluong = db.HoaDons.FirstOrDefault(x => x.id_hoadon == id_hoadon);
                    hd_soluong.soluong = q;
                    hd_soluong.tonggia = sum;
                    db.SaveChanges();
                    return sum;
                }
            }
            return -1;
        }
        public int update_hdct(int? id_hdct,int soluong)
        {
            if (id_hdct == null)
            {
                return 0;
            }
            else
            {
                HoaDonCT hdct = db.HoaDonCTs.Find(id_hdct);
                
                if (hdct != null)
                {
                    hdct.soluong = soluong;
                    db.SaveChanges();
                    return 1;
                }
            }
            return -1;
        }
        #endregion

        #region 22/05
        public List<HoaDonCT> hdct_khachhang(int? id)
        {
            return db.HoaDonCTs.Where(x => x.HoaDon.id_nguoimua == id && x.trangthai == "Đã Thanh Toán").ToList();
        }
        public List<HoaDonCT> hdct_khachhang_(int? id)
        {
            return db.HoaDonCTs.Where(x=>x.HoaDon.id_nguoimua==id).ToList();
        }
        public string get_hdct_hoadon_(int? id)
        {
            var res = db.HoaDonCTs.Where(x => x.id_hoadon == id && x.trangthai == "Chưa Thanh Toán").ToList();
            foreach (var item in res)
            {
                var res1 = db.SanPhams.FirstOrDefault(x => x.id_sanpham == item.id_sanpham);
                if (res1 != null)
                {
                    res1.soluong -= item.soluong;
                    if (res1.soluong <= 0)
                    {
                        res1.tinhtrang = "HẾT HÀNG";
                        res1.soluong = 0;
                    }
                    else if (res1.soluong < 5)
                    {
                        res1.tinhtrang = "SẮP HẾT";
                    }
                    db.SaveChanges();
                    var res3 = db.Colors.FirstOrDefault(x => x.color1 == item.color && x.Size.id_sanpham == item.id_sanpham);
                    //var res2 = db.Sizes.FirstOrDefault(x => x.id_size == item.id_size);
                    if (res3 != null)
                    {
                        res3.soluong -= item.soluong;
                        if (res3.soluong <= 0)
                        {
                            db.Colors.Remove(res3);
                        }
                        db.SaveChanges();
                        var res2 = db.Sizes.FirstOrDefault(x => x.id_size == item.id_size);
                        //var res3 = db.Colors.FirstOrDefault(x => x.color1 == item.color&&x.Size.id_sanpham==item.id_sanpham);
                        if (res2 != null)
                        {
                            res2.soluong -= item.soluong;
                            if (res2.soluong <= 0)
                            {
                                db.Sizes.Remove(res2);
                            }
                            db.SaveChanges();
                        }
                        //else return "Sản Phẩm: " + item.SanPham.tensp + " Size: " + item.id_size + " Màu:"+item.color+" Không được tìm thấy";
                        else return "Sản Phẩm: " + item.SanPham.tensp + " Size: " + item.id_size + " Không được tìm thấy";
                    }
                    else return "Sản Phẩm: " + item.SanPham.tensp + " Size: " + item.id_size + " Màu:" + item.color + " Không được tìm thấy";
                }
                else
                {
                    return "Sản Phẩm" + item.SanPham.tensp + "Không được tìm thấy";
                }
            }
            foreach (var item in res)
            {
                item.trangthai = "Đã Thanh Toán";
                db.SaveChanges();
            }
            return "Đặt Hàng Thành Công";
        }
        #endregion
        public List<HoaDonCT> get_hoadon_damua_all_exportexcel(List<int> arr, DateTime fromdate, DateTime todate)
        {
            if (arr.Count > 0&& fromdate!=null&& todate!=null)
            {
                var lsthdct = db.HoaDonCTs.Where(x => x.HoaDon.trangthai == "Đã Thanh Toán").ToList();
                List<HoaDonCT> lstexcel = new List<HoaDonCT>();
                foreach (var item in arr)
                {
                    var lst = lsthdct.Where(x => x.id_sanpham == item && x.HoaDon.thoigian >= fromdate&& x.HoaDon.thoigian<= todate.AddDays(1)).ToList();
                    if (lst.Count > 0)
                    {
                        foreach(var res in lst)
                        {
                            lstexcel.Add(res);
                        }
                    }
                }
                return lstexcel;
            }
            return null;
            
        }
    }
}
