﻿using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository : IRepositoryBase<Project>
    {
        Task<PaginationResult<Project>> GetAllAsync(string query, int page = 1);
        Task<Project> GetByIdAsync(int id);
        Task AddAsync(Project project);
        Task StartAsync();
        Task UpdateAsync();
        Task FinishAsync();
        Task DeleteAsync();
        Task SaveChangesAsync();
    }
}
