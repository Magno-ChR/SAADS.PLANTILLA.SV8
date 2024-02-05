using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using SAADS.PLANTILLA.SV8.Helpers;
using SAADS.PLANTILLA.SV8.Components.Pages.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SAADS.PLANTILLA.SV8.Repositorios
{
    public class Repositorio : IRepositorio
    {
        private readonly HttpClient httpClient;
        public static readonly string TOKENKEY = "TOKENKEY";

        private readonly IJSRuntime js;
        private readonly IConfiguration configuration;
        private readonly NavigationManager navigationManager;

        public Repositorio(HttpClient httpClient, IJSRuntime js, IConfiguration configuration, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.js = js;
            this.configuration = configuration;
            this.navigationManager = navigationManager;
        }

        private JsonSerializerOptions OpcionesPorDefectoJSON =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };



        public void AsignarUrlBase(string url)
        {
            httpClient.BaseAddress = new Uri(url);
        }


        private void logOut()
        {
            //get the current uri
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
            var url =  navigationManager.GetEncodeHtml(uri.ToString());
            navigationManager.NavigateTo($"/Logout?returnpage={url}");
        }

        public void AsignarUrlBase(Endpoint endpoint)
        {
            switch (endpoint)
            {
                case Endpoint.api:
                    httpClient.BaseAddress = new Uri(configuration["endpoints:api"]!);
                    break;
                case Endpoint.auth:
                    httpClient.BaseAddress = new Uri(configuration["endpoints:auth"]!);
                    break;
                default:
                    break;
            }

        }

        private async Task AsignarToken()
        {
            string token = await js.GetFromLocalStorage(TOKENKEY);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            await AsignarToken();
            var responseHTTP = await httpClient.GetAsync(url);


            if (responseHTTP.StatusCode == HttpStatusCode.Unauthorized)
            {
                logOut();
               
            }

            if (responseHTTP.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<T>(responseHTTP, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<T>(response, false, responseHTTP);
            }
            else
            {
              
               return new HttpResponseWrapper<T>(default, true, responseHTTP);
                
                
            }
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
        {
            await AsignarToken();
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);

            if (responseHttp.StatusCode == HttpStatusCode.Unauthorized)
            {
                logOut();

            }


            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<TResponse>(string url)
        {
            await AsignarToken();
            var responseHttp = await httpClient.PostAsync(url, null);

            if (responseHttp.StatusCode == HttpStatusCode.Unauthorized)
            {
                logOut();
            }



            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<TResponse>(responseHttp, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, true, responseHttp);
            }

        }
        public async Task<HttpResponseWrapper<string>> PostSimple<T, TResponse>(string url, T enviar)
        {
            await AsignarToken();
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);


            if (responseHttp.StatusCode == HttpStatusCode.Unauthorized)
            {
                logOut();
            }


            if (responseHttp.IsSuccessStatusCode)
            {
                var responseString = await responseHttp.Content.ReadAsStringAsync();
                //var response = await DeserializarRespuesta<TResponse>(responseHttp, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<string>(responseString, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<string>(default, true, responseHttp);
            }
        }
        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar)
        {
            await AsignarToken();
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PutAsync(url, enviarContent);

            if (responseHttp.StatusCode == HttpStatusCode.Unauthorized)
            {
                logOut();
            }
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar)
        {
            await AsignarToken();
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);

            if (responseHttp.StatusCode == HttpStatusCode.Unauthorized)
            {
                logOut();
            }


            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<TResponse>(responseHttp, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, true, responseHttp);
            }
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            await AsignarToken();
            var responseHTTP = await httpClient.DeleteAsync(url);
            if (responseHTTP.StatusCode == HttpStatusCode.Unauthorized)
            {
                logOut();

            }
            return new HttpResponseWrapper<object>(null, !responseHTTP.IsSuccessStatusCode, responseHTTP);
        }

        private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            await AsignarToken();
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, jsonSerializerOptions);
        }

    }

    public enum Endpoint
    {
        api,
        auth
    }
}
