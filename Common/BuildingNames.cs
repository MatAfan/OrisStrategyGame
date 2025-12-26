namespace Common;

public static class BuildingNames
{
    public static string GetName(BuildingType type)
    {
        return type switch
        {
            BuildingType.Logging => "Лесозаготовка",
            BuildingType.Quarry => "Карьер",
            BuildingType.Mine => "Шахта",
            BuildingType.Farm => "Поле",
            BuildingType.Sawmill => "Лесопилка",
            BuildingType.Kiln => "Печь обжига",
            BuildingType.Smelter => "Плавильня",
            BuildingType.Charcoal => "Углежог",
            BuildingType.Crusher => "Дробилка",
            BuildingType.Bakery => "Пекарня",
            BuildingType.Carpentry => "Столярная",
            BuildingType.Masonry => "Каменотёс",
            BuildingType.Forge => "Кузница",
            BuildingType.Glassworks => "Стекловарня",
            BuildingType.Armory => "Оружейная",
            BuildingType.Barracks => "Казармы",
            BuildingType.Laboratory => "Лаборатория",
            BuildingType.AlchemyFurnace => "Алхимическая печь",
            BuildingType.Barricade => "Баррикада",
            BuildingType.DefenseTower => "Оборонительная башня",
            _ => type.ToString()
        };
    }
}
