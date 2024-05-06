namespace Kira;

using System;
using Sandbox.UI;

public sealed class Player : Component, Component.ExecuteInEditor
{
    private ShopUI shopUI;
    private static DataJson _gameData { get; set; }
    public static DataJson GameData => _gameData;

    [Property]
    public int Gold { get; set; } = 20;

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

            Plot plot = trace.GameObject.Components.Get<Plot>();
            if (plot.IsValid)
            {
                int xSlot = (int)(endpos.x / 32);
                int ySlot = (int)(endpos.y / 32);

                Vector2Int slotId = new Vector2Int(xSlot, ySlot);
                plot.Hovering(slotId);

                if (Input.Pressed("attack1"))
                {
                    Gold -= shopUI.ItemSelected.Cost;
                    shopUI.HasItemSelected = false;
                    plot.PlantSlot(slotId, shopUI.ItemSelected);
                }
            }
        }
        else
        {
            if (Input.Pressed("attack1"))
            {
                shopUI.HasItemSelected = false;
            }
        }
    }
}