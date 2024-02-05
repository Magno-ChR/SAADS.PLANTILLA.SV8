

using SAADS.PLANTILLA.SV8.Helpers;
using SAADS.PLANTILLA.SV8.RN;
using System.Reflection;


namespace SAADS.PLANTILLA.WA8.Configuration
{
    public static class confRN
    {
        public static IServiceCollection AddConfRN(this IServiceCollection services)
        {
            var ruleTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.Contains("RN") && !t.IsAbstract);

            foreach (var ruleType in ruleTypes)
            {
                services.AddScoped(ruleType);
            }



            return services;
        }
    }
}
