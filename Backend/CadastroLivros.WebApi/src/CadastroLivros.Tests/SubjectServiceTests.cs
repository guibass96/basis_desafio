using CadastroLivros.Application.Services;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Interfaces;
using Moq;
using System.Linq.Expressions;


public class SubjectServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly SubjectService _subjectService;

    public SubjectServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _subjectService = new SubjectService(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task AddSubjectAsync_ShouldReturnResult_WhenSuccessful()
    {
        
        var subject = new SubjectEntity { Description = "Test Subject" };
        _unitOfWorkMock.Setup(x => x.Subject.Add(subject));
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).ReturnsAsync(1);

      
        var result = await _subjectService.AddSubjectAsync(subject);

        
        Assert.Equal(1, result);
        _unitOfWorkMock.Verify(x => x.Subject.Add(subject), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateSubjectAsync_ShouldReturnTrue_WhenSubjectExists()
    {
        
        var existingSubject = new SubjectEntity { SubjectId = 1, Description = "Old Description" };
        var updateSubject = new SubjectEntity { SubjectId = 1, Description = "New Description" };

        _unitOfWorkMock.Setup(x => x.Subject.GetByIdAsync(It.IsAny<Expression<Func<SubjectEntity, bool>>>()))
                       .ReturnsAsync(existingSubject);
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).ReturnsAsync(1);

       
        var result = await _subjectService.UpdateSubjectAsync(updateSubject);

       
        Assert.True(result);
        Assert.Equal("New Description", existingSubject.Description);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAuthorAsync_ShouldReturnTrue_WhenSubjectExists()
    {
        
        var existingSubject = new SubjectEntity { SubjectId = 1,Description = "teste" };
        _unitOfWorkMock.Setup(x => x.Subject.GetByIdAsync(It.IsAny<Expression<Func<SubjectEntity, bool>>>()))
                       .ReturnsAsync(existingSubject);
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).ReturnsAsync(1);

        
        var result = await _subjectService.DeleteSubjectAsync(existingSubject);

       
        Assert.True(result);
        _unitOfWorkMock.Verify(x => x.Subject.Remove(existingSubject), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllSubjectsAsync_ShouldReturnSubjects()
    {
        // Arrange
        var subjects = new List<SubjectEntity> { new SubjectEntity { SubjectId = 1, Description = "Subject 1" } };
        _unitOfWorkMock.Setup(x => x.Subject.GetAllAsync()).ReturnsAsync(subjects);

        // Act
        var result = await _subjectService.GetAllSubjectsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetSubjectByIdAsync_ShouldReturnSubject_WhenExists()
    {
        // Arrange
        var subject = new SubjectEntity { SubjectId = 1, Description = "Subject 1" };
        _unitOfWorkMock.Setup(x => x.Subject.GetByIdAsync(It.IsAny<Expression<Func<SubjectEntity, bool>>>()))
                       .ReturnsAsync(subject);

        // Act
        var result = await _subjectService.GetSubjectByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.SubjectId);
    }
}
