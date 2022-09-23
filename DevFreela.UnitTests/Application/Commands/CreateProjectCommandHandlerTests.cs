using System.Threading;
using System.Threading.Tasks;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Validators;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            //Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Titulo de Teste",
                Description = "Alguma Descrição",
                TotalCost = 50000,
                IdClient = 1,
                IdFreelancer = 2
            };           

            var createProjectCommandHandler = new CreateProjectCommandHandler(projectRepositoryMock.Object);

            //Act
            var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

            //Assert
            Assert.True(id >= 0);

            projectRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task InputDataIsInvalid_Executed_ReturnTrue()
        {
            //Arrange
            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Colocando um título grande o suficiente para estourar max caracteres",
                Description = "Colocando uma descrição grande o suficiente para estourar max caracteres",
                TotalCost = 100,
                IdClient = 1,
                IdFreelancer = 2
            };

            var validator = new CreateProjectCommandValidator();         

            //Act
            var validationResult = await validator.ValidateAsync(createProjectCommand);

            //Asset
            Assert.True(validationResult.Errors.Count > 0);
        }
    }
}