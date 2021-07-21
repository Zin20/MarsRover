using System;
using MarsRoverLibrary.Classes;
using MarsRoverLibrary.Interfaces;
using NUnit.Framework;

namespace MarsRoverTests
{
    [TestFixture]
    public class BoardTests
    {
        private Board testBoard;
        private IVehicle testVehicle;

        [SetUp]
        public void Setup()
        {
            testBoard = new Board(5,5);
        }

        [Test]
        public void Board_ConstructorInvalidSize([Values(-2, -1)]int xSize, [Values(-2, -1)] int ySize)
        { 
            Assert.Throws<ArgumentException>(() => testBoard = new Board(xSize, ySize));
        }

        [Test]
        public void Board_Constructor_CreateBoardSizeOfOneCell()
        {
            testBoard = new Board(0, 0);
            Assert.Null(testBoard.GetVehicleAtCords(0, 0));
        }
        [Test]
        public void Board_GetNoVehicleAtCords()
        {
            IVehicle vehicle = testBoard.GetVehicleAtCords(0, 0);
            Assert.AreEqual(null, vehicle);
        }

        [Test]
        public void Board_MoveRoverThenCheckRoverAtCords()
        {
            
            testVehicle = new Rover(0,0);
            testBoard.PutVehicleOnBoard(testVehicle);
            testBoard.MoveVehicleTo(testVehicle.xCord, testVehicle.yCord, 1, 1);
            Assert.AreNotEqual(null, testBoard.GetVehicleAtCords(1,1));
        }

        [Test]
        public void Board_MoveRoverOnTopOfOtherRover()
        {

            testVehicle = new Rover(0, 0);
            testBoard.PutVehicleOnBoard(testVehicle);
            IVehicle tempVehicle = new Rover(1, 1);
            testBoard.PutVehicleOnBoard(tempVehicle);
            Assert.False(testBoard.MoveVehicleTo(testVehicle.xCord, testVehicle.yCord, 1, 1));
        }

        [Test]
        public void Board_CheckBoardSize()
        {
            Assert.AreEqual(5, testBoard.xSize);
            Assert.AreEqual(5, testBoard.ySize);
        }

        [Test]
        public void Board_PutVehicleOnBoardThenCompareVehicles()
        {
            testVehicle = new Rover(1, 1, "E");

            testBoard.PutVehicleOnBoard(testVehicle);
            Assert.AreEqual(testVehicle, testBoard.GetVehicleAtCords(testVehicle.xCord, testVehicle.yCord));
        }

        [Test]
        public void Board_PutVehicleOnBoard_CheckIfVehicleAtCords()
        {
            testVehicle = new Rover(2, 3);
            testBoard.PutVehicleOnBoard(testVehicle);
            Assert.True(testBoard.IsVehicleAtCords(testVehicle.xCord, testVehicle.yCord));
        }

        [Test]
        public void Board_PutVehicleOnBoardWithValidCords([Values(0, 5)] int xCord, [Values(0, 5)] int yCord)
        {
            testVehicle = new Rover(xCord, yCord);
            testBoard.PutVehicleOnBoard(testVehicle);
            Assert.AreEqual(testVehicle, testBoard.GetVehicleAtCords(xCord, yCord));
        }


        [Test]
        public void Board_PutVehicleOnBoardWithInvalidCords([Values(-1, 9)] int xCord, [Values(-1, 9)] int yCord)
        {
            testVehicle = new Rover(xCord, yCord);
            Assert.Throws<IndexOutOfRangeException>(() => testBoard.PutVehicleOnBoard(testVehicle));
        }

        [Test]
        public void Board_PutVehicleOnBoard_ThenTryToPutVehicleOnTopOf()
        {
            testVehicle = new Rover(0, 0);
            testBoard.PutVehicleOnBoard(testVehicle);
            IVehicle tempVehicle = new Rover(0, 0);
            Assert.Throws<ArgumentException>(()=>testBoard.PutVehicleOnBoard(tempVehicle));
        }

        [Test]
        public void Board_PutVehicleOnBoard_MoveWithInvalidCords([Values(-1, 9)] int xCord, [Values(-1, 9)] int yCord)
        {
            testVehicle = new Rover(4, 4);
            Assert.False(testBoard.MoveVehicleTo(testVehicle.xCord, testVehicle.yCord, xCord, yCord));
        }

        [Test]
        public void Board_GetVehicleAtInvalidCords([Values(-1,9)]int xCord, [Values(-1,9)] int yCord)
        {
            Assert.Throws<IndexOutOfRangeException>(()=>testBoard.GetVehicleAtCords(xCord, yCord));
        }


    }
}