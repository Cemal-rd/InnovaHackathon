using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        ITransactionRepository TransactionRepository { get; }
        IRepository<User> Users { get; }
        IRepository<Transaction> Transactions { get; }
        IRepository<AggregatedTransaction> AggregatedTransactions { get; }
        Task SaveAsync();
    }
}
