using Core.Interfaces;
using Core.Models;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private Repository<User> _userRepository;
        private Repository<Transaction> _transactionRepository;
        private Repository<AggregatedTransaction> _aggregatedTransactionRepository;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public IRepository<User> Users => _userRepository ??= new Repository<User>(_context);
        public IRepository<Transaction> Transactions => _transactionRepository ??= new Repository<Transaction>(_context);
        public IRepository<AggregatedTransaction> AggregatedTransactions => _aggregatedTransactionRepository ??= new Repository<AggregatedTransaction>(_context);

        public ITransactionRepository TransactionRepository => throw new NotImplementedException();

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
