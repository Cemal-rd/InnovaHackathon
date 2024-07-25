using Core.Interfaces;
using Core.Models;

namespace InnovaHackathon.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<decimal> GetTotalSpendingByUserId(string userId)
        {
            var transactions = await _unitOfWork.TransactionRepository
                .FindAsync(t => t.UserId == userId);

            return transactions.Sum(t => t.Amount);
        }

        public async Task AggregateDailyTransactions()
        {
            var yesterday = DateTime.UtcNow.Date.AddDays(-1);
            var dailyTransactions = await _unitOfWork.Transactions.GetAllAsync();
            var userTransactions = dailyTransactions
                .Where(t => t.Date.Date == yesterday)
                .GroupBy(t => t.UserId)
                .Select(g => new AggregatedTransaction
                {
                    UserId = g.Key,
                    DailyTotal = g.Sum(t => t.Amount),
                    Date = yesterday
                }).ToList();

            foreach (var userTransaction in userTransactions)
            {
                await _unitOfWork.AggregatedTransactions.AddAsync(userTransaction);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task AggregateWeeklyTransactions()
        {
            var lastWeekStart = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek - 6);
            var lastWeekEnd = lastWeekStart.AddDays(7);
            var weeklyTransactions = await _unitOfWork.Transactions.GetAllAsync();
            var userTransactions = weeklyTransactions
                .Where(t => t.Date.Date >= lastWeekStart && t.Date.Date < lastWeekEnd)
                .GroupBy(t => t.UserId)
                .Select(g => new AggregatedTransaction
                {
                    UserId = g.Key,
                    WeeklyTotal = g.Sum(t => t.Amount),
                    Date = lastWeekStart
                }).ToList();

            foreach (var userTransaction in userTransactions)
            {
                await _unitOfWork.AggregatedTransactions.AddAsync(userTransaction);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task AggregateMonthlyTransactions()
        {
            var lastMonthStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(-1);
            var lastMonthEnd = lastMonthStart.AddMonths(1);
            var monthlyTransactions = await _unitOfWork.Transactions.GetAllAsync();
            var userTransactions = monthlyTransactions
                .Where(t => t.Date.Date >= lastMonthStart && t.Date.Date < lastMonthEnd)
                .GroupBy(t => t.UserId)
                .Select(g => new AggregatedTransaction
                {
                    UserId = g.Key,
                    MonthlyTotal = g.Sum(t => t.Amount),
                    Date = lastMonthStart
                }).ToList();

            foreach (var userTransaction in userTransactions)
            {
                await _unitOfWork.AggregatedTransactions.AddAsync(userTransaction);
            }

            await _unitOfWork.SaveAsync();
        }
    }
}
