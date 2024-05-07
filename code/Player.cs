namespace Kira;

using Sandbox.UI;

public sealed partial class Player : Component
{
    private InventoryUI inventoryUI;

    private static DataJson _gameData { get; set; }
    public static DataJson GameData => _gameData;
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
        inventoryUI = Scene.Components.GetAll<InventoryUI>().First();
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }

    // doing this for a reason trust
    public void RemoveGold(int amount)
    {
        Gold -= amount;
    }

    protected override void OnUpdate()
    {
        if (inspectUi == null) return;
        if (!inspectUi.IsInspectOn && !inventoryUI.HasItemSelected) return;

        var pos = Mouse.Position;
        var ray = Scene.Camera.ScreenPixelToRay(pos.WithX(pos.x));
        var trace = Scene.Trace.Ray(ray, 1000f).WithTag("plot").Run();

        if (Input.Pressed("attack2"))
        {
            inventoryUI.HasItemSelected = false;
        }

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

                if (inventoryUI.HasItemSelected && inventoryUI.ItemSelected.Amount > 0)
                {
                    if (Input.Down("attack1"))
                    {
                        if (slot.IsOccupied) return;
                        plot.PlantSlot(slotId, inventoryUI.ItemSelected.Seed, CategoryTypes.Seeds);
                        OnUseItem(inventoryUI.ItemSelected.Seed.SeedId);
                    }
                }
            }
        }
        else
        {
            inspectUi.IsHovering = false;
            if (Input.Pressed("attack1"))
            {
                // inventoryUi.HasItemSelected = false;
            }
        }
    }
}