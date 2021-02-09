using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayVkBot
{
    public class BirthDayVk
    {
        public BirthDayVk(string FI, long id, string dayOfBirthday)
        {
            this.FI = FI; 
            this.id = id;
            this.dayOfBirthday = dayOfBirthday;
        }


        [JsonProperty("FI")]
        public string FI { get; set; }


        [JsonProperty("id")]
        public long id { get; set; }

        private Date dayOfBirthday_;

        [JsonProperty("dayOfBirthday")]
        public string dayOfBirthday
        { 
            get 
            {
                return dayOfBirthday_?.ToString();
            } 
            set 
            {
                var arr = value?.Split('.');
                if(arr == null || arr.Length <= 1)
                {
                    arr = value?.Split('/');
                    if (arr == null || arr.Length <= 1)
                    {
                        dayOfBirthday_ = null;
                        return;
                    }
                } 
                dayOfBirthday_ = new Date(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
            } 
        }

        public Date getDateOfBirthday()
        {
            return dayOfBirthday_;
        }
    }
}
