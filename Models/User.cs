using Newtonsoft.Json;
using System;

namespace BirthdayVkBot
{
    [Serializable]
    public class User
    {
        [JsonProperty("login")]
        public string login { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }
    }
}
