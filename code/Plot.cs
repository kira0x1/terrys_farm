namespace Kira;

using System;
using Sandbox.UI;

public sealed class Plot : Component
{
    [Property]
    public int PlotX { get; private set; } = 3;

    [Property]
    public int PlotY { get; private set; } = 3;

    private const float scale = 30f;
    private const float offset = 1.1f;

    private Player player;
    private DevUI devUi;

    public Dictionary<Vector2Int, PlotSlot> Slots = new Dictionary<Vector2Int, PlotSlot>();

    protected override void OnStart()
    {
        base.OnStart();

        player = Scene.Components.GetAll<Player>().FirstOrDefault();
        Slots = new Dictionary<Vector2Int, PlotSlot>();
        devUi = Scene.Components.GetAll<DevUI>().FirstOrDefault();
        CreatePlots();
    }

    protected override void OnUpdate()
    {
        foreach (var slot in Slots.Values)
        {
            slot.Tick();
        }

        if (devUi.PlantDisplayOn)
        {
            DisplayPlantGizmos();
        }
    }

    private void DisplayPlantGizmos()
    {
        foreach (var slot in Slots)
        {
            Hovering(slot.Key);
        }
    }

    public void Hovering(Vector2Int slotId)
    {
        var slot = Slots[slotId];

        using (Gizmo.Scope("slot"))
        {
            if (slot.IsOccupied)
            {
                Gizmo.Draw.Color = slot.HasBloomed ? Color.Yellow : Color.Red;
            }

            var box = BBox.FromPositionAndSize(slot.position.WithZ(20f), 14f);
            Gizmo.Draw.SolidBox(box);
        }
    }

    public void PlantSlot(Vector2Int slotId, CategoryItem item, CategoryTypes category)
    {
        PlotSlot slot = Slots[slotId];
        slot.PlaceItem(item, category);
    }

    private void OnHarvest(CategoryItem item)
    {
        // calculate yields
        YieldData[] yieldData = item.Yield;

        foreach (YieldData yield in yieldData)
        {
            int amount = yield.IsRandom ? Random.Shared.Int(yield.MinAmount, yield.MaxAmount) : yield.Amount;

            if (yield.YieldType == "seed")
            {
                player.AddToInventory(item, CategoryTypes.Seeds, amount);
            }

            if (yield.YieldType == "gold")
            {
                player.AddGold(amount);
            }
        }
    }

    private void CreatePlots()
    {
        for (int y = 0; y < PlotY; y++)
        {
            for (int x = 0; x < PlotX; x++)
            {
                Vector3 pos = Transform.LocalPosition;
                pos.x -= scale * offset;
                pos.x += x * scale * offset;

                pos.y -= scale * offset;
                pos.y += y * scale * offset;

                int xSlot = (int)(pos.x / 32);
                int ySlot = (int)(pos.y / 32);

                PlotSlot slot = new PlotSlot(xSlot, ySlot, pos);
                Slots.Add(new Vector2Int(xSlot, ySlot), slot);
                slot.OnHarvest += OnHarvest;
            }
        }
    }
}