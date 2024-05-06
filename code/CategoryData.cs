namespace Kira;

public enum CategoryTypes
{
    Seeds,
    Furniture,
    Animals
}

public class CategoryData
{
    public string Name { get; set; }
    public CategoryTypes CategoryType { get; set; }
    public string Icon { get; set; }
    public CategoryItem[] Items { get; set; }
}

public class CategoryItem
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public int Cost { get; set; }
    public YieldData[] Yield { get; set; }
}

public class YieldData
{
    public string YieldType { get; set; }
    public int MinAmount { get; set; }
    public int MaxAmount { get; set; }
    public int Amount { get; set; }
    public bool IsRandom { get; set; }
}