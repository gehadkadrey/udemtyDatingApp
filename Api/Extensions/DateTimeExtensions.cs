using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly bob)
        {
            //mean get date only from datetime.now
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age=today.Year-bob.Year;
            if(bob>today.AddYears(-age)) age--;
            return age;
        }
    }
}