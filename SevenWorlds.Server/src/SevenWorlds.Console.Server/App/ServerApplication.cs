using Autofac;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using SevenWorlds.GameServer.Account;
using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Gameplay.Actions.Base;
using SevenWorlds.GameServer.Gameplay.Actions.Executor;
using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Battle.AI;
using SevenWorlds.GameServer.Gameplay.Battle.Base;
using SevenWorlds.GameServer.Gameplay.Battle.Executor;
using SevenWorlds.GameServer.Gameplay.Battle.Factories;
using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Gameplay.Equipment;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Loop;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Gameplay.Quests;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Talent;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.GameServer.Server;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
using System;
using System.IO;
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
                System.Console.WriteLine("The args did not have Length == 1. The path Env Path must be passed");
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
                    configurator.ReadConfigurations(Path.Combine(configPath, "ServerConfigurations.json"));
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
            builder.RegisterType<GameFactory>().As<IGameFactory>().SingleInstance();
            builder.RegisterType<WorldCollection>().As<IWorldCollection>().SingleInstance();
            builder.RegisterType<SectionCollection>().As<ISectionCollection>().SingleInstance();
            builder.RegisterType<AreaCollection>().As<IAreaCollection>().SingleInstance();
            builder.RegisterType<GameLoopSimulator>().As<IGameLoopSimulator>().SingleInstance();
            builder.RegisterType<PlayerCollection>().As<IPlayerCollection>().SingleInstance();
            builder.RegisterType<HubService>().As<IHubService>().SingleInstance();
            builder.RegisterType<GameStateService>().As<IGameStateService>().SingleInstance();
            builder.RegisterType<PlayerActionCollection>().As<IPlayerActionCollection>().SingleInstance();
            builder.RegisterType<AccountService>().As<IAccountService>().SingleInstance();
            builder.RegisterType<Configurator>().As<IConfigurator>().SingleInstance();
            builder.RegisterType<DatabaseService>().As<IDatabaseService>().SingleInstance();
            builder.RegisterType<LoginService>().As<ILoginService>().SingleInstance();
            builder.RegisterType<CharacterCollection>().As<ICharacterCollection>().SingleInstance();
            builder.RegisterType<CharacterPlacementService>().As<ICharacterPlacementService>().SingleInstance();
            builder.RegisterType<BattleCollection>().As<IBattleCollection>().SingleInstance();
            builder.RegisterType<PlayerActionExecutor>().As<IPlayerActionExecutor>().SingleInstance();
            builder.RegisterType<BattleSimulator>().As<IBattleSimulator>().SingleInstance();
            builder.RegisterType<MonsterDataFactory>().As<IMonsterDataFactory>().SingleInstance();
            builder.RegisterType<BattleFactory>().As<IBattleFactory>().SingleInstance();
            builder.RegisterType<CharacterFactory>().As<ICharacterFactory>().SingleInstance();
            builder.RegisterType<SkillFactory>().As<ISkillFactory>().SingleInstance();
            builder.RegisterType<MonsterAIService>().As<IMonsterAIService>().SingleInstance();
            builder.RegisterType<SkillSimulator>().As<ISkillSimulator>().SingleInstance();
            builder.RegisterType<EquipmentFactory>().As<IEquipmentFactory>().SingleInstance();
            builder.RegisterType<TalentFactory>().As<ITalentFactory>().SingleInstance();
            builder.RegisterType<QuestFactory>().As<IQuestFactory>().SingleInstance();
            builder.RegisterType<QuestGiver>().As<IQuestGiver>().SingleInstance();


            container = builder.Build();
            GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);
        }
    }
}
