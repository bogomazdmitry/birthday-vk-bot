using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BirthdayVkBot
{
    [Serializable]
    public class Updates
    {
        /// <summary>
        /// ��� �������
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// ������, �������������� �������
        /// ��������� ������� ������� �� ���� �����������
        /// </summary>
        [JsonProperty("object")]
        public JObject Object { get; set; }

        /// <summary>
        /// ID ����������, � ������� ��������� �������
        /// </summary>
        [JsonProperty("group_id")]
        public long GroupId { get; set; }
    }
}
