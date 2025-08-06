namespace SimpleSnake.GameObjects;

public class Wall : Point
{
    private const char HorizontalWallSymbol = '-';
    private const char VerticalWallSymbol = '|';
    
    public Wall(int left, int top) 
        : base(left, top)
    {
        InitializeWalls();
    }

    private void InitializeWalls()
    {
        SetHorizontalWall(0);
        SetHorizontalWall(Top - 1);
        
        SetVerticalWall(0);
        SetVerticalWall(Left);
    }

    private void SetHorizontalWall(int top)
    {
        for (int left = 0; left < Left; left++)
        {
            Draw(left, top, HorizontalWallSymbol);
        }
    }

    private void SetVerticalWall(int left)
    {
        for (int top = 0; top < Top; top++)
        {
            Draw(left, top, VerticalWallSymbol);
        }
    }
    
    public bool IsPointOfWall(Point snake)
        => snake.Left == 0 || snake.Left == Left ||
           snake.Top == 0 || snake.Top == Top;
}