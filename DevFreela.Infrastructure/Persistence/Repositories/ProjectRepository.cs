using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.Context;
using DevFreela.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private const int PAGE_SIZE = 2;
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<PaginationResult<Project>> GetAllAsync(string query, int page )
        {
            IQueryable<Project> projects = _dbContext.Projects;
            
            if(!string.IsNullOrEmpty(query)) 
            {
                projects = projects.Where(p => p.Title.Contains(query) ||
                                          p.Description.Contains(query));
            }
            return await projects.GetPaged(page, PAGE_SIZE);
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _dbContext.Projects
                                   .Include(p => p.Client)
                                   .Include(p => p.Freelancer)
                                   .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await SaveChangesAsync();
        }

        public async Task StartAsync()
        {
            await SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            await SaveChangesAsync();
        }
        public async Task FinishAsync()
        {
            await SaveChangesAsync();
        }

        public async Task DeleteAsync()
        {
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
