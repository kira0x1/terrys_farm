namespace Kira;

using Sandbox;

public class PlotSlot
{
    public bool Occupied { get; set; }
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

    public Dictionary<Vector2Int, PlotSlot> Slots = new Dictionary<Vector2Int, PlotSlot>();

    protected override void OnStart()
    {
        base.OnStart();

        Slots = new Dictionary<Vector2Int, PlotSlot>();
        CreatePlots();
    }

    // protected override void OnUpdate()
    // {
    //     foreach (PlotSlot slot in Slots.Values)
    //     {
    //         using (Gizmo.Scope("plot"))
    //         {
    //             Gizmo.Draw.LineThickness = 1f;
    //             Gizmo.Draw.Color = Color.Cyan.WithAlpha(0.5f);
    //             Gizmo.Draw.LineBBox(BBox.FromPositionAndSize(slot.position.WithZ(25f), scale));
    //         }
    //     }
    // }

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

                PlotSlot slot = new PlotSlot(x, y, pos);
                Slots.Add(new Vector2Int(x, y), slot);
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