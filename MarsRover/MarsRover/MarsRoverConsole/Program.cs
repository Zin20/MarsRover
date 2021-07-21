using System;

namespace MarsRoverConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please Enter the top right cord for the board (EG:5 5):");



                Console.ReadLine();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
