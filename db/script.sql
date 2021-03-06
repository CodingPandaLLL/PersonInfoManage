USE [master]
GO
/****** Object:  Database [student]    Script Date: 2019/9/16 16:28:26 ******/
CREATE DATABASE [student]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'student', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\student.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'student_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\student_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [student] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [student].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [student] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [student] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [student] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [student] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [student] SET ARITHABORT OFF 
GO
ALTER DATABASE [student] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [student] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [student] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [student] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [student] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [student] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [student] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [student] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [student] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [student] SET  DISABLE_BROKER 
GO
ALTER DATABASE [student] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [student] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [student] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [student] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [student] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [student] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [student] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [student] SET RECOVERY FULL 
GO
ALTER DATABASE [student] SET  MULTI_USER 
GO
ALTER DATABASE [student] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [student] SET DB_CHAINING OFF 
GO
ALTER DATABASE [student] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [student] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [student] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'student', N'ON'
GO
ALTER DATABASE [student] SET QUERY_STORE = OFF
GO
USE [student]
GO
/****** Object:  User [admin]    Script Date: 2019/9/16 16:28:27 ******/
CREATE USER [admin] FOR LOGIN [admin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[person]    Script Date: 2019/9/16 16:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[person](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](50) NULL,
	[SEX] [nvarchar](50) NULL,
	[HIGH] [nvarchar](50) NULL,
	[WEIGHT] [nvarchar](50) NULL,
	[WORKOR] [nvarchar](50) NULL,
	[IMAGE] [nvarchar](255) NULL,
	[PHONE] [nvarchar](50) NULL,
	[EMAIL] [nvarchar](50) NULL,
	[ADDRESS] [nvarchar](50) NULL,
 CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[register]    Script Date: 2019/9/16 16:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[register](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[P_ID] [int] NULL,
	[NAME] [nvarchar](50) NULL,
	[VALUE] [nvarchar](255) NULL,
 CONSTRAINT [PK_register] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[syspic]    Script Date: 2019/9/16 16:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[syspic](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IMG_TYPE] [nvarchar](50) NULL,
	[IMG_PATH] [nvarchar](255) NULL,
	[IMG_NAME] [nvarchar](255) NULL,
 CONSTRAINT [PK_syspic] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 2019/9/16 16:28:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](50) NULL,
	[PASSWORD] [nvarchar](50) NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [student] SET  READ_WRITE 
GO
