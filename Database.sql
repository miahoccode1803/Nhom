USE [master]
GO
/****** Object:  Database [Dangkihoc]    Script Date: 10/01/2024 2:36:26 CH ******/
CREATE DATABASE [Dangkihoc]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Dangkihoc', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Dangkihoc.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Dangkihoc_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Dangkihoc_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Dangkihoc] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Dangkihoc].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Dangkihoc] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Dangkihoc] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Dangkihoc] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Dangkihoc] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Dangkihoc] SET ARITHABORT OFF 
GO
ALTER DATABASE [Dangkihoc] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Dangkihoc] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Dangkihoc] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Dangkihoc] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Dangkihoc] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Dangkihoc] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Dangkihoc] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Dangkihoc] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Dangkihoc] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Dangkihoc] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Dangkihoc] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Dangkihoc] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Dangkihoc] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Dangkihoc] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Dangkihoc] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Dangkihoc] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Dangkihoc] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Dangkihoc] SET RECOVERY FULL 
GO
ALTER DATABASE [Dangkihoc] SET  MULTI_USER 
GO
ALTER DATABASE [Dangkihoc] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Dangkihoc] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Dangkihoc] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Dangkihoc] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Dangkihoc] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Dangkihoc] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Dangkihoc', N'ON'
GO
ALTER DATABASE [Dangkihoc] SET QUERY_STORE = ON
GO
ALTER DATABASE [Dangkihoc] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Dangkihoc]
GO
/****** Object:  Table [dbo].[Dangkihoc]    Script Date: 10/01/2024 2:36:26 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dangkihoc](
	[ID] [int] NOT NULL,
	[masv] [varchar](50) NULL,
 CONSTRAINT [PK_Dangkihoc_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Giaovien]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Giaovien](
	[magv] [varchar](50) NOT NULL,
	[tengv] [nvarchar](150) NULL,
	[diachi] [nvarchar](150) NULL,
	[gioitinh] [bit] NULL,
	[ngaysinh] [date] NULL,
	[email] [varchar](100) NULL,
	[sdt] [varchar](30) NULL,
 CONSTRAINT [PK_Giaovien] PRIMARY KEY CLUSTERED 
(
	[magv] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Khoa]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Khoa](
	[makhoa] [varchar](50) NOT NULL,
	[tenkhoa] [nvarchar](150) NULL,
 CONSTRAINT [PK_Khoa] PRIMARY KEY CLUSTERED 
(
	[makhoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lop]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lop](
	[magv] [varchar](50) NULL,
	[mamh] [varchar](50) NULL,
	[manganh] [varchar](50) NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[thoigian] [varchar](50) NULL,
	[phonghoc] [nvarchar](150) NULL,
	[trangthai] [bit] NULL,
	[hocki] [varchar](50) NULL,
 CONSTRAINT [PK_Lop] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Monhoc]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Monhoc](
	[mamh] [varchar](50) NOT NULL,
	[tenmh] [nvarchar](150) NULL,
	[sotin] [int] NULL,
 CONSTRAINT [PK_Monhoc] PRIMARY KEY CLUSTERED 
(
	[mamh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nganh]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nganh](
	[manganh] [varchar](50) NOT NULL,
	[tennganh] [nvarchar](150) NULL,
	[makhoa] [varchar](50) NULL,
 CONSTRAINT [PK_Nganh] PRIMARY KEY CLUSTERED 
(
	[manganh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sinhvien]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sinhvien](
	[masv] [varchar](50) NOT NULL,
	[hoten] [nvarchar](150) NULL,
	[ngaysinh] [date] NULL,
	[gioitinh] [bit] NULL,
	[lop] [varchar](50) NULL,
	[manganh] [varchar](50) NULL,
	[quequan] [nvarchar](150) NULL,
	[diachi] [nvarchar](150) NULL,
	[email] [varchar](150) NULL,
	[sdt] [varchar](30) NULL,
 CONSTRAINT [PK_Sinhvien] PRIMARY KEY CLUSTERED 
(
	[masv] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[tentk] [varchar](50) NOT NULL,
	[matkhau] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[tentk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Dangkihoc] ([ID], [masv]) VALUES (3, N'73DCTT23241')
INSERT [dbo].[Dangkihoc] ([ID], [masv]) VALUES (4, N'73DCTT23241')
INSERT [dbo].[Dangkihoc] ([ID], [masv]) VALUES (5, N'73DCTT23241')
GO
INSERT [dbo].[Giaovien] ([magv], [tengv], [diachi], [gioitinh], [ngaysinh], [email], [sdt]) VALUES (N'GV002', N'Nguyễn Văn Tuấn', N'Văn Quán ,Hà Đông ,Hà Nội', 1, CAST(N'1977-02-19' AS Date), N'tuangt002@gmail.com', N'0343434546')
INSERT [dbo].[Giaovien] ([magv], [tengv], [diachi], [gioitinh], [ngaysinh], [email], [sdt]) VALUES (N'GV003', N'Hà Văn Hương', N'Hoàng Mai,Hà Nội', 0, CAST(N'1989-08-28' AS Date), N'vanhuong09@gmail.com', N'0378787466')
INSERT [dbo].[Giaovien] ([magv], [tengv], [diachi], [gioitinh], [ngaysinh], [email], [sdt]) VALUES (N'GV004', N'Nguyễn Thị C', N'AGBXYZ', 0, CAST(N'1976-08-23' AS Date), N'cthi234@gmail.com', N'0347443422')
INSERT [dbo].[Giaovien] ([magv], [tengv], [diachi], [gioitinh], [ngaysinh], [email], [sdt]) VALUES (N'GV005', N'Lê Mạnh D', N'', 1, CAST(N'1975-06-13' AS Date), N'', N'0334343432')
INSERT [dbo].[Giaovien] ([magv], [tengv], [diachi], [gioitinh], [ngaysinh], [email], [sdt]) VALUES (N'GV006', N'Dương Văn D', N'', 1, CAST(N'1988-12-03' AS Date), N'', N'')
GO
INSERT [dbo].[Khoa] ([makhoa], [tenkhoa]) VALUES (N'CK', N'Cơ khí')
INSERT [dbo].[Khoa] ([makhoa], [tenkhoa]) VALUES (N'CNTT', N'Công nghệ thông tin')
INSERT [dbo].[Khoa] ([makhoa], [tenkhoa]) VALUES (N'CT', N'Công trình')
INSERT [dbo].[Khoa] ([makhoa], [tenkhoa]) VALUES (N'KTVT', N'Kinh tế vận tải')
GO
SET IDENTITY_INSERT [dbo].[Lop] ON 

INSERT [dbo].[Lop] ([magv], [mamh], [manganh], [ID], [thoigian], [phonghoc], [trangthai], [hocki]) VALUES (N'GV002', N'CTDLGT04', N'HT', 1, N'', N'', 1, NULL)
INSERT [dbo].[Lop] ([magv], [mamh], [manganh], [ID], [thoigian], [phonghoc], [trangthai], [hocki]) VALUES (N'GV003', N'TCC03', N'KT', 2, N'', N'', 0, NULL)
INSERT [dbo].[Lop] ([magv], [mamh], [manganh], [ID], [thoigian], [phonghoc], [trangthai], [hocki]) VALUES (N'GV002', N'LTHDT03', N'TT', 3, N'', N'', 1, NULL)
INSERT [dbo].[Lop] ([magv], [mamh], [manganh], [ID], [thoigian], [phonghoc], [trangthai], [hocki]) VALUES (N'GV003', N'NLHDH03', N'TT', 4, N'', N'', 1, N'2023-2024_1')
INSERT [dbo].[Lop] ([magv], [mamh], [manganh], [ID], [thoigian], [phonghoc], [trangthai], [hocki]) VALUES (N'GV002', N'CTDLGT04', N'TT', 5, N'', N'', 1, NULL)
INSERT [dbo].[Lop] ([magv], [mamh], [manganh], [ID], [thoigian], [phonghoc], [trangthai], [hocki]) VALUES (N'GV006', N'LTHDT03', N'CN', 6, N'7h-8h', N'A2.101', 1, NULL)
INSERT [dbo].[Lop] ([magv], [mamh], [manganh], [ID], [thoigian], [phonghoc], [trangthai], [hocki]) VALUES (N'GV006', N'TCC03', N'KT', 7, N'9h-12h', N'C3.204', 0, NULL)
INSERT [dbo].[Lop] ([magv], [mamh], [manganh], [ID], [thoigian], [phonghoc], [trangthai], [hocki]) VALUES (N'GV002', N'LTHDT03', N'TT', 8, N'14h-16h', N'A6.504', 1, NULL)
INSERT [dbo].[Lop] ([magv], [mamh], [manganh], [ID], [thoigian], [phonghoc], [trangthai], [hocki]) VALUES (N'GV006', N'LTTQ03', N'HT', 9, N'', N'', 1, N'2023-2024_2')
SET IDENTITY_INSERT [dbo].[Lop] OFF
GO
INSERT [dbo].[Monhoc] ([mamh], [tenmh], [sotin]) VALUES (N'ATTT03', N'An toàn thông tin', 3)
INSERT [dbo].[Monhoc] ([mamh], [tenmh], [sotin]) VALUES (N'CNXH02', N'Chủ nghĩa xã hội', 2)
INSERT [dbo].[Monhoc] ([mamh], [tenmh], [sotin]) VALUES (N'CTDLGT04', N'Cấu trúc dữ liệu và giải thuật ', 4)
INSERT [dbo].[Monhoc] ([mamh], [tenmh], [sotin]) VALUES (N'KTCT02', N'Kinh tế chính trị', 2)
INSERT [dbo].[Monhoc] ([mamh], [tenmh], [sotin]) VALUES (N'LTHDT03', N'Lập trình hướng đối tượng C++', 3)
INSERT [dbo].[Monhoc] ([mamh], [tenmh], [sotin]) VALUES (N'LTTQ03', N'Lập trình trực quan C#', 3)
INSERT [dbo].[Monhoc] ([mamh], [tenmh], [sotin]) VALUES (N'NLHDH03', N'Nguyên lý hệ điều hành', 3)
INSERT [dbo].[Monhoc] ([mamh], [tenmh], [sotin]) VALUES (N'SBVL04', N'Sức bền vật liệu', 4)
INSERT [dbo].[Monhoc] ([mamh], [tenmh], [sotin]) VALUES (N'T103', N'Toán 1', 3)
INSERT [dbo].[Monhoc] ([mamh], [tenmh], [sotin]) VALUES (N'T204', N'Toán 2', 4)
INSERT [dbo].[Monhoc] ([mamh], [tenmh], [sotin]) VALUES (N'TCC03', N'Toán cao cấp', 3)
INSERT [dbo].[Monhoc] ([mamh], [tenmh], [sotin]) VALUES (N'VKT04', N'Vẽ kỹ thuật', 4)
GO
INSERT [dbo].[Nganh] ([manganh], [tennganh], [makhoa]) VALUES (N'CD', N'Công nghệ kỹ thuật xây dựng cầu đường', N'CT')
INSERT [dbo].[Nganh] ([manganh], [tennganh], [makhoa]) VALUES (N'CN', N'Công nghệ kĩ thuật cơ điện tử', N'CK')
INSERT [dbo].[Nganh] ([manganh], [tennganh], [makhoa]) VALUES (N'HT', N'Hệ thống thông tin', N'CNTT')
INSERT [dbo].[Nganh] ([manganh], [tennganh], [makhoa]) VALUES (N'KT', N'Kế toán', N'KTVT')
INSERT [dbo].[Nganh] ([manganh], [tennganh], [makhoa]) VALUES (N'OT', N'Công nghệ kỹ thuật Oto', N'CK')
INSERT [dbo].[Nganh] ([manganh], [tennganh], [makhoa]) VALUES (N'QM', N'Quản trị marketing', N'KTVT')
INSERT [dbo].[Nganh] ([manganh], [tennganh], [makhoa]) VALUES (N'QT', N'Quản trị doanh nghiệp', N'KTVT')
INSERT [dbo].[Nganh] ([manganh], [tennganh], [makhoa]) VALUES (N'QX', N'Quản lý xây dựng', N'CT')
INSERT [dbo].[Nganh] ([manganh], [tennganh], [makhoa]) VALUES (N'TD', N'Thương mại điện tử', N'KTVT')
INSERT [dbo].[Nganh] ([manganh], [tennganh], [makhoa]) VALUES (N'TG', N'Trí tuệ nhân tạo và giao thông thông minh', N'CNTT')
INSERT [dbo].[Nganh] ([manganh], [tennganh], [makhoa]) VALUES (N'TM', N'Mạng máy tính và truyền thông ', N'CNTT')
INSERT [dbo].[Nganh] ([manganh], [tennganh], [makhoa]) VALUES (N'TN', N'Tài chính ngân hàng', N'KTVT')
INSERT [dbo].[Nganh] ([manganh], [tennganh], [makhoa]) VALUES (N'TT', N'Công nghệ thông tin', N'CNTT')
INSERT [dbo].[Nganh] ([manganh], [tennganh], [makhoa]) VALUES (N'VL', N'Logistic và quản lý chuỗi cung ứng ', N'KTVT')
GO
INSERT [dbo].[Sinhvien] ([masv], [hoten], [ngaysinh], [gioitinh], [lop], [manganh], [quequan], [diachi], [email], [sdt]) VALUES (N'71DCHT345', N'Nguyễn Văn An', CAST(N'2001-04-18' AS Date), 1, N'71DCHT23', N'HT', N'', N'', N'', N'')
INSERT [dbo].[Sinhvien] ([masv], [hoten], [ngaysinh], [gioitinh], [lop], [manganh], [quequan], [diachi], [email], [sdt]) VALUES (N'72DCCK22334', N'Hà Văn Huy', CAST(N'2003-07-23' AS Date), 1, N'72DCCK22', NULL, N'', N'Helo', N'', N'')
INSERT [dbo].[Sinhvien] ([masv], [hoten], [ngaysinh], [gioitinh], [lop], [manganh], [quequan], [diachi], [email], [sdt]) VALUES (N'72DCHT2003', N'Nguyễn Văn Hoa', CAST(N'2003-05-21' AS Date), 0, N'73DCHT23', N'HT', N'Vĩnh Phúc', N'Trần Đại Nghĩa,Hai Bà Trưng,Hà Nội', N'hoanguyen2003@gmail.com', N'03455656567')
INSERT [dbo].[Sinhvien] ([masv], [hoten], [ngaysinh], [gioitinh], [lop], [manganh], [quequan], [diachi], [email], [sdt]) VALUES (N'72DCQM23434', N'Lê Thị Thu', CAST(N'2003-01-01' AS Date), 0, N'72DCQM23', N'QM', N'', N'', N'', N'')
INSERT [dbo].[Sinhvien] ([masv], [hoten], [ngaysinh], [gioitinh], [lop], [manganh], [quequan], [diachi], [email], [sdt]) VALUES (N'73DCKT88989', N'Nguyên Hoàng', CAST(N'2004-12-02' AS Date), 1, N'73DCKT25', N'KT', N'Hải Dương', N'Hà Nội', N'', N'')
INSERT [dbo].[Sinhvien] ([masv], [hoten], [ngaysinh], [gioitinh], [lop], [manganh], [quequan], [diachi], [email], [sdt]) VALUES (N'73DCTM23445', N'Lê Hoàng Anh', CAST(N'2004-02-21' AS Date), 1, N'73DCTM23', N'TM', N'ABCXYZ', N'ABCXZ', N'anle123@gmail.com', N'0345345334')
INSERT [dbo].[Sinhvien] ([masv], [hoten], [ngaysinh], [gioitinh], [lop], [manganh], [quequan], [diachi], [email], [sdt]) VALUES (N'73DCTT22397', N'Đặng Thu Hà', CAST(N'2004-02-27' AS Date), 0, N'73DCQT23', N'QT', N'DakLak', N'Thanh Xuân', N'thuhaa2702@gmail.com', N'037594939')
INSERT [dbo].[Sinhvien] ([masv], [hoten], [ngaysinh], [gioitinh], [lop], [manganh], [quequan], [diachi], [email], [sdt]) VALUES (N'73DCTT23241', N'Nguyễn Trà My', CAST(N'2004-03-18' AS Date), 0, N'73DCTT23', N'TT', N'Thanh Oai', N'Kim Bài ,Thanh Oai ,Hà Nội', N'jinsama346@gmail.com', N'0348754706')
INSERT [dbo].[Sinhvien] ([masv], [hoten], [ngaysinh], [gioitinh], [lop], [manganh], [quequan], [diachi], [email], [sdt]) VALUES (N'74DCOT21232', N'Hoàng Thanh', CAST(N'2005-03-25' AS Date), 1, N'74DCOT21', N'OT', N'', N'', N'', N'')
GO
INSERT [dbo].[User] ([tentk], [matkhau]) VALUES (N'admin', N'admin')
GO
/****** Object:  Index [IX_Dangkihoc]    Script Date: 10/01/2024 2:36:27 CH ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Dangkihoc] ON [dbo].[Dangkihoc]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Dangkihoc]  WITH CHECK ADD  CONSTRAINT [FK_Dangkihoc_Lop] FOREIGN KEY([ID])
REFERENCES [dbo].[Lop] ([ID])
GO
ALTER TABLE [dbo].[Dangkihoc] CHECK CONSTRAINT [FK_Dangkihoc_Lop]
GO
ALTER TABLE [dbo].[Dangkihoc]  WITH CHECK ADD  CONSTRAINT [FK_Dangkihoc_Sinhvien] FOREIGN KEY([masv])
REFERENCES [dbo].[Sinhvien] ([masv])
GO
ALTER TABLE [dbo].[Dangkihoc] CHECK CONSTRAINT [FK_Dangkihoc_Sinhvien]
GO
ALTER TABLE [dbo].[Lop]  WITH CHECK ADD  CONSTRAINT [FK_Lop_Giaovien] FOREIGN KEY([magv])
REFERENCES [dbo].[Giaovien] ([magv])
GO
ALTER TABLE [dbo].[Lop] CHECK CONSTRAINT [FK_Lop_Giaovien]
GO
ALTER TABLE [dbo].[Lop]  WITH CHECK ADD  CONSTRAINT [FK_Lop_Monhoc] FOREIGN KEY([mamh])
REFERENCES [dbo].[Monhoc] ([mamh])
GO
ALTER TABLE [dbo].[Lop] CHECK CONSTRAINT [FK_Lop_Monhoc]
GO
ALTER TABLE [dbo].[Lop]  WITH CHECK ADD  CONSTRAINT [FK_Lop_Nganh] FOREIGN KEY([manganh])
REFERENCES [dbo].[Nganh] ([manganh])
GO
ALTER TABLE [dbo].[Lop] CHECK CONSTRAINT [FK_Lop_Nganh]
GO
ALTER TABLE [dbo].[Nganh]  WITH CHECK ADD  CONSTRAINT [FK_Nganh_Khoa] FOREIGN KEY([makhoa])
REFERENCES [dbo].[Khoa] ([makhoa])
GO
ALTER TABLE [dbo].[Nganh] CHECK CONSTRAINT [FK_Nganh_Khoa]
GO
ALTER TABLE [dbo].[Sinhvien]  WITH CHECK ADD  CONSTRAINT [FK_Sinhvien_Nganh] FOREIGN KEY([manganh])
REFERENCES [dbo].[Nganh] ([manganh])
GO
ALTER TABLE [dbo].[Sinhvien] CHECK CONSTRAINT [FK_Sinhvien_Nganh]
GO
/****** Object:  StoredProcedure [dbo].[Dangkitin]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Dangkitin]
    @masinhvien varchar(50),
    @manganh varchar(50)
AS 
BEGIN
    SELECT 
        l.id AS 'ID',
        l.mamh AS N'Mã học phần',
        m.tenmh AS N'Tên học phần',
        m.sotin AS N'Số tín chỉ',
        gv.tengv AS N'Giáo viên phụ trách',
        l.manganh AS N'Ngành',
        l.thoigian AS N'Thời gian',
        l.phonghoc AS N'Phòng học',
        m.hocki AS N'Học kì',
		l.trangthai  AS N'Trạng thái'
    FROM 
        Dangkihoc d
        INNER JOIN Sinhvien sv ON sv.masv = d.masv
        INNER JOIN Lop l ON l.id = d.id
        INNER JOIN Giaovien gv ON gv.magv = l.magv
        INNER JOIN Monhoc m ON m.mamh = l.mamh
        INNER JOIN Nganh n ON n.manganh = l.manganh
    WHERE 
        sv.masv = @masinhvien
        AND n.manganh = @manganh;
END
GO
/****** Object:  StoredProcedure [dbo].[dangnhap]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[dangnhap]
 @tentk varchar(50),
 @vaitro nvarchar(150),
 @matkhau varchar(50)
 as 
 begin
 if  @vaitro = 'ad'
	select * from [User] 
	where @tentk = tentk and @matkhau = matkhau;
else
	select * from Sinhvien
	where @tentk = masv and @matkhau = masv;
end;
GO
/****** Object:  StoredProcedure [dbo].[DeleteDangki]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create  PROCEDURE [dbo].[DeleteDangki]
    @masv VARCHAR(50),
    @ID VARCHAR(50)
AS
BEGIN
    -- Kiểm tra xem kết hợp giữa mã sinh viên và ID của lớp học có tồn tại trong Dangkihoc không
    IF EXISTS (SELECT 1 FROM Dangkihoc WHERE MaSV = @masv AND ID = @ID)
    BEGIN
        -- Xóa bản ghi khỏi bảng Dangkihoc
        DELETE FROM Dangkihoc WHERE MaSV = @masv AND ID = @ID;

        PRINT 'Xóa thành công.';
    END
    ELSE
    BEGIN
        PRINT 'Không tìm thấy kết hợp mã sinh viên và ID lớp học.';
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteGV]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteGV]
    @Magv VARCHAR(50)
AS
BEGIN
  
    IF EXISTS (SELECT 1 FROM Giaovien WHERE Magv = @MagV)
    BEGIN
        -- Xóa sinh viên
        DELETE FROM Giaovien WHERE magv = @Magv;
   End
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteLH]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteLH]
    @ID bigint
AS 
BEGIN 
	IF EXISTS (SELECT 1 FROM lop WHERE ID = @ID)
    BEGIN
    Delete from Lop
	where @ID = Lop.ID
	end;
	end
GO
/****** Object:  StoredProcedure [dbo].[DeleteMH]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[DeleteMH]
    @mahocphan VARCHAR(50)
AS 
BEGIN 
    Delete from monhoc 
	where mamh = @mahocphan

END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteNganh]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[DeleteNganh]
    @manganh VARCHAR(50)
AS 
BEGIN 
    Delete from nganh 
	where manganh = @manganh

END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteSinhVien]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSinhVien]
    @MaSV VARCHAR(50)
AS
BEGIN
    -- Kiểm tra xem sinh viên có tồn tại không
    IF EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = @MaSV)
    BEGIN
        -- Xóa sinh viên
        DELETE FROM SinhVien WHERE MaSV = @MaSV;
        PRINT 'Xóa sinh viên thành công';
    END
    ELSE
    BEGIN
        PRINT 'Sinh viên không tồn tại';
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertDangki]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertDangki]
    @masv VARCHAR(50),
    @MaMH VARCHAR(50),
    @ID int
AS
BEGIN
    -- Kiểm tra xem ID của lớp học có tồn tại trong bảng Lop không
    IF EXISTS (SELECT 1 FROM Lop WHERE ID = @ID)
    BEGIN
        -- Kiểm tra xem lớp học đã đạt đến giới hạn 60 sinh viên chưa
        IF (SELECT COUNT(*) FROM Dangkihoc WHERE ID = @ID) < 60
        BEGIN
            -- Kiểm tra xem sinh viên đã không đăng ký cho bất kỳ lớp học nào có cùng mã môn học chưa
            IF NOT EXISTS (SELECT 1 FROM Dangkihoc dh
                            WHERE dh.MaSV = @masv AND dh.ID <> @ID
                            AND EXISTS (SELECT 1 FROM Lop l WHERE l.ID = dh.ID AND l.MaMH = @MaMH))
            BEGIN
                -- Kiểm tra xem kết hợp giữa mã sinh viên và ID của lớp học có tồn tại trong Dangkihoc không
                IF NOT EXISTS (SELECT 1 FROM Dangkihoc WHERE MaSV = @masv AND ID = @ID)
                BEGIN
                    -- Chèn một bản ghi mới vào bảng Dangkihoc
                    INSERT INTO Dangkihoc (MaSV, ID)
                    VALUES (@masv, @ID);

                    -- Sử dụng RETURN để trả về kết quả
                    RETURN 1;-- Chèn thành công
                END
                ELSE
                BEGIN
                    -- Sử dụng RETURN để trả về kết quả
                    RETURN 2;-- Sinh viên đã đăng ký cho lớp học này trước đó
                END
            END
            ELSE
            BEGIN
                -- Sử dụng RETURN để trả về kết quả
                RETURN 3; -- Sinh viên đã đăng ký cho lớp học có cùng mã môn học trước đó
            END
        END
        ELSE
        BEGIN
            -- Sử dụng RETURN để trả về kết quả
            RETURN 4; -- Lớp học đã đủ 60 sinh viên
        END
    END
    ELSE
    BEGIN
        -- Sử dụng RETURN để trả về kết quả
        RETURN 5; -- Lớp học không tồn tại
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertGV]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertGV]
    @MaGV VARCHAR(50),
    @HoTen NVARCHAR(150),
    @GioiTinh BIT,
    @NgaySinh DATE,
    @DiaChi NVARCHAR(150),
    @Email VARCHAR(150),
    @SDT VARCHAR(30)
AS
BEGIN

		INSERT INTO Giaovien(MaGV, tengv, GioiTinh, NgaySinh, DiaChi, Email, SDT)
        VALUES (@MaGV, @HoTen, @GioiTinh, @NgaySinh, @DiaChi, @Email, @SDT);
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertLH]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertLH]
    @tennganh NVARCHAR(150),
    @tengv NVARCHAR(150),
    @tenmh NVARCHAR(150),
    @Phonghoc NVARCHAR(150),
    @thoigian VARCHAR(50),
	@hocki varchar(50),
    @trangthai BIT
AS
BEGIN
    DECLARE @manganh VARCHAR(50);
    DECLARE @magv VARCHAR(50);
    DECLARE @mamh VARCHAR(50);

    -- Kiểm tra xem Nganh có tồn tại không, nếu không thì không chèn
    SELECT @manganh = manganh FROM Nganh WHERE tennganh = @tennganh;
    IF @manganh IS NULL
    BEGIN
        PRINT 'Nganh không tồn tại.';
        RETURN;
    END;

    -- Kiểm tra xem GiaoVien có tồn tại không, nếu không thì không chèn
    SELECT @magv = magv FROM GiaoVien WHERE tengv = @tengv;
    IF @magv IS NULL
    BEGIN
        PRINT 'GiaoVien không tồn tại.';
        RETURN;
    END;

    -- Kiểm tra xem MonHoc có tồn tại không, nếu không thì không chèn
    SELECT @mamh = mamh FROM MonHoc WHERE tenmh = @tenmh;
    IF @mamh IS NULL
    BEGIN
        PRINT 'MonHoc không tồn tại.';
        RETURN;
    END;

    -- Thêm dữ liệu vào bảng Lop
    INSERT INTO Lop (manganh, magv, mamh, Phonghoc, thoigian, trangthai,hocki)
    VALUES (@manganh, @magv, @mamh, @Phonghoc, @thoigian, @trangthai,@hocki);

    PRINT 'Lớp học đã được thêm thành công.';
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertMH]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertMH]
    @mahocphan VARCHAR(50),
	@tenhocphan nvarchar(150),
	@sotinchi int 
AS 
BEGIN 
    Insert into monhoc(mamh,tenmh,sotin)
	values (@mahocphan,@tenhocphan,@sotinchi)

END;
GO
/****** Object:  StoredProcedure [dbo].[InsertNganh]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertNganh]
    @manganh VARCHAR(50),
    @tennganh NVARCHAR(150),
    @tenkhoa NVARCHAR(150)
AS
BEGIN
    BEGIN TRY
        -- Kiểm tra xem ngành có tồn tại hay không trước khi chèn
        IF NOT EXISTS (SELECT 1 FROM Nganh WHERE manganh = @manganh)
        BEGIN
            -- Chèn dữ liệu mới vào bảng "Nganh"
            INSERT INTO Nganh (manganh, tennganh, makhoa)
            VALUES (@manganh, @tennganh, (SELECT makhoa FROM Khoa WHERE tenkhoa = @tenkhoa));
        END
        ELSE
        BEGIN
            -- Ngành đã tồn tại, có thể thực hiện các thao tác khác tùy thuộc vào yêu cầu của bạn
            PRINT 'Nganh already exists';
        END
    END TRY
    BEGIN CATCH
        -- Xử lý lỗi nếu có
        -- Bạn có thể thực hiện các thao tác cần thiết, ví dụ như ghi log hoặc thông báo lỗi
        PRINT 'Error: ' + ERROR_MESSAGE();
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertSV]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertSV]
    @MaSV VARCHAR(50),
    @HoTen NVARCHAR(150),
    @GioiTinh BIT,
    @NgaySinh DATE,
    @Lop VARCHAR(50),
    @TenNganh NVARCHAR(150),
	@Tenkhoa NVARCHAR(150),
    @DiaChi NVARCHAR(150),
    @QueQuan NVARCHAR(150),
    @Email VARCHAR(150),
    @SDT VARCHAR(30)
AS
BEGIN
    DECLARE @MaNganh Varchar(50);
    DECLARE @CorrectKhoa NVARCHAR(150);

    -- Lấy MaNganh và TenKhoa từ bảng Nganh dựa trên TenNganh
    SELECT @MaNganh = MaNganh, @TenKhoa = k.TenKhoa
    FROM Nganh ng
    INNER JOIN Khoa k ON ng.MaKhoa = k.MaKhoa
    WHERE ng.tennganh = @TenNganh ;

    -- Kiểm tra nếu MaNganh không tồn tại
    IF @MaNganh IS NULL
    BEGIN
        PRINT 'Nganh không tồn tại';
        RETURN;
    END

    -- Kiểm tra xem ngành có thuộc khoa hay không
    IF @TenKhoa IS NULL
    BEGIN
        PRINT 'Nganh không thuộc khoa nào';
    END
    ELSE
    BEGIN
        -- Set tên khoa đúng để người dùng nhập lại
        SET @CorrectKhoa = @TenKhoa;
        PRINT 'Nhập sai khoa, ngành đó phải thuộc khoa ' + @CorrectKhoa;
		INSERT INTO SinhVien (MaSV, HoTen, GioiTinh, NgaySinh, Lop, MaNganh, DiaChi, QueQuan, Email, SDT)
        VALUES (@MaSV, @HoTen, @GioiTinh, @NgaySinh, @Lop, @MaNganh, @DiaChi, @QueQuan, @Email, @SDT);
        PRINT 'Thêm sinh viên thành công';
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[selectallClass]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
PROCEDURE [dbo].[selectallClass]
    @tukhoa NVARCHAR(50)
AS
BEGIN
    SELECT
        l.id AS 'ID',
        l.mamh AS N'Mã học phần',
        m.tenmh AS N'Tên học phần',
        m.sotin AS N'Số tín chỉ',
        gv.tengv AS N'Giáo viên phụ trách',
        l.thoigian AS N'Thời gian',
        l.Phonghoc AS N'Phòng học',
		m.hocki as N'Học kì',
        l.siso AS N'Sĩ số'
    FROM
        Lop l
    INNER JOIN giaovien gv ON l.magv = gv.magv
    INNER JOIN Monhoc m ON m.mamh = l.mamh
    INNER JOIN Nganh n ON n.manganh = l.manganh
    WHERE
		l.ID like '%' + LOWER(@tukhoa) + '%' or
        LOWER(l.mamh) LIKE '%' + LOWER(@tukhoa) + '%' OR
        LOWER(m.tenmh) LIKE '%' + LOWER(@tukhoa) + '%' OR
        LOWER(gv.tengv) LIKE '%' + LOWER(@tukhoa) + '%' OR
        LOWER(l.thoigian) LIKE '%' + LOWER(@tukhoa) + '%' OR
        LOWER(l.Phonghoc) LIKE '%' + LOWER(@tukhoa) + '%' OR
        LOWER(l.siso) LIKE '%' + LOWER(@tukhoa) + '%';
END;

GO
/****** Object:  StoredProcedure [dbo].[SelectallDSdangki]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectallDSdangki]
    @tukhoa NVARCHAR(200) = null,
    @tennganh NVARCHAR(150) ,
    @tenhocphan NVARCHAR(150) = null,
    @trangthai nvarchar(150) = null,
    @hocki VARCHAR(50) = null
AS
BEGIN
    SELECT 
        l.id AS 'ID',
        l.mamh AS N'Mã học phần',
        m.tenmh AS N'Tên học phần',
        m.sotin AS N'Số tín chỉ',
        gv.tengv AS N'Giáo viên phụ trách',
        l.thoigian AS N'Thời gian',
        l.phonghoc AS N'Phòng học',
		l.hocki AS N'Học kì',
        CASE WHEN l.trangthai = 1 THEN N'Đang mở' ELSE N'Đã kết thúc' END AS N'Trạng thái'
    FROM 
        lop l
        INNER JOIN Giaovien gv ON gv.magv = l.magv
        INNER JOIN Monhoc m ON m.mamh = l.mamh
        INNER JOIN Nganh n ON n.manganh = l.manganh
    WHERE 
        (
    (@tukhoa IS NULL OR 
     LOWER(m.mamh) LIKE '%' + LOWER(@tukhoa) + '%' OR
     LOWER(m.tenmh) LIKE '%' + LOWER(@tukhoa) + '%' OR
     m.sotin LIKE '%' + LOWER(@tukhoa) + '%' OR
     LOWER(l.hocki) LIKE '%' + LOWER(@tukhoa) + '%' OR
     l.ID LIKE '%' + LOWER(@tukhoa) + '%' OR
     gv.tengv LIKE '%' + LOWER(@tukhoa) + '%'
    )
    AND (@tennganh = n.tennganh)
    AND (@tenhocphan IS NULL OR m.tenmh = @tenhocphan OR @tenhocphan = '')
    AND (
        (@trangthai IS NULL)
        OR 
        (
            (l.trangthai = 1 AND @trangthai = N'Đang mở') OR
            (l.trangthai = 0 AND @trangthai = N'Đã kết thúc')
        )
        OR 
        (@trangthai = '')
    )
    AND (@hocki IS NULL OR l.hocki = @hocki OR @hocki = '')
)
END;
GO
/****** Object:  StoredProcedure [dbo].[SelectallGV]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SelectallGV]
    @tukhoa NVARCHAR(255) 
AS
BEGIN
    SELECT 
        gv.magv as N'Mã GV',
        gv.tengv as N'Họ tên',
        CASE WHEN gv.GioiTinh = 1 THEN 'Nam' ELSE N'Nữ' END AS N'Giới tính',
        Convert(varchar(10),gv.NgaySinh,103) as N'Ngày sinh',
        gv.DiaChi as N'Địa chỉ',
        gv.Email as N'Email',
        gv.SDT as N'Số điện thoại'
		from giaovien gv
    -- Thêm điều kiện tìm kiếm gần đúng nếu @tukhoa được cung cấp
    WHERE
        (@tukhoa IS NULL
        OR LOWER(gv.magv) LIKE '%' + LOWER(TRIM(@tukhoa)) + '%'
        OR LOWER(gv.tengv) LIKE '%' + LOWER(TRIM(@tukhoa)) + '%'
        OR LOWER(gv.DiaChi) LIKE '%' + LOWER(TRIM(@tukhoa)) + '%'
        OR LOWER(gv.Email) LIKE '%' + LOWER(TRIM(@tukhoa)) + '%'
        OR LOWER(gv.SDT) LIKE '%' + LOWER(TRIM(@tukhoa)) + '%'
        OR CONVERT(varchar(10), gv.NgaySinh, 103) LIKE '%' + @tukhoa + '%')
		order by gv.tengv;
END;
GO
/****** Object:  StoredProcedure [dbo].[SelectallKetquaDangki]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectallKetquaDangki]
    @manganh VARCHAR(50),
    @masv VARCHAR(50),
	@hocki varchar(50)=null,
	@trangthai nvarchar(150) = null,
	@tenhocphan nvarchar(50) = null
AS 
BEGIN
    SELECT 
        d.id AS 'ID',
        l.mamh AS N'Mã học phần',
        m.tenmh AS N'Tên học phần',
        m.sotin AS N'Số tín chỉ',
        gv.tengv AS N'Giáo viên phụ trách',
        l.manganh AS N'Ngành',
        l.thoigian AS N'Thời gian',
        l.phonghoc AS N'Phòng học',
        l.hocki AS N'Học kì',
        l.[trangthai] AS N'Trạng thái',
        SUM(m.sotin) OVER (PARTITION BY l.hocki) AS 'Tổng số tín chỉ'
    FROM 
        Dangkihoc d
        INNER JOIN Sinhvien sv ON sv.masv = d.masv
        INNER JOIN Lop l ON l.id = d.id
        INNER JOIN Giaovien gv ON gv.magv = l.magv
        INNER JOIN Monhoc m ON m.mamh = l.mamh
        INNER JOIN Nganh n ON n.manganh = l.manganh
    WHERE( (n.manganh = @manganh AND d.masv = @masv)
    AND (@tenhocphan IS NULL OR m.tenmh = @tenhocphan OR @tenhocphan = '')
    AND (@hocki IS NULL OR l.hocki = @hocki OR @hocki = '')
	AND (
        (@trangthai IS NULL)
        OR 
        (
            (l.trangthai = 1 AND @trangthai = N'Đang mở') OR
            (l.trangthai = 0 AND @trangthai = N'Đã kết thúc')
        )
        OR 
        (@trangthai = '')))
    GROUP BY 
        d.id,
        l.mamh,
        m.tenmh,
        m.sotin,
        gv.tengv,
        l.manganh,
        l.thoigian,
        l.phonghoc,
        l.hocki,
        l.[trangthai];
END
GO
/****** Object:  StoredProcedure [dbo].[SelectAllLH]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectAllLH]
    @tukhoa NVARCHAR(100) = NULL,
    @hocki VARCHAR(50) = NULL,
    @tennganh NVARCHAR(150) = NULL,
    @mahocphan VARCHAR(50) = NULL
AS 
BEGIN 
    SELECT 
        l.ID AS N'ID',
        l.mamh AS N'Mã học phần',
        m.tenmh AS N'Tên học phần',
        m.sotin AS N'Số tín chỉ',
        g.tengv AS N'Giáo viên giảng dạy',
        n.tennganh AS N'Ngành',
        l.thoigian AS N'Thời gian',
        l.phonghoc AS N'Phòng học',
        l.hocki AS N'Học kì',
        CASE WHEN l.trangthai = 1 THEN N'Đang mở' ELSE N'Đã kết thúc' END AS N'Trạng thái'
    FROM 
        lop l
        INNER JOIN nganh n ON l.manganh = n.manganh
        INNER JOIN monhoc m ON l.mamh = m.mamh
        INNER JOIN giaovien g ON l.magv = g.magv
    WHERE 
        (
            @tukhoa IS NULL 
            OR LOWER(CAST(l.ID AS NVARCHAR(100))) LIKE '%' + LOWER(@tukhoa) + '%'  -- Search by ID
            OR LOWER(CONVERT(NVARCHAR(100), l.thoigian, 120)) LIKE '%' + LOWER(@tukhoa) + '%'  -- Search by thoigian
            OR LOWER(l.phonghoc) LIKE '%' + LOWER(@tukhoa) + '%'  -- Search by phonghoc
            OR LOWER(m.tenmh) LIKE '%' + LOWER(@tukhoa) + '%'  -- Additional search by tenmh
            OR LOWER(g.tengv) LIKE '%' + LOWER(@tukhoa) + '%'  -- Additional search by tengv
            OR LOWER(n.tennganh) LIKE '%' + LOWER(@tukhoa) + '%'  -- Additional search by tennganh
        )
        AND (
            @hocki IS NULL 
            OR l.hocki = @hocki 
            OR @hocki = ''
        )  
        AND (
            @tennganh IS NULL 
            OR n.tennganh = @tennganh 
            OR @tennganh = ''
        ) 
        AND (
            @mahocphan IS NULL 
            OR m.mamh = @mahocphan 
            OR @mahocphan = ''
        )
END;
GO
/****** Object:  StoredProcedure [dbo].[SelectallMH]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SelectallMH]
@tukhoa NVARCHAR(100) = NULL,
    @mahocphan VARCHAR(50) = NULL
AS 
BEGIN 
    SELECT 
        m.mamh AS N'Mã học phần',
        m.tenmh AS N'Tên học phần',
        m.sotin AS N'Số tín chỉ'

       from monhoc m
	   WHERE 
        (@tukhoa IS NULL 
			 OR LOWER(m.mamh) LIKE '%' + LOWER(@tukhoa) + '%'
            OR LOWER(m.tenmh) LIKE '%' + LOWER(@tukhoa) + '%'  -- Additional search by tenmh
            OR m.sotin LIKE '%' + LOWER(@tukhoa) + '%' ) -- Additional search by tengv

        AND (@mahocphan IS NULL OR m.mamh = @mahocphan OR @mahocphan = '')
END;
GO
/****** Object:  StoredProcedure [dbo].[SelectallNH]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[SelectallNH]
@tukhoa NVARCHAR(100) = NULL,
    @tenkhoa NVARCHAR(50) = NULL
AS 
BEGIN 
    SELECT 
        n.manganh as N'Mã ngành',
		n.tennganh as N'Tên ngành',
		k.tenkhoa as N'Tên khoa'
       from Nganh n
	   inner join Khoa k on n.makhoa = k.makhoa
	   WHERE 
        (@tukhoa IS NULL 
			 OR LOWER(n.manganh) LIKE '%' + LOWER(@tukhoa) + '%'
            OR LOWER(n.tennganh) LIKE '%' + LOWER(@tukhoa) + '%'  -- Additional search by tenmh
            OR LOWER(k.tenkhoa) LIKE '%' + LOWER(@tukhoa) + '%' ) 
        AND ( @tenkhoa IS NULL OR @tenkhoa = k.tenkhoa OR @tenkhoa = '')
END;
GO
/****** Object:  StoredProcedure [dbo].[SelectallSV]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectallSV]
    @tukhoa NVARCHAR(255) 
AS
BEGIN
    SELECT 
        sv.masv as N'Mã SV',
        sv.HoTen as N'Họ tên',
        CASE WHEN sv.GioiTinh = 1 THEN 'Nam' ELSE N'Nữ' END AS N'Giới tính',
        Convert(varchar(10),sv.NgaySinh,103) as N'Ngày sinh',
        sv.Lop as N'Lớp',
        ng.tennganh AS N'Ngành',
        k.TenKhoa AS N'Khoa',
        sv.DiaChi as N'Địa chỉ',
        sv.QueQuan as N'Quê quán',
        sv.Email as N'Email',
        sv.SDT as N'Số điện thoại'
        
    FROM 
        SinhVien sv
    INNER JOIN Nganh ng ON sv.MaNganh = ng.MaNganh
    INNER JOIN Khoa k ON ng.MaKhoa = k.MaKhoa

    -- Thêm điều kiện tìm kiếm gần đúng nếu @tukhoa được cung cấp
    WHERE
        (@tukhoa IS NULL
        OR LOWER(sv.masv) LIKE '%' + LOWER(TRIM(@tukhoa)) + '%'
        OR LOWER(sv.HoTen) LIKE '%' + LOWER(TRIM(@tukhoa)) + '%'
        OR LOWER(ng.tennganh) LIKE '%' + LOWER(TRIM(@tukhoa)) + '%'
        OR LOWER(k.TenKhoa) LIKE '%' + LOWER(TRIM(@tukhoa)) + '%'
        OR LOWER(sv.DiaChi) LIKE '%' + LOWER(TRIM(@tukhoa)) + '%'
        OR LOWER(sv.QueQuan) LIKE '%' + LOWER(TRIM(@tukhoa)) + '%'
        OR LOWER(sv.Email) LIKE '%' + LOWER(TRIM(@tukhoa)) + '%'
        OR LOWER(sv.SDT) LIKE '%' + LOWER(TRIM(@tukhoa)) + '%'
        OR CONVERT(varchar(10), sv.NgaySinh, 103) LIKE '%' + @tukhoa + '%')
		order by sv.Hoten;
END;
GO
/****** Object:  StoredProcedure [dbo].[SelectGV]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CreaTE procedure [dbo].[SelectGV]
	@Magv varchar(50)
AS
	BEGIN
			SELECT 
					gv.magv as N'Mã gv',
					gv.tengv as N'Họ tên',
					 gv.GioiTinh as N'Giới tính',
				   Convert  (varchar(10),gv.NgaySinh,103) as N'Ngày sinh',
					gv.DiaChi as N'Địa chỉ',
					gv.Email as N'Email',
					gv.SDT as N'Số điện thoại'
				FROM Giaovien gv
	Where gv.magv = @Magv
	End;
GO
/****** Object:  StoredProcedure [dbo].[SelectLH]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectLH]
    @ID INT
AS 
BEGIN 
    IF EXISTS (SELECT 1 FROM Lop WHERE ID = @ID)
    BEGIN
        SELECT 
            l.ID AS N'ID',
            l.mamh AS N'Mã học phần',
            m.tenmh AS N'Tên học phần',
            m.sotin AS N'Số tín chỉ',
            g.tengv AS N'Giáo viên phụ trách',
            n.tennganh AS N'Ngành',
            l.thoigian AS N'Thời gian',
            l.phonghoc AS N'Phòng học',
            l.hocki AS N'Học kì',
            l.trangthai AS N'Trạng thái'
        FROM 
            lop l
            INNER JOIN nganh n ON l.manganh = n.manganh
            INNER JOIN monhoc m ON l.mamh = m.mamh
            INNER JOIN giaovien g ON l.magv = g.magv
        WHERE 
            l.ID = @ID;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[SelectMH]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SelectMH]
    @mahocphan VARCHAR(50) = NULL
AS 
BEGIN 
    SELECT 
        m.mamh AS N'Mã học phần',
        m.tenmh AS N'Tên học phần',
        m.sotin AS N'Số tín chỉ',
        m.hocki AS N'Học kì'
       from monhoc m
	   WHERE 
        @mahocphan = mamh
END;
GO
/****** Object:  StoredProcedure [dbo].[SelectNH]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[SelectNH]
    @manganh NVARCHAR(50) = NULL
AS 
BEGIN 
    SELECT 
        n.manganh as N'Mã ngành',
		n.tennganh as N'Tên ngành',
		k.tenkhoa as N'Tên khoa'
       from Nganh n
	   inner join Khoa k on n.makhoa = k.makhoa
	   WHERE @manganh = manganh
		
END;
GO
/****** Object:  StoredProcedure [dbo].[SelectSV]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SelectSV]
	@Masv varchar(50)
AS
	BEGIN
			SELECT 
					sv.masv as N'Mã SV',
					sv.HoTen as N'Họ tên',
					 sv.GioiTinh as N'Giới tính',
				   Convert  (varchar(10),sv.NgaySinh,103) as N'Ngày sinh',
					sv.Lop as N'Lớp',
					ng.tennganh AS N'Ngành',
					k.TenKhoa AS N'Khoa',
					sv.DiaChi as N'Địa chỉ',
					sv.QueQuan as N'Quê quán',
					sv.Email as N'Email',
					sv.SDT as N'Số điện thoại'
				FROM SinhVien sv
    INNER JOIN Nganh ng ON sv.MaNganh = ng.MaNganh
    INNER JOIN Khoa k ON ng.MaKhoa = k.MaKhoa
	Where sv.masv = @Masv
	End;
GO
/****** Object:  StoredProcedure [dbo].[UpdateGV]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[UpdateGV]
    @MaGV VARCHAR(50),
	@NewMaGV VARCHAR(50),
    @HoTen NVARCHAR(150),
    @GioiTinh BIT,
    @NgaySinh DATE,
    @DiaChi NVARCHAR(150),
    @Email VARCHAR(150),
    @SDT VARCHAR(30)
	AS
    BEGIN
        UPDATE Giaovien
        SET 
            Magv = @NewMagv, -- Mã sinh viên mới
            tengv = @HoTen,
            GioiTinh = @GioiTinh,
            NgaySinh = @NgaySinh,
            DiaChi = @DiaChi,
            Email = @Email,
            SDT = @SDT
        WHERE
            MagV = @MagV; -- Mã sinh viên cần cập nhật

END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateLH]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateLH]
    @ID varchar(50),
    @tennganh NVARCHAR(150),
    @tengv NVARCHAR(150),
    @tenmh NVARCHAR(150),
    @Phonghoc NVARCHAR(150),
    @thoigian varchar(50),
	@hocki varchar(50),
    @trangthai BIT
AS
BEGIN
    DECLARE @manganh VARCHAR(50);
    DECLARE @magv VARCHAR(50);
    DECLARE @mamh VARCHAR(50);

    -- Kiểm tra xem ID có tồn tại trong bảng Lop không
    IF NOT EXISTS (SELECT 1 FROM Lop WHERE ID = @ID)
    BEGIN
        -- Handle the case where ID does not exist
        PRINT 'ID không tồn tại trong bảng Lop.';
        RETURN;
    END;

    -- Kiểm tra xem Nganh có tồn tại không, nếu không thì không chèn
    SELECT @manganh = manganh FROM Nganh WHERE tennganh = @tennganh;
    IF @manganh IS NULL
    BEGIN
        -- Handle the case where Nganh does not exist
        PRINT 'Nganh không tồn tại.';
        RETURN;
    END;

    -- Kiểm tra xem GiaoVien có tồn tại không, nếu không thì không chèn
    SELECT @magv = magv FROM GiaoVien WHERE tengv = @tengv;
    IF @magv IS NULL
    BEGIN
        -- Handle the case where GiaoVien does not exist
        PRINT 'GiaoVien không tồn tại.';
        RETURN;
    END;

    -- Kiểm tra xem MonHoc có tồn tại không, nếu không thì không chèn
    SELECT @mamh = mamh FROM MonHoc WHERE tenmh = @tenmh;
    IF @mamh IS NULL
    BEGIN
        -- Handle the case where MonHoc does not exist
        PRINT 'MonHoc không tồn tại.';
        RETURN;
    END;

    -- Update dữ liệu trong bảng Lop
    UPDATE Lop
    SET
        manganh = @manganh,
        magv = @magv,
        mamh = @mamh,
        Phonghoc = @Phonghoc,
        thoigian = @thoigian,
        trangthai = @trangthai,
		hocki = @hocki
    WHERE
       ID = @ID;

    PRINT 'Dữ liệu trong bảng Lop đã được cập nhật thành công.';
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateMH]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UpdateMH]
    @mahocphan VARCHAR(50),
	@newMahp	VARCHAR(50),
	@tenhocphan nvarchar(150),
	@sotinchi int 
AS 
BEGIN 
    Update Monhoc
	set
	mamh = @newMahp,
	tenmh =@tenhocphan,
	sotin = @sotinchi

where @mahocphan = mamh
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateNganh]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateNganh]
    @manganh VARCHAR(50),
    @newManganh VARCHAR(50),
    @tennganh NVARCHAR(150),
    @tenkhoa NVARCHAR(150)
AS
BEGIN
    BEGIN TRY
        -- Chuyển mã ngành trong bảng "Sinhvien" thành NULL nếu giống với giá trị mới
        UPDATE Sinhvien
        SET manganh = NULL
        WHERE manganh = @Manganh; 

        -- Cập nhật bảng "Nganh"
        UPDATE Nganh
        SET
            manganh = @newManganh,
            tennganh = @tennganh,
			makhoa = (SELECT makhoa FROM Khoa WHERE tenkhoa = @tenkhoa)
        WHERE manganh = @manganh;
    END TRY
    BEGIN CATCH
        -- Xử lý lỗi nếu có
        -- Bạn có thể thực hiện các thao tác cần thiết, ví dụ như ghi log hoặc thông báo lỗi
        PRINT 'Error: ' + ERROR_MESSAGE();
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateSV]    Script Date: 10/01/2024 2:36:27 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSV]
    @MaSV VARCHAR(50),
    @NewMaSV VARCHAR(50), -- Mã sinh viên mới
    @HoTen NVARCHAR(150),
    @GioiTinh BIT,
    @NgaySinh DATE,
    @Lop VARCHAR(50),
    @TenNganh NVARCHAR(150),
    @TenKhoa NVARCHAR(150),
    @DiaChi NVARCHAR(150),
    @QueQuan NVARCHAR(150),
    @Email VARCHAR(150),
    @SDT VARCHAR(30)
AS
BEGIN
    DECLARE @MaNganh VARCHAR(50);
    DECLARE @KhoaID VARCHAR(50);

    -- Lấy MaNganh và ID của Khoa từ bảng Nganh dựa trên TenNganh và TenKhoa
    SELECT @MaNganh = ng.MaNganh, @KhoaID = k.MaKhoa
    FROM Nganh ng
    INNER JOIN Khoa k ON ng.MaKhoa = k.MaKhoa
    WHERE ng.tennganh = @TenNganh AND k.TenKhoa = @TenKhoa;

    -- Kiểm tra nếu MaNganh hoặc KhoaID không tồn tại
    IF @MaNganh IS NULL OR @KhoaID IS NULL
    BEGIN
        PRINT 'Nganh hoặc Khoa không tồn tại';
        RETURN;
    END

    BEGIN TRY
        -- Cập nhật thông tin sinh viên bao gồm cả MaNganh và MaSV
        UPDATE SinhVien
        SET 
            MaSV = @NewMaSV, -- Mã sinh viên mới
            HoTen = @HoTen,
            GioiTinh = @GioiTinh,
            NgaySinh = @NgaySinh,
            Lop = @Lop,
            MaNganh = @MaNganh,
            DiaChi = @DiaChi,
            QueQuan = @QueQuan,
            Email = @Email,
            SDT = @SDT
        WHERE
            MaSV = @MaSV; -- Mã sinh viên cần cập nhật

        PRINT 'Cập nhật thông tin sinh viên thành công';
    END TRY
    BEGIN CATCH
        PRINT 'Lỗi khi cập nhật thông tin sinh viên: ' + ERROR_MESSAGE();
    END CATCH;
END;
GO
USE [master]
GO
ALTER DATABASE [Dangkihoc] SET  READ_WRITE 
GO
