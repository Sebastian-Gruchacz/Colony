namespace ColonyModel.Testing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Colony.Model;
    using Colony.Model.Core;
    using Colony.Model.Expedition;
    using Colony.Model.Fight;
    using NUnit.Framework;
    using Colony.Model.Printers;
    using Colony.Model.Resources;
    using Colony.Model.Units;

    public class ExpeditionBalancing
    {
        private static ExpeditionLogic ObtainExpeditionLogic(GameState agameState = null)
        {
            var randomProvider = new RandomProvider();
            var gameState = agameState ?? new GameState();
            var unitLogic = new UnitLogic(gameState, randomProvider);
            var fightLogic = new FightLogic(randomProvider, unitLogic);
            var expeditionLogic = new ExpeditionLogic(randomProvider, unitLogic, fightLogic);
            return expeditionLogic;
        }

        [Test]
        public void TestSimpleOneRoundExpeditionWithOneWorker()
        {
            var sendUnits = new UnitCollection(new UnitAmount(UnitInfo.WorkerUnitType, 1));
            ExpeditionData data = new ExpeditionData
            {
                Owner = null, // not important
                StartingRound = 1,
                EndingRound = 1,
                AssignedUnits = sendUnits,
                RemainingUnits = sendUnits.Clone(),
                CurrentRange = 0.0m,
                DiscoveredResources = new ResourceCollection()
            };

            ExpeditionLogic expeditionLogic = ObtainExpeditionLogic();

            var statePrinter = StatePrinters.For(data);

            statePrinter.PrintState(data, Console.Out);
            expeditionLogic.ProcessExpedition(data, 1);
            statePrinter.PrintState(data, Console.Out);
        }

       

        [Test]
        public void TestSimpleOneRoundExpeditionWithOneSoldier()
        {
            var sendUnits = new UnitCollection(new UnitAmount(UnitInfo.SoldierUnitType, 1));
            ExpeditionData data = new ExpeditionData
            {
                Owner = null, // not important
                StartingRound = 1,
                EndingRound = 1,
                AssignedUnits = sendUnits,
                RemainingUnits = sendUnits.Clone(),
                CurrentRange = 0.0m,
                DiscoveredResources = new ResourceCollection()
            };

            ExpeditionLogic expeditionLogic = ObtainExpeditionLogic();

            var statePrinter = StatePrinters.For(data);

            statePrinter.PrintState(data, Console.Out);
            expeditionLogic.ProcessExpedition(data, 1);
            statePrinter.PrintState(data, Console.Out);
        }

        [Test]
        public void TestSimpleOneRoundExpeditionWithOneScientist()
        {
            var sendUnits = new UnitCollection(new UnitAmount(UnitInfo.ScientistUnitType, 1));
            ExpeditionData data = new ExpeditionData
            {
                Owner = null, // not important
                StartingRound = 1,
                EndingRound = 1,
                AssignedUnits = sendUnits,
                RemainingUnits = sendUnits.Clone(),
                CurrentRange = 0.0m,
                DiscoveredResources = new ResourceCollection()
            };

            ExpeditionLogic expeditionLogic = ObtainExpeditionLogic();

            var statePrinter = StatePrinters.For(data);

            statePrinter.PrintState(data, Console.Out);
            expeditionLogic.ProcessExpedition(data, 1);
            statePrinter.PrintState(data, Console.Out);
        }

        [Test]
        public void TestSimpleOneRoundExpeditionWithOneOfAllUnits()
        {
            var sendUnits = new UnitCollection(
                new UnitAmount(UnitInfo.WorkerUnitType, 1),
                new UnitAmount(UnitInfo.SoldierUnitType, 1),
                new UnitAmount(UnitInfo.ScientistUnitType, 1));
            ExpeditionData data = new ExpeditionData
            {
                Owner = null, // not important
                StartingRound = 1,
                EndingRound = 1,
                AssignedUnits = sendUnits,
                RemainingUnits = sendUnits.Clone(),
                CurrentRange = 0.0m,
                DiscoveredResources = new ResourceCollection()
            };

            ExpeditionLogic expeditionLogic = ObtainExpeditionLogic();

            var statePrinter = StatePrinters.For(data);

            statePrinter.PrintState(data, Console.Out);
            expeditionLogic.ProcessExpedition(data, 1);
            statePrinter.PrintState(data, Console.Out);
        }

        [Test]
        public void TestSimpleOneRoundExpeditionWithTenOfAllUnits()
        {
            var sendUnits = new UnitCollection(
                new UnitAmount(UnitInfo.WorkerUnitType, 10),
                new UnitAmount(UnitInfo.SoldierUnitType, 10),
                new UnitAmount(UnitInfo.ScientistUnitType, 10));
            ExpeditionData data = new ExpeditionData
            {
                Owner = null, // not important
                StartingRound = 1,
                EndingRound = 1,
                AssignedUnits = sendUnits,
                RemainingUnits = sendUnits.Clone(),
                CurrentRange = 0.0m,
                DiscoveredResources = new ResourceCollection()
            };

            ExpeditionLogic expeditionLogic = ObtainExpeditionLogic();

            var statePrinter = StatePrinters.For(data);

            statePrinter.PrintState(data, Console.Out);
            expeditionLogic.ProcessExpedition(data, 1);
            statePrinter.PrintState(data, Console.Out);
        }

        [Test]
        public void TestThreeRoundExpeditionWithTenOfAllUnits()
        {
            var sendUnits = new UnitCollection(
                new UnitAmount(UnitInfo.WorkerUnitType, 10),
                new UnitAmount(UnitInfo.SoldierUnitType, 10),
                new UnitAmount(UnitInfo.ScientistUnitType, 10));
            ExpeditionData data = new ExpeditionData
            {
                Owner = null, // not important
                StartingRound = 1,
                EndingRound = 3,
                AssignedUnits = sendUnits,
                RemainingUnits = sendUnits.Clone(),
                CurrentRange = 0.0m,
                DiscoveredResources = new ResourceCollection()
            };

            var gameState = new GameState();
            ExpeditionLogic expeditionLogic = ObtainExpeditionLogic(gameState);

            var enemy1 = new Player("Nosgoh", PlayerType.Enemy);
            enemy1.BaseUnits = new UnitCollection(
                new UnitAmount(UnitInfo.SoldierUnitType, 40),
                new UnitAmount(UnitInfo.WargUnitType, 45));
            gameState.AddPlayer(enemy1);


            var statePrinter = StatePrinters.For(data);

            statePrinter.PrintState(data, Console.Out);
            expeditionLogic.ProcessExpedition(data, 1);
            statePrinter.PrintState(data, Console.Out);
            expeditionLogic.ProcessExpedition(data, 2);
            statePrinter.PrintState(data, Console.Out);
            expeditionLogic.ProcessExpedition(data, 3);
            statePrinter.PrintState(data, Console.Out);
        }
    }
}
