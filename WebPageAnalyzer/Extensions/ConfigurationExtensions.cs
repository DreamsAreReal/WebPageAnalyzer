using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;


namespace WebPageAnalyzer.Extensions;

internal static class ConfigurationExtensions
{
    public static ConfigurationManager Setup(this ConfigurationManager configuration)
    {
        configuration.AddJsonFile("appsettings.json", true, true);
        configuration.AddEnvironmentVariables();

        return configuration;
    } 
}