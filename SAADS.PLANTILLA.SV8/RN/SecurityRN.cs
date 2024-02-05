using SAADS.PLANTILLA.SV8.DTOs.Security;
using SAADS.PLANTILLA.SV8.DTOs;
using SAADS.PLANTILLA.SV8.Repositorios;

namespace SAADS.PLANTILLA.SV8.RN
{
    public class SecurityRN
    {
        private readonly IRepositorio repositorio;
        private readonly IConfiguration configuration;

        public SecurityRN(IRepositorio repositorio, IConfiguration configuration)
        {
            this.repositorio = repositorio;
            this.configuration = configuration;
        }
        public async Task<ResponseDTO<FuncionarioDTO>> getFuncionario()
        {
            var result = await repositorio.Get<FuncionarioDTO>($"Accesos/Funcionario");
            return await result.getResponse();
        }
        public async Task<ResponseDTO<PersonaDTO>> getPersona()
        {
            var result = await repositorio.Get<PersonaDTO>($"Accesos/Persona");
            return await result.getResponse();
        }
        public async Task<ResponseDTO<List<ModuloDTO>>> getModulos()
        {
            var result = await repositorio.Get<List<ModuloDTO>>($"Accesos/Modulos");
            return await result.getResponse();
        }
        public async Task<ResponseDTO<List<InterfaceDTO>>> getInterfaces(int idModulo)
        {
            var result = await repositorio.Get<List<InterfaceDTO>>($"Accesos/Interfaces/{idModulo}");
            return await result.getResponse();
        }
        public async Task<ResponseDTO<List<InterfaceDTO>>> getInterfacesTareas(int idModulo)
        {
            var result = await repositorio.Get<List<InterfaceDTO>>($"Accesos/InterfacesTareas/{idModulo}");
            return await result.getResponse();
        }
        public async Task<ResponseDTO<List<TareaDTO>>> getTareas(int idInterface)
        {
            var result = await repositorio.Get<List<TareaDTO>>($"Accesos/Tareas/{idInterface}");
            return await result.getResponse();
        }
        public async Task<ResponseDTO<bool>> VerificarTarea(int permiso)
        {
            var result = await repositorio.Get<bool>($"Accesos/VerificarTarea/{permiso}");
            return await result.getResponse();
        }
        public async Task<ResponseDTO<UserToken>> renovarToken()
        {
            var result = await repositorio.Post<UserToken>($"Accesos/RenovarToken");
            return await result.getResponse();
        }
    }
}
