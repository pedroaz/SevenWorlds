using Autofac;
using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Simulation;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
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
            try {
                Start();
            }
            catch (Exception e) {

                System.Console.WriteLine(e.Message);
                throw;
            }

        }

        private static void Start()
        {
            System.Console.WriteLine("Starting the Server via Console");
            SetupDependencies();
            Task.Run(StartServer);
            while (true) {
                var exitKey = System.Console.ReadKey();
                if (exitKey.Key == ConsoleKey.Q) {
                    break;
                }
            }

            System.Console.WriteLine("\nQ was pressed. Application will exit in 1 seconds");
            Thread.Sleep(1000);
        }

        private static async Task StartServer()
        {
            try {
                using (var scope = Container.BeginLifetimeScope()) {
                    var serverManager = scope.Resolve<IServerManager>();
                    await serverManager.StartServer();
                }
            }
            catch (Exception e) {

                System.Console.WriteLine(e.Message);
                throw;
            }

            
        }

        private static void SetupDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ServerManager>().As<IServerManager>();
            builder.RegisterType<LogService>().As<ILogService>();
            builder.RegisterType<UniverseCollection>().As<IUniverseCollection>();
            builder.RegisterType<UniverseFactory>().As<IUniverseFactory>();
            builder.RegisterType<WorldCollection>().As<IWorldCollection>();
            builder.RegisterType<SectionCollection>().As<ISectionCollection>();
            builder.RegisterType<AreaCollection>().As<IAreaCollection>();
            builder.RegisterType<GameLoopSimulator>().As<IGameLoopSimulator>();

            Container = builder.Build();
        }
    }
}
