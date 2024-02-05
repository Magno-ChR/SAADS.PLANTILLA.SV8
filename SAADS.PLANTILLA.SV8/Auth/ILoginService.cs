using SAADS.PLANTILLA.SV8.DTOs;
using SAADS.PLANTILLA.SV8.DTOs.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAADS.PLANTILLA.SV8.Auth
{
    public interface ILoginService
    {
        Task Login(UserToken userToken);
        Task<bool> Login(string token);
        Task Logout();
        Task ManejarRenovacionToken();
        Task<bool> existeToken();
    }
}
