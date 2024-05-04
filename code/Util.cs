namespace Kira;

public static class Util
{
    public static CategoryData[] LoadCategories()
    {
        var jsonData = FileSystem.Mounted.ReadJson<CategoryData[]>("category.json");
        return jsonData;
    }

    public static ToolData[] LoadToolData()
    {
        var jsonData = FileSystem.Mounted.ReadJson<ToolData[]>("tools.json");
        return jsonData;
    }
}

public class ToolData
{
    public string Name { get; set; }
    public string Icon { get; set; }
}