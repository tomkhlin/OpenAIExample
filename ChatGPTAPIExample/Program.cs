using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;

namespace ChatGPTAPIExample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //API endpoint URL and key
            const string endpoint = "你的API URL";
            const string key = "你的Key";

            //Create a new HttpClient and set the authorization header
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);

            //Create a new message and add it to the list of messages
            var message = new Message
            {
                Role = "user",
                Content = "Hello, I'm a user."
            };
            var content = new OpenAIRequestBody
            {
                Model = "gpt-3.5-turbo",
                Messages = new List<Message>()
            };
            content.Messages.Add(message);

            //Serialize the content to JSON and send it to the API
            string json_string = JsonSerializer.Serialize(content);
            using StringContent jsonContent = new StringContent(json_string, Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync(endpoint, jsonContent);

            //If the response is OK, print the response body
            if(response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            Console.ReadKey();
        }
    }
}
