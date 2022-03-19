namespace Image_Colour_Swap;

public class PixelData
{
    public int Blue { get; }
    public int Green { get; }
    public int Id { get; }
    public int Red { get; }
    
    public PixelData(int id, int red, int green, int blue)
    {
        Blue = blue;
        Green = green;
        Id = id;
        Red = red;
    }
}