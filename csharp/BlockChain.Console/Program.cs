﻿namespace BlockChainDemo.Console
{
  using Microsoft.Extensions.Configuration;

  public class Program
  {
    public static void Main(string[] args)
    {
      var chain = new BlockChain();
      var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();
      var server = new WebServer(chain, config);
      System.Console.Read();
    }
  }
}
