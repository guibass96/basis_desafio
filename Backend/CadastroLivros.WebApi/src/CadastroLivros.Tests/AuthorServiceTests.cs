using System.Linq.Expressions;
using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using Moq;

public class AuthorServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AuthorService _authorService;

    public AuthorServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _authorService = new AuthorService(_unitOfWorkMock.Object); 
    }

    [Fact]
    public async Task AddAuthorAsync_ShouldReturnResult_WhenSuccessful()
    {

        var author = new AuthorEntity { Name = "Author Test" };
        _unitOfWorkMock.Setup(x => x.Author.Add(author));
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).ReturnsAsync(1);

    
        var result = await _authorService.AddAuthorAsync(author); 

    
        Assert.Equal(1, result);
        _unitOfWorkMock.Verify(x => x.Author.Add(author), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAuthorAsync_ShouldReturnTrue_WhenAuthorExists()
    {
 
        var existingAuthor = new AuthorEntity { AuthorId = 1, Name = "Old Name" };
        var updateAuthor = new AuthorEntity { AuthorId = 1, Name = "New Name" };

        _unitOfWorkMock.Setup(x => x.Author.GetByIdAsync(It.IsAny<Expression<Func<AuthorEntity, bool>>>()))
                       .ReturnsAsync(existingAuthor);
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).ReturnsAsync(1);

    
        var result = await _authorService.UpdateAuthorAsync(updateAuthor); 

     
        Assert.True(result);
        _unitOfWorkMock.Verify(x => x.Author.Update(existingAuthor), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAuthorAsync_ShouldReturnTrue_WhenAuthorExists()
    {
     
        var existingAuthor = new AuthorEntity { AuthorId = 1,Name = "test" };
        _unitOfWorkMock.Setup(x => x.Author.GetByIdAsync(It.IsAny<Expression<Func<AuthorEntity, bool>>>()))
                       .ReturnsAsync(existingAuthor);

        _unitOfWorkMock.Setup(x => x.CompleteAsync()).ReturnsAsync(1);

    
        var result = await _authorService.DeleteAuthorAsync(existingAuthor); 

       
        Assert.True(result);
        _unitOfWorkMock.Verify(x => x.Author.Remove(existingAuthor), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
    }


}
