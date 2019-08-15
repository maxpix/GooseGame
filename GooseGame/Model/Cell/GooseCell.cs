namespace GooseGame.Model.Cell
{

    public class GooseCell : StdCell
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="position">Posizione della casella sul tabellone</param>
        public GooseCell(int position) : base(position)
        {
        }

        /// <summary>
        /// Imposta il tipo della casella
        /// </summary>
        protected override void SetCellType() { this.Type = CellType.Goose; }

        /// <summary>
        /// Nome della casella
        /// </summary>
        public override string Name { get { return this.Position.ToString() + ", the Goose"; } }

        /// <summary>
        /// Indica se il posizionamento di un giocatore sulla casella implica lo spostamento a una cella successiva, secondo una regola definita
        /// </summary>
        /// <param name="points">Il numero di posizioni per effettuare lo spostamento</param>
        /// <returns>La posizione successiva allo spostamento se previsto, oppure "null"</returns>
        public override int? NextPosition(int points)
        {
            return this.Position + points;
        }
    }
}
