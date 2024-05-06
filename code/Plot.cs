namespace Kira;

using Sandbox.UI;

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
}

public sealed class Plot : Component
{
    [Property]
    public int PlotX { get; private set; } = 3;

    [Property]
    public int PlotY { get; private set; } = 3;

    private const float scale = 30f;
    private const float offset = 1.1f;

    private DevUI devUi;

    public Dictionary<Vector2Int, PlotSlot> Slots = new Dictionary<Vector2Int, PlotSlot>();

    protected override void OnStart()
    {
        base.OnStart();

        Slots = new Dictionary<Vector2Int, PlotSlot>();
        devUi = Scene.Components.GetAll<DevUI>().FirstOrDefault();
        CreatePlots();
    }

    protected override void OnUpdate()
    {
        if (!devUi.PlantDisplayOn) return;

        foreach (var slot in Slots.Keys)
        {
            Log.Info(slot);
            Hovering(slot);

            using (Gizmo.Scope("plot"))
            {
                Gizmo.Draw.LineThickness = 1f;
                Gizmo.Draw.Color = Color.Cyan.WithAlpha(0.5f);
                // Gizmo.Draw.LineBBox(BBox.FromPositionAndSize(slot.position.WithZ(25f), scale));
            }
        }
    }

    public void Hovering(Vector2Int slotId)
    {
        var slot = Slots[slotId];

        using (Gizmo.Scope("slot"))
        {
            if (slot.Occupied)
            {
                Gizmo.Draw.Color = Color.Red;
            }

            var box = BBox.FromPositionAndSize(slot.position.WithZ(20f), 14f);
            Gizmo.Draw.SolidBox(box);
        }
    }

    public void PlantSlot(Vector2Int slotId, CategoryItem item)
    {
        var slot = Slots[slotId];
        slot.Occupied = true;
        slot.Item = item;
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
            }
        }
    }

    private void DrawBox(int x, int y)
    {
        const float xOffset = 31.8f;

        var box = Vector3.Zero;
        box.x += x * xOffset + Transform.Position.x * 0.8f;

        box.y -= (PlotY * xOffset) / 2f;


        box.y += y * xOffset + Transform.Position.y;
        var b2 = box;

        b2.x += xOffset;
        b2.y += xOffset;


        var bx = new BBox(box, b2);
        Gizmo.Draw.LineBBox(bx);
    }
}