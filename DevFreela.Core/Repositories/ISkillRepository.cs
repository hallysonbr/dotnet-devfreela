using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface ISkillRepository
    {
        Task<List<SkillDTO>> GetAllAsync();
        Task<Skill> GetByIdAsync(int id);
        Task AddAsync(Skill skill);
        Task UpdateAsync();
        Task SaveChangesAsync();
    }
}
