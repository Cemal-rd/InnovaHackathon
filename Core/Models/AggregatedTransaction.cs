using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AggregatedTransaction
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public decimal DailyTotal { get; set; }
        public decimal WeeklyTotal { get; set; }
        public decimal MonthlyTotal { get; set; }
        public DateTime Date { get; set; }
    }
}
