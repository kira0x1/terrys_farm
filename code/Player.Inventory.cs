namespace Kira;

public sealed partial class Player : Component
{
    public Dictionary<int, InventoryItem> Seeds = new Dictionary<int, InventoryItem>();

    public void AddToInventory(CategoryItem item)
    {
        if (Seeds.TryGetValue(item.SeedId, out InventoryItem seed))
        {
            seed.Amount++;
            Log.Info("adding amount");
        }
        else
        {
            InventoryItem invItem = new InventoryItem(item);
            Seeds.Add(item.SeedId, invItem);
        }
    }
}

public class InventoryItem
{
    public int Amount { get; set; }
    public CategoryItem Seed { get; set; }
    public InventoryType ItemType { get; set; }

    public InventoryItem(CategoryItem seed)
    {
        this.Seed = seed;
        Amount = 1;
        ItemType = InventoryType.Seed;
    }
}

public enum InventoryType
{
    Seed,
    Plant
}