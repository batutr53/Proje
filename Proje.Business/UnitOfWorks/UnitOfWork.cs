using Proje.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Business.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjeContext _context;

        public UnitOfWork(ProjeContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
