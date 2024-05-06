namespace Kira;

public static class Util
{
    public static DataJson LoadData()
    {
        var jsonData = FileSystem.Mounted.ReadJson<DataJson>("data.json");
        return jsonData;
    }

    public static GameObject CreatePlant(Vector3? position = null, GameObject parent = null)
    {
        GameObject plantObject = new GameObject(true, "plant");
        GameObject plantModel = new GameObject(true, "model");

        plantObject.Components.Create<Plant>();
        plantModel.SetParent(plantObject);
        plantModel.Transform.LocalScale = new Vector3(0.25f, 0.25f, 0.25f);
        plantModel.Components.Create<ModelRenderer>();

        if (parent.IsValid())
        {
            plantObject.SetParent(parent);
        }

        if (position.HasValue)
        {
            plantObject.Transform.Position = position.Value;
        }

        return plantObject;
    }
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