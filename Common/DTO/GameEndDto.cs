namespace Common.DTO;

public class GameEndDto
{
    public int WinnerPlayerId { get; set; }
    public int Points { get; set; }
    public List<PlayerScoreDto> AllScores { get; set; } = new List<PlayerScoreDto>();
}

public class PlayerScoreDto
{
    public int PlayerId { get; set; }
    public string Nickname { get; set; } = "";
    public int Points { get; set; }
}
