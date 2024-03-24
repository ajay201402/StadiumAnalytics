USE [Sensor]
GO

/****** Object:  Table [dbo].[SensorData]    Script Date: 25-03-2024 01:12:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SensorData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GateName] [nchar](10) NOT NULL,
	[EventType] [nchar](10) NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[NumOfPeople] [int] NOT NULL,
 CONSTRAINT [PK_SensorData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

