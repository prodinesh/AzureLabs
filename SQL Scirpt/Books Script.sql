USE Experts

BEGIN

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[BOOKS]') AND type in (N'U'))

BEGIN

CREATE TABLE BOOKS
(
Book_Id int,
Internal_Id UNIQUEIDENTIFIER,
Book_Name varchar(500),
Author varchar(200),
Genre varchar(50),
Fiction bit,
Price decimal,
Published_On Datetime
)

END

INSERT INTO BOOKS
VALUES
(1, 
NEWID(),
'Beyond Good & Evil',
'Friedrich Nietzsche',
'Philisophy',
0,
210.00,
GETDATE()-1000
)

INSERT INTO BOOKS
VALUES
(2,
NEWID(),
'The Lord of the Rings',
'J. R. R. Tolkien',
'Fantasy',
1,
700.00,
GETDATE()-400
)

INSERT INTO BOOKS
VALUES
(3,
NEWID(),
'Anna Karenina',
'Leo Tolstoy',
'Novel',
1,
250.00,
GETDATE()-40
)

INSERT INTO BOOKS
VALUES
(4,
NEWID(),
'Hamlet',
'William Shakespeare',
'Tragedy',
1,
140.00,
GETDATE()-50
)

INSERT INTO BOOKS
VALUES
(5,
NEWID(),
'Goals',
'Brian Tracy',
'Self Help',
0,
410.70,
GETDATE()-110
)

INSERT INTO BOOKS
VALUES
(6,
NEWID(),
'The Brothers Karamazov',
'Fyodor Dostoyevsky',
'Crime',
1,
190.50,
GETDATE()-45
)

INSERT INTO BOOKS
VALUES
(
7,
NEWID(),
'The One-Straw Revolution',
'Masanobu Fukuoka',
'Farming',
0,
360,
GETDATE()-120)

END

GO
