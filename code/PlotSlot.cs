namespace Kira;

using System;

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
    private TimeSince BloomTextTime { get; set; } = 6;
    private TimeSince HarvestTextTime { get; set; } = 6;

    private const float harvestDelay = 4f;
    private TimeSince harvestTime = 0;

    public Action<CategoryItem> OnHarvest;


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
        if (Category != CategoryTypes.Seeds)
            return;


        if (HarvestTextTime < 2)
        {
            Gizmo.Draw.Text("Harvested!!!", new Transform(position));
        }

        if (!IsOccupied) return;

        if (TimeSinceTick > Item.TickRate)
        {
            TickCount++;
            TimeSinceTick = 0f;
        }

        // On Bloom
        if (!HasBloomed && TickCount > Item.BloomTicks)
        {
            OnBloom();
        }

        if (BloomTextTime < 2)
        {
            Gizmo.Draw.Text("Bloomed!!", new Transform(position));
        }


        HasBloomed = TickCount > Item.BloomTicks;

        if (HasBloomed && harvestTime > harvestDelay)
        {
            Harvest();
        }
    }

    private void OnBloom()
    {
        BloomTextTime = 0;
        harvestTime = 0;
    }

    private void Harvest()
    {
        HarvestTextTime = 0;
        TickCount = 0;
        IsOccupied = false;
        HasBloomed = false;

        BloomTextTime = 10;
        harvestTime = 10;

        OnHarvest?.Invoke(Item);
    }
}