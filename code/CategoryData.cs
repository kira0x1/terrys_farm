namespace Kira;

using System;

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
}