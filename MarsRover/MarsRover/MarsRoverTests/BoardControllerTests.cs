using System;
using System.Collections.Generic;
using MarsRoverLibrary.Classes;
using NUnit.Framework;

namespace MarsRoverTests
{
    [TestFixture]
    public class BoardControllerTests
    {
        private BoardController testBoardController;
        [SetUp]
        public void Setup()
        {
            testBoardController = new BoardController();
        }

        [Test]
        public void BoardController_ProcessCommandLine()
        {
            string commandLine = "5 5\n1 2 N\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM\n";

            List<string> output = testBoardController.ProcessCommandLine(commandLine);

            Assert.AreEqual("1 3 N", output[0]);
            Assert.AreEqual("5 1 E", output[1]);
        }

        [Test]
        public void BoardController_ProcessCommandLine_NoMovementForSecondVehicle()
        {
            string commandLine = "5 5\n1 2 N\nLMLMLMLMM\n3 3 E\n\n";

            List<string> output = testBoardController.ProcessCommandLine(commandLine);

            Assert.AreEqual("1 3 N", output[0]);
            Assert.AreEqual("3 3 E", output[1]);
        }

        [Test]
        public void BoardController_ProcessCommandLine_NoMovementForFirstVehicle()
        {
            string commandLine = "5 5\n1 2 N\n\n3 3 E\nMMRMMRMRRM\n";

            List<string> output = testBoardController.ProcessCommandLine(commandLine);

            Assert.AreEqual("1 2 N", output[0]);
            Assert.AreEqual("5 1 E", output[1]);
        }

        [Test]
        public void BoardController_ProcessCommandLine_VehicleAttemptsToGoOutsideBounds()
        {
            string commandLine = "2 2\n0 0 N\nLM\n";

            List<string> output = testBoardController.ProcessCommandLine(commandLine);

            Assert.AreEqual("0 0 W", output[0]);
        }

        [Test]
        public void BoardController_ProcessCommandLine_VehicleMove_CommandTryToPutNewVehicleAtCords()
        {
            string commandLine = "2 2\n0 0 N\nLLLM\n1 0 N\nRRRM\n";

            Assert.Throws<ArgumentException>(()=> testBoardController.ProcessCommandLine(commandLine));
        }

        [Test]
        public void BoardController_ProcessCommandLine_VehicleAttemptsToGoOnTopOfOtherVehicle()
        {
            string commandLine = "2 2\n0 0 N\nLLL\n1 0 N\nRRRM\n";

            List<string> output = testBoardController.ProcessCommandLine(commandLine);

            Assert.AreEqual("0 0 E", output[0]);
            Assert.AreEqual("1 0 W", output[1]);
        }

        [Test]
        public void BoardController_ProcessNoGivenCommandLine()
        {
            string commandLine = "";
            Assert.Throws<ArgumentException>(()=> testBoardController.ProcessCommandLine(commandLine));
        }

        [Test]
        public void BoardController_ProcessIncompleteCommandLine()
        {
            string commandLine = "5 5\n1 2 N";
            Assert.Throws<ArgumentException>(() => testBoardController.ProcessCommandLine(commandLine));
        }

        [Test]
        public void BoardController_ProcessInvalidCommandLine_InvalidBoardSize()
        {
            string commandLine = "5 -2\n1 2 N\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM\n";

            Assert.Throws<ArgumentException>(() => testBoardController.ProcessCommandLine(commandLine));
        }


        [Test]
        public void BoardController_ProcessInvalidCommandLine_InvalidVehicleLocation()
        {
            string commandLine = "5 5\n1 10 N\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM\n";

            Assert.Throws<IndexOutOfRangeException>(() => testBoardController.ProcessCommandLine(commandLine));
        }


        [Test]
        public void BoardController_ProcessInvalidCommandLine_InvalidMovementCommand()
        {
            string commandLine = "5 5\n1 2 N\nLMLMLMLMM\n3 3 E\nMMRMMZMRRM\n";

            Assert.Throws<ArgumentException>(() => testBoardController.ProcessCommandLine(commandLine));
        }
    }
}