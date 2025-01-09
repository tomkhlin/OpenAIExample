using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using OpenAI;
using OpenAI.Chat;
using System.Collections.Generic;

namespace ChatGPTAPIExample;

internal class Program
{
    static async Task Main(string[] args)
    {
        await UseHttpClientToCallOpenAIAPIAsync();
        await UseAzureOpenAISDKToCallOpenAIAPIAsync();
    }

    static async Task UseHttpClientToCallOpenAIAPIAsync()
    {
        //API endpoint URL and key
        const string endpoint = "你的API URL";
        const string key = "你的Key";

        //Create a new HttpClient and set the authorization header
        HttpClient client = new();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);

        //Create a new message and add it to the list of messages
        var message = new Message
        {
            Role = "user",
            Content = "Hello, I'm a user."
        };
        var content = new OpenAIRequestBody
        {
            Model = "gpt-4o-mini",
            Messages = []
        };
        content.Messages.Add(message);

        //Serialize the content to JSON and send it to the API
        string json_string = JsonSerializer.Serialize(content);
        using StringContent jsonContent = new(json_string, Encoding.UTF8, "application/json");
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

    static async Task UseAzureOpenAISDKToCallOpenAIAPIAsync()
    {
        //API endpoint URL and key
        const string key = "你的Key";

        var client = new OpenAIClient(key); //Create a new OpenAI client
        var chatClient = client.GetChatClient("gpt-4o-mini"); //Create a new chat client with the model name

        //Create a new message and add it to the list of messages
        var message = new List<ChatMessage>
        {
            new UserChatMessage("Hello, I'm a user.")
        };

        //Send the message to the API and print the response
        var response = await chatClient.CompleteChatAsync();

        //Print the response
        Console.WriteLine(response.Value.Content[0].Text);
    }
