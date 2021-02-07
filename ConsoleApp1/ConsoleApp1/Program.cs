using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    /*class IndustryIdentifiers
    {
        
    }
    class VolumeInfo
    {
        public string title { get; set; }
        public string[] authors { get; set; }
        public string publisher { get; set; }
        public DateTime publishedDate { get; set; }
        public string description { get; set; }
        
    }*/
    class Book
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string etag { get; set; }
        public string selfLink { get; set; }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://www.googleapis.com/books/v1/volumes?q=pride+prejudice"));
            GetWeather(client, request).Wait();
            /*HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://www.googleapis.com/books/v1/volumes?q=pride+prejudice");
            GetBooks(httpClient);*/
        }

        static async Task GetWeather(HttpClient client, HttpRequestMessage request)
        {
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                JToken jToken = JContainer.Parse(body);
                JToken jTok = jToken["items"];
                List<Book> Books = new List<Book>();
                foreach (var jt in jTok)
                {
                    Console.WriteLine(jt.ToString());
                    Books.Add(JsonConvert.DeserializeObject<Book>(jt.ToString()));
                }
            }
        }

        /*static async Task GetBooks(HttpClient httpClient)
        {
            using (httpClient.GetAsync())
            {
                HttpResponseMessage httpResponseMessage = await httpClient.Conte("").Result;
                httpResponseMessage.EnsureSuccessStatusCode();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    JArray jArray = JArray.Parse(httpResponseMessage.Content.ToString());
                    foreach (var jtoken in jArray)
                    {
                        Console.WriteLine(jtoken.ToString());
                    }
                }
            }
        }*/
    }
}