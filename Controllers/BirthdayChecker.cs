using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Model.RequestParams;

namespace BirthdayVkBot.Controllers
{
    public class BirthdayChecker
    {
        private readonly IVkApi _vkApi;

        public BirthdayChecker(IVkApi vkApi)
        {
            _vkApi = vkApi;
        }

        public void StartBot()
        {
            TimeSpan startTime = TimeSpan.FromHours(6) + TimeSpan.FromMinutes(0);
            var temp = startTime - DateTime.UtcNow.TimeOfDay;
            if (temp.TotalMilliseconds > 0)
            {
                Thread.Sleep(temp);
            }
            while (true)
            {
                foreach(var element in StaticData.list)
                {
                    if (element.getDateOfBirthday() != null && element.getDateOfBirthday().day == DateTime.Now.Day && element.getDateOfBirthday().month == DateTime.Now.Month)
                    {
                        SendBirthdayTextTo(element.id);
                    }
                }
                temp = startTime - DateTime.UtcNow.TimeOfDay;
                if (temp.TotalMilliseconds > 0)
                {
                        Thread.Sleep(temp);
                }
                else
                {
                        Thread.Sleep(temp + TimeSpan.FromDays(1));
                }
            }
        }

        public void SendBirthdayTextTo(long id)
        {
            try
            {
                _vkApi.Messages.Send(new MessagesSendParams
                {
                    RandomId = new DateTime().Millisecond,
                    PeerId = id,
                    Message = StaticData.text
                });
            }
            catch { }
        }
    }
}
