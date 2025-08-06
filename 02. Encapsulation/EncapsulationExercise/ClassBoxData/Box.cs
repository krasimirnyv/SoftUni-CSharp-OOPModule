namespace ClassBoxData;

public class Box
{
    private double length;
    private double width;
    private double height;

    public Box(double length, double width, double height)
    {
        Length = length;
        Width = width;
        Height = height;
    }
    
    public double Length
    {
        get => length;
        private set
        {
            if (!IsPositiveNumber(value))
            {
                throw new ArgumentException($"{nameof(Length)} cannot be zero or negative.");
            }
            
            length = value;
        }
    }

    public double Width
    {
        get => width;
        private set
        {
            if (!IsPositiveNumber(value))
            {
                throw new ArgumentException($"{nameof(Width)} cannot be zero or negative.");
            }
            
            width = value;
        }
    }

    public double Height
    {
        get => height;
        private set
        {
            if (!IsPositiveNumber(value))
            {
                throw new ArgumentException($"{nameof(Height)} cannot be zero or negative.");
            }
            
            height = value;
        }
    }

    public double SurfaceArea()
    {
        // Formula: 2lw + 2lh + 2wh
        
        return (2 * Length * Width) +
               (2 * Length * Height) +
               (2 * Width * Height);
    }

    public double LateralSurfaceArea()
    {
        // Formula: 2lh + 2wh
        
        return (2 * Length * Height) +
               (2 * Width * Height);
    }

    public double Volume()
    {
        // Formula: l * w * h
        
        return Length * Width * Height;
    }
    
    private static bool IsPositiveNumber(double value)
        => value > 0;
}