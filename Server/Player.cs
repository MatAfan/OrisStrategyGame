using Common;
using System.Net.Sockets;

namespace Server;

public class Player
{
    public int Id;
    public string Nickname = "";
    public string Email = "";
    public TcpClient Client = null!;
    public NetworkStream Stream = null!;
    public ArchetypeType Archetype = ArchetypeType.Neutral;
    
    public Dictionary<Resources, int> ResourceStorage = new();
    public List<Building> Buildings = [];
    public int Soldiers = 1000;
    public int SoldiersCreatedThisTurn = 0;
    public HashSet<int> AttackedPlayersThisTurn = [];

    public void InitResources()
    {
        foreach (Resources res in Enum.GetValues<Resources>())
            ResourceStorage[res] = 1;
    }

    public bool HasResource(Resources res, int amount)
    {
        if (!ResourceStorage.TryGetValue(res, out var value)) return false;
        return value >= amount;
    }

    public void AddResource(Resources res, int amount)
    {
        ResourceStorage.TryAdd(res, 0);
        ResourceStorage[res] += amount;
    }

    public void RemoveResource(Resources res, int amount)
    {
        if (ResourceStorage.ContainsKey(res))
            ResourceStorage[res] -= amount;
    }

    public int GetDefense()
    {
        int def = 0;
        foreach (var b in Buildings)
            switch (b.Type)
            {
                case BuildingType.Barricade when b.Level == 1:
                    def += 5;
                    break;
                case BuildingType.Barricade:
                    if (b.Level == 2) def += 10;
                    break;
                case BuildingType.DefenseTower when b.Level == 1:
                    def += 20;
                    break;
                case BuildingType.DefenseTower:
                    if (b.Level == 2) def += 30;
                    break;
            }

        def = Archetype switch
        {
            ArchetypeType.Greedy => (int)(def * 1.3),
            ArchetypeType.Patron => (int)(def * 0.75),
            _ => def
        };

        return def;
    }

    public int GetMaxSoldiersPerTurn()
    {
        return Buildings.Where(b => b.Type == BuildingType.Barracks).Sum(b => GameLogic.GetProduction(BuildingType.Barracks, b.Level));
    }

    public int CalcPoints()
    {
        int pts = 0;
        foreach (var r in ResourceStorage)
        {
            int baseP = GetResourcePoints(r.Key);

            int pointsPerUnit = Archetype switch
            {
                ArchetypeType.Greedy => (int)(baseP * 0.8),
                ArchetypeType.Patron => (int)(baseP * 1.25),
                ArchetypeType.Engineer => (int)(baseP * 0.8),
                ArchetypeType.Alchemist when r.Key == Resources.Gold || r.Key == Resources.Emerald =>
                    (int)(baseP * 1.25),
                ArchetypeType.Alchemist => (int)(baseP * 0.7),
                _ => baseP
            };

            if (pointsPerUnit < 1) pointsPerUnit = 1;

            pts += pointsPerUnit * r.Value;
        }
        return pts;
    }

    private static int GetResourcePoints(Resources res)
    {
        return res switch
        {
            Resources.Wood or Resources.Stone or Resources.Ore or Resources.Wheat => 1,
            Resources.Lumber or Resources.Bricks or Resources.Coal or Resources.Sand => 3,
            Resources.Metal or Resources.Bread => 4,
            Resources.Walls => 9,
            Resources.Furniture or Resources.Tools or Resources.Glass => 11,
            Resources.Weapon => 14,
            Resources.Gold => 43,
            Resources.Emerald => 62,
            _ => 0
        };
    }
}
