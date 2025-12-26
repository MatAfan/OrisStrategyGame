namespace Common.DTO;

public class AttackReceivedDto
{
    public int FromPlayerId { get; set; }
    public string FromNickname { get; set; } = "";
    public int SoldiersAttacked { get; set; }
    public int SoldiersLost { get; set; }
    public Dictionary<string, int> LostResources { get; set; } = new();
}
