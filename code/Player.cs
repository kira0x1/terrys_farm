namespace Kira;

using Sandbox.UI;

public sealed class Player : Component, Component.ExecuteInEditor
{
    private ShopUI shopUI;
    private static DataJson _gameData { get; set; }
    public static DataJson GameData => _gameData;

    [Property]
    public int Gold { get; set; } = 20;

    private InspectUI inspectUi;

    protected override void OnAwake()
    {
        base.OnAwake();

        _gameData = Util.LoadData();
    }

    protected override void OnStart()
    {
        base.OnStart();

        inspectUi = Scene.Components.GetAll<InspectUI>().First();
        shopUI = Scene.Components.GetAll<ShopUI>().First();
    }

    protected override void OnUpdate()
    {
        if (inspectUi == null) return;
        if (!shopUI.HasItemSelected && !inspectUi.IsInspectOn) return;

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

                var slot = plot.Slots[slotId];

                inspectUi.SlotHovering = slot;
                inspectUi.IsHovering = true;

                if (Input.Pressed("attack1") && shopUI.HasItemSelected)
                {
                    if (slot.IsOccupied) return;

                    Gold -= shopUI.ItemSelected.Cost;
                    shopUI.HasItemSelected = false;
                    plot.PlantSlot(slotId, shopUI.ItemSelected, shopUI.CategorySelectedType);
                }
            }
        }
        else
        {
            inspectUi.IsHovering = false;
            if (Input.Pressed("attack1"))
            {
                shopUI.HasItemSelected = false;
            }
        }
    }
}