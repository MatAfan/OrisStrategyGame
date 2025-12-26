namespace Common.DTO;

public class PlayerLeftDto
{
    public int PlayerId { get; set; }
    public string Nickname { get; set; } = "";
    public List<PlayerInfoDto> RemainingPlayers { get; set; } = new();
}
