namespace Kira;

using Sandbox.UI;
using Component = Component;

public sealed class Player : Component, Component.ExecuteInEditor
{
    private ShopUI shopUI;

    protected override void OnStart()
    {
        base.OnStart();
        shopUI = Scene.Components.GetAll<ShopUI>().First();
    }

    protected override void OnUpdate()
    {
        var pos = Mouse.Position;
        var ray = Scene.Camera.ScreenPixelToRay(pos.WithX(pos.x));
        var trace = Scene.Trace.Ray(ray, 1000f).WithTag("plot").Run();

        if (trace.Hit)
        {
            var endpos = trace.EndPosition.SnapToGrid(32f);
            var box = BBox.FromPositionAndSize(endpos.WithZ(20f), 14f);
            Gizmo.Draw.SolidBox(box);
        }
    }
}