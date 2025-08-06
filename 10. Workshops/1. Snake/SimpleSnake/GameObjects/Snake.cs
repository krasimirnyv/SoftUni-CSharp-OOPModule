using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSnake.GameObjects;

public class Snake
{
    private const char SnakeSymbol = '*';
    private const char EmptySpaceSymbol = ' ';
    
    private readonly Queue<Point> _snake;
    private Wall wall;
    
    private int nextLeft;
    private int nextTop;
    
    private Food[] food;
    private int foodIndex;
    private int points;
    
    public Snake(Wall wall) 
    {
        this.wall = wall;
        _snake = new Queue<Point>();
        food = new Food[3];
        foodIndex = randomFoodNumber;
        points = 0;
        GetFood();
        CreateSnake();
        CreateFood();
    }
    
    private int randomFoodNumber => new Random().Next(0, food.Length);

    private void CreateSnake()
    {
        for (int top = 1; top <= 6; top++)
            _snake.Enqueue(new Point(2, top));
    }

    private void CreateFood()
        => food[foodIndex].SetRandomPosition(_snake);

    private void GetFood()
    {
        food[0] = new FoodHash(wall);
        food[1] = new FoodDollar(wall);
        food[2] = new FoodAsterisk(wall);
    }

    public bool IsMoving(Point direction)
    {
        Point snakeHead = _snake.Last();
        GetNextPoint(direction, snakeHead);

        bool isPointOfSnake = _snake.Any(p => p.Left == nextLeft && p.Top == nextTop);
        if (isPointOfSnake)
            return false;
        
        Point snakeNewHead = new Point(nextLeft, nextTop);

        if (this.wall.IsPointOfWall(snakeNewHead))
            return false;
        
        _snake.Enqueue(snakeNewHead);
        snakeNewHead.Draw(SnakeSymbol);

        if (food[foodIndex].IsFoodPoint(snakeNewHead))
            Eat(direction, snakeNewHead);

        Point snakeTail = _snake.Dequeue();
        snakeTail.Draw(EmptySpaceSymbol);
        return true;
    }

    private void GetNextPoint(Point direction, Point snakeHead)
    {
        nextLeft = snakeHead.Left + direction.Left;
        nextTop = snakeHead.Top + direction.Top;
    }

    private void Eat(Point direction, Point snakeHead)
    {
        int length = food[foodIndex].FoodPoints;
        for (int i = 0; i < length; i++)
        {
            _snake.Enqueue(new Point(nextLeft, nextTop));
            GetNextPoint(direction, snakeHead);

            points += length;
            VisualizePoints();
        }

        foodIndex = randomFoodNumber;
        food[foodIndex].SetRandomPosition(_snake);
    }

    private void VisualizePoints()
    {
        int left = this.wall.Left + 1;
        int top = 3;
        
        Console.SetCursorPosition(left + 4, top + 1);
        Console.Write($"Score: {points}");
    }
}