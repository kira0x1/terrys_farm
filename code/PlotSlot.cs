namespace Kira;

public class PlotSlot
{
    public bool IsOccupied { get; set; }
    public CategoryItem Item { get; set; }
    public CategoryTypes Category { get; set; }

    public readonly int x;
    public readonly int y;
    public Vector3 position;

    /// <summary>
    /// Ticks since planted
    /// </summary>
    public int TickCount { get; set; }

    public bool HasBloomed { get; set; }
    private TimeSince TimeSinceTick { get; set; }
    private TimeSince BloomTextTime { get; set; }


    public PlotSlot(int x, int y, Vector3 position = new Vector3())
    {
        this.x = x;
        this.y = y;
        this.position = position;
    }

    public void PlaceItem(CategoryItem item, CategoryTypes category)
    {
        this.Item = item;
        this.Category = category;
        IsOccupied = true;
    }

    public void Tick()
    {
        if (!IsOccupied || Category != CategoryTypes.Seeds)
            return;

        if (TimeSinceTick > Item.TickRate)
        {
            TickCount++;
            TimeSinceTick = 0f;
        }

        if (!HasBloomed && TickCount > Item.BloomTicks)
        {
            BloomTextTime = 0;
        }

        if (BloomTextTime < 2)
        {
            Gizmo.Draw.Text("Bloomed!!", new Transform(position));
        }

        HasBloomed = TickCount > Item.BloomTicks;
    }
}