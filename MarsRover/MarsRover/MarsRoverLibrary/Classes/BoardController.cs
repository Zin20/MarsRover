using System;
using System.Collections.Generic;
using System.Linq;
using MarsRoverLibrary.Interfaces;

namespace MarsRoverLibrary.Classes
{
    public class BoardController : IBoardController
    {
        private IBoard board;
        private List<IVehicle> listOfVehicles;

        public BoardController()
        {
            listOfVehicles = new List<IVehicle>();
        }

        public List<string> ProcessCommandLine(string commandLine)
        {
            List<string> output = new List<string>();
            try
            {

                if(ValidateCommandLine(commandLine))
                {
                    List<string> commands = commandLine.Split("\n").ToList();

                    ProcessCreateBoardCommand(commands[0]);
                    IVehicle currentVehicle = null;

                    for (int index = 1; index < commands.Count; ++index)
                    {
                        if(!String.IsNullOrEmpty(commands[index]))
                        {
                            if(index % 2 == 0)
                                ProcessVehicleMovementCommands(currentVehicle, commands[index]);
                            else
                                currentVehicle = ProcessPutOnBoardVehicleCommands(commands[index]);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }

            output = ProcessVehicleOutput();

            return output;
        }

        private List<string> ProcessVehicleOutput()
        {
            List<string> output = new List<string>();
            foreach(var vehicle in listOfVehicles)
                output.Add(vehicle.xCord + " " + vehicle.yCord + " " + vehicle.cardinalDirection);

            return output;
        }

        private void ProcessCreateBoardCommand(string createBoardCommand)
        {
            List<int> boardSize = createBoardCommand.Split(" ").Select(x => Convert.ToInt32(x)).ToList();
            board = new Board(boardSize[0], boardSize[1]);
        }
        private IVehicle ProcessPutOnBoardVehicleCommands(string vehicleCommands)
        {

            List<string> parsedVehicleCommands = vehicleCommands.Split(" ").ToList();
            Rover tempRover = new Rover(Convert.ToInt32(parsedVehicleCommands[0]), Convert.ToInt32(parsedVehicleCommands[1]), parsedVehicleCommands[2]);
            listOfVehicles.Add(tempRover);
            board.PutVehicleOnBoard(tempRover);

            return tempRover;
        }

        private void ProcessVehicleMovementCommands(IVehicle currentVehicle, string movementCommands)
        {
            foreach(char currentMovementCommand in movementCommands)
            {
                if(currentMovementCommand == 'L' || currentMovementCommand == 'R')
                    ProcessRotationCommand(currentVehicle, currentMovementCommand);
                else if(currentMovementCommand == 'M')
                    ProcessMovement(currentVehicle);
                else
                    throw new ArgumentException("Invalid Vehicle Movement Command");
            }
        }

      
        private void ProcessRotationCommand(IVehicle currentVehicle, char currentMovementCommand)
        {
            currentVehicle.ChangeDirection(currentMovementCommand);
        }
        private void ProcessMovement(IVehicle currentVehicle)
        {
            Tuple<int, int> futureCords = currentVehicle.GetMoveCords();
            bool didMove = board.MoveVehicleTo(currentVehicle.xCord, currentVehicle.yCord, futureCords.Item1, futureCords.Item2);
            if(didMove)
                currentVehicle.Move();
        }


        private bool ValidateCommandLine(string commandLine)
        {

            if (string.IsNullOrEmpty(commandLine))
                throw new ArgumentException("Command Line is Empty or Null");

            List<string> parsedCommands = commandLine.Split("\n").ToList();

            if (parsedCommands.Count < 3)
                throw new ArgumentException("Incomplete Command Line");

            return true;
        }
    }
}