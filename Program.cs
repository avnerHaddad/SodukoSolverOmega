// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddIniFile("app.config")
    .Build();
    }
}