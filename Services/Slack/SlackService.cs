using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SlackAPI;

namespace Coupons.Services.Slack
{
    public class SlackService : ISlackService
    {
        private readonly HttpClient _httpClient;
        private readonly string _webhookUrl;

        public SlackService(HttpClient httpClient, string webhookUrl)
        {
            _httpClient = httpClient;
            _webhookUrl = webhookUrl;
        }
        public async Task SlackNotifier(string message)
        {
            var payload = new {text = $"New bug\nError:{message}\nDate: {DateTime.Now}"};
            var JsonPayload = JsonConvert.SerializeObject(payload);
            var httpContent = new StringContent(JsonPayload, Encoding.UTF8, "application/json");

            var client = new SlackTaskClient(_webhookUrl);
            try
            {
                var response = await _httpClient.PostAsync(_webhookUrl, httpContent);
                Console.WriteLine(httpContent.ToString());
            }
            catch (Exception e)
            {
                
                throw new Exception($"Error sending message to Slack: {e.Message}");
            }
        }
    }
}