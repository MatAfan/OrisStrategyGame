namespace Common.DTO;

public class StartGameDto
{
    public int PlayerId { get; set; }
    public int PlayerCount { get; set; }
    public List<PlayerInfoDto> Players { get; set; } = new List<PlayerInfoDto>();
}

public class PlayerInfoDto
{
    public int Id { get; set; }
    public string Nickname { get; set; } = "";
    public string Email { get; set; } = "";
}
