using Newtonsoft.Json;
using System;

namespace BirthdayVkBot.Models
{
    [Serializable]
    public class Group
    {
        [JsonProperty("id")]
        public long id;
    }
}
