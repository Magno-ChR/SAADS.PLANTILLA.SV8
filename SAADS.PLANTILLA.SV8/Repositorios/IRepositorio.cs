using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAADS.PLANTILLA.SV8.Repositorios
{
    public interface IRepositorio
    {
        Task<HttpResponseWrapper<object>> Delete(string url);
        Task<HttpResponseWrapper<T>> Get<T>(string url);
        Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar);
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar);
        Task<HttpResponseWrapper<TResponse>> Post<TResponse>(string url);
        Task<HttpResponseWrapper<string>> PostSimple<T, TResponse>(string url, T enviar);
        Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar);
        void AsignarUrlBase(string url);
        void AsignarUrlBase(Endpoint endpoint);

    }
}
