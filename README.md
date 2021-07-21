# MarsRover
Design:
With IVehicle I wanted to make it so if in the future we wanted to add another type of vehicle it would be easy to do. As far as all the classes that implement the interface all they should worry about is where they are and how they handle their own movement. This allows for classes that implement IVehicle to be pretty module when it comes to the design since it doesn't have to worry about where on the board it is just where it thinks it is. 

IBoard is designed such that anything that implements it will be able to decide what data structures it wishes to use for the board and implement specific logic when it comes to if you should place a IVehicle onto another or if you can move a IVehicle onto another. It also allows in the future if we wanted to add the entire x,y cords (negatives and so fourth) it would be able to do so. 

IBoardController is meant to be the communication between IBoard and IVehicles while handling the logic for if a command is a valid one. IBoardController is meant to be the business logic behind most of the application. 

While designing this application I was trying to hold true to TDD (Test Driven Development) such that as I wrote code I had a way of making sure that I didn't break anything with new changes to the code base. I also tried to keep functions small and their names make sense so it is easier on the reader such that it makes sense when you read the code from a high or even low level. 

Assumptions:
Each command would come in as capital characters
We evaluate each rover in sequence and once we have evaluated one we move onto adding the new one to the board/running their commands. Rover 1 will run to complete then rover 2 will be placed and run to completion.
If a rover attempts to move into another rover then it simply doesn't move and will continue to run the commands until completion. 
If a rover attempts to move outside of the bounds of the board I will just have the command not run and then continue to run commands to completion. 
If attempting to give a upper right cord to the board I will throw a argumentexception since that is invalid size for the board.
If attempting to get a vehicle outside of the bounds of the board I will throw an IndexOutOfRangeException.
If any commands even have invalid aspects of them I will throw an exception.

Note: Use the console application as of writing. I'm still working on the GUI as a side project for myself. 
