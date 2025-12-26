using System.Net;
using System.Net.Sockets;
using Server;

Console.WriteLine("=== Сервер игры ===");

var host = Dns.GetHostEntry(Dns.GetHostName());
var localIP = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
const int port = 5000;

Console.WriteLine($"IP адрес сервера: {localIP}");
Console.WriteLine($"Порт: {port}");

Console.WriteLine("Ожидание 4 игроков для автостарта");
Console.WriteLine("Или введите 'start' когда подключатся минимум 2 игрока");

var server = new GameServer();

Task.Run(() => server.Start(port));

while (true)
{
    string? input = Console.ReadLine();
    if (input == "start")
        server.ForceStart();
}
