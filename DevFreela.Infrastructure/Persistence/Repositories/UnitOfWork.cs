using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly DevFreelaDbContext _context;

        public UnitOfWork(DevFreelaDbContext context, 
                          IProjectRepository projects, 
                          IUserRepository users,
                          ISkillRepository skillRepository)
        {
            Projects = projects;
            Users = users;
            Skills= skillRepository;
            _context = context;
        }

        public IProjectRepository Projects { get; }

        public IUserRepository Users { get; }

        public ISkillRepository Skills { get; }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) 
        {
            if (disposing)           
              _context.Dispose();           
        }
    }
}
