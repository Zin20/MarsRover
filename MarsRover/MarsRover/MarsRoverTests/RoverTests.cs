using System;
using MarsRoverLibrary.Classes;
using NUnit.Framework;

namespace MarsRoverTests
{
    public class RoverTests
    {
        private Rover testRover;
        [SetUp]
        public void Setup()
        {
            testRover = new Rover();
        }

        [Test]
        public void Rover_CheckDefaultConstructor()
        {
            Assert.AreEqual(0, testRover.xCord);
            Assert.AreEqual(0, testRover.yCord);
            Assert.AreEqual("N", testRover.cardinalDirection);
        }

        [Test]
        public void Rover_CheckValuesConstructor([Values(-1, 0, 1)] int xInput, [Values(-1, 0, 1)] int yInput, [Values("N","E","S","W")] string cardinalInput)
        {
            testRover = new Rover(xInput, yInput, cardinalInput);

            Assert.AreEqual(xInput, testRover.xCord);
            Assert.AreEqual(yInput, testRover.yCord);
            Assert.AreEqual(cardinalInput, testRover.cardinalDirection);
        }

        [Test]
        public void Rover_Constructor_InvalidCardinalDirection()
        {
            Assert.Throws<ArgumentException>(() => { testRover = new Rover(0, 0, "Z"); });
        }

        [Test]
        public void Rover_MoveNorthOnce()
        {
            testRover.Move();
            Assert.AreEqual(0, testRover.xCord);
            Assert.AreEqual(1, testRover.yCord);
        }

        [Test]
        public void Rover_MoveEastOnce()
        {
            testRover = new Rover(1, 1, "E");
            testRover.Move();
            Assert.AreEqual(2, testRover.xCord);
            Assert.AreEqual(1, testRover.yCord);
        }


        [Test]
        public void Rover_MoveSouthOnce()
        {
            testRover = new Rover(1, 1, "S");
            testRover.Move();
            Assert.AreEqual(1, testRover.xCord);
            Assert.AreEqual(0, testRover.yCord);
        }

        [Test]
        public void Rover_MoveWestOnce()
        {
            testRover = new Rover(1, 1, "W");
            testRover.Move();
            Assert.AreEqual(0, testRover.xCord);
            Assert.AreEqual(1, testRover.yCord);
        }

        [Test]
        public void Rover_RotateVehicleLeft()
        {
            testRover.ChangeDirection('L');
            Assert.AreEqual("W", testRover.cardinalDirection);
            testRover = new Rover(1, 1,"E");
            
            testRover.ChangeDirection('L');
            Assert.AreEqual("N", testRover.cardinalDirection);
        }

        [Test]
        public void Rover_RotateVehicleRight()
        {
            testRover.ChangeDirection('R');
            Assert.AreEqual("E", testRover.cardinalDirection);

            testRover = new Rover(1, 1, "W");
            testRover.ChangeDirection('R');
            Assert.AreEqual("N", testRover.cardinalDirection);
        }

        [Test]
        public void Rover_ChangeToInValidDirection()
        {
            Assert.Throws<ArgumentException>(() => { testRover.ChangeDirection('Z'); });
        }

        [Test]
        public void Rover_ChangeCardinalDirection_InvalidObject()
        {
            Assert.Throws<ArgumentException>(() => { testRover.ChangeDirection(5); });
            Assert.Throws<ArgumentException>(() => { testRover.ChangeDirection("Potato"); });
        }
    }
}