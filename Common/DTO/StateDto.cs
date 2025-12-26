namespace Common.DTO;

public class StateDto
{
    public Dictionary<string, int> Resources { get; set; }
    public int Soldiers { get; set; }
    public int Defense { get; set; }
    public List<BuildingStateDto> Buildings { get; set; }
}

public class BuildingStateDto
{
    public int PlaceId { get; set; }
    public BuildingType Type { get; set; }
    public int Level { get; set; }
}
