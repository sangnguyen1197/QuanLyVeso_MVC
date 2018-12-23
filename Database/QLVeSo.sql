
create database QuanLyVESO

go
use QuanLyVESO

create table DaiLy
(
	MaDaiLy varchar(20) primary key,
	TenDaiLy nvarchar(50),
	DiaChi nvarchar(100),
	SDT varchar(15),
	Flag bit
)


create table LoaiVeso
(
	MaLoaiVeSo varchar(20) primary key,
	Tinh nvarchar(20),
	GiaBan decimal(19,3),
	Flag bit
)

create table SoLuongDK 
(
	MaSoLuongDK varchar(20) primary key,
	MaDaiLy varchar(20),
	NgayDK date,
	SoLuongDK int,
	Flag bit,

	foreign key (MaDaiLy) references DaiLy(MaDaiLy)
)

create table PhatHanh
(
	MaPhatHanh varchar(20) primary key,
	MaDaiLy varchar(20),
	MaLoaiVeSo varchar(20),
	SoLuong int,
	NgayNhan date,
	SLBan int,
	
	DoanhThuDPH decimal(19,3),		/*doanhthu = slban * 10000*/

	HoaHong decimal(5, 2),			/*vi du: min:10, max: 50 */

	TienThanhToan decimal(19,3),	/* (100-HoaHong)/100* DoanhThu */

	Flag bit,

	foreign key (MaDaiLy) references DaiLy(MaDaiLy),
	foreign key (MaLoaiVeSo) references LoaiVeso(MaLoaiVeSo)
)

create table Giai 
(
	MaGiai varchar(20) primary key,
	TenGiai nvarchar(30),
	SoTienNhan decimal(19, 3),
	Flag bit

)

create table KetQuaSoXo
(
	MaKetQua varchar(20) primary key,  
	MaLoaiVeSo varchar(20),
	MaGiai varchar(20),
	NgaySo date,
	SoTrung varchar(10),
	Flag bit,

	foreign key (MaLoaiVeSo) references LoaiVeSo(MaLoaiVeSo),
	foreign key (MaGiai) references Giai(MaGiai)

)

create table PhieuThu 
(
	MaPhieuThu varchar(20) primary key,
	MaDaiLy varchar(20),
	NgayNop date,
	SoTienNop decimal(19,3),
	Flag bit
	
	foreign key (MaDaiLy) references DaiLy(MaDaiLy)
)

create table CongNo
(
	MaCongNo varchar(20) primary key,
	MaDaiLy varchar(20),
	NgayNo date,
	SoTienNo decimal(19,3),
	Flag bit

	foreign key (MaDaiLy) references DaiLy(MaDaiLy)
)

create table PhieuChi
(
	MaPhieuChi varchar(20) primary key,
	NgayChi date,
	NoiDung nvarchar(200),
	SoTien decimal (19, 3),
	Flag bit
)


/*NHẬP DỮ LIỆU*/

insert into DaiLy values ('DL01', N'Đại lý vé số Phương Trang', N'93 Phan Đình Phùng, Phường 17, Quận Phú Nhuận, TP.Hồ Chí Minh', '098 877 7444',1)
insert into DaiLy values ('DL02', N'Đại lý vé số Đổi Đời', N'155 Vạn Kiếp, Phường 3, Quận Bình Thạnh, TP.Hồ Chí Minh', '090 364 2129', 1)
insert into DaiLy values ('DL03', N'Đại lý vé số Kế Thanh', N'122 Đinh Tiên Hoàng, Phường Đa Kao, Quận 1, TP.Hồ Chí Minh', '090 382 2982', 1)
insert into DaiLy values ('DL04', N'Đại lý vé số Bình An', N'92 Bà Huyện Thanh Quan, Phường 9, Quận 3, TP.Hồ Chí Minh', '096 772 9729', 1)
insert into DaiLy values ('DL05', N'Đại lý vé số Phát Tài', N'329 Phan Đình Phùng, Phường 1, Quận Phú Nhuận, TP.Hồ Chí Minh', '093 412 8268', 1)
insert into DaiLy values ('DL06', N'Đại lý vé số Hòa Phát', N'14 Xô Viết Nghệ Tĩnh, Phường 19, Quận Bình Thạnh, TP.Hồ Chí Minh', '090 398 0280', 1)


insert into LoaiVeso values ('AG67-T03', N'An Giang', 10000, 1)
insert into LoaiVeso values ('BL31-T07', N'Bạc Liêu', 10000, 1)
insert into LoaiVeso values ('BTRE34-T08', N'Bến Tre', 10000, 1)
insert into LoaiVeso values ('BD53-T12', N'Bình Dương', 10000, 1)
insert into LoaiVeso values ('BPH93-T04', N'Bình Phước', 10000, 1)
insert into LoaiVeso values ('BTH86-T10', N'Bình Thuận', 10000, 1)
insert into LoaiVeso values ('CM69-T13', N'Cà Mau', 10000, 1)
insert into LoaiVeso values ('CT65-T14', N'Cần Thơ', 10000, 1)
insert into LoaiVeso values ('DL49-T15', N'Đà Lạt', 10000, 1)
insert into LoaiVeso values ('DNAI22-T05', N'Đồng Nai', 10000, 1)
insert into LoaiVeso values ('DT25-T06', N'Đồng Tháp', 10000, 1)
insert into LoaiVeso values ('HG95-T16', N'Hậu Giang', 10000, 1)
insert into LoaiVeso values ('KG68-T17', N'Kiên Giang', 10000, 1)
insert into LoaiVeso values ('LA01-T01', N'Long An', 10000, 1)
insert into LoaiVeso values ('STR45-T11', N'Sóc Trăng', 10000, 1)
insert into LoaiVeso values ('TN39-T09', N'Tây Ninh', 10000, 1)
insert into LoaiVeso values ('TG08-T02', N'Tiền Giang', 10000, 1)
insert into LoaiVeso values ('TP32-T08', N'Thành Phố HCM', 10000, 1)
insert into LoaiVeso values ('TV84-T18', N'Trà Vinh', 10000, 1)
insert into LoaiVeso values ('VL64-T19', N'Vĩnh Long', 10000, 1)
insert into LoaiVeso values ('VT72-T20', N'Vũng Tàu', 10000, 1)


insert into Giai values ('GI01', N'Giải nhất', 30000000, 1)
insert into Giai values ('GI02', N'Giải nhì', 15000000, 1)
insert into Giai values ('GI03', N'Giải ba', 10000000, 1)
insert into Giai values ('GI04', N'Giải tư', 3000000, 1)
insert into Giai values ('GI05', N'Giải năm', 1000000, 1)
insert into Giai values ('GI06', N'Giải sáu', 400000, 1)
insert into Giai values ('GI07', N'Giải bảy', 200000, 1)
insert into Giai values ('GI08', N'Giải tám', 100000, 1)
insert into Giai values ('GIDB', N'Giải đặc biệt', 2000000000, 1)
insert into Giai values ('GIPDB', N'Giải phụ đặc biệt', 50000000, 1)
insert into Giai values ('GIKK', N'Giải Khuyến khích', 6000000, 1)


insert into SoLuongDK values ('DK01', 'DL01', '02/23/2018', 100, 1)
insert into SoLuongDK values ('DK02', 'DL02', '03/22/2018', 150, 1)
insert into SoLuongDK values ('DK03', 'DL03', '04/21/2018', 100, 1)
insert into SoLuongDK values ('DK04', 'DL04', '05/26/2018', 150, 1)
insert into SoLuongDK values ('DK05', 'DL05', '06/27/2018', 200, 1)
insert into SoLuongDK values ('DK06', 'DL06', '07/29/2018', 300, 1)
insert into SoLuongDK values ('DK07', 'DL02', '08/23/2018', 200, 1)


/*so luong giao tiep theo = sldk * ti le ban 3 dot gan nhat*/
insert into PhatHanh values ('PH01', 'DL01', 'TN39-T09', 100, '2018/10/04', 80, 800000, 10, 720000, 1)
insert into PhatHanh values ('PH02', 'DL01', 'AG67-T03', 100, '2018/10/04', 90, 900000, 10, 810000, 1)
insert into PhatHanh values ('PH03', 'DL01', 'BTH86-T10', 100, '2018/10/04', 100, 1000000, 10, 900000, 1)
insert into PhatHanh values ('PH04', 'DL02', 'TN39-T09', 150, '2018/10/04', 130, 1300000, 10, 1170000, 1)
insert into PhatHanh values ('PH05', 'DL02', 'AG67-T03', 150, '2018/10/04', 140, 1400000, 10, 1260000, 1)
insert into PhatHanh values ('PH06', 'DL03', 'BTH86-T10', 100, '2018/10/04', 95, 950000, 10, 855000, 1)
insert into PhatHanh values ('PH07', 'DL05', 'TN39-T09', 200, '2018/10/04', 190, 1900000, 10, 1710000, 1)
insert into PhatHanh values ('PH08', 'DL05', 'BTH86-T10', 200, '2018/10/04', 180, 1800000, 10, 1620000, 1)
insert into PhatHanh values ('PH09', 'DL06', 'AG67-T03', 300, '2018/10/04', 250, 2500000, 10, 2250000, 1)
insert into PhatHanh values ('PH10', 'DL01', 'VL64-T19', 90, '2018/10/05', 80, 800000, 10, 720000, 1)
insert into PhatHanh values ('PH11', 'DL01', 'BD53-T12', 90, '2018/10/05', 70, 700000, 10, 630000, 1)
insert into PhatHanh values ('PH12', 'DL02', 'VL64-T19', 135, '2018/10/05', 135, 1350000, 10, 1215000, 1)
insert into PhatHanh values ('PH13', 'DL02', 'BD53-T12', 135, '2018/10/05', 130, 1300000, 10, 1170000, 1)
insert into PhatHanh values ('PH14', 'DL06', 'BD53-T12', 250, '2018/10/05', 240, 2400000, 10, 2160000, 1)
insert into PhatHanh values ('PH15', 'DL04', 'TP32-T08', 145, '2018/10/06', 145, 1450000, 10, 1305000, 1)
insert into PhatHanh values ('PH16', 'DL04', 'LA01-T01', 145, '2018/10/06', 140, 1400000, 10, 1260000, 1)


insert into KetQuaSoXo values ('KQ01', 'TP32-T08', 'GI01','2018/10/06','77282',1)
insert into KetQuaSoXo values ('KQ02', 'TP32-T08', 'GI02','2018/10/06','75104',1)
insert into KetQuaSoXo values ('KQ03', 'TP32-T08', 'GI03','2018/10/06','42663',1)
insert into KetQuaSoXo values ('KQ04', 'TP32-T08', 'GI03','2018/10/06','30772',1)
insert into KetQuaSoXo values ('KQ05', 'TP32-T08', 'GI04','2018/10/06','35641',1)
insert into KetQuaSoXo values ('KQ06', 'TP32-T08', 'GI04','2018/10/06','15591',1)
insert into KetQuaSoXo values ('KQ07', 'TP32-T08', 'GI04','2018/10/06','03619',1)
insert into KetQuaSoXo values ('KQ08', 'TP32-T08', 'GI04','2018/10/06','30705',1)
insert into KetQuaSoXo values ('KQ09', 'TP32-T08', 'GI04','2018/10/06','99993',1)
insert into KetQuaSoXo values ('KQ10', 'TP32-T08', 'GI04','2018/10/06','36204',1)
insert into KetQuaSoXo values ('KQ11', 'TP32-T08', 'GI04','2018/10/06','74553',1)
insert into KetQuaSoXo values ('KQ12', 'TP32-T08', 'GI05','2018/10/06','9840',1)
insert into KetQuaSoXo values ('KQ13', 'TP32-T08', 'GI06','2018/10/06','7076',1)
insert into KetQuaSoXo values ('KQ14', 'TP32-T08', 'GI06','2018/10/06','5152',1)
insert into KetQuaSoXo values ('KQ15', 'TP32-T08', 'GI06','2018/10/06','2296',1)
insert into KetQuaSoXo values ('KQ16', 'TP32-T08', 'GI07','2018/10/06','279',1)
insert into KetQuaSoXo values ('KQ17', 'TP32-T08', 'GI08','2018/10/06','38',1)
insert into KetQuaSoXo values ('KQ18', 'TP32-T08', 'GIDB','2018/10/06','075811',1)


insert into PhieuThu values ('PTH01', 'DL02', '2018/10/05',1000000,1)
insert into PhieuThu values ('PTH02', 'DL05', '2018/10/05',500000,1)
insert into PhieuThu values ('PTH03', 'DL01', '2018/10/06',1000000,1)
insert into PhieuThu values ('PTH04', 'DL02', '2018/10/06',1000000,1)
insert into PhieuThu values ('PTH05', 'DL03', '2018/10/06',500000,1)
insert into PhieuThu values ('PTH06', 'DL06', '2018/10/06',500000,1)


insert into CongNo values ('CN01', 'DL02', '2018/10/05', 1420000, 1)
insert into CongNo values ('CN02', 'DL05', '2018/10/05', 2830000, 1)
insert into CongNo values ('CN03', 'DL01', '2018/10/06', 1430000, 1)
insert into CongNo values ('CN04', 'DL02', '2018/10/06', 420000, 1)
insert into CongNo values ('CN05', 'DL03', '2018/10/06', 355000, 1)
insert into CongNo values ('CN06', 'DL06', '2018/10/06', 3910000, 1)


insert into PhieuChi values ('PCH01', '2018/10/05',N'Trúng giải đặc biệt',2000000000, 1)
insert into PhieuChi values ('PCH02', '2018/10/06',N'Trúng 8 giải tám',8000000, 1)


/*TRIGGER*/

go
create trigger tr_UpdateCongNo1 on PhatHanh
for Update
as
begin
	declare @madl varchar(20)
	select @madl = MaDaiLy from inserted
	
	declare @oldTienThanhToan decimal (19, 3)
	select @oldTienThanhToan = TienThanhToan from deleted where MaPhatHanh = deleted.MaPhatHanh
	declare @newTienThanhToan decimal (19, 3)
	select @newTienThanhToan = TienThanhToan from inserted where MaPhatHanh = inserted.MaPhatHanh

	declare @saisoTTT decimal (19, 3)
	select @saisoTTT = @newTienThanhToan - @oldTienThanhToan

	Update CongNo set SoTienNo = SoTienNo + @saisoTTT where CongNo.MaDaiLy = @madl
	
end


go
create trigger tr_UpdateCongNo2 on PhieuThu
for Update
as
begin
	declare @madl varchar(20)
	select @madl = MaDaiLy from inserted
	
	declare @oldSoTienNop decimal (19, 3)
	select @oldSoTienNop = SoTienNop from deleted where MaDaiLy = deleted.MaDaiLy
	declare @newSoTienNop decimal (19, 3)
	select @newSoTienNop = SoTienNop from inserted where MaDaiLy = inserted.MaDaiLy

	declare @saisoTN decimal (19, 3)
	select @saisoTN = @newSoTienNop - @oldSoTienNop

	Update CongNo set SoTienNo = SoTienNo - @saisoTN where CongNo.MaDaiLy = @madl
end


go
create trigger tr_UpdateDoanhThu on PhatHanh
for Update
as
begin
	declare @maph varchar(20)
	select @maph = MaPhatHanh from inserted

	declare @giave decimal (19, 3)
	select @giave = GiaBan from LoaiVeso, inserted where LoaiVeso.MaLoaiVeSo = inserted.MaLoaiVeSo
	
	declare @newSLBan int
	select @newSLBan = SLBan from inserted where MaPhatHanh = inserted.MaPhatHanh

	declare @newdoanhthu int
	select @newdoanhthu = @newSLBan * @giave

	declare @newHoaHong decimal(5, 2)
	select @newHoaHong = HoaHong from inserted where MaPhatHanh = inserted.MaPhatHanh

	Update PhatHanh set DoanhThuDPH = @newdoanhthu, TienThanhToan = @newdoanhthu * (1 - (@newHoaHong / 100)) where PhatHanh.MaPhatHanh = @maph
	
end


/*Test Trigger
update PhatHanh set TienThanhToan = 755000 where MaDaiLy = 'DL03'
update PhieuThu set SoTienNop = 300000 where MaDaiLy = 'DL03'

update PhatHanh set SLBan = 70 where MaPhatHanh = 'PH1'

update PhatHanh set HoaHong = 20 where MaPhatHanh = 'PH1'


/*Test Scalar
select top 1 MaPhatHanh from PhatHanh order by MaPhatHanh desc

select MaPhieuChi, NgayChi, NoiDung, SoTien from PhieuChi where NoiDung like '%trúng%'

select Tinh from LoaiVeso
select MaLoaiVeSo from LoaiVeso where Tinh = N'An Giang'
