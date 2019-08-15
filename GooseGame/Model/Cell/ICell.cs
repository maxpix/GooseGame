namespace GooseGame.Model.Cell
{
    /// <summary>
    /// Interface; rappresenta una casella del tabellone di gioco
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Tipo della casella
        /// </summary>
        CellType Type { get; }

        /// <summary>
        /// Posizione della casella nel tabellone di gioco, da 0 (start) all'ultima casella
        /// </summary>
        int Position { get; }

        /// <summary>
        /// Nome della casella
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Indica se il posizionamento di un giocatore sulla casella implica lo spostamento a una cella successiva, secondo una regola definita
        /// </summary>
        /// <param name="points">Il numero di posizioni per effettuare lo spostamento</param>
        /// <returns>La posizione successiva allo spostamento se previsto, oppure "null"</returns>
        int? NextPosition(int points);
    }

}
