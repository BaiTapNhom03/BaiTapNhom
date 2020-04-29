-- TẠO CSDL
Create Database Qly_CuaHangInternet
On 
(	Name = QL_CH_data,
	Filename = 'D:\GitExample\QuanLyCuaHangInternet.mdf',
	size = 10,
	Maxsize = 20,
	filegrowth = 4
)
Log on
(	Name = ql_ch_log,
	Filename = 'D:\GitExample\QuanLyCuaHangInternet.ldf',
	size = 10mb,
	Filegrowth = 4mb
)
-----------------------------
--TẠO BẢNG 
--Phong (MaPhong, TenPhong, SoMay)
Create table Phong
(	MaPhong nvarchar(30) primary key not null,
	TenPhong nvarchar(20) not null,
	SoMay int not null
) 
--Ổ cứng (MaOCung, TenOCung)
Create table O_Cung
(	MaOCung nvarchar(30) primary key not null,
	TenOCung nvarchar(30) not null
)
--Dung lượng (MaDLuong, TenDLuong)
Create table Dung_Luong
(	MaDLuong nvarchar(30) primary key not null,
	TenDLuong nvarchar(30) not null
)
--Chip (MaChip, TenChip)
Create table Chip
(	MaChip nvarchar(30) primary key not null,
	TenChip nvarchar(30) not null
)
--Ram (MaRam, TenRam)
Create table Ram
(	MaRam nvarchar(30) primary key not null,
	TenRam nvarchar(30) not null
)
--Tốc Độ (MaTocDo, TenTocDo)
Create table Toc_Do
(	MaTocDo nvarchar(30) primary key not null,
	TenTocDo nvarchar(30) not null
)
--Màn Hình (MaManHinh, TenManHinh)
Create table Man_Hinh
(	MaManHinh nvarchar(30) primary key not null,
	TenManHinh nvarchar(30) not null
)
--SizeManHing (MaManHinh, TenManHinh)
Create table SizeMH
(	MaSizeMH nvarchar(30) primary key not null,
	TenSizeMH nvarchar(30) not null
)
--Máy tinh (MaMay, TenMay)
Create table MayTinh
(	MaMay nvarchar(30) primary key not null,
	TenMay nvarchar(30) not null,
	MaPhong nvarchar(30) not null,
	MaOCung nvarchar(30) not null,
	MaDLuong nvarchar(30) not null,
	MaChip nvarchar(30) not null,
	MaRam nvarchar(30) not null,
	MaTocDo nvarchar(30) not null,
	MaManHinh nvarchar(30) not null,
	MaSizeMH nvarchar(30) not null,
	MaChuot nvarchar(30) not null,
	MaBanPhim nvarchar(30) not null,
	MaODia nvarchar(30) not null,
	MaLoa nvarchar(30) not null,
	TinhTrang nvarchar(30) not null,
	Ghichu nvarchar(100)
)
--Chuột (MaChuot, TenChuot)
Create table Chuot
(	MaChuot nvarchar(30) primary key not null,
	TenChuot nvarchar(30) not null
)
--BanPhim (MaBanPhim, TenBanPhim)
Create table BanPhim
(	MaBanPhim nvarchar(30) primary key not null,
	TenBanPhim nvarchar(30) not null
)
--O_Dia (MaODia, TenODia)
Create table O_Dia
(	MaODia nvarchar(30) primary key not null,
	TenODia nvarchar(30) not null
)
--Loa (MaLoa, TenLoa)
Create table Loa
(	MaLoa nvarchar(30) primary key not null,
	TenLoa nvarchar(30) not null
)
--NhanVien(MaNV, TenNV)
Create table NhanVien
(	MaNV nvarchar(30) primary key not null,
	TenNV nvarchar(30) not null,
	MaCa nvarchar(30) not null,
	NamSinh datetime not null,
	GioiTinh char(10) not null,
	DiaChi nvarchar(100),
	SDT nvarchar(30)
)

--CaLam (MaCa, TenCa)
Create table CaLam
(	MaCa nvarchar(30) primary key not null,
	TenCa nvarchar(30) not null,
	ThoiGian nvarchar(30) not null
)
--ThueMay (MaSTT, TenKhach)
Create table ThueMay
(	MaSTT nvarchar(30) primary key not null,
	MaPhong nvarchar(30) not null,
	MaMay nvarchar(30) not null,
	TenKhach nvarchar(30) not null,
	NgayThue date not null,
	GioVao time not null,
	GioRa time not null,
	MaNV nvarchar(30) not null,
	TongTien float not null,
	GhiChu nvarchar(50)
)
--DonGia (MaDonGia, TenDonGia)
Create table DonGia
(	
	MaDonGia nvarchar(30) not null,
	TenDonGio nvarchar(30) not null,
	DonGiaThueGio money not null
)
--GiaiPhap (MaGiaiPhap, TenGiaiPhap)
Create table GiaiPhap
(	MaGiaiPhap nvarchar(30) primary key not null,
	TenGiaiPhap nvarchar(30) not null,
	ChiPhi money not null
)
--BaoTri (MaBaoTri, TenBaoTri)
Create table BaoTri
(	MaBaoTri nvarchar(30) primary key not null,
	NgayBaoTri datetime not null,
	MaNBT nvarchar(30) not null,
	TongChiPhi money not null
)

--ChiTietBaoTri (MaGiaiPhap, TenGiaiPhap)
Create table ChiTietBaoTri
(	
	MaBaoTri nvarchar(30) not null,
	MaMay nvarchar(30) not null,
	MaNguyenNhan nvarchar(30) not null,
	MaTinhTrang nvarchar(30) not null,
	MaGiaiPhap nvarchar(30) not null,
	ThanhTien float not null,
	Constraint PK_CTBT primary key (MaBaoTri, MaMay)
)
--TinhTrang (MaGiaiPhap, TenGiaiPhap)
Create table TinhTrang
(	MaTinhTrang nvarchar(30) primary key not null,
	TenTinhTrang nvarchar(30) not null
)
--NguyenNhan (MaGiaiPhap, TenGiaiPhap)
Create table NguyenNhan
(	MaNguyenNhan nvarchar(30) primary key not null,
	TenNguyenNhan nvarchar(30) not null
)
--NhaBaoTri (MaGiaiPhap, TenGiaiPhap)
Create table NhaBaoTri
(	MaNBT nvarchar(30) primary key not null,
	TenNBT nvarchar(30) not null,
	DiaChi nvarchar(100) not null,
	SDT nvarchar(30) not null
)
---------------------
-- TẠO CÁC RÀNG BUỘC
--Khóa ngoại của máy tính
Alter table MayTinh
Add constraint FK_MT_Phong Foreign key (MaPhong) references Phong
Alter table MayTinh
Add constraint FK_MT_OC Foreign key (MaOCung) references O_Cung
Alter table MayTinh
Add constraint FK_MT_DL Foreign key (MaDLuong) references Dung_Luong
Alter table MayTinh
Add constraint FK_MT_Chip Foreign key (MaChip) references Chip
Alter table MayTinh
Add constraint FK_MT_Ram Foreign key (MaRam) references Ram
Alter table MayTinh
Add constraint FK_MT_TD Foreign key (MaTocDo) references Toc_Do
Alter table MayTinh
Add constraint FK_MT_MH Foreign key (MaManHinh) references Man_Hinh
Alter table MayTinh
Add constraint FK_MT_SMH Foreign key (MaSizeMH) references SizeMH
Alter table MayTinh
Add constraint FK_MT_Chuot Foreign key (MaChuot) references Chuot
Alter table MayTinh
Add constraint FK_MT_BP Foreign key (MaBanPhim) references BanPhim
Alter table MayTinh
Add constraint FK_MT_OD Foreign key (MaODia) references O_Dia
Alter table MayTinh
Add constraint FK_MT_Loa Foreign key (MaLoa) references Loa
--Khóa ngoại của NhanVien
Alter table NhanVien
Add constraint FK_NV_Ca Foreign key (MaCa) references CaLam
--khóa ngoại của ThueMay
Alter table ThueMay
Add constraint FK_TM_Phong Foreign key (MaPhong) references Phong
Alter table ThueMay
Add constraint FK_TM_May Foreign key (MaMay) references MayTinh
Alter table ThueMay
Add constraint FK_TM_NV Foreign key (MaNV) references NhanVien
--Khóa ngoại của BaoTri
Alter table BaoTri
Add constraint FK_BT_NBT Foreign key (MaNBT) references NhaBaoTri
--Khóa ngoại của CTBT
Alter table ChiTietBaoTri
Add constraint FK_CT_TT Foreign key (MaTinhTrang) references TinhTrang
Alter table ChiTietBaoTri
Add constraint FK_CT_NN Foreign key (MaNguyenNhan) references NguyenNhan
Alter table ChiTietBaoTri
Add constraint FK_CT_GP Foreign key (MaGiaiPhap) references GiaiPhap
Alter table ChiTietBaoTri
Add constraint FK_CT_MT Foreign key (MaMay) references MayTinh
Alter table ChiTietBaoTri
Add constraint FK_CT_BT Foreign key (MaBaoTri) references BaoTri
--Kiểm tra
Alter table MayTinh
Add constraint CC_MT Check(TinhTrang = 'Trong' or TinhTrang = 'Đa Thue')
Alter table NhanVien
Add constraint CC_NV Check(GioiTinh = 'Nam' or GioiTinh = 'Nu')
------------------------------------------
--Insert dữ liệu
Insert into Phong (MaPhong, TenPhong, SoMay) Values ('P01', 'Phòng 1', '15')
Insert into Phong (MaPhong, TenPhong, SoMay) Values ('P02', 'Phòng 2', '20')
Insert into Phong (MaPhong, TenPhong, SoMay) Values ('P03', 'Phòng 3', '25')

Insert into O_Cung(MaOCung,TenOCung) Values ('OC1', 'HDD')
Insert into O_Cung(MaOCung,TenOCung) Values ('OC2', 'SSD')

Insert into Dung_Luong(MaDLuong,TenDLuong) Values('DL1','1000B')
Insert into Dung_Luong(MaDLuong,TenDLuong) Values('DL2','4000MB')
Insert into Dung_Luong(MaDLuong,TenDLuong) Values('DL3','15000B')

Insert into Chip(MaChip,TenChip) Values('MC1','Intel Core i7 7700HQ')
Insert into Chip(MaChip,TenChip) Values('MC2','Intel Core i5 8550U')
Insert into Chip(MaChip,TenChip) Values('MC3','Intel Core i7 8550U')

Insert into Ram(MaRam,TenRam) Values('R1','8GB')
Insert into Ram(MaRam,TenRam) Values('R2','16GB')
Insert into Ram(MaRam,TenRam) Values('R3','8GB')

Insert into Toc_Do(MaTocDo,TenTocDo) Values('TD1','1,8GHz')
Insert into Toc_Do(MaTocDo,TenTocDo) Values('TD2','3,4GHz')
Insert into Toc_Do(MaTocDo,TenTocDo) Values('TD3','3,6GHz')

Insert into Man_Hinh(MaManHinh,TenManHinh) Values('MH1','Dell')
Insert into Man_Hinh(MaManHinh,TenManHinh) Values('MH2','HP')
Insert into Man_Hinh(MaManHinh,TenManHinh) Values('MH3','ASUS')

Insert into SizeMH(MaSizeMH,TenSizeMH) Values('SZ1','17 Inch 144Hz')
Insert into SizeMH(MaSizeMH,TenSizeMH) Values('SZ2', '15 Inch 165Hz')
Insert into SizeMH(MaSizeMH,TenSizeMH) Values('SZ3','17 Inch 165Hz')

Insert into Chuot(MaChuot,TenChuot) Values('C1','Genius')
Insert into Chuot(MaChuot,TenChuot) Values('C2','Logitech')
Insert into Chuot(MaChuot,TenChuot) Values('C3','Fuhlen')

Insert into BanPhim(MaBanPhim,TenBanPhim) Values('BP1','Logitech')
Insert into BanPhim(MaBanPhim,TenBanPhim) Values('BP2','Geezer')
Insert into BanPhim(MaBanPhim,TenBanPhim) Values('BP3','Justgogo')

Insert into O_Dia(MaODia,TenODia) Values('OD1','DVD')
Insert into O_Dia(MaODia,TenODia) Values('OD2','CD')
Insert into O_Dia(MaODia,TenODia) Values('OD3','DVD')

Insert into Loa(MaLoa,TenLoa) Values('L1','SONY')
Insert into Loa(MaLoa,TenLoa) Values('L2','BOSE')
Insert into Loa(MaLoa,TenLoa) Values('L3','LOGITECH')

Insert into MayTinh(MaMay,TenMay,MaPhong,MaOCung,MaDLuong,MaChip,MaRam,MaTocDo,MaManHinh,MaSizeMH,MaChuot,MaBanPhim,MaODia,MaLoa,TinhTrang,Ghichu)
Values ('MT1', 'ASUS','P02','OC1','DL3','MC1','R1','TD1','MH3','SZ1','C1','BP1','OD1','L2','Trong','')
Insert into MayTinh(MaMay,TenMay,MaPhong,MaOCung,MaDLuong,MaChip,MaRam,MaTocDo,MaManHinh,MaSizeMH,MaChuot,MaBanPhim,MaODia,MaLoa,TinhTrang,Ghichu)
Values ('MT2', 'DELL','P03','OC2','DL2','MC2','R2','TD2','MH2','SZ2','C2','BP2','OD2','L1','Đa Thue','')
Insert into MayTinh(MaMay,TenMay,MaPhong,MaOCung,MaDLuong,MaChip,MaRam,MaTocDo,MaManHinh,MaSizeMH,MaChuot,MaBanPhim,MaODia,MaLoa,TinhTrang,Ghichu)
Values ('MT3', 'ASUS','P01','OC1','DL3','MC3','R1','TD3','MH1','SZ3','C3','BP3','OD3','L3','Trong','')

Insert into NhanVien(MaNV, TenNV, MaCa, NamSinh, GioiTinh, DiaChi, SDT)
Values ('NV1', 'Tran Ngoc Hanh Nguyen','01', '12/12/1996','Nu', '', '0359853210')
Insert into NhanVien(MaNV, TenNV, MaCa, NamSinh, GioiTinh, DiaChi, SDT)
Values ('NV2', 'Pham Van Tuan','03', '06/12/1995','Nam', '', '0345689101')
Insert into NhanVien(MaNV, TenNV, MaCa, NamSinh, GioiTinh, DiaChi, SDT)
Values ('NV3', 'Ha Đang Ngoc','03', '12/08/1997','Nam', '', '0359123458')
Insert into NhanVien(MaNV, TenNV, MaCa, NamSinh, GioiTinh, DiaChi, SDT)
Values ('NV4', 'Nguyen Thuy Duong','01', '12/12/1999','Nu', '', '0343120579')
Insert into NhanVien(MaNV, TenNV, MaCa, NamSinh, GioiTinh, DiaChi, SDT)
Values ('NV5', 'Luu Quang Anh','02', '12/12/1993','Nam', '', '0999682135')
Insert into NhanVien(MaNV, TenNV, MaCa, NamSinh, GioiTinh, DiaChi, SDT)
Values ('NV6', 'Truong Ngoc Linh','01', '12/12/2000','Nu', '', '0343171069')

Insert into CaLam(MaCa,TenCa,ThoiGian) Values('01','Ca 1','8.00-13.00')
Insert into CaLam(MaCa,TenCa,ThoiGian) Values('02','Ca 2','13.00-18.00')
Insert into CaLam(MaCa,TenCa,ThoiGian) Values('03','Ca 3','18.00-23.00')

Insert into ThueMay(MaSTT, MaPhong,MaMay,TenKhach,NgayThue,GioVao,GioRa,MaNV,TongTien,GhiChu) 
Values('STT1','P01','MT1','Nguyen Van A','01/30/2020','10:30','11:30','NV2',20000,'')
Insert into ThueMay(MaSTT, MaPhong,MaMay,TenKhach,NgayThue,GioVao,GioRa,MaNV,TongTien,GhiChu) 
Values('STT2','P03','MT2','Nguyen Thi B','02/15/2020','09:00','13:15','NV1',42000,'')
Insert into ThueMay(MaSTT, MaPhong,MaMay,TenKhach,NgayThue,GioVao,GioRa,MaNV,TongTien,GhiChu) 
Values('STT3','P02','MT3','Tran Ngoc Minh Thanh','03/05/2020','16:00','17:00','NV3',20000,'')
Insert into ThueMay(MaSTT, MaPhong,MaMay,TenKhach,NgayThue,GioVao,GioRa,MaNV,TongTien,GhiChu) 
Values('STT4','P01','MT2','Nguyen Minh Ngoc','04/02/2020','10:30','11:30','NV2',20000,'')
Insert into ThueMay(MaSTT, MaPhong,MaMay,TenKhach,NgayThue,GioVao,GioRa,MaNV,TongTien,GhiChu) 
Values('STT5','P02','MT3','Pham Manh Hung','04/10/2020','09:00','13:15','NV1',40000,'')
Insert into ThueMay(MaSTT, MaPhong,MaMay,TenKhach,NgayThue,GioVao,GioRa,MaNV,TongTien,GhiChu) 
Values('STT6','P03','MT1','Ha Dac Huy','04/20/2020','16:00','17:00','NV3',100000,'')

Insert into DonGia(MaDonGia,TenDonGio,DonGiaThueGio) Values('DG1','đơn giá 1','10000')

Insert into GiaiPhap(MaGiaiPhap,TenGiaiPhap,ChiPhi) Values('GP1','giải pháp 1',2000000)
Insert into GiaiPhap(MaGiaiPhap,TenGiaiPhap,ChiPhi) Values('GP2','giải pháp 2',1000000)
Insert into GiaiPhap(MaGiaiPhap,TenGiaiPhap,ChiPhi) Values('GP3','giải pháp 3',1500000)

Insert into BaoTri(MaBaoTri,NgayBaoTri,MaNBT,TongChiPhi) Values('BT1','03/15/2020','NBT1',1000000)
Insert into BaoTri(MaBaoTri,NgayBaoTri,MaNBT,TongChiPhi) Values('BT2','04/15/2019','NBT3',1850000)
Insert into BaoTri(MaBaoTri,NgayBaoTri,MaNBT,TongChiPhi) Values('BT3','02/20/2020','NBT2',2100000)

Insert into ChiTietBaoTri(MaBaoTri,MaMay,MaTinhTrang,MaNguyenNhan,MaGiaiPhap,ThanhTien) 
Values('BT1','MT2','TT1','NN2','GP1',1000000)
Insert into ChiTietBaoTri(MaBaoTri,MaMay,MaTinhTrang,MaNguyenNhan,MaGiaiPhap,ThanhTien) 
Values('BT2','MT3','TT2','NN2','GP2',2500000)
Insert into ChiTietBaoTri(MaBaoTri,MaMay,MaTinhTrang,MaNguyenNhan,MaGiaiPhap,ThanhTien) 
Values('BT3','MT1','TT3','NN1','GP3',1500000)
Insert into ChiTietBaoTri(MaBaoTri,MaMay,MaTinhTrang,MaNguyenNhan,MaGiaiPhap,ThanhTien) 
Values('BT1','MT3','TT1','NN2','GP1',1000000)
Insert into ChiTietBaoTri(MaBaoTri,MaMay,MaTinhTrang,MaNguyenNhan,MaGiaiPhap,ThanhTien) 
Values('BT2','MT4','TT2','NN2','GP2',2500000)
Insert into ChiTietBaoTri(MaBaoTri,MaMay,MaTinhTrang,MaNguyenNhan,MaGiaiPhap,ThanhTien) 
Values('BT3','MT5','TT3','NN1','GP3',1500000)

Insert into TinhTrang(MaTinhTrang,TenTinhTrang) Values('TT1','Hỏng bàn phím')
Insert into TinhTrang(MaTinhTrang,TenTinhTrang) Values('TT2','Hỏng ổ cứng')
Insert into TinhTrang(MaTinhTrang,TenTinhTrang) Values('TT3','Hỏng chuột')

Insert into NguyenNhan(MaNguyenNhan,TenNguyenNhan) Values('NN1','NSX')
Insert into NguyenNhan(MaNguyenNhan,TenNguyenNhan) Values('NN2','KH')

Insert into NhaBaoTri(MaNBT,TenNBT,DiaChi,SDT) Values('NBT1','Trần Văn Hùng','Hà Nội','034526797')
Insert into NhaBaoTri(MaNBT,TenNBT,DiaChi,SDT) Values('NBT2','Phạm Tiến Thắng','Bắc Ninh','037543789')
Insert into NhaBaoTri(MaNBT,TenNBT,DiaChi,SDT) Values('NBT3','Vũ Ngọc Hải','Hà Nội','013965478')

Insert into BaoTri(MaBaoTri,NgayBaoTri,MaNBT,TongChiPhi) Values('BT1','03/15/2020','NBT1',1000000)
Insert into BaoTri(MaBaoTri,NgayBaoTri,MaNBT,TongChiPhi) Values('BT2','04/15/2019','NBT3',1850000)
Insert into BaoTri(MaBaoTri,NgayBaoTri,MaNBT,TongChiPhi) Values('BT3','02/20/2020','NBT2',2100000)
update Phong set SoMay=(SoMay+1) WHERE MaPhong ='P01'
SELECT MayTinh.MaPhong,TenPhong,MayTinh.MaMay,TenMay,TinhTrang FROM MayTinh join Phong on MayTinh.MaPhong=Phong.MaPhong
select ThueMay.MaPhong,ThueMay.MaMay,TenMay,TinhTrang from ThueMay join MayTinh on ThueMay.MaMay=MayTinh.MaMay 
select ThueMay.MaPhong, ThueMay.MaMay, TenMay, NgayThue, TongTien from ThueMay join MayTinh on ThueMay.MaMay=MayTinh.MaMay where TinhTrang='Trong'