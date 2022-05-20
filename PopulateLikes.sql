USE [TM_DOTNET]
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='xmlLike' AND xtype='U')
	CREATE TABLE xmlLike(
		intcol int IDENTITY PRIMARY KEY,
		xmlcol xml
	);
GO

INSERT INTO xmlLike(xmlcol)
SELECT * FROM OPENROWSET(
	BULK 'D:\Projects\Trademarkia\Dotnet\dotnetWebAPI\Assets\likes.xml',
	SINGLE_CLOB
) AS x;
GO

DECLARE @xml as xml, @hDoc as int, @sql VARCHAR (MAX)

SELECT @xml=xmlcol from xmlLike

EXEC sp_xml_preparedocument @hDoc OUTPUT, @xml


INSERT into likes
SELECT id, liker, likee
from OPENXML(@hDoc, 'root/row')
with
(
	id [int] 'id',
	liker [int] 'who_likes',
	likee [int] 'who_is_liked'
)

EXEC sp_xml_removedocument @hDoc
GO


