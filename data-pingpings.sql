create database pingpings
go

use pingpings 
go

--create table NhaSanXuat --vd 1 nhà sản xuất tự đăng thì sao? cách 1 là tạo 1 nsx 'Tư Nhân'
--(
--	id_nhasx int identity primary key,
--	tennsx nvarchar(50),
--	thongtin nvarchar(max),
--	hinhanh varchar(max)
--) --
--go

create table LoaiSanPham
(
	id_loaisp int identity primary key,
	--id_nhasx int,
	tenloai nvarchar(50),
	tenngan nvarchar(50),
	thongtin nvarchar(max),
	hinhanh varchar(max),
	xeploai nvarchar(100) check(xeploai in('Trending Item','Hot Item','Onsale','Best Saller','Top Viewed')) default 'Hot Item',
	theloai nvarchar(20)
)
alter table HoaDon
add duyet bit default 0
create table SanPham
(
	id_sanpham int identity primary key,
	tensp nvarchar(200),
	id_loaisp int,
	tenngan nvarchar(50),
	soluong int,
	dongia float,
	giasale float,
	trangthai nvarchar(5) check(trangthai in(N'CŨ',N'MỚI')) default N'MỚI',
	hienthi nvarchar(5) check(hienthi in(N'ẨN',N'HIỆN')) default N'HIỆN',
	barcode varchar(100),
	tinhtrang nvarchar(10) check(tinhtrang in(N'CÒN HÀNG ',N'SẮP HẾT',N'HẾT HÀNG',N'TỒN KHO ĐÃ LÂU',N'HÀNG LỖI')),
	thongtin nvarchar(max),
	hinhanh1 varchar(max),
	hinhanh2 varchar(max),
	hinhanh3 varchar(max),
	hinhanh4 varchar(max),
	size varchar(2)

	CONSTRAINT FK_SanPham_LoaiSanPham FOREIGN KEY (id_loaisp) REFERENCES LoaiSanPham (id_loaisp) ON DELETE CASCADE,
	--id_danhgia int,
	--id_thetich int
	--id_binhluan int
)
--create table DanhGia
--(
--	id_danhgia int identity primary key,
--	id_sanpham int,
--	danhgia nvarchar(11) check(danhgia in (N'TỆ',N'TRUNG BÌNH',N'TỐT',N'RẤT TỐT')),
--	thoigian datetime,
--	id_nguoimua int
--)
create table TheTich
(
	id_thetich int identity primary key,
	id_sanpham int unique, --duy nhất
	chieucao float,
	chieurong float,
	chieudai float,
	cannang float
	CONSTRAINT FK_TheTich_SanPham FOREIGN KEY (id_sanpham) REFERENCES SanPham (id_sanpham) ON DELETE CASCADE,
)
--create table BinhLuan
--(
--	id_binhluan int identity primary key,
--	id_sanpham int,
--	noidung nvarchar(2500),
--	hienthi nvarchar(5) check(hienthi in(N'ẨN',N'HIỆN')) default N'HIỆN',
--	id_nguoimua int, --ko lien kết
--	id_nguoiban int  --ko liên kết
--)

create table TaiKhoan
(
	id_taikhoan int identity primary key,
	username varchar(250),
	hoten nvarchar(150),
	password varchar(250),
	password_old varchar(250), -- default=password
	email varchar(250), 
	loaitk bit default 1, -- 1=người mua, 0=người bán, 0=người quản lý pingpings,ai biết n
)

create table NguoiMua
(
	id_nguoimua int identity primary key,
	id_taikhoan int,
	phone int,
	street nvarchar(100),
	ward nvarchar(100),
	district nvarchar(100),
	province nvarchar(100),
	CONSTRAINT FK_NguoiMua_TaiKhoan FOREIGN KEY (id_taikhoan) REFERENCES TaiKhoan (id_taikhoan) ON DELETE CASCADE,
)
create table NguoiBan
(
	id_nguoiban int identity primary key,
	id_taikhoan int,
	taikhoanng varchar(100),
	nganhang nvarchar(100),
	phone int,
	street nvarchar(100),
	ward nvarchar(100),
	district nvarchar(100),
	province nvarchar(100),
	CONSTRAINT FK_NguoiBan_TaiKhoan FOREIGN KEY (id_taikhoan) REFERENCES TaiKhoan (id_taikhoan) ON DELETE CASCADE,
)

create table HoaDon
(
	id_hoadon int identity primary key,
	id_sanpham int,
	id_loaisp int,
	id_nguoimua int,
	mahd varchar(100),
	tonggia float,
	thoigian datetime,
	hinhthuctt nvarchar(100),
	soluong int,
	freeship float,
	trangthaigh nvarchar(50),
	trangthai nvarchar(50) check(trangthai in(N'Chưa Thanh Toán',N'Đã Thanh Toán')) default N'Chưa Thanh Toán',
	CONSTRAINT FK_HoaDon_NguoiMua FOREIGN KEY (id_nguoimua) REFERENCES NguoiMua (id_nguoimua) ON DELETE CASCADE,
)

create table HoaDonCT
(
	id_hoadonct int identity primary key,
	id_hoadon int,
	id_sanpham int,
	dongia float,
	thoigian datetime, --hdct add đồng thời sau hd
	soluong int,
	trangthai nvarchar(50) check(trangthai in(N'Chưa Thanh Toán',N'Đã Thanh Toán')) default N'Chưa Thanh Toán',
	size varchar(5), --chỉ lấy size
	color nvarchar(10), --chỉ lấy màu
	CONSTRAINT FK_HoaDonCT_HoaDon FOREIGN KEY (id_hoadon) REFERENCES HoaDon (id_hoadon) ON DELETE CASCADE,
	CONSTRAINT FK_HoaDonCT_SanPham FOREIGN KEY (id_sanpham) REFERENCES SanPham (id_sanpham) ON DELETE CASCADE,
)
alter table HoaDon
add trangthaigh nvarchar(50)
create table PhieuThanhToan
(
	id_phieutt int identity primary key,
	id_hoadon int,
	tensp nvarchar(250),
	soluong int,
	dongia float,
	thoigian datetime,
	trangthai nvarchar(50) check(trangthai in(N'Chưa Thanh Toán',N'Đã Thanh Toán')) default N'Đã Thanh Toán'
)

create table Sale
(
	id_sale int identity primary key,
	id_sanpham int,
	thoigianbd datetime default GETDATE(),
	thoigiankt datetime,
	sale int, --%
	thoigianc datetime,
	trangthai nvarchar(50) check(trangthai in(N'Hoạt Động',N'Ngưng Hoạt Động')) default N'Hoạt Động'
	CONSTRAINT FK_Sale_SanPham FOREIGN KEY (id_sanpham) REFERENCES SanPham (id_sanpham)
)
go
create table Size
(
	id_size int identity primary key,
	id_sanpham int,
	size varchar(5),
	soluong int
	CONSTRAINT FK_Size_SanPham FOREIGN KEY (id_sanpham) REFERENCES SanPham (id_sanpham)
)
create table Color
(
	id_color int identity primary key,
	id_size int,
	color nvarchar(10),
	soluong int
	CONSTRAINT FK_Color_Size FOREIGN KEY (id_size) REFERENCES Size (id_size)
)

--đấu gia theo giá thứ nhất(max)
create table DauGia
(
	id_daugia int identity(1,1) primary key,
	id_sanpham int,
	status_ nvarchar(20) check(status_ in(N'Đang áp dụng', N'Kết thúc')) default N'Đang áp dụng', --bắt đầu or kết thúc
	time_start datetime,
	time_end datetime,
	time_left datetime,
	status_use nvarchar(20) check(status_use in(N'Đã Sử Dụng',N'Chưa Sử Dụng'))  default N'Chưa Sử Dụng',
	result nvarchar(20),
	CONSTRAINT FK_DauGia_SanPham FOREIGN KEY (id_sanpham) REFERENCES SanPham(id_sanpham),
)
select *from DauGia where status_=N'Kết Thúc'	
drop table LichSuDG
create table LichSuDG
(
	id_lichsudg int identity primary key,
	id_taikhoan int,
	id_daugia int,
	value float,
	time_update datetime
	CONSTRAINT FK_LichSuDG_DauGia FOREIGN KEY (id_daugia) REFERENCES DauGia(id_daugia),
	CONSTRAINT FK_LichSuDG_TaiKhoan FOREIGN KEY (id_taikhoan) REFERENCES TaiKhoan(id_taikhoan)
)

create table LuongTruyCap
(
	id_ltc int identity primary key,
	namepage nvarchar(20),
	soluong_vl int,
	soluong_kh int,
	time_update datetime
)

create table Coupon(
	id_coupon int identity primary key,
	--id_khachhang int kh nào cũng use đc khi có mã
	id_sanpham int,
	thestart varchar(20),
	theend varchar(20),
	discount varchar(10),
	status_ nvarchar(50) check(status_ in(N'Chưa Sử Dụng',N'Đã Sử Dụng',N'Đã Hết Hạn')) default N'Chưa Sử Dụng',
	Ma_Coupon varchar(50)
)

create table GHN_Ship(
	id_ship int identity primary key,
	id_hoadon int,
	payment_type_id int,
	note nvarchar(500), 
	required_note nvarchar(30) check(required_note in('CHOTHUHANG','CHOXEMHANGKHONGTHU','KHONGCHOXEMHANG')) default 'KHONGCHOXEMHANG',
	return_phone varchar(11),
	return_address nvarchar(200),
	return_district_id int,
	return_ward_code int,
	return_name_ward nvarchar(150),
	return_name_district nvarchar(150),
	client_order_code nvarchar(200) default '',
	to_name nvarchar(200),
	to_phone varchar(11),
	to_address nvarchar(200),
	to_ward_code int,
	to_district_id int,
	to_name_ward nvarchar(150),
	to_name_district nvarchar(150),
	to_name_province nvarchar(150),
	cod_amount float,
	weight float,
	length float,
	width float,
	height float,
	pick_station_id int default 0,
	insurance_value float default 1000000,
	service_id int,
	content nvarchar(500) default '',
	feeship float,
	CONSTRAINT FK_GHN_HOADON FOREIGN KEY (id_hoadon) REFERENCES HoaDon(id_hoadon),
)
alter table GHN_Ship
add to_name_province nvarchar(150),
create trigger trg_RamdomCoupon on Coupon after insert
as declare @ramdom varchar(50)
begin
	set @ramdom = ''
	SELECT @ramdom = SUBSTRING(CONVERT(varchar(50), NEWID()),0,9)

	if(@ramdom!='')
		begin 
			update dbo.Coupon set Ma_Coupon= @ramdom
			where id_coupon = (select id_coupon from inserted)
		end
end
go
alter table D
create trigger trg_createDauGia on DauGia after insert
as
begin
	insert into Coupon(id_sanpham) values((select id_sanpham from inserted))
end
go
select *from TaiKhoan
select *from NguoiMua
select *from TaiKhoan
select *from GHN_Ship

delete from LuongTruyCap
delete from HoaDonCT