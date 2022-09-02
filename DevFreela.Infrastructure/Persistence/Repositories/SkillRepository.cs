using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public SkillRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task AddAsync(Skill skill)
        {
            await _dbContext.Skills.AddAsync(skill);
            await SaveChangesAsync();            
        }

        public async Task<List<SkillDTO>> GetAllAsync()
        {
            //Usando Dapper
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var script = "SELECT Id, Description FROM Skills";
                var skills = await sqlConnection.QueryAsync<SkillDTO>(script);

                return skills.ToList();
            }
        }

        public async Task<Skill> GetByIdAsync(int id)
        {
            return await _dbContext.Skills.SingleOrDefaultAsync(s => s.Id == id);           
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            await SaveChangesAsync();
        }
    }
}
