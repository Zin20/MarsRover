using System;

namespace MarsRoverLibrary.Interfaces
{
    public interface IVehicle
    {
        public int xCord { get; }
        public int yCord { get; }
        public string cardinalDirection { get; }
        public void Move();
        public Tuple<int,int> GetMoveCords();
        public void ChangeDirection(object rotationDirection);


    }
}