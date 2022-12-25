// See https://aka.ms/new-console-template for more information
<<<<<<< HEAD
using System.Configuration;
=======
using Microsoft.Extensions.Configuration;
>>>>>>> configurtionFIle

internal class Program
{
    private static void Main(string[] args)
    {
<<<<<<< HEAD

        string value = ConfigurationManager.AppSettings["Boardhight"];
=======
        IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddIniFile("app.config")
    .Build();
>>>>>>> configurtionFIle
    }
}