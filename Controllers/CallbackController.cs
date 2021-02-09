using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace BirthdayVkBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        private readonly IConfiguration _configuration;

        private readonly IVkApi _vkApi;
        private static int threadBot = 0;

        public CallbackController(IVkApi vkApi, IConfiguration configuration)
        {
            _vkApi = vkApi;
            _configuration = configuration;
            if(threadBot == 0)
            {
                BirthdayChecker bc = new BirthdayChecker(_vkApi);
                Thread botThread = new Thread(new ThreadStart(bc.StartBot));
                botThread.Start();
            }
            threadBot = 1;
        }

        [HttpPost]
        public IActionResult Callback([FromBody] Updates updates)
        {
            // Проверяем, что находится в поле "type" 
            switch (updates.Type)
            {
                // Если это уведомление для подтверждения адреса
                case "confirmation":
                    // Отправляем строку для подтверждения 
                    return Ok(_configuration["Config:Confirmation"]);

                //case "message_new":
                //    {
                //        // Десериализация
                //        var msg = Message.FromJson(new VkResponse(updates.Object));

                //        // Отправим в ответ полученный от пользователя текст
                //        _vkApi.Messages.Send(new MessagesSendParams
                //        {
                //            RandomId = new DateTime().Millisecond,
                //            PeerId = msg.PeerId.Value,
                //            Message = msg.Text
                //        });
                //        break;
                //    }
            }
            // Возвращаем "ok" серверу Callback API
            return Ok("ok");
        }

        
    }
}
