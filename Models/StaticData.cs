using Newtonsoft.Json;
using System.Collections.Generic;

namespace BirthdayVkBot.Controllers
{
    static public class StaticData
    {
        private static List<BirthDayVk> _list;
        private static string _text;


        public static List<BirthDayVk> list { 
            get
            {
                return _list;
            }
            set
            {
                _list = value;
                Serialize();
            }
        }
        public static string text { 
            get
            {
                return _text;
            }

            set 
            {
                _text = value;
                System.IO.File.WriteAllText("text.json", _text);
            } 
        }

        static StaticData()
        {
            Deserialize();
            try
            {
                _text = System.IO.File.ReadAllText("text.json");
            }
            catch
            {
                _text = "";
            }
        }

        static public void Serialize()
        {
            System.IO.File.WriteAllText("birthdays.json", System.Text.Json.JsonSerializer.Serialize<List<BirthDayVk>>(_list));
        }

        static public void Deserialize()
        {
            _list = JsonConvert.DeserializeObject<List<BirthDayVk>>(System.IO.File.ReadAllText("birthdays.json"));
        }

    }
}
