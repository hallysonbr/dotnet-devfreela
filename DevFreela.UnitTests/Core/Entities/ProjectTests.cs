using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using Xunit;

namespace DevFreela.UnitTests.Core.Entities
{
    public class ProjectTests
    {
        [Fact]
        public void ProjectStartWorks()
        {
            var project = new Project("Projeto de Teste", "Descrição de um projeto teste", 1, 2, 1000);

            Assert.Equal(ProjectStatusEnum.Created, project.Status);
            Assert.NotNull(project.CreatedAt);
            
            Assert.NotNull(project.Title);
            Assert.NotEmpty(project.Title);

            project.Start();

            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
        }
    }
}