using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SAADS.PLANTILLA.SV8.DTOs;
using SAADS.PLANTILLA.SV8.DTOs.Security;
using SAADS.PLANTILLA.SV8.Helpers;
using SAADS.PLANTILLA.SV8.Repositorios;
using SAADS.PLANTILLA.SV8.RN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace SAADS.PLANTILLA.SV8.Auth
{
    public class ProveedorAutenticacionJWT : AuthenticationStateProvider, ILoginService
    {

        public ProveedorAutenticacionJWT(IJSRuntime js, HttpClient httpClient,
            IRepositorio repositorio, SecurityRN securityRN)
        {
            this.js = js;
            this.httpClient = httpClient;
            this.repositorio = repositorio;
            this.securityRN = securityRN;
        }

        public static readonly string TOKENKEY = "TOKENKEY";
        public static readonly string EXPIRATIONTOKENKEY = "EXPIRATIONTOKENKEY";
        private readonly IJSRuntime js;
        private readonly HttpClient httpClient;
        private readonly IRepositorio repositorio;
        private readonly SecurityRN securityRN;

        private AuthenticationState Anonimo =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));


        public async Task<bool> existeToken()
        {
            var token = await js.GetFromLocalStorage(TOKENKEY);

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            return true;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetFromLocalStorage(TOKENKEY);

            if (string.IsNullOrEmpty(token))
            {
                return Anonimo;
            }

            var tiempoExpiracionString = await js.GetFromLocalStorage(EXPIRATIONTOKENKEY);
            DateTime tiempoExpiracion;

            if (DateTime.TryParse(tiempoExpiracionString, out tiempoExpiracion))
            {
                if (TokenExpirado(tiempoExpiracion))
                {
                    await Limpiar();
                    return Anonimo;
                }

                if (DebeRenovarToken(tiempoExpiracion))
                {
                    token = await RenovarToken(token);

                    if (string.IsNullOrEmpty(token))
                    {
                        await Limpiar();
                        return Anonimo;
                    }
                }

            }

            return ConstruirAuthenticationState(token);
        }

        public async Task ManejarRenovacionToken()
        {
            var tiempoExpiracionString = await js.GetFromLocalStorage(EXPIRATIONTOKENKEY);
            DateTime tiempoExpiracion;

            if (DateTime.TryParse(tiempoExpiracionString, out tiempoExpiracion))
            {
                if (TokenExpirado(tiempoExpiracion))
                {
                    await Logout();
                }

                if (DebeRenovarToken(tiempoExpiracion))
                {
                    var token = await js.GetFromLocalStorage(TOKENKEY);
                    var nuevoToken = await RenovarToken(token);
                    var authState = ConstruirAuthenticationState(nuevoToken);
                    NotifyAuthenticationStateChanged(Task.FromResult(authState));
                }
            }
        }

        private async Task<string> RenovarToken(string token)
        {
            Console.WriteLine("Renovando el token...");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
           // var nuevoTokenResponse = await repositorio.Get<UserToken>("accesos/RenovarToken");

            var nuevoTokenResponse = await securityRN.renovarToken();

            if (nuevoTokenResponse.Success)
            {
                var nuevoToken = nuevoTokenResponse.value;
                await js.SetInLocalStorage(TOKENKEY, nuevoToken!.Token);
                await js.SetInLocalStorage(EXPIRATIONTOKENKEY, nuevoToken.Expiracion.ToString());
                return nuevoToken.Token;
            }


            return "";


        }

        private bool DebeRenovarToken(DateTime tiempoExpiracion)
        {
            return tiempoExpiracion.Subtract(DateTime.UtcNow) < TimeSpan.FromMinutes(5);
        }

        private bool TokenExpirado(DateTime tiempoExpiracion)
        {
            return tiempoExpiracion <= DateTime.UtcNow;
        }

        public AuthenticationState ConstruirAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        private string getValueToken(string jwt, string tipo)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            var value = keyValuePairs![tipo].ToString();

            return value!;
        }


        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task Login(UserToken userToken)
        {
            await js.SetInLocalStorage(TOKENKEY, userToken.Token);
            await js.SetInLocalStorage(EXPIRATIONTOKENKEY, userToken.Expiracion.ToString());
            var authState = ConstruirAuthenticationState(userToken.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }
        public async Task<bool> Login(string token)
        {
            try
            {

                long expiracion = long.Parse(getValueToken(token, "expiracion"));
                var expiracionToken = expiracion.UnixTimeToDateTime();

                await js.SetInLocalStorage(TOKENKEY, token);
                await js.SetInLocalStorage(EXPIRATIONTOKENKEY, expiracionToken.ToString());
                var authState = ConstruirAuthenticationState(token);


                NotifyAuthenticationStateChanged(Task.FromResult(authState));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }

        }

        public async Task Logout()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
            await Limpiar();

        }

        private async Task Limpiar()
        {
            await js.RemoveItem(TOKENKEY);
            await js.RemoveItem(EXPIRATIONTOKENKEY);
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
