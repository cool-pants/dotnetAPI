CREATE DATABASE TM_DOTNET
GO

USE TM_DOTNET
GO

CREATE TABLE IF NOT EXISTS users(
    id int primary key,
    name varchar(100),
    location float,
    gender varchar(100),
    email varchar(100)
);

CREATE TABLE IF NOT EXISTS likes(
    id int primary key,
    liker int,
    likee int
);
GO