USE [master]
GO
/****** Object:  Database [ExpenseSystem]    Script Date: 03/15/2011 00:54:23 ******/
CREATE DATABASE [ExpenseSystem] ON  PRIMARY 
( NAME = N'ExpenseSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\ExpenseSystem.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ExpenseSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\ExpenseSystem_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ExpenseSystem] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ExpenseSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ExpenseSystem] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [ExpenseSystem] SET ANSI_NULLS OFF
GO
ALTER DATABASE [ExpenseSystem] SET ANSI_PADDING OFF
GO
ALTER DATABASE [ExpenseSystem] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [ExpenseSystem] SET ARITHABORT OFF
GO
ALTER DATABASE [ExpenseSystem] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [ExpenseSystem] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [ExpenseSystem] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [ExpenseSystem] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [ExpenseSystem] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [ExpenseSystem] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [ExpenseSystem] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [ExpenseSystem] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [ExpenseSystem] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [ExpenseSystem] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [ExpenseSystem] SET  DISABLE_BROKER
GO
ALTER DATABASE [ExpenseSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [ExpenseSystem] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [ExpenseSystem] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [ExpenseSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [ExpenseSystem] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [ExpenseSystem] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [ExpenseSystem] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [ExpenseSystem] SET  READ_WRITE
GO
ALTER DATABASE [ExpenseSystem] SET RECOVERY FULL
GO
ALTER DATABASE [ExpenseSystem] SET  MULTI_USER
GO
ALTER DATABASE [ExpenseSystem] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [ExpenseSystem] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'ExpenseSystem', N'ON'
GO
USE [ExpenseSystem]
GO
/****** Object:  Table [dbo].[User]    Script Date: 03/15/2011 00:54:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TagTest]    Script Date: 03/15/2011 00:54:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TagTest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ParentId] [int] NULL,
	[UserId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 03/15/2011 00:54:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ParentId] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExpenseRecord]    Script Date: 03/15/2011 00:54:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExpenseRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Price] [money] NOT NULL,
	[TagId] [int] NOT NULL,
	[DateStamp] [date] NOT NULL,
 CONSTRAINT [PK_ExpenseRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Tag_Tag]    Script Date: 03/15/2011 00:54:26 ******/
ALTER TABLE [dbo].[Tag]  WITH CHECK ADD  CONSTRAINT [FK_Tag_Tag] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Tag] ([Id])
GO
ALTER TABLE [dbo].[Tag] CHECK CONSTRAINT [FK_Tag_Tag]
GO
/****** Object:  ForeignKey [FK_Tag_User]    Script Date: 03/15/2011 00:54:26 ******/
ALTER TABLE [dbo].[Tag]  WITH CHECK ADD  CONSTRAINT [FK_Tag_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tag] CHECK CONSTRAINT [FK_Tag_User]
GO
/****** Object:  ForeignKey [FK_ExpenseRecord_Tag]    Script Date: 03/15/2011 00:54:26 ******/
ALTER TABLE [dbo].[ExpenseRecord]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseRecord_Tag] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tag] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ExpenseRecord] CHECK CONSTRAINT [FK_ExpenseRecord_Tag]
GO
