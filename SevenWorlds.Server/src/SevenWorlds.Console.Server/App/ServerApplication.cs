using Autofac;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using SevenWorlds.GameServer.Account;
using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Gameplay.Actions.Executor;
using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Gameplay.Encounter;
using SevenWorlds.GameServer.Gameplay.Encounter.Executor;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Simulation;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.GameServer.Server;
using SevenWorlds.GameServer.Utils.Config;
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
                Task mainTask = Start(args);
                mainTask.Wait();
            }
            catch (AggregateException e) {
                System.Console.WriteLine("Aggregate Exception! :(");
                System.Console.WriteLine(e.Message);
            }
            catch (Exception e) {
                System.Console.WriteLine("Exception! :(");
                System.Console.WriteLine(e.Message);
                throw;
            }

            Thread.Sleep(Timeout.Infinite);
        }

        private static async Task Start(string[] args)
        {
            System.Console.WriteLine("----- Starting the Server via Console -----");
            if (args.Length != 1) {
                System.Console.WriteLine("The args did not have Length == 1. The ServerConfigurations.json file must be passed to the server ");
                return;
            }

            SetupDependencies();
            await StartServer(args[0]);
        }

        private static async Task StartServer(string configPath)
        {
            try {
                using (var scope = container.BeginLifetimeScope()) {
                    var configurator = scope.Resolve<IConfigurator>();
                    configurator.ReadConfigurations(configPath);
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
            builder.RegisterType<GameServerFactory>().As<IGameServerFactory>().SingleInstance();
            builder.RegisterType<WorldCollection>().As<IWorldCollection>().SingleInstance();
            builder.RegisterType<SectionCollection>().As<ISectionCollection>().SingleInstance();
            builder.RegisterType<AreaCollection>().As<IAreaCollection>().SingleInstance();
            builder.RegisterType<GameLoopSimulator>().As<IGameLoopSimulator>().SingleInstance();
            builder.RegisterType<PlayerCollection>().As<IPlayerCollection>().SingleInstance();
            builder.RegisterType<HubService>().As<IHubService>().SingleInstance();
            builder.RegisterType<GameStateService>().As<IGameStateService>().SingleInstance();
            builder.RegisterType<PlayerActionQueue>().As<IPlayerActionQueue>().SingleInstance();
            builder.RegisterType<AccountService>().As<IAccountService>().SingleInstance();
            builder.RegisterType<Configurator>().As<IConfigurator>().SingleInstance();
            builder.RegisterType<DatabaseService>().As<IDatabaseService>().SingleInstance();
            builder.RegisterType<LoginService>().As<ILoginService>().SingleInstance();
            builder.RegisterType<CharacterCollection>().As<ICharacterCollection>().SingleInstance();
            builder.RegisterType<CharacterPlacementService>().As<ICharacterPlacementService>().SingleInstance();
            builder.RegisterType<EncounterCollection>().As<IEncounterCollection>().SingleInstance();
            builder.RegisterType<PlayerActionExecutor>().As<IPlayerActionExecutor>().SingleInstance();
            builder.RegisterType<EncounterExecutor>().As<IEncounterExecutor>().SingleInstance();
            

            container = builder.Build();
            GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);
        }
    }
}
