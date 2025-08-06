using System;

namespace SimpleSnake.GameObjects;

public class Point
{
    public Point(int left, int top)
    {
        Left = left;
        Top = top;
    }
    
    public int Left { get; set; }
    public int Top { get; set; }

    public void Draw(char symbol)
    {
        Console.SetCursorPosition(Left, Top);
        Console.Write(symbol);
    }
    
    public void Draw(int left, int top, char symbol)
    {
        Console.SetCursorPosition(left, top);
        Console.Write(symbol);
    }
}