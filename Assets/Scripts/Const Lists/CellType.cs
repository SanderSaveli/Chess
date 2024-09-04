namespace OFG.ChessPeak
{
    public enum CellType
    {
        /// <summary>
        /// Стандартная клетка
        /// </summary>
        defaultCell,
        /// <summary>
        /// Пропасть, фигуры не могут стоять на ней
        /// </summary>
        abyss,
        /// <summary>
        /// Портал, переходя на эту клетку фигура перемещается на вторую клетку портала
        /// </summary>
        portal,
        /// <summary>
        /// Клетка превращения, становясь на эту клетку фигуры превращается в другую фигуру
        /// </summary>
        transformationCell
    }
}
