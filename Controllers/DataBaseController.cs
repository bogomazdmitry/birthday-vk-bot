using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VkNet.Abstractions;

namespace BirthdayVkBot.Controllers
{
    [Authorize]
    public class DataBaseController : Controller
    {
        private string birthText;
        private readonly IVkApi _vkApi;
        private List<BirthDayVk> list;

        public DataBaseController(IVkApi vkApi)
        {
            _vkApi = vkApi;
           list = StaticData.list;
           birthText = StaticData.text;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("Index", (list, birthText));
        }

        List<BirthDayVk> AddMembersOfConversation( long idNewMember)
        {
            var members = _vkApi.Messages.GetConversationMembers(idNewMember, new List<string> { "bdate" });
            if(members.Profiles != null)
            {
                var listMembers = new List<BirthDayVk>();
                foreach (var member in members.Profiles)
                {
                    if (list.Find(element => (member.Id == element.id)) == null)
                    {
                        var newMember = new BirthDayVk(member.LastName + " " + member.FirstName, member.Id, member.BirthDate);
                        list.Add(newMember);
                        listMembers.Add(newMember);
                    }
                }
                return listMembers;
            }
            return null;
        }

        [HttpPost]
        public bool SaveTextChanges([FromBody] string text)
        {
            birthText = text;
            StaticData.text = text;
            return true;
        }

        [HttpPost]
        public bool DeleteMember([FromBody] long deleteMember)
        {
            list.Remove(list.Find(element=>(deleteMember == element.id)));
            StaticData.list = list;
            return true;
        }

        [HttpPost]
        public string AddMember([FromBody] long idNewMembers)
        {
            var newMembers = AddMembersOfConversation(idNewMembers);
            StaticData.list = list;
            return System.Text.Json.JsonSerializer.Serialize(newMembers);
        }

        [HttpPost]
        public bool ClearAll()
        {
            list.Clear();
            StaticData.list = list;
            return true;
        }

        [HttpPost]
        public bool EditBirthday([FromBody] string member)
        {
            list.Find(element => (Convert.ToInt64(member.Split('-')[0]) == element.id)).dayOfBirthday = member.Split('-')[1];
            StaticData.list = list;
            return true;
        }
    }
}
