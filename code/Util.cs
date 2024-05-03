namespace Kira;

public static class Util
{
    public static CategoryData[] LoadCategories()
    {
        var jsonData = FileSystem.Mounted.ReadJson<CategoryData[]>("category.json");
        return jsonData;
    }
}
