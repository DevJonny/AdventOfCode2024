using App.Menus;
using InteractiveCLI;
using Microsoft.Extensions.Hosting;

var host = 
    Host.CreateDefaultBuilder()
        .AddInteractiveCli()
        .Build();

host.UseInteractiveCli<TopLevelMenu>(_ => 
    new TopLevelMenu(), args);