using GooseGame.Model.Cell;

namespace GooseGame.Model.Player
{
    /// <summary>
    /// Rappresenta un partecipante
    /// </summary>
    public class Player
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">Nome del giocatore</param>
        public Player(string name)
        {
            this.Name = name;
            Cell = new StartCell();
        }

        /// <summary>
        /// Nome del giocatore
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Posizione del giocatore sul tabellone di gioco
        /// </summary>
        public ICell Cell { get; set; }
    }
}
