using System.Text;

namespace Common.Services;

/// <summary>
/// Статический класс для преобразования строковых сообщений в байты и обратно.
/// Используется для передачи данных по TCP с XOR-шифрованием.
/// </summary>
public static class ByteConverter
{
    private static readonly Encoding Encoding = Encoding.UTF8;

    private static readonly byte[] EncryptionKey = "Happy_New_Year_2_0_2_6_!!!"u8.ToArray();

    /// <summary>
    /// Выполняет XOR-шифрование или дешифрование данных (симметричная операция).
    /// </summary>
    /// <param name="data">Массив байтов для шифрования/дешифрования.</param>
    /// <returns>Зашифрованный или дешифрованный массив байтов.</returns>
    private static byte[] XorCrypt(byte[] data)
    {
        var result = new byte[data.Length];
        for (int i = 0; i < data.Length; i++)
            result[i] = (byte)(data[i] ^ EncryptionKey[i % EncryptionKey.Length]);
        return result;
    }

    /// <summary>
    /// Преобразует строку в массив байтов для отправки по сети.
    /// Добавляет в начало 4 байта с длиной сообщения и шифрует содержимое XOR.
    /// </summary>
    /// <param name="message">Строка для преобразования.</param>
    /// <returns>Массив байтов в формате: [4 байта длины][зашифрованное сообщение].</returns>
    public static byte[] StringToBytes(string message)
    {
        var messageBytes = Encoding.GetBytes(message);
        var encryptedBytes = XorCrypt(messageBytes);
        var lengthBytes = BitConverter.GetBytes(encryptedBytes.Length);

        var result = new byte[4 + encryptedBytes.Length];
        Buffer.BlockCopy(lengthBytes, 0, result, 0, 4);
        Buffer.BlockCopy(encryptedBytes, 0, result, 4, encryptedBytes.Length);

        return result;
    }

    /// <summary>
    /// Преобразует массив байтов в строку с дешифровкой.
    /// </summary>
    /// <param name="buffer">Буфер с данными.</param>
    /// <param name="offset">Смещение в буфере.</param>
    /// <param name="length">Количество байтов для чтения.</param>
    /// <returns>Дешифрованная строка.</returns>
    public static string BytesToString(byte[] buffer, int offset, int length)
    {
        var encrypted = new byte[length];
        Buffer.BlockCopy(buffer, offset, encrypted, 0, length);
        var decrypted = XorCrypt(encrypted);
        return Encoding.GetString(decrypted);
    }

    /// <summary>
    /// Пытается прочитать полное сообщение из буфера TCP.
    /// Учитывает, что TCP может передавать данные частями.
    /// </summary>
    /// <param name="buffer">Буфер с полученными данными.</param>
    /// <param name="offset">Смещение в буфере.</param>
    /// <param name="available">Количество доступных байтов.</param>
    /// <param name="message">Прочитанное сообщение (если успешно).</param>
    /// <param name="bytesRead">Количество прочитанных байтов (включая заголовок длины).</param>
    /// <returns>
    /// <see langword="true"/>, если сообщение полностью получено и прочитано;
    /// <see langword="false"/>, если данных недостаточно.
    /// </returns>
    public static bool TryReadMessage(byte[] buffer, int offset, int available, out string message, out int bytesRead)
    {
        message = null!;
        bytesRead = 0;

        if (available < 4)
            return false;

        int messageLength = BitConverter.ToInt32(buffer, offset);

        if (available < 4 + messageLength)
            return false;

        var encrypted = new byte[messageLength];
        Buffer.BlockCopy(buffer, offset + 4, encrypted, 0, messageLength);
        var decrypted = XorCrypt(encrypted);
        message = Encoding.GetString(decrypted);
        bytesRead = 4 + messageLength;

        return true;
    }
}
