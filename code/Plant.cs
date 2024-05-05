namespace Kira;

public sealed class Plant : Component
{
    public PlantData PlantData { get; set; }

    protected override void OnUpdate()
    {
    }
}

public enum PlantType
{
    Wheat,
    Pumpkin,
    Tomato,
    Strawberry
}

public class PlantData
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public PlantType PlantType { get; set; }
}