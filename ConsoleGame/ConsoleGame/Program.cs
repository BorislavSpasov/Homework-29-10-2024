//Area Creation
int areaX, areaY;
areaX = 20;
areaY = 10;

//Creating top and bottom of playing area
for (int i = 0; i < areaX+2; i++)
{
    Console.SetCursorPosition(i, 0);
    Console.Write("-");
    Console.SetCursorPosition(i, areaY+1);
    Console.Write("-");
}
//Creating Left and Right side of playing area
for (int i = 0; i < areaY; i++)
{
    Console.SetCursorPosition(0, i+1);
    Console.Write("|");
    Console.SetCursorPosition(areaX+1, i+1);
    Console.Write("|");
}

//Treats and Traps location Arrays
int[,] trapLoc = new int[areaX+2, areaY+2];
int[,] treatLoc = new int[areaX+2, areaY+2];

//Giving values for treats and traps
int trapX, trapY, treatX, treatY;
bool mineNear = false;

int numTraps = 100;
int numTreats = (areaX * areaY) /5;

//Treats and Traps location displaying and inputting into arrays
Random rand = new Random();
for (int i = 0; i < numTraps; i++)
{
    mineNear = false;
    trapX = rand.Next(1, areaX + 1);
    trapY = rand.Next(1, areaY + 1);
    //Checking for other mines in a 3x3 grid to see if it can place a mine
    for(int checkX = -1; checkX <= 1; checkX++)
    {
        for(int checkY = -1; checkY <= 1; checkY++)
        {
            if (trapLoc[(checkX + trapX),(checkY + trapY)] == 1)
            {
                mineNear = true;
                break;
            }
        }
        if (mineNear) break;
    }
    //Placing a mine if there is enough space
    if (!mineNear)
    {
        trapLoc[trapX, trapY] = 1;
        Console.SetCursorPosition(trapX, trapY);
        Console.Write("x");
    }
}
for (int i = 0; i < numTreats; i++)
{
    treatX = rand.Next(1, areaX + 1);
    treatY = rand.Next(1, areaY + 1);
    treatLoc[treatX, treatY] = 1;
    //Changing the value of a trap location so you dont have both a trap and treat on the same square
    trapLoc[treatX, treatY] = 0;

    Console.SetCursorPosition(treatX, treatY);
    Console.Write("@");
}

//Ending position
int exitX = areaX - rand.Next(10,areaY), exitY = areaY+1;
if (trapLoc[exitX, exitY-1] == 1)
{
    //Removing a trap if there is one right ontop of the exit
    trapLoc[exitX, exitY - 1] = 0;
    Console.SetCursorPosition(exitX, exitY-1);
    Console.Write(" ");
}
Console.SetCursorPosition(exitX, exitY);
Console.Write('E');


//Default Player Values

char player = 'H';
int playerX = 1;
int playerY = 1;
Console.SetCursorPosition(playerX, playerY);

//Changing the value of treat and trap location so they are not on the default spawn point of the player
trapLoc[playerX, playerY] = 0;
treatLoc[playerX, playerY] = 0;
Console.Write(player);
Console.CursorVisible = false;

int treatCounter = 0;

//Loop for movement
while (true)
{
    //Checking for exceptions
    try
    {
        Console.SetCursorPosition(playerX, playerY);
        //Adding to treats counter
        if (treatLoc[playerX, playerY] == 1)
        {
            treatCounter++;
            //Sound for picking up treat
            Console.Beep(740, 7);
            treatLoc[playerX, playerY] = 0;
        }
        //Check for game over conditions
        else if (trapLoc[playerX, playerY] == 1)
        {
            Console.Clear();
            Console.WriteLine("Game Over! Try Again!");
            Console.Beep(600, 120);
            Console.Beep(500, 120);
            Console.Beep(400, 120);
            Console.Beep(200, 400);
            return;
        }
    }
    catch (IndexOutOfRangeException) { }

    //Check if player has won
    if (playerX == exitX && playerY == exitY)
    {
        Console.Clear();
        Console.WriteLine($"You win!!!! You collected {treatCounter} treats!");
        //Song for winning
        for (int j = 0; j < 2; j++)
        {
            for (int k = 0; k < 3; k++)
            {
                Console.Beep(850, 100);
            }
            Console.Beep(750, 600);
            Console.Beep(1040, 400);
        }
        Thread.Sleep(500); 
        for (int j = 0; j < 2; j++) Console.Beep(950, 100);
        for (int j = 0; j < 2; j++) Console.Beep(850, 100);
        for (int j = 0; j < 2; j++) Console.Beep(780, 100);
        Console.Beep(650, 500);
        Thread.Sleep(500);
        return;
    }

    //Check for input
    switch (Console.ReadKey().Key)
    {
        //Arrow left
        case ConsoleKey.LeftArrow:
            //Out of bounds check
            if (playerX != 1)
            {
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(" ");
                playerX--;
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(player);
                Console.Beep(250, 40);
            }
            break;

        //Arrown Right
        case ConsoleKey.RightArrow:
            //Out of bounds check
            if (playerX != areaX)
            {
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(" ");
                playerX++;
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(player);
                Console.Beep(250, 40);
            }
            break;

        //Arrow Up
        case ConsoleKey.UpArrow:
            //Out of bounds check
            if (playerY != 1)
            {
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(" ");
                playerY--;
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(player);
                Console.Beep(250, 40);
            }
            break;

        //Arrow Down
        case ConsoleKey.DownArrow:
            //Out of bounds check + Exception for allowing movement on the End Tile
            if (playerY != areaY || playerX == exitX)
            {
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(" ");
                playerY++;
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(player);
                Console.Beep(250, 40);
            }
            break;

        //Default value if player inputs anything other than arrow keys
        default:
            Console.SetCursorPosition(playerX, playerY);
            Console.Write("H");
            break;
    }
}