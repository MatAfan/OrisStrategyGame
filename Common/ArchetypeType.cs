namespace Common;

/// <summary>
/// Представляет тип архетипа персонажа в игре.
/// </summary>
public enum ArchetypeType
{
    Greedy,
    Patron,
    Warrior,
    Recruit,
    Engineer,
    Alchemist,
    Glutton,
    Neutral
}

/// <summary>
/// Предоставляет методы расширения для <see cref="ArchetypeType"/>.
/// </summary>
public static class ArchetypeTypeExtensions
{
    /// <summary>
    /// Возвращает локализованное название архетипа на русском языке.
    /// </summary>
    /// <param name="type">Тип архетипа.</param>
    /// <returns>Строка с названием архетипа.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Выбрасывается, если передано неизвестное значение <paramref name="type"/>.
    /// </exception>
    public static string GetName(this ArchetypeType type) => type switch
    {
        ArchetypeType.Greedy => "Жадина",
        ArchetypeType.Patron => "Меценат",
        ArchetypeType.Warrior => "Воин",
        ArchetypeType.Recruit => "Новобранец",
        ArchetypeType.Engineer => "Инженер",
        ArchetypeType.Alchemist => "Алхимик",
        ArchetypeType.Glutton => "Обжора",
        ArchetypeType.Neutral => "Нормис",
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };
}