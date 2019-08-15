namespace GooseGame.Model.Cell
{
    public class StdCell : ICell
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="position">Posizione della casella sul tabellone</param>
        public StdCell(int position)
        {
            SetCellType();
            this.Position = position;
        }

        /// <summary>
        /// Imposta il tipo della casella
        /// </summary>
        protected virtual void SetCellType()
        {
            this.Type = CellType.Standard;
        }

        /// <summary>
        /// Tipo della casella
        /// </summary>
        public CellType Type { get; protected set; }

        /// <summary>
        /// Posizione della casella sul tabellone
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        /// Nome della casella
        /// </summary>
        public virtual string Name { get { return this.Position.ToString(); } }

        /// <summary>
        /// Indica se il posizionamento di un giocatore sulla casella implica lo spostamento a una cella successiva, secondo una regola definita
        /// </summary>
        /// <param name="points">Il numero di posizioni per effettuare lo spostamento</param>
        /// <returns>La posizione successiva allo spostamento se previsto, oppure "null"</returns>
        public virtual int? NextPosition(int points)
        {
            return null;
        }
    }

}
