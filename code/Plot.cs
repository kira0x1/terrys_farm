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
        var startPos = Transform.Position;

        var startX = startPos.x + PlotX * 16;
        var endX = startPos.x - PlotX * 16;

        var startY = startPos.y;

        var lineLeft = startPos.WithY((startY + 1f) * 48f);
        var lineRight = startPos.WithY(-(startY + 1f) * 48f);

        // Gizmo.Draw.Line(lineLeft.WithX(startX), lineLeft.WithX(endX));
        // Gizmo.Draw.Line(lineRight.WithX(startX), lineRight.WithX(endX));


        for (int y = 0; y < PlotY; y++)
        {
            for (int x = 0; x < PlotX; x++)
            {
                Vector3 pos = Transform.LocalPosition;
                pos.x -= 31f;
                pos.x += x * 31f;

                pos.y -= 31f;
                pos.y += y * 31f;

                Gizmo.Draw.SolidSphere(pos, 4f);
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