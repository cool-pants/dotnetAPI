USE [TM_DOTNET]
GO

DECLARE @xml as xml, @hDoc as int, @sql VARCHAR (MAX)

SELECT @xml=xmlcol from xmlUser

EXEC sp_xml_preparedocument @hDoc OUTPUT, @xml


INSERT into users
SELECT id, name, location, gender, email
from OPENXML(@hDoc, 'root/row')
with
(
	id [int] 'id',
	name [varchar](100) 'name',
	location [float] 'location',
	gender [varchar](100) 'gender',
	email [varchar](100) 'email'
)

EXEC sp_xml_removedocument @hDoc
GO