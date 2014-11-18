using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleToWebApi
{
    internal class HttpClientSingletonWrapper : HttpClient
    {
        private static readonly Lazy<HttpClientSingletonWrapper> Lazy = new Lazy<HttpClientSingletonWrapper>(() => new HttpClientSingletonWrapper());

        public static HttpClientSingletonWrapper Instance { get { return Lazy.Value; } }

        private HttpClientSingletonWrapper()
        {
            //Add custom settings here
            BaseAddress = new Uri("http://localhost:49487/");
            Timeout = TimeSpan.FromMilliseconds(15000);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));           
        }
    }
}
