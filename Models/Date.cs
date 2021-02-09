using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayVkBot
{
    public class Date
    {
        public Date(int day, int month)
        {
            this.day = day;
            this.month = month;
        }

        public int day { get; set; }
        
        public int month { get; set; }

        public override string ToString()  
        {
            return (day / 10 == 0 ? "0" : "") + day.ToString() + "." + (month / 10 == 0 ? "0" : "") + month.ToString();
        }
    }
}
