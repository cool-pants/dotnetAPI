USE [TM_DOTNET]
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='xmlUser' AND xtype='U')
	CREATE TABLE xmlUser(
		intcol int IDENTITY PRIMARY KEY,
		xmlcol xml
	);
GO

INSERT INTO xmlUser(xmlcol)
SELECT * FROM OPENROWSET(
	BULK 'D:\Projects\Trademarkia\Dotnet\dotnetWebAPI\Assets\users.xml',
	SINGLE_CLOB
) AS x;
GO

SELECT * FROM xmlUser;
GO