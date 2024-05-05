namespace Kira;

using System;
using Sandbox.UI;

public sealed class Player : Component, Component.ExecuteInEditor
{
    private ShopUI shopUI;
    private static DataJson _gameData { get; set; }
    public static DataJson GameData => _gameData;

    protected override void OnAwake()
    {
        base.OnAwake();

        _gameData = Util.LoadData();
    }

    protected override void OnStart()
    {
        base.OnStart();
        shopUI = Scene.Components.GetAll<ShopUI>().First();
    }

    protected override void OnUpdate()
    {
        if (!shopUI.HasItemSelected) return;

        if (Input.Pressed("attack1"))
        {
            shopUI.HasItemSelected = false;
        }

        var pos = Mouse.Position;
        var ray = Scene.Camera.ScreenPixelToRay(pos.WithX(pos.x));
        var trace = Scene.Trace.Ray(ray, 1000f).WithTag("plot").Run();

        if (trace.Hit)
        {
            var endpos = trace.EndPosition.SnapToGrid(32f);
            if (endpos.x > 32 || endpos.x < -32 || endpos.y > 32 || endpos.y < -32)
            {
                return;
            }

            var box = BBox.FromPositionAndSize(endpos.WithZ(20f), 14f);
            Gizmo.Draw.SolidBox(box);
        }
    }
}