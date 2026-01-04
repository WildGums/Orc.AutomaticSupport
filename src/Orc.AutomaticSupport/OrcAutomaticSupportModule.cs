namespace Orc.AutomaticSupport
{
    using Catel.Services;
    using Catel.ThirdPartyNotices;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// Core module which allows the registration of default services in the service collection.
    /// </summary>
    public static class OrcAutomaticSupportModule
    {
        public static IServiceCollection AddOrcAutomaticSupport(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IAutomaticSupportService, AutomaticSupportService>();

            serviceCollection.AddSingleton<ILanguageSource>(new LanguageResourceSource("Orc.AutomaticSupport", "Orc.AutomaticSupport.Properties", "Resources"));

            serviceCollection.AddSingleton<IThirdPartyNotice>((x) => new LibraryThirdPartyNotice("Orc.AutomaticSupport", "https://github.com/wildgums/orc.automaticsupport"));

            return serviceCollection;
        }
    }
}
