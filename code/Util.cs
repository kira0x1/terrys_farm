﻿namespace Kira;

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
        Log.Info("loaded data");

        var item = jsonData.Categories[0].Items[0];
        Log.Info($"item: {item.Name}");

        var yields = item.Yield;

        foreach (YieldData yieldData in yields)
        {
            Log.Info($"yield: {yieldData.YieldType}, max: {yieldData.MaxAmount}");
        }

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