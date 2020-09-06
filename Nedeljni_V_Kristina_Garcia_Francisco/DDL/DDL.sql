IF OBJECT_ID('tblPostLikes', 'U') IS NOT NULL DROP TABLE tblPostLikes;
IF OBJECT_ID('tblPost', 'U') IS NOT NULL DROP TABLE tblPost;
IF OBJECT_ID('tblRelationship', 'U') IS NOT NULL DROP TABLE tblRelationship;
IF OBJECT_ID('tblUser', 'U') IS NOT NULL DROP TABLE tblUser;

-- Checks if the database already exists.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BetweenUsDB')
CREATE DATABASE BetweenUsDB;
GO

USE BetweenUsDB
CREATE TABLE tblUser(
	UserID			INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	FirstName		VARCHAR (40)					NOT NULL,
	LastName		VARCHAR (40)					NOT NULL,
	Gender			VARCHAR	(10)					NOT NULL,
	DateOfBirth		DATE							NOT NULL,
	Email			VARCHAR (50)					NOT NULL,
	UserLocation	VARCHAR (50),                    
	Username		VARCHAR (40) UNIQUE				NOT NULL,
	UserPassword	CHAR (1000)						NOT NULL,
);

CREATE TABLE tblRelationship(
	RelationshipID		INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	RelationshipStatus	VARCHAR (10)					NOT NULL,
	User1ID INT FOREIGN KEY REFERENCES tblUser(UserID)	NOT NULL,
	User2ID INT FOREIGN KEY REFERENCES tblUser(UserID)	NOT NULL,
);

 CREATE TABLE tblPost (
    PostID			INT IDENTITY(1,1) PRIMARY KEY		NOT NULL,
	DateOfPost		DATETIME								NOT NULL,
	PostText		VARCHAR(100)						NOT NULL,
	NumberOfLikes	INT									NOT NULL,
	UserID INT FOREIGN KEY REFERENCES tblUser(UserID)	NOT NULL,
);

 CREATE TABLE tblPostLikes (
    PostLikeID			INT IDENTITY(1,1) PRIMARY KEY	NOT NULL,
	PostID INT FOREIGN KEY REFERENCES tblPost(PostID)	NOT NULL,
	UserID INT FOREIGN KEY REFERENCES tblUser(UserID)	NOT NULL,
);