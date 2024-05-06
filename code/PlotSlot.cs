namespace Kira;

public class PlotSlot
{
    public bool Occupied { get; set; }
    public CategoryItem Item { get; set; }

    public readonly int x;
    public readonly int y;
    public Vector3 position;

    /// <summary>
    /// Ticks since planted
    /// </summary>
    public int TickCount { get; set; }

    public bool HasBloomed { get; set; }

    private TimeSince TimeSinceTick { get; set; }

    public PlotSlot(int x, int y, Vector3 position = new Vector3())
    {
        this.x = x;
        this.y = y;
        this.position = position;
    }

    public void Tick()
    {
        if (TimeSinceTick > Item.TickRate)
        {
            TickCount++;
            TimeSinceTick = 0f;
        }

        HasBloomed = TickCount > Item.BloomTicks;
    }
}