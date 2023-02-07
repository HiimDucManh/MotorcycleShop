create database MOTORBIKEMANAGEMENT
use MOTORBIKEMANAGEMENT
go

set dateformat dmy
--NGUOIDUNG
CREATE TABLE NGUOIDUNG(
		TAIKHOAN VARCHAR(100) NOT NULL,
		MATKHAU VARCHAR(100) NOT NULL,
		LOAIND INT NOT NULL,
		CHEDOND BIT NOT NULL,
		CONSTRAINT PK_ND PRIMARY KEY(TAIKHOAN)
)
select *from NGUOIDUNG
select*from KHACHHANG

--NHANVIEN
CREATE TABLE NHANVIEN
(
		MANV VARCHAR(40) NOT NULL,
		TENNV NVARCHAR(100),
		HONV NVARCHAR(100),
		TAIKHOANNV VARCHAR(100) NOT NULL,
		NGAYSINH DATE,
		GIOITINH NVARCHAR(10),
		EMAIL VARCHAR(100),
		SDT VARCHAR(20),
		DIACHI NVARCHAR(500),
		NGAYVAOLAM DATE,
		LUONG MONEY,
		IMG VARBINARY(MAX),
		CONSTRAINT PK_NV PRIMARY KEY(MANV)
)


--KHACHHANG
CREATE TABLE KHACHHANG(
		MAKH VARCHAR(40) NOT NULL,
		TENKH NVARCHAR(100),
		HOKH NVARCHAR(100),
		TAIKHOANKH VARCHAR(100),
		NGAYSINH DATE,
		GIOITINH NVARCHAR(10),
		EMAIL VARCHAR(100),
		SDT VARCHAR(20),
		DIACHI NVARCHAR(50),
		NGAYDANGKY DATE,
		DOANHSO MONEY,
		SOLUONGSANPHAM INT,
		IMG VARBINARY(MAX),
		CONSTRAINT PK_KH PRIMARY KEY(MAKH)
)

--MAU
CREATE TABLE MAU(
		MAMAU VARCHAR(40),
		TENMAU NVARCHAR(100),
		IMG VARBINARY(MAX),
		CONSTRAINT PK_MAU PRIMARY KEY(MAMAU)
)


--SANPHAM
CREATE TABLE SANPHAM(
		MASP VARCHAR(40) NOT NULL,
		TENSP NVARCHAR(100) NOT NULL,		
		GIAGOC MONEY,
		GIABAN MONEY,
		CONGSUAT NVARCHAR(200),
		MOMEN NVARCHAR(200),
		DUNGTICH NVARCHAR(200),
		TRONGLUONG NVARCHAR(200),
		GHICHU NVARCHAR(1000),
		MALOAISP VARCHAR(40),
		CONSTRAINT PK_SP PRIMARY KEY(MASP)
)

--CHITIETSANPHAM
CREATE TABLE CHITIETSANPHAM(
		MASP VARCHAR(40) NOT NULL,
		MAMAUSP VARCHAR(40),
		SLTON INT,
		SLBAN INT,
		GHICHU NVARCHAR(1000),
		IMG VARBINARY(MAX),
		CONSTRAINT PK_CTSP PRIMARY KEY(MASP,MAMAUSP)
)


--LOAISANPHAM
CREATE TABLE LOAISP(
		MALOAI VARCHAR(40) NOT NULL,
		TENLOAI NVARCHAR(100),
		IMG VARBINARY(MAX),
		IMGDV1 VARBINARY(MAX),
		IMGDV2 VARBINARY(MAX),
		CONSTRAINT PK_LOAISP PRIMARY KEY(MALOAI)
)

--KHUYENMAI
CREATE TABLE KHUYENMAI(
		MAKM VARCHAR(40) NOT NULL,
		TENKM NVARCHAR(100),
		PHANTRAMKM FLOAT,
		NGAYBATDAU DATETIME,
		NGAYKETTHUC DATETIME,
		CONSTRAINT PK_KM PRIMARY KEY(MAKM)
)

--CHITIETKHUYENMAI
CREATE TABLE CHITIETKHUYENMAI(
		MAKM VARCHAR(40) NOT NULL,
		MALOAISPKM VARCHAR(40) NOT NULL,
		TRANGTHAI BIT,
		CONSTRAINT PK_CTKM PRIMARY KEY(MAKM,MALOAISPKM)
)

--HOADONMUAHANG
CREATE TABLE HOADONMH(
		MAHDMH VARCHAR(40) NOT NULL,
		NGAYHDMH DATETIME,
		MASPMH VARCHAR(40),
		MAMAUSPMH VARCHAR(40),
		IMGSPMH VARBINARY(MAX),
		MAKHMH VARCHAR(40) NOT NULL,
		MANVMH VARCHAR(40),
		LOAITHANHTOAN bit,
		TONGTIEN MONEY,
		MAKM VARCHAR(40),
		SOTIENKM MONEY,
		SOTIENPHAITRA MONEY,
		TINHTRANG NVARCHAR(40),
		CONSTRAINT PK_HOADON PRIMARY KEY(MAHDMH)
)
select*from NGUOIDUNG
--HOADONBAOTRI
CREATE TABLE HOADONBT(
		MAHDBT VARCHAR(40) NOT NULL,
		NGAYHDBT DATETIME,
		MALOAISPBT VARCHAR(40),
		MAKHBT VARCHAR(40) NOT NULL,
		MANVBT VARCHAR(40),
		SOTIENPHAITRA MONEY,
		TINHTRANG NVARCHAR(40),
		GHICHU VARCHAR(1000),
		CONSTRAINT PK_HOADONBT PRIMARY KEY(MAHDBT)
)

--KHOA NGOAI KHACHHANG & NHANVIEN
ALTER TABLE NHANVIEN ADD CONSTRAINT FK_TAIKHOANNV_TAIKHOAN FOREIGN KEY(TAIKHOANNV) REFERENCES NGUOIDUNG(TAIKHOAN);
ALTER TABLE KHACHHANG ADD CONSTRAINT FK_TAIKHOANKH_TAIKHOAN FOREIGN KEY(TAIKHOANKH) REFERENCES NGUOIDUNG(TAIKHOAN);

--KHOA NGOAI SANPHAM
ALTER TABLE SANPHAM ADD CONSTRAINT FK_MALOAISP_MALOAI FOREIGN KEY(MALOAISP) REFERENCES LOAISP(MALOAI);

--KHOA NGOAI CHITIETSANPHAM
ALTER TABLE CHITIETSANPHAM ADD CONSTRAINT FK_MASP FOREIGN KEY(MASP) REFERENCES SANPHAM(MASP);
ALTER TABLE CHITIETSANPHAM ADD CONSTRAINT FK_MAMAUSP FOREIGN KEY(MAMAUSP) REFERENCES MAU(MAMAU);

--KHOA NGOAI CHITIETKHUYENMAI
ALTER TABLE CHITIETKHUYENMAI ADD CONSTRAINT FK_MALOAISPKM_MALOAI FOREIGN KEY(MALOAISPKM) REFERENCES LOAISP(MALOAI);
ALTER TABLE CHITIETKHUYENMAI ADD CONSTRAINT FK_MAKM_MAKM FOREIGN KEY(MAKM) REFERENCES KHUYENMAI(MAKM);

--KHOA NGOAI HOADONMUAHANG
ALTER TABLE HOADONMH ADD CONSTRAINT FK_MASPMH FOREIGN KEY(MASPMH) REFERENCES SANPHAM(MASP);
ALTER TABLE HOADONMH ADD CONSTRAINT FK_MAMAUSPMH FOREIGN KEY(MAMAUSPMH) REFERENCES MAU(MAMAU);
ALTER TABLE HOADONMH ADD CONSTRAINT FK_MAKHMH FOREIGN KEY(MAKHMH) REFERENCES KHACHHANG(MAKH);
ALTER TABLE HOADONMH ADD CONSTRAINT FK_MANVMH FOREIGN KEY(MANVMH) REFERENCES NHANVIEN(MANV);
ALTER TABLE HOADONMH ADD CONSTRAINT FK_MAKM FOREIGN KEY(MAKM) REFERENCES KHUYENMAI(MAKM);

--KHOA NGOAI HOADONBAOTRI
ALTER TABLE HOADONBT ADD CONSTRAINT FK_MALOAISPBT FOREIGN KEY(MALOAISPBT) REFERENCES LOAISP(MALOAI);
ALTER TABLE HOADONBT ADD CONSTRAINT FK_MAKHBT FOREIGN KEY(MAKHBT) REFERENCES KHACHHANG(MAKH);
ALTER TABLE HOADONBT ADD CONSTRAINT FK_MANVBT FOREIGN KEY(MANVBT) REFERENCES NHANVIEN(MANV);



-- Thêm người dùng
insert into NGUOIDUNG (TAIKHOAN,MATKHAU,LOAIND,CHEDOND)  values ('NVHoangNam',N'12345',0,0)
insert into NGUOIDUNG (TAIKHOAN,MATKHAU,LOAIND,CHEDOND)  values ('NVQuangManh',N'12345',0,0)
insert into NGUOIDUNG (TAIKHOAN,MATKHAU,LOAIND,CHEDOND)  values ('NVDucManh',N'12345',0,0)
insert into NGUOIDUNG (TAIKHOAN,MATKHAU,LOAIND,CHEDOND)  values ('NVAnhKhoa',N'12345',0,0)
insert into NGUOIDUNG (TAIKHOAN,MATKHAU,LOAIND,CHEDOND)  values ('phanchibao',N'12345',1,0)
insert into NGUOIDUNG (TAIKHOAN,MATKHAU,LOAIND,CHEDOND)  values ('vuducmanh',N'12345',1,0)
insert into NGUOIDUNG (TAIKHOAN,MATKHAU,LOAIND,CHEDOND)  values ('hoangnam',N'12345',1,0)
insert into NGUOIDUNG (TAIKHOAN,MATKHAU,LOAIND,CHEDOND)  values ('quangmanh',N'12345',1,0)
insert into NGUOIDUNG (TAIKHOAN,MATKHAU,LOAIND,CHEDOND)  values ('vietquoc',N'12345',1,0)
insert into NGUOIDUNG (TAIKHOAN,MATKHAU,LOAIND,CHEDOND)  values ('luonghieu',N'12345',1,0)
insert into NGUOIDUNG (TAIKHOAN,MATKHAU,LOAIND,CHEDOND)  values ('admin',N'12345',2,0)
select *from NGUOIDUNG

--Thêm nhân viên
insert into NHANVIEN(MANV,HONV,TENNV,TAIKHOANNV,NGAYSINH,GIOITINH,EMAIL,SDT,DIACHI,NGAYVAOLAM,LUONG,IMG)  values ('NV01',N'Nguyễn Hoàng',N'Nam','NVHoangNam','12/2/2002','Nam','20520628@gm.uit.edu.vn','234342335',N'Tp Hồ Chí Minh','8/6/2021',5700000,(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Employee\male.png', SINGLE_BLOB) as T1))
insert into NHANVIEN(MANV,HONV,TENNV,TAIKHOANNV,NGAYSINH,GIOITINH,EMAIL,SDT,DIACHI,NGAYVAOLAM,LUONG,IMG)  values ('NV02',N'Đinh Quang',N'Mạnh','NVQuangManh','5/4/2002','Nam','20520628@gm.uit.edu.vn','195323445',N'Dak Lak','3/2/2020',3500000,(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Employee\male.png', SINGLE_BLOB) as T1))
insert into NHANVIEN(MANV,HONV,TENNV,TAIKHOANNV,NGAYSINH,GIOITINH,EMAIL,SDT,DIACHI,NGAYVAOLAM,LUONG,IMG)  values ('NV03',N'Vũ Đức',N'Mạnh','NVDucManh','6/2/1996','Nam','20520630@gm.uit.edu.vn','582132132',N'Dak Nông','5/4/2021',6000000,(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Employee\male.png', SINGLE_BLOB) as T1))
insert into NHANVIEN(MANV,HONV,TENNV,TAIKHOANNV,NGAYSINH,GIOITINH,EMAIL,SDT,DIACHI,NGAYVAOLAM,LUONG,IMG)  values ('NV04',N'Nguyễn Anh',N'Khoa','NVAnhKhoa','9/2/1999','Nam','20520628@gm.uit.edu.vn','338842343',N'Quảng Nam','9/4/2020',4900000,(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Employee\male.png', SINGLE_BLOB) as T1))
select*from NHANVIEN

--Thêm khách hàng
insert into KHACHHANG (MAKH,HOKH,TENKH,TAIKHOANKH,NGAYSINH,GIOITINH,EMAIL,SDT,DIACHI,NGAYDANGKY,DOANHSO,SOLUONGSANPHAM,IMG ) values ('KH02',N'Phan Chí',N'Bảo','phanchibao','7/4/2002',N'Nam','20520628@gm.uit.edu.vn','4549823741',N'Bến Tre','10/8/2021',0,0,(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Customer\Customer.png', SINGLE_BLOB) as T1))
insert into KHACHHANG (MAKH,HOKH,TENKH,TAIKHOANKH,NGAYSINH,GIOITINH,EMAIL,SDT,DIACHI,NGAYDANGKY,DOANHSO,SOLUONGSANPHAM,IMG ) values ('KH03',N'Vũ Đức',N'Mạnh','vuducmanh','6/4/2002',N'Nam','20520628@gm.uit.edu.vn','7839432423',N'Dak Nông','10/8/2021',0,0,(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Customer\Customer.png', SINGLE_BLOB) as T1))
insert into KHACHHANG (MAKH,HOKH,TENKH,TAIKHOANKH,NGAYSINH,GIOITINH,EMAIL,SDT,DIACHI,NGAYDANGKY,DOANHSO,SOLUONGSANPHAM,IMG ) values ('KH04',N'Hoàng',N'Nam','hoangnam','7/4/2002',N'Nam','20520628@gm.uit.edu.vn','2343231235',N'TP Hồ Chí Minh','11/8/2021',0,0,(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Customer\Customer.png', SINGLE_BLOB) as T1))
insert into KHACHHANG (MAKH,HOKH,TENKH,TAIKHOANKH,NGAYSINH,GIOITINH,EMAIL,SDT,DIACHI,NGAYDANGKY,DOANHSO,SOLUONGSANPHAM,IMG ) values ('KH05',N'Quang',N'Mạnh','quangmanh','8/4/2002',N'Nam','20520628@gm.uit.edu.vn','3242331235',N'Dak Lak','12/8/2021',0,0,(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Customer\Customer.png', SINGLE_BLOB) as T1))
insert into KHACHHANG (MAKH,HOKH,TENKH,TAIKHOANKH,NGAYSINH,GIOITINH,EMAIL,SDT,DIACHI,NGAYDANGKY,DOANHSO,SOLUONGSANPHAM,IMG ) values ('KH06',N'Việt',N'Quốc','vietquoc','9/4/2002',N'Nam','20520628@gm.uit.edu.vn','4732231235',N'TP Hồ Chí Minh','13/8/2021',0,0,(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Customer\Customer.png', SINGLE_BLOB) as T1))
insert into KHACHHANG (MAKH,HOKH,TENKH,TAIKHOANKH,NGAYSINH,GIOITINH,EMAIL,SDT,DIACHI,NGAYDANGKY,DOANHSO,SOLUONGSANPHAM,IMG ) values ('KH07',N'Lương',N'Hiếu','luonghieu','10/4/2002',N'Nam','20520628@gm.uit.edu.vn','2343230097',N'TP Hồ Chí Minh','14/8/2021',0,0,(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Customer\Customer.png', SINGLE_BLOB) as T1))
select*from KHACHHANG

--Thêm màu
insert into MAU (MAMAU,TENMAU,IMG)  values ('GREY1',N'Aviator Grey',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Aviator_Grey.jpg', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('GREY2',N'Aviator Grey Forged Wheels',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Aviator_Grey_Forged_Wheels.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('GREY3',N'Avigator Grey Spoked Wheels',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Avigator_Grey_Spoked_Wheels.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('GREY4',N'Street Grey',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Street_Grey.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('GREY5',N'Titanium Grey',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Titanium_Grey.jpg', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('BLACK1',N'Black and Steel',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Black_and_Steel.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('BLACK2',N'Black Cemento',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Black_Cemento.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('BLACK3',N'Black India',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Black_India.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('BLACK4',N'Black Selva',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Black_Selva.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('BLACK5',N'Black Siam',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Black_Siam.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('BLACK6',N'Black Steel',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Black_Steel.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('BLACK7',N'Dark Stealth',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Dark_Stealth.jpg', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('BLACK8',N'Thrilling Black',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Thrilling_Black.jpg', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('BLACK9',N'Total Black',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Total_Black.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('BLACK10',N'Winter Test Livery',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Winter_Test_Livery.jpg', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('RED1',N'Ducati Red',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Ducati_Red.jpg', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('RED2',N'Ducati Red Forged Wheels',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Ducati_Red_Forged_Wheels.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('RED3',N'Ducati Red Spoked Wheels',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Ducati_Red_Spoked_Wheels.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('RED4',N'Graffiti',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Graffiti.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('RED5',N'RR22',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\RR22.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('RED6',N'SP Livery',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\SP_Livery.png', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('WHITE1',N'Star White Silk',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Star_White_Silk.jpg', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('WHITE2',N'White Riosso Livery',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\White_Riosso_Livery.jpg', SINGLE_BLOB) as T1))
insert into MAU (MAMAU,TENMAU,IMG)  values ('GREEN1',N'Storm Green',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Colours\Storm_Green.png', SINGLE_BLOB) as T1))

-- Thêm LOAISP
insert into LOAISP (MALOAI,TENLOAI,IMG,IMGDV1)  values ('DIA',N'Diavel',
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\DIAVEL\Diavel_1260_S_Total_Black.png', SINGLE_BLOB) as T1),
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\DIAVEL\Service-Diavel.jpg', SINGLE_BLOB) as T1))
insert into LOAISP (MALOAI,TENLOAI,IMG,IMGDV1)  values ('HYP',N'HyperMotard',
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\HYPERMOTARD\Hypermotard_950_RVE.png', SINGLE_BLOB) as T1),
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\HYPERMOTARD\Service-Hypermotard.jpg', SINGLE_BLOB) as T1))
insert into LOAISP (MALOAI,TENLOAI,IMG,IMGDV1)  values ('MON',N'Monster',
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MONSTER\Monster_937_Plus_Red.png', SINGLE_BLOB) as T1),
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MONSTER\Service-Monster.jpg', SINGLE_BLOB) as T1))
insert into LOAISP (MALOAI,TENLOAI,IMG,IMGDV1)  values ('MUL',N'Multistrada',
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MULTISTRADA\Multistrada_V2.png', SINGLE_BLOB) as T1),
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MULTISTRADA\Service-Multistrada.jpg', SINGLE_BLOB) as T1))
insert into LOAISP (MALOAI,TENLOAI,IMG,IMGDV1,IMGDV2)  values ('PAN',N'Panigale',
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\PANIGALE\Panigale_V2_White.png', SINGLE_BLOB) as T1),
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\PANIGALE\Service-Panigale1.jpg', SINGLE_BLOB) as T1),
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\PANIGALE\Service-Panigale2.jpg', SINGLE_BLOB) as T1))
insert into LOAISP (MALOAI,TENLOAI,IMG,IMGDV1)  values ('STR',N'StreetFighter',
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\STREETFIGHTER\Streetfighter_V4_S_Dark.png', SINGLE_BLOB) as T1),
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\STREETFIGHTER\Service-StreetfighterV4.jpg', SINGLE_BLOB) as T1))
insert into LOAISP (MALOAI,TENLOAI,IMG,IMGDV1)  values ('SUP',N'SuperSport',
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\SUPERSPORT\Supersport_950.png', SINGLE_BLOB) as T1),
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\SUPERSPORT\Service-SuperSport.jpg', SINGLE_BLOB) as T1))
insert into LOAISP (MALOAI,TENLOAI,IMG,IMGDV1)  values ('XDI',N'XDiavel',
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\XDIAVEL\XDiavel_Dark.png', SINGLE_BLOB) as T1),
(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\XDIAVEL\Service-Diavel.jpg', SINGLE_BLOB) as T1))
select *from LOAISP

--THEM SAN PHAM
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP01','Diavel 1260',800000000,900000000,
N'119 kW',N'129 Nm',N'1,262 cc',N'223 kg',
N'ĐÁNH GỤC MỌI TRÁI TIM,
THU HÚT MỌI ÁNH NHÌN','DIA')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP02','Diavel 1260 S',800000000,900000000,
N'119 kW',N'129 Nm',N'1,262 cc',N'221 kg',
N'ĐÁNH GỤC MỌI TRÁI TIM,
THU HÚT MỌI ÁNH NHÌN','DIA')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP03','XDiavel Nera',600000000,677150000,
N'118 kW',N'127 Nm',N'1,262 cc',N'221 kg',
N'PHIÊN BẢN GIỚI HẠN
SANG TRỌNG ĐẾN CHOÁNG NGỢP','XDI')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP04','XDiavel S',800000000,926900000,
N'118 kW',N'127 Nm',N'1,262 cc',N'223 kg',
N'TẬP HỢP QUYỀN NĂNG
CỦA NHỮNG MẶT ĐỐI LẬP','XDI')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP05','XDiavel Dark',480000000,558000000,
N'118 kW',N'127 Nm',N'1,262 cc',N'221 kg',
N'TẬP HỢP QUYỀN NĂNG
CỦA NHỮNG MẶT ĐỐI LẬP','XDI')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP06','Hypermotard 950 SP',400000000,460000000,
N'84 kW',N'96 Nm',N'937 cc',N'176 kg',
N'GIỜ CHƠI ĐÃ ĐIỂM
ĐÃ ĐẾN LÚC CHO MÀN PHÔ DIỄN','HYP')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP07','Hypermotard 950 RVE',410000000,474000000,
N'84 kW',N'96 Nm',N'937 cc',N'178 kg',
N'GIỜ CHƠI ĐÃ ĐIỂM
ĐÃ ĐẾN LÚC CHO MÀN PHÔ DIỄN','HYP')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP08','Hypermotard 950',400000000,460000000,
N'84 kW',N'96 Nm',N'937 cc',N'178 kg',
N'GIỜ CHƠI ĐÃ ĐIỂM
ĐÃ ĐẾN LÚC CHO MÀN PHÔ DIỄN','HYP')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP09','Monster 937',340000000,439000000,
N'82 kW',N'93 Nm',N'937 cc',N'166 kg',
N'GỌN NHẸ, RẮN CHẮC,
LINH HOẠT, CUỐN HÚT','MON')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP10','Monster 937 PLUS',350000000,441000000,
N'82 kW',N'93 Nm',N'937 cc',N'166 kg',
N'GỌN NHẸ, RẮN CHẮC,
LINH HOẠT, CUỐN HÚT','MON')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP11','Streetfighter V4',400000000,512000000,
N'153 kW',N'123 Nm',N'1,103 cc',N'180 kg',
N'PHONG ĐỘ, CHUẨN XÁC,
THẦN THÁI','STR')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP12','Streetfighter V4 S',450000000,589000000,
N'153 kW',N'123 Nm',N'1,103 cc',N'180 kg',
N'PHONG ĐỘ, CHUẨN XÁC,
THẦN THÁI','STR')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP13','Streetfighter V2',400000000,505000000,
N'112,3 kW',N'101,4 Nm',N'955 cc',N'178 kg',
N'TÍNH CÁCH ĐỘC ĐÁO,
ĐẶC BIỆT','STR')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP14','Multistrada V4',650000000,770000000,
N'125 kW',N'125 Nm',N'1,158 cc',N'215 kg',
N'THỐNG TRỊ MỌI
KHUNG ĐƯỜNG','MUL')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP15','Multistrada V4 S',750000000,899000000,
N'125 kW',N'125 Nm',N'1,158 cc',N'218 kg',
N'THỐNG TRỊ MỌI
KHUNG ĐƯỜNG','MUL')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP16','Multistrada V2',450000000,550000000,
N'83 kW',N'96 Nm',N'937 cc',N'199 kg',
N'MỖI NGÀY ĐỀU LÀ
TRẢI NGHIỆM TUYỆT VỜI','MUL')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP17','Multistrada V2 S',550000000,617000000,
N'83 kW',N'96 Nm',N'937 cc',N'202 kg',
N'MỖI NGÀY ĐỀU LÀ
TRẢI NGHIỆM TUYỆT VỜI','MUL')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP18','Panigale V2',550000000,653000000,
N'114 kW',N'104 Nm',N'955 cc',N'176 kg',
N'KHÍ CHẤT TÁO BẠO
DIỆN MẠO TINH KHÔI','PAN')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP19','Panigale V4',700000000,841000000,
N'125 kW',N'124 Nm',N'1,103 cc',N'175 kg',
N'ĐỈNH CAO TỐC ĐỘ','PAN')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP20','Panigale V4 S',800000000,996000000,
N'114 kW',N'124 Nm',N'1,103 cc',N'174 kg',
N'ĐỈNH CAO TỐC ĐỘ','PAN')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP21','Panigale V4 SP',1100000000,1364000000,
N'114 kW',N'124 Nm',N'1,103 cc',N'173 kg',
N'NHANH NHƯ CHỚP','PAN')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP22','Panigale V2 Bayliss',650000000,780000000,
N'114 kW',N'104 Nm',N'955 cc',N'174,5 kg',
N'VƯỢT THỜI GIAN','PAN')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP23','Supersport 950',500000000,579000000,
N'81 kW',N'93 Nm',N'937 cc',N'183 kg',
N'KHÍ CHẤT TỎA SÁNG','SUP')
insert into SANPHAM (MASP,TENSP,GIAGOC,GIABAN,CONGSUAT,MOMEN,DUNGTICH,TRONGLUONG,GHICHU,MALOAISP) values ('SP24','Supersport 950 S',500000000,600000000,
N'81 kW',N'93 Nm',N'937 cc',N'183 kg',
N'KHÍ CHẤT TỎA SÁNG','SUP')
select *from SANPHAM


--THEM CHITIETSANPHAM
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP01','BLACK7',10,0,
N'Diavel 1260 Dark Stealth với khung màu
đen và bánh xe màu đen',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\DIAVEL\Diavel_1260_Dark.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP02','BLACK9',10,0,
N'Diavel 1260 S Total Black với khung màu
đỏ và bánh xe màu đen.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\DIAVEL\Diavel_1260_S_Total_Black.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP02','BLACK1',10,0,
N'Ở phiên bản Black & Stealth: sử dụng đặc
trưng đồ họa bất đối xứng, kết hợp với
màu đen mờ chủ đạo đi cùng màu sơn xám
bóng, với các điểm nhấn thể thao màu
vàng (Race Yellow)',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\DIAVEL\Diavel_1260_S_Black_Steel.png', SINGLE_BLOB) as T1))


insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP03','BLACK5',10,0,
N'Black on Black livery with 5 saddle
colors available',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\XDIAVEL\XDiavel_Nera_Siam.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP03','BLACK6',10,0,
N'Black on Black livery with 5 saddle
colors available',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\XDIAVEL\XDiavel_Nera_Steel.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP03','BLACK2',10,0,
N'Black on Black livery with 5 saddle
colors available',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\XDIAVEL\XDiavel_Nera_Cemento.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP03','BLACK3',10,0,
N'Black on Black livery with 5 saddle
colors available',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\XDIAVEL\XDiavel_Nera_India.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP03','BLACK4',10,0,
N'Black on Black livery with 5 saddle
colors available',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\XDIAVEL\XDiavel_Nera_Selva.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP04','BLACK8',10,0,
N'Sắc đen ấn tượng điểm tô với những chi
tiết đen đỏ xen kẽ, khung xe với những
đường viền xám phóng khoáng và bánh xe
14 chấu đen loáng bóng.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\XDIAVEL\XDiavel_S.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP05','BLACK7',10,0,
N'Dark Stealth với khung đen Carbon
và bánh xe đen mờ',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\XDIAVEL\XDiavel_Dark.png', SINGLE_BLOB) as T1))


insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP06','RED6',10,0,
N'Phiên bản SP với nước sơn gợi cảm hứng
từ giải đua xe thế giới MotoGP, màu sắc
và họa tiết của tem mang tinh thần “thể
thao tự do”, bật lên cá tính ‘fun-bike’
độc đáo của chiếc xe.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\HYPERMOTARD\Hypermotard_950_SP.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP07','RED4',10,0,
N'Phiên bản RVE thiết kế theo phong cách
Hip Hop đầy hấp dẫn với dòng chữ HYPER
cách điệu trên khu vực bình xăng. Yên xe
được bọc 2 lớp vải màu cùng dòng chữ nổi,
cặp vành đúc hợp kim sơn 2 màu cân đối đỏ
và đen đầy hấp dẫn.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\HYPERMOTARD\Hypermotard_950_RVE.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP08','RED1',10,0,
N'Ducati đỏ với bánh đen',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\HYPERMOTARD\Hypermotard_950.png', SINGLE_BLOB) as T1))


insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP09','RED1',10,0,
N'Ducati đỏ với bánh đen',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MONSTER\Monster_937_Red.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP09','GREY1',10,0,
N'Xám Aviator với bánh GP đỏ',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MONSTER\Monster_937_Grey.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP09','BLACK7',10,0,
N'Dark Stealth với bánh đen',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MONSTER\Monster_937_Dark.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP10','RED1',10,0,
N'Dark Stealth với bánh đen',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MONSTER\Monster_937_Plus_Red.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP10','GREY1',10,0,
N'Xám Aviator với bánh GP đỏ',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MONSTER\Monster_937_Plus_Grey.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP10','BLACK7',10,0,
N'Dark Stealth với bánh đen',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MONSTER\Monster_937_Plus_Dark.png', SINGLE_BLOB) as T1))


insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP11','RED1',10,0,
N'Ducati Red với khung màu xám đậm và
bánh xe màu đen.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\STREETFIGHTER\Streetfighter_V4.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP12','RED1',10,0,
N'Ducati Red với khung màu xám đậm và
bánh xe màu đen.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\STREETFIGHTER\Streetfighter_V4_S_Red.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP12','BLACK7',10,0,
N'Dark Stealth với khung màu xám đậm
và bánh xe màu đen.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\STREETFIGHTER\Streetfighter_V4_S_Dark.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP13','GREEN1',10,0,
N'Xanh lá cây kim loại và tem đỏ hoặc phiên
bản màu đỏ Ducati thể thao.Hãy chọn một
trong những phù hợp với tính cách của
bạn nhất.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\STREETFIGHTER\Streetfighter_V2_Green.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP13','RED1',10,0,
N'Xanh lá cây kim loại và tem đỏ hoặc phiên
bản màu đỏ Ducati thể thao.Hãy chọn một
trong những phù hợp với tính cách của
bạn nhất.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\STREETFIGHTER\Streetfighter_V2_Red.png', SINGLE_BLOB) as T1))


insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP14','RED1',10,0,
N'Ducati Red với khung màu xám đậm và
bánh xe màu đen.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MULTISTRADA\Multistrada_V4.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP15','RED2',10,0,
N'Ducati Red with glossy black rims
with red tag.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MULTISTRADA\Multistrada_V4_S_Red_Forged.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP15','RED3',10,0,
N'Ducati Red with glossy black rims
with red tag.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MULTISTRADA\Multistrada_V4_S_Red_Spoked.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP15','GREY2',10,0,
N'Aviator Grey with gloss black rims
with red tag.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MULTISTRADA\Multistrada_V4_S_Grey_Forged.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP15','GREY3',10,0,
N'Aviator Grey with gloss black rims
with red tag.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MULTISTRADA\Multistrada_V4_S_Grey_Spoked.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP16','RED1',10,0,
N'Phiên bản V2 Màu đỏ Ducati cùng vành
đen lôi cuốn',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MULTISTRADA\Multistrada_V2.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP17','RED1',10,0,
N'Phiên bản V2 S có thêm màu Street Grey
với khung màu đen và vành màu Red GP',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MULTISTRADA\Multistrada_V2_S_Red.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP17','GREY4',10,0,
N'Phiên bản V2 S có thêm màu Street Grey
với khung màu đen và vành màu Red GP',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\MULTISTRADA\Multistrada_V2_S_Grey.png', SINGLE_BLOB) as T1))


insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP18','WHITE2',10,0,
N'Bảng màu của Panigale V2 có màu đỏ
Ducati Red cổ điển đậm chất thể thao
và màu trắng White Rosso mới - sự kết
hợp hài hòa giữa sắc trắng thuần khiết
Star White Silk với những điểm nhấn
sắc đỏ Ducati Red ấn tượng trên mâm xe,
cửa hút gió phía trước và khe hướng gió
bên hông xe.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\PANIGALE\Panigale_V2_White.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP18','RED1',10,0,
N'Bảng màu của Panigale V2 có màu đỏ
Ducati Red cổ điển đậm chất thể thao
và màu trắng White Rosso mới - sự kết
hợp hài hòa giữa sắc trắng thuần khiết
Star White Silk với những điểm nhấn
sắc đỏ Ducati Red ấn tượng trên mâm xe,
cửa hút gió phía trước và khe hướng gió
bên hông xe.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\PANIGALE\Panigale_V2_Red.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP19','RED1',10,0,
N'Màu đỏ Ducati Red với khung xe màu xám
đậm và bánh xe màu đen',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\PANIGALE\Panigale_V4.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP20','RED1',10,0,
N'Màu đỏ Ducati Red với khung xe màu xám
đậm và bánh xe màu đen',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\PANIGALE\Panigale_V4_S.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP21','BLACK10',10,0,
N'Phiên bản Winter Test có màu đen mờ với
các chi tiết màu đỏ rực và bình nhiên liệu
lộ bằng nhôm xước',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\PANIGALE\Panigale_V4_SP.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP22','RED1',10,0,
N'Panigale V2 Bayliss 1st Championship 20th
Anniversary mới, mẫu xe kỷ niệm dành riêng
cho một trong những tay đua thành công nhất
mọi thời đại và là tượng đài bất hữu của
giới đua xe - Troy Bayliss.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\PANIGALE\Panigale_V2_Bayliss.png', SINGLE_BLOB) as T1))


insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP23','GREY5',10,0,
N'Kính chắn gió màu xám Titanium Grey,
khung và bánh xe màu đỏ Ducati Red,
bộ giảm xóc Marzocchi 43 mm có thể điều
chỉnh hoàn toàn và bộ giảm xóc Sachs
có thể điều chỉnh.',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\SUPERSPORT\Supersport_950.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP24','WHITE1',10,0,
N'Kết cấu thân vỏ màu trắng, điểm nhấn
là khung sườn và bộ mâm xe màu đỏ, mang
lại điểm khác biệt hoàn hảo so với 
Ducati Red truyền thống',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\SUPERSPORT\Supersport_950.png', SINGLE_BLOB) as T1))
insert into CHITIETSANPHAM (MASP,MAMAUSP,SLTON,SLBAN,GHICHU,IMG) values ('SP24','RED1',10,0,
N'Màu đỏ Ducati Red với khung xe màu xám
đậm và bánh xe màu đen',(SELECT * FROM OPENROWSET(BULK N'E:\Workspace\Image\MotorbikeManagement\Moto\Ducati\SUPERSPORT\Supersport_950_S_Red.png', SINGLE_BLOB) as T1))
select*from KHUYENMAI

--THEM KHUYENMAI
insert into KHUYENMAI(MAKM,TENKM,PHANTRAMKM,NGAYBATDAU,NGAYKETTHUC) values ('KM01','Lunar New Year','30','2023/01/01 00:00:00','2023/02/15 23:59:59')

--THEM CHITIETKHUYENMAI
insert into CHITIETKHUYENMAI(MAKM,MALOAISPKM,TRANGTHAI) values ('KM01','DIA','1')
insert into CHITIETKHUYENMAI(MAKM,MALOAISPKM,TRANGTHAI) values ('KM01','XDI','1')
insert into CHITIETKHUYENMAI(MAKM,MALOAISPKM,TRANGTHAI) values ('KM01','HYP','1')
insert into CHITIETKHUYENMAI(MAKM,MALOAISPKM,TRANGTHAI) values ('KM01','MON','1')
insert into CHITIETKHUYENMAI(MAKM,MALOAISPKM,TRANGTHAI) values ('KM01','STR','0')
insert into CHITIETKHUYENMAI(MAKM,MALOAISPKM,TRANGTHAI) values ('KM01','MUL','0')
insert into CHITIETKHUYENMAI(MAKM,MALOAISPKM,TRANGTHAI) values ('KM01','PAN','0')
insert into CHITIETKHUYENMAI(MAKM,MALOAISPKM,TRANGTHAI) values ('KM01','SUP','0')

select *from NGUOIDUNG
select *from SANPHAM
select *from HOADONMH
select *from KHACHHANG

delete CHITIETSANPHAM
delete SANPHAM
delete LOAISP
delete KHACHHANG
delete CHITIETKHUYENMAI
delete KHUYENMAI
drop table KHACHHANG