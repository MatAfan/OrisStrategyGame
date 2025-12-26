using Common;

namespace Server;

public static class GameLogic
{
    public static Dictionary<Resources, int> GetBuildCost(BuildingType type)
        => GetUpgradeCost(type, 1);

    public static Dictionary<Resources, int> GetUpgradeCost(BuildingType type, int toLevel)
    {
        var c = new Dictionary<Resources, int>();

        switch (toLevel)
        {
            case 1 when type == BuildingType.Logging:
                c[Resources.Stone] = 2;
                break;
            case 1 when type == BuildingType.Quarry:
                c[Resources.Wood] = 2;
                break;
            case 1 when type == BuildingType.Mine:
                c[Resources.Wood] = 2; c[Resources.Stone] = 1;
                break;
            case 1 when type == BuildingType.Farm:
                c[Resources.Wood] = 2;
                break;
            case 1 when type == BuildingType.Sawmill:
                c[Resources.Wood] = 2; c[Resources.Stone] = 1;
                break;
            case 1 when type == BuildingType.Kiln:
                c[Resources.Stone] = 2; c[Resources.Wood] = 1;
                break;
            case 1 when type == BuildingType.Smelter:
                c[Resources.Stone] = 2; c[Resources.Ore] = 2;
                break;
            case 1 when type == BuildingType.Charcoal:
                c[Resources.Wood] = 2;
                break;
            case 1 when type == BuildingType.Crusher:
                c[Resources.Stone] = 2;
                break;
            case 1 when type == BuildingType.Bakery:
                c[Resources.Lumber] = 2; c[Resources.Stone] = 1;
                break;
            case 1 when type == BuildingType.Carpentry:
                c[Resources.Lumber] = 3; c[Resources.Bricks] = 1;
                break;
            case 1 when type == BuildingType.Masonry:
                c[Resources.Bricks] = 2; c[Resources.Stone] = 2;
                break;
            case 1 when type == BuildingType.Forge:
                c[Resources.Metal] = 2; c[Resources.Lumber] = 2;
                break;
            case 1 when type == BuildingType.Glassworks:
                c[Resources.Sand] = 2; c[Resources.Coal] = 1; c[Resources.Bricks] = 1;
                break;
            case 1 when type == BuildingType.Armory:
                c[Resources.Metal] = 2; c[Resources.Lumber] = 2;
                break;
            case 1 when type == BuildingType.Barracks:
                c[Resources.Lumber] = 2; c[Resources.Weapon] = 1;
                break;
            case 1 when type == BuildingType.Laboratory:
                c[Resources.Glass] = 2; c[Resources.Tools] = 2; c[Resources.Coal] = 1;
                break;
            case 1 when type == BuildingType.AlchemyFurnace:
                c[Resources.Metal] = 2; c[Resources.Coal] = 2; c[Resources.Tools] = 2;
                break;
            case 1 when type == BuildingType.Barricade:
                c[Resources.Lumber] = 2; c[Resources.Stone] = 1;
                break;
            case 1 when type == BuildingType.DefenseTower:
                c[Resources.Walls] = 1; c[Resources.Tools] = 1; c[Resources.Weapon] = 1;
                break;
            case 2 when type == BuildingType.Logging:
                c[Resources.Stone] = 1; c[Resources.Lumber] = 1;
                break;
            case 2 when type == BuildingType.Quarry:
                c[Resources.Wood] = 1; c[Resources.Bricks] = 1;
                break;
            case 2 when type == BuildingType.Mine:
                c[Resources.Bricks] = 2;
                break;
            case 2 when type == BuildingType.Farm:
                c[Resources.Wood] = 1; c[Resources.Lumber] = 1;
                break;
            case 2 when type == BuildingType.Sawmill:
                c[Resources.Lumber] = 2;
                break;
            case 2 when type == BuildingType.Kiln:
                c[Resources.Bricks] = 2;
                break;
            case 2 when type == BuildingType.Smelter:
                c[Resources.Metal] = 1; c[Resources.Bricks] = 1;
                break;
            case 2 when type == BuildingType.Charcoal:
                c[Resources.Lumber] = 1; c[Resources.Wood] = 1;
                break;
            case 2 when type == BuildingType.Crusher:
                c[Resources.Bricks] = 1; c[Resources.Stone] = 1;
                break;
            case 2 when type == BuildingType.Bakery:
                c[Resources.Bread] = 1; c[Resources.Lumber] = 1;
                break;
            case 2 when type == BuildingType.Carpentry:
                c[Resources.Furniture] = 2;
                break;
            case 2 when type == BuildingType.Masonry:
                c[Resources.Walls] = 1; c[Resources.Bricks] = 1;
                break;
            case 2 when type == BuildingType.Forge:
                c[Resources.Tools] = 1; c[Resources.Metal] = 1;
                break;
            case 2 when type == BuildingType.Glassworks:
                c[Resources.Glass] = 1; c[Resources.Sand] = 1;
                break;
            case 2 when type == BuildingType.Armory:
                c[Resources.Weapon] = 1; c[Resources.Metal] = 1;
                break;
            case 2 when type == BuildingType.Barracks:
                c[Resources.Walls] = 1; c[Resources.Bread] = 1;
                break;
            case 2 when type == BuildingType.Laboratory:
                c[Resources.Emerald] = 1; c[Resources.Glass] = 1;
                break;
            case 2 when type == BuildingType.AlchemyFurnace:
                c[Resources.Gold] = 1; c[Resources.Metal] = 1;
                break;
            case 2 when type == BuildingType.Barricade:
                c[Resources.Metal] = 1;
                break;
            case 2 when type == BuildingType.DefenseTower:
                c[Resources.Weapon] = 3;
                break;
            case 3 when type == BuildingType.Logging:
                c[Resources.Lumber] = 2; c[Resources.Bricks] = 1;
                break;
            case 3 when type == BuildingType.Quarry:
                c[Resources.Bricks] = 2; c[Resources.Lumber] = 1;
                break;
            case 3 when type == BuildingType.Mine:
                c[Resources.Walls] = 1; c[Resources.Tools] = 1;
                break;
            case 3 when type == BuildingType.Farm:
                c[Resources.Lumber] = 2; c[Resources.Bread] = 1;
                break;
            case 3 when type == BuildingType.Sawmill:
                c[Resources.Bricks] = 1; c[Resources.Lumber] = 2;
                break;
            case 3 when type == BuildingType.Kiln:
                c[Resources.Walls] = 1; c[Resources.Lumber] = 1;
                break;
            case 3 when type == BuildingType.Smelter:
                c[Resources.Metal] = 2; c[Resources.Walls] = 1;
                break;
            case 3 when type == BuildingType.Charcoal:
                c[Resources.Lumber] = 2; c[Resources.Coal] = 1;
                break;
            case 3 when type == BuildingType.Crusher:
                c[Resources.Bricks] = 2; c[Resources.Sand] = 1;
                break;
            case 3 when type == BuildingType.Bakery:
                c[Resources.Bread] = 2; c[Resources.Bricks] = 1;
                break;
            case 3 when type == BuildingType.Carpentry:
                c[Resources.Furniture] = 1; c[Resources.Walls] = 1;
                break;
            case 3 when type == BuildingType.Masonry:
                c[Resources.Walls] = 2; c[Resources.Tools] = 1;
                break;
            case 3 when type == BuildingType.Forge:
                c[Resources.Tools] = 2; c[Resources.Walls] = 1;
                break;
            case 3 when type == BuildingType.Glassworks:
                c[Resources.Glass] = 2; c[Resources.Tools] = 1;
                break;
            case 3 when type == BuildingType.Armory:
                c[Resources.Weapon] = 2; c[Resources.Tools] = 1;
                break;
            case 3 when type == BuildingType.Barracks:
                c[Resources.Weapon] = 1; c[Resources.Walls] = 1; c[Resources.Bread] = 1;
                break;
            case 3 when type == BuildingType.Laboratory:
                c[Resources.Emerald] = 2; c[Resources.Tools] = 1;
                break;
            case 3 when type == BuildingType.AlchemyFurnace:
                c[Resources.Gold] = 2; c[Resources.Tools] = 1;
                break;
        }
        return c;
    }

    public static int GetProduction(BuildingType type, int level)
    {
        switch (type)
        {
            case BuildingType.Logging:
            case BuildingType.Quarry:
            case BuildingType.Mine:
            case BuildingType.Farm:
                switch (level)
                {
                    case 1:
                        return 2;
                    case 2:
                        return 3;
                    case 3:
                        return 6;
                }
                break;
            case BuildingType.Sawmill:
            case BuildingType.Kiln:
            case BuildingType.Smelter:
            case BuildingType.Charcoal:
            case BuildingType.Crusher:
            case BuildingType.Bakery:
                switch (level)
                {
                    case 1:
                        return 1;
                    case 2:
                        return 2;
                    case 3:
                        return 4;
                }
                break;
            case BuildingType.Carpentry:
            case BuildingType.Masonry:
            case BuildingType.Forge:
            case BuildingType.Glassworks:
            case BuildingType.Armory:
                switch (level)
                {
                    case 1:
                        return 1;
                    case 2:
                        return 2;
                    case 3:
                        return 3;
                }
                break;
            case BuildingType.Barracks when level == 1:
                return 1;
            case BuildingType.Barracks when level == 2:
                return 2;
            case BuildingType.Barracks when level == 3:
                return 3;
            case BuildingType.Laboratory:
            case BuildingType.AlchemyFurnace:
                switch (level)
                {
                    case 1:
                    case 2:
                        return 1;
                    case 3:
                        return 2;
                }
                break;
        }
        return 0;
    }

    public static Resources GetProducerOutput(BuildingType type)
        => type switch
        {
            BuildingType.Logging => Resources.Wood,
            BuildingType.Quarry => Resources.Stone,
            BuildingType.Mine => Resources.Ore,
            BuildingType.Farm => Resources.Wheat,
            _ => Resources.Wood
        };

    public static Resources GetProcessorOutput(BuildingType type)
        => type switch
        {
            BuildingType.Sawmill => Resources.Lumber,
            BuildingType.Kiln => Resources.Bricks,
            BuildingType.Smelter => Resources.Metal,
            BuildingType.Charcoal => Resources.Coal,
            BuildingType.Crusher => Resources.Sand,
            BuildingType.Bakery => Resources.Bread,
            BuildingType.Carpentry => Resources.Furniture,
            BuildingType.Masonry => Resources.Walls,
            BuildingType.Forge => Resources.Tools,
            BuildingType.Glassworks => Resources.Glass,
            BuildingType.Armory => Resources.Weapon,
            BuildingType.Laboratory => Resources.Emerald,
            BuildingType.AlchemyFurnace => Resources.Gold,
            _ => Resources.Wood
        };

    public static Dictionary<Resources, int> GetProcessorInput(BuildingType type)
    {
        var inp = new Dictionary<Resources, int>();
        
        switch (type)
        {
            case BuildingType.Sawmill:
                inp[Resources.Wood] = 2;
                break;
            case BuildingType.Kiln:
                inp[Resources.Stone] = 2;
                break;
            case BuildingType.Smelter:
                inp[Resources.Ore] = 3;
                break;
            case BuildingType.Charcoal:
                inp[Resources.Wood] = 2;
                break;
            case BuildingType.Crusher:
                inp[Resources.Stone] = 2;
                break;
            case BuildingType.Bakery:
                inp[Resources.Wheat] = 2; inp[Resources.Wood] = 1;
                break;
            case BuildingType.Carpentry:
                inp[Resources.Lumber] = 3;
                break;
            case BuildingType.Masonry:
                inp[Resources.Bricks] = 2; inp[Resources.Stone] = 1;
                break;
            case BuildingType.Forge:
                inp[Resources.Metal] = 2; inp[Resources.Wood] = 1;
                break;
            case BuildingType.Glassworks:
                inp[Resources.Sand] = 2; inp[Resources.Coal] = 1;
                break;
            case BuildingType.Armory:
                inp[Resources.Metal] = 2; inp[Resources.Lumber] = 1;
                break;
            case BuildingType.Laboratory:
                inp[Resources.Glass] = 2; inp[Resources.Coal] = 1; inp[Resources.Tools] = 1;
                break;
            case BuildingType.AlchemyFurnace:
                inp[Resources.Metal] = 2; inp[Resources.Coal] = 2; inp[Resources.Tools] = 1;
                break;
        }
        
        return inp;
    }

    public static bool IsProducer(BuildingType type)
    {
        return type is BuildingType.Logging or BuildingType.Quarry or BuildingType.Mine or BuildingType.Farm;
    }

    public static bool IsProcessor(BuildingType type)
    {
        return type is BuildingType.Sawmill or BuildingType.Kiln or BuildingType.Smelter or BuildingType.Charcoal 
                       or BuildingType.Crusher or BuildingType.Bakery or BuildingType.Carpentry or BuildingType.Masonry 
                       or BuildingType.Forge or BuildingType.Glassworks or BuildingType.Armory or BuildingType.Laboratory 
                       or BuildingType.AlchemyFurnace;
    }

    public static Dictionary<Resources, int> GetSoldierCost(ArchetypeType arch)
    {
        var c = new Dictionary<Resources, int>();
        
        switch (arch)
        {
            case ArchetypeType.Warrior:
                c[Resources.Bread] = 5;
                c[Resources.Weapon] = 2;
                break;
            case ArchetypeType.Recruit:
                c[Resources.Bread] = 1;
                c[Resources.Weapon] = 1;
                break;
            case ArchetypeType.Glutton:
                c[Resources.Bread] = 6;
                c[Resources.Weapon] = 1;
                break;
            default:
                c[Resources.Bread] = 3;
                c[Resources.Weapon] = 1;
                break;
        }
        
        return c;
    }
}
