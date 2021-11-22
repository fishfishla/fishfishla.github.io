CREATE DATABASE [dbCooking];

USE [dbCooking];

CREATE TABLE [t會員](
    [f會員Id] int PRIMARY KEY identity(1,1),
	[f會員信箱] nvarchar(50) NOT NULL,
	[f會員密碼] nvarchar(MAX) NOT NULL,
	[f會員姓名] nvarchar(50),
	[f會員電話] nvarchar(50),
	[f性別] int,
	[f自我介紹] nvarchar(MAX),
	[f會員照片] nvarchar(MAX),
	[f權限] int,
	[f會員建立日期] smalldatetime not null DEFAULT sysdatetime()
);

CREATE TABLE [t聚會](
    [f聚會Id] int PRIMARY KEY IDENTITY(1,1),
	[f主辦人] int NOT NULL FOREIGN KEY REFERENCES [t會員](f會員Id), 
	[f聚會名稱] nvarchar(50) NOT NULL,
	[f聚會內容] nvarchar(50),
	[f聚會照片] nvarchar(MAX),
	[f聚會日期] smalldatetime NOT NULL,
	[f聚會開始時間] smalldatetime NOT NULL,
	[f聚會結束時間] smalldatetime,
	[f名額] int,
	[f聚會軟體] nvarchar(50),
	[f聚會軟體URL] nvarchar(MAX),
	[f聚會關鍵字] nvarchar(MAX),
	[f聚會通訊軟體] nvarchar(50),
	[f聚會通訊軟體帳號] nvarchar(50),
        [f主辦人影片URL] nvarchar(MAX),
	[f聚會狀態] int,
	[f聚會垃圾桶] bit,
	[f聚會建立日期] smalldatetime NOT NULL DEFAULT sysdatetime() 
);

CREATE TABLE [t建議食材](
    [f建議食材Id] int PRIMARY KEY IDENTITY(1,1),
	[f聚會Id] int NOT NULL FOREIGN KEY REFERENCES [t聚會](f聚會Id),
	[f食材名稱] nvarchar(50),
	[f數量] int,
	[f單位] nvarchar(50)
);

CREATE TABLE [t參加者](
    [f參加者Id] int PRIMARY KEY IDENTITY(1,1),
	[f會員Id] int NOT NULL FOREIGN KEY REFERENCES [t會員](f會員Id),
	[f聚會Id] int NOT NULL FOREIGN KEY REFERENCES [t聚會](f聚會Id),
	[f審核狀態] bit NOT NULL DEFAULT 0,
	[f報名] bit NOT NULL DEFAULT 0,
	[f參加者建立日期] smalldatetime NOT NULL DEFAULT sysdatetime()
);

CREATE TABLE [t評價](
    [f評價Id] int PRIMARY KEY IDENTITY(1,1),
	[f聚會Id] int NOT NULL FOREIGN KEY REFERENCES [t聚會](f聚會Id),
	[f參加者Id] int NOT NULL FOREIGN KEY REFERENCES [t參加者](f參加者Id),
	[f留言] nvarchar(MAX),
	[f照片] nvarchar(MAX),
	[f評價建立日期] nvarchar(50)
);

CREATE TABLE [t檢舉](
    [f檢舉Id] int PRIMARY KEY IDENTITY(1,1),
	[f檢舉人Id] int NOT NULL FOREIGN KEY REFERENCES [t會員](f會員Id),
	[f被檢舉的聚會Id] int NOT NULL FOREIGN KEY REFERENCES [t聚會](f聚會Id),
	[f檢舉原因] nvarchar(50),
	[f檢舉建立日期] smalldatetime
);

CREATE TABLE [t留言](
    [f留言Id] int PRIMARY KEY IDENTITY(1,1),
	[f留言人Id] int NOT NULL FOREIGN KEY REFERENCES [t會員](f會員Id),
	[f留言的聚會Id] int NOT NULL FOREIGN KEY REFERENCES [t聚會](f聚會Id),
	[f留言文字] nvarchar(50),
	[f留言建立日期] smalldatetime
);