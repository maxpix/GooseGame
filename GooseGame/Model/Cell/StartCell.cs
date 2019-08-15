namespace GooseGame.Model.Cell
{

    public class StartCell: StdCell
    {
        /// <summary>
        /// Empty constructor
        /// </summary>
        public StartCell() : base(0)
        {
        }

        /// <summary>
        /// Imposta il tipo della casella
        /// </summary>
        protected override void SetCellType() { this.Type = CellType.Start; }

        /// <summary>
        /// Nome della casella
        /// </summary>
        public override string Name { get { return "Start"; } }
    }
}
