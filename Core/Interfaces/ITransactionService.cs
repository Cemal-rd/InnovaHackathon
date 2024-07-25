using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITransactionService
    {
        Task<decimal> GetTotalSpendingByUserId(string userId);
        Task AggregateDailyTransactions();
        Task AggregateWeeklyTransactions();
        Task AggregateMonthlyTransactions();
    }

}
