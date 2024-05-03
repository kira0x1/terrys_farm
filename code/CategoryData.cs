namespace Kira;

public enum CategoryTypes
{
    Seeds,
    Furniture,
    Animals
}

[GameResource("Category Data", "category", "Category of items")]
public partial class CategoryData : GameResource
{
    [Property]
    public string Name { get; set; } = "Seed";

    [Property]
    public string Icon { get; set; } = "favorite";

    [Property]
    public CategoryTypes CategoryType { get; set; }

    [Property]
    public List<CategoryItem> Items = new List<CategoryItem>();

    // Access these statically with Clothing.All
    public static IReadOnlyList<CategoryData> All => _all;
    private static readonly List<CategoryData> _all = new();

    protected override void PostLoad()
    {
        base.PostLoad();

        if (!_all.Contains(this))
            _all.Add(this);
    }
}

public class CategoryItem
{
    public string Name { get; set; }
    public string Icon { get; set; }
}