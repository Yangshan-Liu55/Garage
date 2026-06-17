using Microsoft.Extensions.Configuration;

namespace Garage.Helpers;

public static class ConfigurationHelper
{
    private static readonly IConfigurationRoot _configuration =
        new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

    public static int DefaultCapacity => int.TryParse(
        _configuration["GarageSettings:DefaultCapacity"],
        out int value)
        ? value
        : 200;

}