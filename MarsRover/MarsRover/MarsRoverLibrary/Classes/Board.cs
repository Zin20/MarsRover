using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MarsRoverLibrary.Interfaces;

namespace MarsRoverLibrary.Classes
{
    public class Board : IBoard
    {


        protected int _xSize { get; set; }
        protected int _ySize { get; set; }

        public int xSize
        {
            get { return _xSize; }
            protected set
            {
                if (!ValidateSize(value))
                    throw new ArgumentException("Invalid Board Size");
                _xSize = value;
            }
        }
        public int ySize
        {
            get { return _ySize; }
            protected set
            {
                if(!ValidateSize(value))
                    throw new ArgumentException("Invalid Board Size");
                _ySize = value;
            }
        }

        private Dictionary<Tuple<int, int>, IVehicle> board;

        public Board(int xSize = 0, int ySize = 0)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            board = new Dictionary<Tuple<int, int>, IVehicle>();

            for(int x = 0; x <= this.xSize; ++x)
                for(int y = 0; y <= this.ySize; ++y)
                    board.Add(new Tuple<int, int>(x,y), null);
        }

        public IVehicle GetVehicleAtCords(int xCord, int yCord)
        {
            if(ValidateCords(xCord,yCord))
                return board[new Tuple<int, int>(xCord,yCord)];
            throw new IndexOutOfRangeException();
            
        }

        public bool MoveVehicleTo(int oldXCord, int oldYCord, int newXCord, int newYCord)
        {

            Tuple<int, int> newCords = new Tuple<int, int>(newXCord, newYCord);
            if (ValidateCords(oldXCord, oldYCord) && ValidateCords(newXCord, newYCord) && board[newCords] == null)
            {
                Tuple<int, int> oldCords = new Tuple<int, int>(oldXCord, oldYCord);
                board[newCords] = board[oldCords];
                board[oldCords] = null;
                return true;
            }
            else
                return false;

        }

        public bool IsVehicleAtCords(int xCord, int yCord)
        {
            return board[new Tuple<int, int>(xCord, yCord)] != null;
        }

        public void PutVehicleOnBoard(IVehicle vehicle)
        {
            if(ValidateCords(vehicle.xCord, vehicle.yCord))
            {
                if(!IsVehicleAtCords(vehicle.xCord, vehicle.yCord))
                    board[new Tuple<int, int>(vehicle.xCord, vehicle.yCord)] = vehicle;
                else
                    throw new ArgumentException("Tried To Place Vehicle Onto Of Other Vehicle");
            }
            else
                throw new IndexOutOfRangeException();

        }

        private bool ValidateSize(int value)
        {
            return value >= 0;
        }

        private bool ValidateCords(int xCord, int yCord)
        {
            if((xCord >= 0 && xCord <= xSize) && (yCord >= 0 && yCord <= ySize))
                return true;
            else
                return false;
        }
    }
}