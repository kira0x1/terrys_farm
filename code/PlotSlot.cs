namespace Kira;

public class PlotSlot
{
    public bool Occupied { get; set; }
    public CategoryItem Item { get; set; }

    public readonly int x;
    public readonly int y;
    public Vector3 position;

    public PlotSlot(int x, int y, Vector3 position = new Vector3())
    {
        this.x = x;
        this.y = y;
        this.position = position;
    }

    public void Tick()
    {
    }
}