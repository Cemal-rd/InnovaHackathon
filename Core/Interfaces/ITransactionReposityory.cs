using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction> GetByIdAsync(int id);
        Task AddAsync(Transaction transaction);
        Task DeleteAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> FindAsync(System.Linq.Expressions.Expression<Func<Transaction, bool>> predicate);
    }
}
