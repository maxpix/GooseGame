namespace GooseGame.Model.Cell
{

    public class BridgeCell: StdCell
    {
        /// <summary>
        /// Numero di caselle di cui avanza un giocatore che finisca sulla casella "Bridge"
        /// </summary>
        private int _Steps;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="position">Posizione della casella sul tabellone</param>
        /// <param name="steps">Numero di caselle di cui avanza un giocatore che finisca sulla casella "Bridge"</param>
        public BridgeCell(int position, int steps) : base(position)
        {
            _Steps = steps;
        }

        /// <summary>
        /// Imposta il tipo della casella
        /// </summary>
        protected override void SetCellType()
        {
            this.Type = CellType.Bridge;
        }

        /// <summary>
        /// Nome della casella
        /// </summary>
        public override string Name { get { return "the Bridge"; } }

        /// <summary>
        /// Indica se il posizionamento di un giocatore sulla casella implica lo spostamento a una cella successiva, secondo una regola definita
        /// </summary>
        /// <param name="points">Il numero di posizioni per effettuare lo spostamento</param>
        /// <returns>La posizione successiva allo spostamento se previsto, oppure "null"</returns>
        public override int? NextPosition(int points)
        {
            return this.Position + _Steps;
        }

    }
}
