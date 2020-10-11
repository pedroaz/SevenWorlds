using Autofac;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using SevenWorlds.GameServer.Account;
using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Simulation;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.GameServer.Server.Manager;
using SevenWorlds.GameServer.Utils.Log;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SevenWorlds.Console.Server.App
{
    class ServerApplication
    {
        private static IContainer container { get; set; }

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
                using (var scope = container.BeginLifetimeScope()) {
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

            var assembly = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("GameServer")).FirstOrDefault();
            builder.RegisterHubs(assembly);

            builder.RegisterType<ServerManager>().As<IServerManager>().SingleInstance();
            builder.RegisterType<LogService>().As<ILogService>().SingleInstance();
            builder.RegisterType<UniverseCollection>().As<IUniverseCollection>().SingleInstance();
            builder.RegisterType<UniverseFactory>().As<IUniverseFactory>().SingleInstance();
            builder.RegisterType<WorldCollection>().As<IWorldCollection>().SingleInstance();
            builder.RegisterType<SectionCollection>().As<ISectionCollection>().SingleInstance();
            builder.RegisterType<AreaCollection>().As<IAreaCollection>().SingleInstance();
            builder.RegisterType<GameLoopSimulator>().As<IGameLoopSimulator>().SingleInstance();
            builder.RegisterType<PlayerCollection>().As<IPlayerCollection>().SingleInstance();
            builder.RegisterType<HubService>().As<IHubService>().SingleInstance();
            builder.RegisterType<GameStateService>().As<IGameStateService>().SingleInstance();
            builder.RegisterType<PlayerActionQueue>().As<IPlayerActionQueue>().SingleInstance();
            builder.RegisterType<PlayerActionFactory>().As<IPlayerActionFactory>().SingleInstance();
            builder.RegisterType<AccountService>().As<IAccountService>().SingleInstance();

            container = builder.Build();
            GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);
        }
    }
}
