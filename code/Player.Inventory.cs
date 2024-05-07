namespace Kira;

using System;

public sealed partial class Player : Component
{
    public Dictionary<int, InventoryItem> Seeds = new Dictionary<int, InventoryItem>();
    public Action OnInventoryChanged;

    public void AddToInventory(CategoryItem item, CategoryTypes category, int amount = 1)
    {
        if (category != CategoryTypes.Seeds) return;

        if (Seeds.TryGetValue(item.SeedId, out InventoryItem seed))
        {
            seed.Amount += amount;
        }
        else
        {
            InventoryItem invItem = new InventoryItem(item, amount);
            Seeds.Add(item.SeedId, invItem);
        }

        OnInventoryChanged?.Invoke();
    }

    public void OnUseItem(int id)
    {
        Seeds[id].Amount--;
    }
}

public class InventoryItem
{
    public int Amount { get; set; }
    public CategoryItem Seed { get; set; }
    public InventoryType ItemType { get; set; }

    public InventoryItem(CategoryItem seed, int amount = 1)
    {
        this.Seed = seed;
        Amount = amount;
        ItemType = InventoryType.Seed;
    }
}

public enum InventoryType
{
    Seed,
    Plant
}