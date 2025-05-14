
USE BookManagement;
GO

IF OBJECT_ID('vw_AuthorBooks', 'V') IS NOT NULL
    DROP VIEW vw_AuthorBooks;
GO


CREATE VIEW vw_AuthorBooks AS
SELECT 
    a.AuthorId,
    a.Name AS AuthorName,
    b.BookId,
    b.Title AS BookTitle,
    STRING_AGG(s.Description, ', ') AS Subjects
FROM 
    Author a
INNER JOIN 
    Book_Author ba ON a.AuthorId = ba.AuthorId
INNER JOIN 
    Book b ON ba.BookId = b.BookId
LEFT JOIN 
    Book_Subject bs ON b.BookId = bs.BookId
LEFT JOIN 
    Subject s on bs.SubjectId = s.SubjectId
GROUP BY 
    a.AuthorId, a.Name, b.BookId, b.Title;
GO

