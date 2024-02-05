using Microsoft.JSInterop;
using SAADS.PLANTILLA.SV8.DTOs.Security;
using System.Security.Claims;
using System.Text.Json;

namespace SAADS.PLANTILLA.SV8.Helpers
{
    public class UserJWT
    {

        public static readonly string TOKENKEY = "TOKENKEY";
        private readonly IJSRuntime js;

        public UserJWT(IJSRuntime js)
        {

            this.js = js;
        }

        public async Task<string> GetToken()
        {
            var token = await js.GetFromLocalStorage(TOKENKEY);
            return token;
        }

        //serializar token a UserAuth
        public async Task<UserAuth> GetUserAuth()
        {
            var token = await GetToken();
            var userAuth = new UserAuth();
            if (string.IsNullOrEmpty(token))
            {
                return userAuth;
            }


            var payload = token.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var userInfo = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (userInfo != null)
            {
                var xuserAuth = new UserAuth
                {
                    Id = int.Parse(userInfo[ClaimTypes.NameIdentifier].ToString()!),
                    Email = userInfo[ClaimTypes.Email].ToString()!,
                    UserName = userInfo[ClaimTypes.Name].ToString()!,

                    Tipo = userInfo["tipo"].ToString()!,

                    SedeId = int.Parse(userInfo["sedeId"].ToString()!),
                    SedeNombre = userInfo["sede"].ToString()!,
                    Expiracion = long.Parse(userInfo["exp"].ToString()!),
                    timezone = userInfo["timezone"].ToString()!,
                    Token = token,


                };

                if (xuserAuth.Tipo.ToUpper() == "FUNCIONARIO")
                {
                    xuserAuth.AplicacionId = int.Parse(userInfo["aplicacionId"].ToString()!);
                    xuserAuth.CargoId = int.Parse(userInfo["cargoId"].ToString()!);
                    xuserAuth.CargoNombre = userInfo["cargo"].ToString()!;

                }

                return xuserAuth;
            }




            return userAuth;
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


    }
}
