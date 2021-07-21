using System;
using System.Collections.Generic;
using MarsRoverLibrary.Interfaces;

namespace MarsRoverLibrary.Classes
{
    public class Rover : IVehicle
    {
        
        protected int _xCord { get; set; }
        protected int _yCord { get; set; }

        public int xCord
        {
            get { return _xCord;}
            protected set { _xCord = value; }
        }
        public int yCord
        {
            get { return _yCord; }
            protected set { _yCord = value; }
        }

        private string _cardinalDirection;
        public string cardinalDirection
        {
            get { return _cardinalDirection; }
            private set
            {
                if(!ValidateCardinalDirection(value))
                    throw new ArgumentException("Invalid Cardinal Direction");
                _cardinalDirection = value;
            }
        }

  

        private readonly List<string> allValidCardinalDirections = new List<string> {"N", "E", "S", "W"};


        public Rover(int xCord = 0, int yCord = 0, string cardinalDirection = "N")
        {
            this.xCord = xCord;
            this.yCord = yCord;
            this.cardinalDirection = cardinalDirection;
        }



        public void ChangeDirection(object rotationDirection)
        {
            if(!(rotationDirection is char))
                throw new ArgumentException("Invalid Object Type"); 
            
            if(ValidateRotation((char) rotationDirection))
            {
                int cardinalDirectionIndex = allValidCardinalDirections.IndexOf(cardinalDirection);
                if((char)rotationDirection == 'L')
                {
                    if(cardinalDirectionIndex == 0)
                        cardinalDirectionIndex = allValidCardinalDirections.Count-1;
                    else
                        --cardinalDirectionIndex;

                }
                else
                {
                    if(cardinalDirectionIndex == allValidCardinalDirections.Count - 1)
                        cardinalDirectionIndex = 0;
                    else
                        ++cardinalDirectionIndex;
                }

                cardinalDirection = allValidCardinalDirections[cardinalDirectionIndex];
            }
            else
                throw new ArgumentException("Invalid Rotation Command");
      

        }

        public void Move()
        {
            if (cardinalDirection == "N")
                ++yCord;
            else if (cardinalDirection == "S")
                --yCord;
            else if (cardinalDirection == "E")
                ++xCord;
            else if (cardinalDirection == "W")
                --xCord;
        }

        public Tuple<int, int> GetMoveCords()
        {
            int tempXCord = xCord;
            int tempYCord = yCord;

            if(cardinalDirection == "N")
                ++tempYCord;
            else if(cardinalDirection == "S")
                --tempYCord;
            else if(cardinalDirection == "E")
                ++tempXCord;
            else if(cardinalDirection == "W")
                --tempXCord;

            return new Tuple<int, int>(tempXCord, tempYCord);
        }

        private bool ValidateCardinalDirection(string input)
        {
            return allValidCardinalDirections.Contains(input);
        }

        private bool ValidateRotation(char input)
        {
            return input == 'R' || input == 'L';
        }
    }

   
}
