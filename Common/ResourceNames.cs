namespace Common;

public static class ResourceNames
{
    public static string GetName(Resources res)
    {
        return res switch
        {
            Resources.Wood => "🪵 Дерево",
            Resources.Stone => "🪨 Камень",
            Resources.Ore => "⛰️ Руда",
            Resources.Wheat => "🌾 Зерно",
            Resources.Lumber => "🪓 Доски",
            Resources.Bricks => "🧱 Кирпич",
            Resources.Metal => "⚙️ Металл",
            Resources.Coal => "♨️ Уголь",
            Resources.Sand => "🏝️ Песок",
            Resources.Bread => "🍞 Хлеб",
            Resources.Furniture => "🪑 Мебель",
            Resources.Walls => "🏗️ Стены",
            Resources.Tools => "🔧 Инструменты",
            Resources.Glass => "🪟 Стекло",
            Resources.Weapon => "⚔️ Оружие",
            Resources.Gold => "🪙 Золото",
            Resources.Emerald => "💎 Изумруд",
            Resources.Soldier => "🛡️ Солдат",
            _ => res.ToString()
        };
    }

    public static string GetName(string resName)
    {
        if (Enum.TryParse<Resources>(resName, out var res))
            return GetName(res);
        return resName;
    }
}
