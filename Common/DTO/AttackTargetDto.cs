namespace Common.DTO;

public class AttackTargetDto
{
    public int ToPlayerId { get; set; }
    public int Sent { get; set; }
    public int Lost { get; set; }
    public Dictionary<string, int> StolenResources { get; set; }
}
