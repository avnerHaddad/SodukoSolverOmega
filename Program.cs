// See https://aka.ms/new-console-template for more information
using System.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {

        string value = ConfigurationManager.AppSettings["Boardhight"];
    }
}