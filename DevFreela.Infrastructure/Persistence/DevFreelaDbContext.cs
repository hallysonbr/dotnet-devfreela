using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public DevFreelaDbContext()
        {
            Projects = new List<Project>
            {
                new Project("Meu Projeto ASPNET Core 1", "Minha descrição de Projeto 1", 1, 1, 10000),
                new Project("Meu Projeto ASPNET Core 2", "Minha descrição de Projeto 2", 1, 1, 20000),
                new Project("Meu Projeto ASPNET Core 3", "Minha descrição de Projeto 3", 1, 1, 30000)
            };

            Users = new List<User>
            {
                new User("Hallyson Bruno", "hallyson@email.com", new DateTime(1994, 4, 18)),
                new User("Bruce Wayne", "brucewayne@email.com", new DateTime(1990, 5, 15)),
                new User("Fulano José", "fulano@email.com", new DateTime(1993, 6, 10))
            };

            Skills = new List<Skill>
            {
                new Skill(".NET Core"),
                new Skill("C#"),
                new Skill("SQL")
            };
        }

        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ProjectComment> ProjectComments { get; set; }
    }
}
