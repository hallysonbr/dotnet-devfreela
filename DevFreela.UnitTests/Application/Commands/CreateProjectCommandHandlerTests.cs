using System.Collections.Generic;
using System.Linq;
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
            var unitOfWork = new Mock<IUnitOfWork>();
            var projectRepositoryMock = new Mock<IProjectRepository>();
            var skillRepository = new Mock<ISkillRepository>();

            unitOfWork.SetupGet(uow => uow.Projects).Returns(projectRepositoryMock.Object);
            unitOfWork.SetupGet(uow => uow.Skills).Returns(skillRepository.Object);

            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Titulo de Teste",
                Description = "Alguma Descrição",
                TotalCost = 50000,
                IdClient = 1,
                IdFreelancer = 2
            };           

            var createProjectCommandHandler = new CreateProjectCommandHandler(unitOfWork.Object);

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

        [Fact]
        public void Should_Return_True_When_String_Contains_Any_Word()
        {
            var projects = new List<Project>()
            {
                new Project("", "João Pessoa", 0, 0, 0),
                new Project("", "João Pessoa", 0, 0, 0),
                new Project("", "João Pessoa", 0, 0, 0),
                new Project("", "João Pessoa", 0, 0, 0),
                new Project("", "João Pessoa", 0, 0, 0),
                new Project("", "Recife", 0, 0, 0),
                new Project("", "Recife", 0, 0, 0),
                new Project("", "Recife", 0, 0, 0),
                new Project("", "Natal", 0, 0, 0),
                new Project("", "Natal", 0, 0, 0),
                new Project("", "Natal", 0, 0, 0),
                new Project("", "Minas", 0, 0, 0),
                new Project("", "Rio de Janeiro", 0, 0, 0),
                new Project("", "Minas", 0, 0, 0),
                new Project("", "Minas", 0, 0, 0),
                new Project("", "Minas", 0, 0, 0),
            };


            string comparador = "Rio de Janeiro, São Paulo, Minas, Natal, Recife";            
            comparador = "";

            var retorno = projects.Where(p => comparador.Contains(p.Description)
                                        || string.IsNullOrWhiteSpace(comparador))
                                        .ToList();

            //var retorno = projects.Where(p => lista.Any(l => p.Description.Contains(l))
            //                            || string.IsNullOrEmpty(comparador))
            //                            .ToList();

            Assert.True(retorno.Count > 0);
            Assert.NotNull(retorno);
        }
    }
}