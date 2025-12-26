using System.Text.Json;
using Common.DTO;

namespace Common.Services;

public static class MessageDeserializer
{
    public static T Deserialize<T>(NetworkMessage message) where T : class
        => string.IsNullOrEmpty(message.Payload) ? null : JsonSerializer.Deserialize<T>(message.Payload);
    

    public static object DeserializeByType(NetworkMessage message)
    {
        if (string.IsNullOrEmpty(message.Payload))
            return null;

        return message.Type switch
        {
            MessageType.JOIN => JsonSerializer.Deserialize<JoinDto>(message.Payload),
            MessageType.START_GAME => JsonSerializer.Deserialize<StartGameDto>(message.Payload),
            MessageType.ARCHETYPE => JsonSerializer.Deserialize<ArchetypeDto>(message.Payload),
            MessageType.START_TURN => JsonSerializer.Deserialize<StartTurnDto>(message.Payload),
            MessageType.PRODUCTION_RESULT => JsonSerializer.Deserialize<ProductionResultDto>(message.Payload),
            MessageType.MAKE_SOLDIERS => JsonSerializer.Deserialize<MakeSoldiersRequestDto>(message.Payload),
            MessageType.ATTACK => JsonSerializer.Deserialize<AttackRequestDto>(message.Payload),
            MessageType.ATTACK_TARGET => JsonSerializer.Deserialize<AttackTargetDto>(message.Payload),
            MessageType.BUILD => JsonSerializer.Deserialize<BuildRequestDto>(message.Payload),
            MessageType.UPGRADE => JsonSerializer.Deserialize<UpgradeRequestDto>(message.Payload),
            MessageType.TURN_ENDED => JsonSerializer.Deserialize<TurnEndedDto>(message.Payload),
            MessageType.STATE => JsonSerializer.Deserialize<StateDto>(message.Payload),
            MessageType.GAME_END => JsonSerializer.Deserialize<GameEndDto>(message.Payload),
            MessageType.RESPONSE => JsonSerializer.Deserialize<ResponseDto>(message.Payload),
            _ => throw new ArgumentException($"Unknown message type: {message.Type}")
        };
    }
}
