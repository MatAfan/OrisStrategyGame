namespace Common.Services;

/// <summary>
/// Парсер для работы с сообщениями в формате "ТИП|ДАННЫЕ".
/// </summary>
/// <remarks>
/// Формат сообщения: <c>TYPE|PAYLOAD</c>, где TYPE — тип сообщения из <see cref="MessageType"/>,
/// а PAYLOAD — данные в формате JSON.
/// </remarks>
/// <example>
/// Пример сообщения: <c>JOIN|{"nickname":"Player1"}</c>
/// </example>
public static class MessageParser
{
    /// <summary>
    /// Разбирает строку сообщения на тип и данные.
    /// </summary>
    /// <param name="raw">Исходная строка сообщения в формате "ТИП|ДАННЫЕ".</param>
    /// <returns>Объект <see cref="NetworkMessage"/> с разобранным типом и данными.</returns>
    /// <exception cref="ArgumentException">Выбрасывается, если тип сообщения не распознан.</exception>
    public static NetworkMessage Parse(string raw)
    {
        // разделяем по первому символу '|'
        var parts = raw.Split('|', 2);

        return new NetworkMessage
        {
            Type = Enum.Parse<MessageType>(parts[0]),
            Payload = parts.Length > 1 ? parts[1] : string.Empty
        };
    }

    /// <summary>
    /// Сериализует сообщение в строку для передачи по сети.
    /// </summary>
    /// <param name="message">Объект сообщения для сериализации.</param>
    /// <returns>Строка в формате "ТИП|ДАННЫЕ".</returns>
    public static string Serialize(NetworkMessage message)
    {
        return $"{message.Type}|{message.Payload}";
    }
}
