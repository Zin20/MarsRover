using System.Collections.Generic;

namespace MarsRoverLibrary.Interfaces
{
    public interface IBoardController
    { 
        public List<string> ProcessCommandLine(string commandLine);
    }
}