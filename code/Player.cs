namespace Kira;
using Sandbox;

public sealed class Player : Component
{
    protected override void OnUpdate()
    {
        var pos = Mouse.Position;
        var ray = Scene.Camera.ScreenPixelToRay(pos.WithX(pos.x));
        var trace = Scene.Trace.Ray(ray, 1000f).WithTag("plot").Run();
        if (trace.Hit)
        {
            var endpos = trace.EndPosition.SnapToGrid(32f);
            Gizmo.Draw.LineSphere(endpos, 10f);
        }
    }
}