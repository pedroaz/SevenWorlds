using Autofac;
using SevenWorlds.GameServer.Server.Manager;
using SevenWorlds.GameServer.Utils.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SevenWorlds.Console.Server.App
{
    class ServerApplication
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            System.Console.WriteLine("Starting the Server via Console");
            SetupDependencies();
            Task.Run(StartServer);
            while (true) {
                var exitKey = System.Console.ReadKey();
                if(exitKey.Key == ConsoleKey.Q) {
                    break;
                }
            }

            System.Console.WriteLine("\nQ was pressed. Application will exit in 1 seconds");
            Thread.Sleep(1000);
        }

        private static async Task StartServer()
        {
            using (var scope = Container.BeginLifetimeScope()) {
                var serverManager = scope.Resolve<IServerManager>();
                await serverManager.StartServer();
            }
        }

        private static void SetupDependencies()
        {
            var builder = new ContainerBuilder();

            // Usually you're only interested in exposing the type
            // via its interface:
            builder.RegisterType<ServerManager>().As<IServerManager>();
            builder.RegisterType<LogService>().As<ILogService>();

            Container = builder.Build();
        }
    }
}
