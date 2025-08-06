using System;
using System.Threading;
using SimpleSnake.Enums;
using SimpleSnake.GameObjects;

namespace SimpleSnake.Core;

public class Engine
{
    private Wall wall;
    private Snake snake;
    
    private Point[] pointsOfDirections;
    private Direction direction;

    private int verticalSleepTime;
    private int horizontalSleepTime;
    public Engine(Wall wall, Snake snake)
    {
        this.wall = wall;
        this.snake = snake;
        pointsOfDirections = new Point[4];
        direction = Direction.Right;
        verticalSleepTime = 180;
        horizontalSleepTime = 110;
    }
    
    public void Run()
    {
        CreateDirections();
        while (true)
        {
            bool isVertical = direction is Direction.Up or Direction.Down;
            
            if (Console.KeyAvailable) 
                GetNextDirection();
            
            bool isMoving = snake.IsMoving(pointsOfDirections[(int)direction]);

            if (!isMoving)
                AskUserToPlayAgain();
            
            Thread.Sleep(isVertical ? verticalSleepTime : horizontalSleepTime);
        }
    }

    private void CreateDirections()
    {
        pointsOfDirections[0] = new Point(1, 0); // Right
        pointsOfDirections[1] = new Point(-1, 0); // Left
        pointsOfDirections[2] = new Point(0, 1); // Down
        pointsOfDirections[3] = new Point(0, -1); // Up
    }

    private void GetNextDirection()
    {
        ConsoleKeyInfo userInput = Console.ReadKey();
        if (userInput.Key == ConsoleKey.LeftArrow)
        {
            if (direction != Direction.Right)
                direction = Direction.Left;
        }
        else if (userInput.Key == ConsoleKey.RightArrow)
        {
            if (direction != Direction.Left)
                direction = Direction.Right;
        }
        else if (userInput.Key == ConsoleKey.UpArrow)
        {
            if (direction != Direction.Down)
                direction = Direction.Up;
        }
        else if (userInput.Key == ConsoleKey.DownArrow)
        {
            if (direction != Direction.Up)
                direction = Direction.Down;
        }
        
        Console.CursorVisible = false; 
    }

    private void AskUserToPlayAgain()
    {
        int left = this.wall.Left + 1;
        int top = 3;

        Console.SetCursorPosition(left / 2 - 5, top * 4 + 1);
        Console.Write("GAME OVER");
        
        Console.SetCursorPosition(left + 4, top + 3);
        Console.Write("Press 'Enter' to play again...");
        Console.SetCursorPosition(left + 4, top + 5);
        Console.Write("Press 'Space' to exit the game.");

        ConsoleKeyInfo userInput = Console.ReadKey();
        while (true)
        {
            if (userInput.Key is ConsoleKey.Enter or ConsoleKey.Spacebar)
                break;
            
            userInput = Console.ReadKey();
        }
        
        Console.Clear();
        if (userInput.Key == ConsoleKey.Enter)
        {
            StartUp.Main();
        }
        else if (userInput.Key == ConsoleKey.Spacebar)
        {
            StopGame();
        }
    }

    private void StopGame()
    {
        Console.SetCursorPosition(20, 2);
        Console.Write("Thanks for playing!");
        Environment.Exit(0);
    }
}