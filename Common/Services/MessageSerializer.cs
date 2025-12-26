using System.Text.Json;
using Common.DTO;

namespace Common.Services;

public static class MessageSerializer
{
    public static NetworkMessage Serialize<T>(MessageType type, T dto) where T : class
        => new()
        {
            Type = type,
            Payload = JsonSerializer.Serialize(dto)
        };

    public static NetworkMessage CreateJoin(JoinDto dto) 
        => Serialize(MessageType.JOIN, dto);
    public static NetworkMessage CreateStartGame(StartGameDto dto) 
        => Serialize(MessageType.START_GAME, dto);
    public static NetworkMessage CreateArchetype(ArchetypeDto dto) 
        => Serialize(MessageType.ARCHETYPE, dto);
    public static NetworkMessage CreateStartTurn(StartTurnDto dto) 
        => Serialize(MessageType.START_TURN, dto);
    public static NetworkMessage CreateProductionResult(ProductionResultDto dto) 
        => Serialize(MessageType.PRODUCTION_RESULT, dto);
    public static NetworkMessage CreateMakeSoldiers(MakeSoldiersRequestDto dto)
        => Serialize(MessageType.MAKE_SOLDIERS, dto);
    public static NetworkMessage CreateAttack(AttackRequestDto dto) 
        => Serialize(MessageType.ATTACK, dto);
    public static NetworkMessage CreateAttackTarget(AttackTargetDto dto) 
        => Serialize(MessageType.ATTACK_TARGET, dto);
    public static NetworkMessage CreateBuild(BuildRequestDto dto) 
        => Serialize(MessageType.BUILD, dto);
    public static NetworkMessage CreateUpgrade(UpgradeRequestDto dto) 
        => Serialize(MessageType.UPGRADE, dto);
    public static NetworkMessage CreateEndTurn() 
        => Serialize<object>(MessageType.END_TURN, null);
    public static NetworkMessage CreateTurnEnded(TurnEndedDto dto) 
        => Serialize(MessageType.TURN_ENDED, dto);
    public static NetworkMessage CreateState(StateDto dto) 
        => Serialize(MessageType.STATE, dto);
    public static NetworkMessage CreateGameEnd(GameEndDto dto) 
        => Serialize(MessageType.GAME_END, dto);
    public static NetworkMessage CreateResponse(ResponseDto dto) 
        => Serialize(MessageType.RESPONSE, dto);
}
