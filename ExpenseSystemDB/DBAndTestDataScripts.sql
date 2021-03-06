USE [ExpenseSystem]
GO
/****** Object:  Table [dbo].[User]    Script Date: 03/01/2011 05:58:57 ******/
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
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [FirstName], [MiddleName], [LastName], [Email], [Phone], [Login], [Password]) VALUES (1, N'Sergey', N'Sergeevich', N'Kliuev', N'sergey@ego-cms.com', N'+380958520389', N'sergkl', N'333577')
INSERT [dbo].[User] ([Id], [FirstName], [MiddleName], [LastName], [Email], [Phone], [Login], [Password]) VALUES (22, N's', NULL, N's', N'sergkl@bk.ru', NULL, N's', N's')
INSERT [dbo].[User] ([Id], [FirstName], [MiddleName], [LastName], [Email], [Phone], [Login], [Password]) VALUES (23, N'Sergey', NULL, N'Sergey', N'sergkl@bk.ru', NULL, N'Sergey', N'Sergey')
INSERT [dbo].[User] ([Id], [FirstName], [MiddleName], [LastName], [Email], [Phone], [Login], [Password]) VALUES (24, N'Sergey', N'Sergeevich', N'Kliuev', N'sergkl@bk.ru', N'0958520389', N'serg', N'qwer')
INSERT [dbo].[User] ([Id], [FirstName], [MiddleName], [LastName], [Email], [Phone], [Login], [Password]) VALUES (25, N'test', N'test', N'test', N'test@bk.ru', NULL, N'test', N'test')
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[TagTest]    Script Date: 03/01/2011 05:58:57 ******/
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
SET IDENTITY_INSERT [dbo].[TagTest] ON
INSERT [dbo].[TagTest] ([Id], [Name], [ParentId], [UserId]) VALUES (1, N'ExprensesTag', NULL, 1)
INSERT [dbo].[TagTest] ([Id], [Name], [ParentId], [UserId]) VALUES (2, N'Food', 1, 1)
INSERT [dbo].[TagTest] ([Id], [Name], [ParentId], [UserId]) VALUES (3, N'Rest', 1, 1)
INSERT [dbo].[TagTest] ([Id], [Name], [ParentId], [UserId]) VALUES (4, N'Drinks', 2, 1)
INSERT [dbo].[TagTest] ([Id], [Name], [ParentId], [UserId]) VALUES (5, N'Garnish', 2, 1)
INSERT [dbo].[TagTest] ([Id], [Name], [ParentId], [UserId]) VALUES (6, N'Parties', 3, 1)
INSERT [dbo].[TagTest] ([Id], [Name], [ParentId], [UserId]) VALUES (7, N'Nescafe Series', 4, 1)
INSERT [dbo].[TagTest] ([Id], [Name], [ParentId], [UserId]) VALUES (8, N'Nestle Series', 4, 1)
INSERT [dbo].[TagTest] ([Id], [Name], [ParentId], [UserId]) VALUES (9, N'Lipton Tea', 4, 1)
INSERT [dbo].[TagTest] ([Id], [Name], [ParentId], [UserId]) VALUES (10, N'Sport', 1, 1)
INSERT [dbo].[TagTest] ([Id], [Name], [ParentId], [UserId]) VALUES (11, N'Fun', 1, 1)
INSERT [dbo].[TagTest] ([Id], [Name], [ParentId], [UserId]) VALUES (12, N'Circus', 11, 1)
SET IDENTITY_INSERT [dbo].[TagTest] OFF
/****** Object:  Table [dbo].[Tag]    Script Date: 03/01/2011 05:58:57 ******/
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
SET IDENTITY_INSERT [dbo].[Tag] ON
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (1, N'ExpensesTag', NULL, 1)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (3, N'Rest', 1, 1)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (6, N'Parties', 3, 1)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (10, N'Sport', 1, 1)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (11, N'Fun', 1, 1)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (12, N'Circus', 11, 1)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (17, N'Parties', 11, 1)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (18, N'Text', 3, 1)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (19, N'New node', 3, 1)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (21, N'ExpensesTag', NULL, 22)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (25, N'ExpensesTag', NULL, 23)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (26, N'New node', 25, 23)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (27, N'New node', 26, 23)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (28, N'New node', 26, 23)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (39, N'ExpensesTag', NULL, 24)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (40, N'New node', 39, 24)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (46, N'Expenses', 21, 22)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (47, N'Sport', 46, 22)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (48, N'Party', 46, 22)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (49, N'Health', 46, 22)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (50, N'Food', 46, 22)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (51, N'Drinks', 50, 22)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (52, N'Sweets', 50, 22)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (53, N'ExpensesTag', NULL, 25)
INSERT [dbo].[Tag] ([Id], [Name], [ParentId], [UserId]) VALUES (54, N'Tag', 53, 25)
SET IDENTITY_INSERT [dbo].[Tag] OFF
/****** Object:  Table [dbo].[ExpenseRecord]    Script Date: 03/01/2011 05:58:57 ******/
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
SET IDENTITY_INSERT [dbo].[ExpenseRecord] ON
INSERT [dbo].[ExpenseRecord] ([Id], [Description], [Price], [TagId], [DateStamp]) VALUES (4, N'Spent amount 3', 15.0000, 3, CAST(0xF3330B00 AS Date))
INSERT [dbo].[ExpenseRecord] ([Id], [Description], [Price], [TagId], [DateStamp]) VALUES (10, N'Gym', 30.0000, 47, CAST(0xF2330B00 AS Date))
INSERT [dbo].[ExpenseRecord] ([Id], [Description], [Price], [TagId], [DateStamp]) VALUES (11, N'Potato', 5.0000, 50, CAST(0xF3330B00 AS Date))
INSERT [dbo].[ExpenseRecord] ([Id], [Description], [Price], [TagId], [DateStamp]) VALUES (12, N'Coca Cola', 4.0000, 51, CAST(0xF2330B00 AS Date))
INSERT [dbo].[ExpenseRecord] ([Id], [Description], [Price], [TagId], [DateStamp]) VALUES (13, N'Fanta', 3.0000, 51, CAST(0xF4330B00 AS Date))
INSERT [dbo].[ExpenseRecord] ([Id], [Description], [Price], [TagId], [DateStamp]) VALUES (14, N'Sweetmeats', 6.0000, 52, CAST(0xF3330B00 AS Date))
INSERT [dbo].[ExpenseRecord] ([Id], [Description], [Price], [TagId], [DateStamp]) VALUES (15, N'Ice cream', 5.5000, 52, CAST(0xF4330B00 AS Date))
INSERT [dbo].[ExpenseRecord] ([Id], [Description], [Price], [TagId], [DateStamp]) VALUES (16, N'Test', 0.5000, 54, CAST(0xF4330B00 AS Date))
SET IDENTITY_INSERT [dbo].[ExpenseRecord] OFF
/****** Object:  ForeignKey [FK_Tag_Tag]    Script Date: 03/01/2011 05:58:57 ******/
ALTER TABLE [dbo].[Tag]  WITH CHECK ADD  CONSTRAINT [FK_Tag_Tag] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Tag] ([Id])
GO
ALTER TABLE [dbo].[Tag] CHECK CONSTRAINT [FK_Tag_Tag]
GO
/****** Object:  ForeignKey [FK_Tag_User]    Script Date: 03/01/2011 05:58:57 ******/
ALTER TABLE [dbo].[Tag]  WITH CHECK ADD  CONSTRAINT [FK_Tag_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tag] CHECK CONSTRAINT [FK_Tag_User]
GO
/****** Object:  ForeignKey [FK_ExpenseRecord_Tag]    Script Date: 03/01/2011 05:58:57 ******/
ALTER TABLE [dbo].[ExpenseRecord]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseRecord_Tag] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tag] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ExpenseRecord] CHECK CONSTRAINT [FK_ExpenseRecord_Tag]
GO
