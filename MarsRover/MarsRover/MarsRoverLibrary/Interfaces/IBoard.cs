namespace MarsRoverLibrary.Interfaces
{
    public interface IBoard
    {
        public int xSize { get; }
        public int ySize { get; }

        public IVehicle GetVehicleAtCords(int xCord, int yCord);
        public bool MoveVehicleTo(int oldXCord, int oldYCord, int newXCord, int newYCord);
        public bool IsVehicleAtCords(int xCord, int yCord);

        public void PutVehicleOnBoard(IVehicle vehicle);
    }
}