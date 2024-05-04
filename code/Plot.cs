namespace Kira;

using Sandbox;

public sealed class Plot : Component
{
    [Property]
    public int PlotX { get; private set; } = 3;

    [Property]
    public int PlotY { get; private set; } = 3;

    protected override void OnUpdate()
    {
        for (int y = 0; y < PlotY; y++)
        {
            for (int x = 0; x < PlotX; x++)
            {
                const float scale = 30f;
                const float offset = 1.1f;

                Vector3 pos = Transform.LocalPosition;
                pos.x -= scale * offset;
                pos.x += x * scale * offset;

                pos.y -= scale * offset;
                pos.y += y * scale * offset;

                using (Gizmo.Scope("plot"))
                {
                    Gizmo.Draw.LineThickness = 1f;
                    Gizmo.Draw.Color = Color.Cyan.WithAlpha(0.5f);
                    Gizmo.Draw.LineBBox(BBox.FromPositionAndSize(pos.WithZ(25f), scale));
                }
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