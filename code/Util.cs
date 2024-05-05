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

    public static DataJson LoadData()
    {
        var jsonData = FileSystem.Mounted.ReadJson<DataJson>("data.json");
        return jsonData;
    }

/// <summary>
/// Structure of the data.json file, which holds all the games data such as categories, tools, and plants
/// </summary>
public class DataJson
{
    public CategoryData[] Categories { get; set; }
    public ToolData[] Tools { get; set; }
}

public class ToolData
{
    public string Name { get; set; }
    public string Icon { get; set; }
}