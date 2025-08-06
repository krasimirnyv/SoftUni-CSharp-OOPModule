using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSnake.GameObjects;

public abstract class Food : Point
{
    private Wall wall;
    private Random random;
    private char foodSymbol;
    
    protected Food(Wall wall, char foodSymbol, int points) 
        : base(wall.Left, wall.Top)
    {
        this.wall = wall;
        this.foodSymbol = foodSymbol;
        FoodPoints = points;
        random = new Random();
    }
    
    public int FoodPoints { get; private set; }

    public void SetRandomPosition(Queue<Point> snake)
    {
        do
        {
            Left = random.Next(2, wall.Left - 2);
            Top = random.Next(2, wall.Top - 2);
        } while (snake.Any(p => p.Left == Left && p.Top == Top));
        
        Console.BackgroundColor = ConsoleColor.Red;
        this.Draw(foodSymbol);
        Console.BackgroundColor = ConsoleColor.White;
    }

    public bool IsFoodPoint(Point snake)
        => snake.Top == Top && snake.Left == Left;
}