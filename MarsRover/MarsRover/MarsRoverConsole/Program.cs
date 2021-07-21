using System;
using System.Collections.Generic;
using MarsRoverLibrary.Classes;

namespace MarsRoverConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BoardController boardController = new BoardController();
            while(true)
            {
                try
                {
                    Console.WriteLine("Please Enter the top right cord for the board (EG:5 5):");
                    string commandLine = Console.ReadLine() + "\n";
                    commandLine = HandleRoverCommands(commandLine);
                    List<string> output = boardController.ProcessCommandLine(commandLine);
                    PrintOutput(output);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
                }

                Console.WriteLine("Run Another Simulation? (Y)es/(N)o");
                string confirmationCommand = Console.ReadLine();

                if (confirmationCommand.Equals("No", StringComparison.InvariantCultureIgnoreCase)
                    || confirmationCommand.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }
            }
           
            Console.WriteLine("Close Program");
            Console.ReadLine();
        }

        private static void PrintOutput(List<string> output)
        {
            Console.WriteLine("Output: ");
            foreach(string currentOutput in output)
                Console.WriteLine(currentOutput);
        }

        private static string HandleRoverCommands(string commandLine)
        {
            string currentCommand;
            bool isNextCommandAVehicleCommand = true;

            while (true)
            {
                if(isNextCommandAVehicleCommand)
                {
                    Console.WriteLine(
                        "Please enter coordinates and facing for new rover (EG: 1 2 N) or use the (C)ancel command:");
                    isNextCommandAVehicleCommand = false;
                }
                else
                {
                    Console.WriteLine("Please enter commands for the rover (EG: LMLMLMLMM) or use the (C)ancel command:");
                    isNextCommandAVehicleCommand = true;
                }

                currentCommand = Console.ReadLine();
                if(currentCommand.Equals("Cancel", StringComparison.InvariantCultureIgnoreCase)
                   || currentCommand.Equals("C", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }

                commandLine += currentCommand + "\n";
            }

            return commandLine;
        }
    }
}
