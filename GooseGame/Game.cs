using GooseGame.Model.Cell;
using GooseGame.Model.Player;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GooseGame
{
    public class Game
    {
        /// <summary>
        /// Lista dei partecipanti
        /// </summary>
        private List<Player> _GamePlayers;

        /// <summary>
        /// Lista delle caselle costituenti il percorso
        /// </summary>
        private List<ICell> _Cells;

        /// <summary>
        /// Posizione finale sul tabellone
        /// </summary>
        private int _LastCell;

        /// <summary>
        /// constructor
        /// </summary>
        public Game()
        {
            this.Stop = false;

            List<int> gooseCellsPositions = null;
            int bridgePosition;
            int bridgeSteps;

            #region Lettura della configurazione
            
            // Posizione dell'ultima casella
            if (string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["LastCell"]))
                throw new Exception("Last cell value must be specified");
            try
            {
                _LastCell = int.Parse(System.Configuration.ConfigurationManager.AppSettings["LastCell"]);
            }
            catch(Exception ex)
            {
                throw new Exception("Last cell value must be a number");
            }

            // Posizione della casella "Bridge"
            if (string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["Bridge"]))
                throw new Exception("Bridge cell value must be specified");
            try
            {
                bridgePosition = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Bridge"]);
            }
            catch (Exception ex)
            {
                throw new Exception("Bridge cell value must be a number");
            }

            // Numero di caselle di cui un giocatore avanza se conclude una mossa sulla casella "Bridge"
            if (string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["BridgeSteps"]))
                throw new Exception("Bridge steps value must be specified");
            try
            {
                bridgeSteps = int.Parse(System.Configuration.ConfigurationManager.AppSettings["BridgeSteps"]);
            }
            catch (Exception ex)
            {
                throw new Exception("Bridge steps value must be a number");
            }

            // Lista delle posizioni delle caselle "Goose"
            try
            {
                var gooseCellsList = System.Configuration.ConfigurationManager.AppSettings["GooseCells"];
                if (!string.IsNullOrWhiteSpace(gooseCellsList))
                    gooseCellsPositions = gooseCellsList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim())).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Goose cells must  be specified as a list of integers separated by commas");
            }

            #endregion Lettura della configurazione

            // Inizializza la lista dei giocatori
            _GamePlayers = new List<Player>();

            // Inizializza la lista delle caselle
            _Cells = new List<ICell>();
            for(int i = 0; i <= _LastCell; i++)
            {
                if (i == 0)
                    _Cells.Add(new StartCell());
                else if (i == bridgePosition)
                    _Cells.Add(new BridgeCell(i, bridgeSteps));
                else if (gooseCellsPositions.Contains(i))
                    _Cells.Add(new GooseCell(i));
                else
                    _Cells.Add(new StdCell(i));

            }
        }

        /// <summary>
        /// Lista dei partecipanti
        /// </summary>
        public ReadOnlyCollection<Player> Players
        {
            get { return _GamePlayers.AsReadOnly(); }
        }

        /// <summary>
        /// Flag: true quando un giocatore termina il gioco arrivando sull'ultima casella
        /// </summary>
        public bool Stop { get; private set; }

        /// <summary>
        /// Verifica se esiste un giocatore con un nome specificato nella lista dei partecipanti 
        /// </summary>
        /// <param name="playerName">Nome del giocatore</param>
        /// <returns>true se il giocatore è tra i partecipanti</returns>
        public bool PlayerExists(string playerName)
        {
            return (GetPlayer(playerName) != null);
        }

        /// <summary>
        /// Restituisce un giocatore (oggetto "Player") identificato dal nome specificato dalla lista dei partecipanti 
        /// </summary>
        /// <param name="playerName">Nome del giocatore</param>
        /// <returns>Il giocatore (oggetto "Player") identificato dal nome se esiste; "null" in caso contrario</returns>
        private Player GetPlayer(string playerName)
        {
            return _GamePlayers.FirstOrDefault(x => string.Compare(x.Name, playerName, true) == 0);
        }

        /// <summary>
        /// Aggiunge un giocatore alla lista dei partecipanti, se il nome che lo identifica non è già attribuito a un altro partecipante
        /// </summary>
        /// <param name="playerName">Nome del giocatore</param>
        /// <returns>true se il giocatore può essere aggiunto; false in caso contrario</returns>
        public bool AddPlayer(string playerName)
        {
            if (GetPlayer(playerName) != null) // il nome è già utilizzato
                return false; 
            else
            {
                _GamePlayers.Add(new Player(playerName));
                return true;
            }

        }

        /// <summary>
        /// Dato il nome del partecipante e il punteggio dei dati, esegue la mossa
        /// </summary>
        /// <param name="playerName">Nome del giocatore da muovere</param>
        /// <param name="dice1">Punteggio del primo dado</param>
        /// <param name="dice2">Punteggio del secondo dado</param>
        /// <returns>Descrizione della mossa eseguita</returns>
        public string MovePlayer(string playerName, int dice1, int dice2)
        { 
            // return value
            string res = string.Empty;
            // giocatore
            var player = GetPlayer(playerName);
            // flag: true se la mossa porta il giocatore oltre l'ultima casella
            bool bounceBack = false;
            // Posizione del giocatore prima della mossa
            ICell currCell = player.Cell;

            // Nuova posizione del giocatore
            int newPos = currCell.Position + dice1 + dice2;
            if (newPos > _Cells.Count - 1)
            {
                newPos = _LastCell - (newPos - _LastCell);
                bounceBack = true;
            }

            // Recupero della casella corrispondente e aggiornamento della descrizione della mossa
            ICell newCell = _Cells[newPos];
            res = playerName + " rolls " + dice1.ToString() + ", " + dice2.ToString() + ". "
                + playerName + " moves from " + currCell.Name + " to " + newCell.Name + ". ";

            // Il giocatore termina
            if (newCell.Position == _LastCell)
            {
                res += playerName + " wins!!";
                this.Stop = true;
            }
            else
            {
                // Il giocatore andrebbe oltre l'ultima casella e deve tornare indietro
                if (bounceBack)
                {
                    res = playerName + " rolls " + dice1.ToString() + ", " + dice2.ToString() + ". "
                        + playerName + " moves from " + currCell.Name + " to " + _Cells[_LastCell].Name + ". ";
                    res += playerName + " bounces! " + playerName + " returns to " + newCell.Name + ". ";
                }
                // Verifica del tipo di cella per l'esecuzione di ulteriori mosse
                while (newCell.NextPosition(dice1 + dice2).HasValue)
                {
                    switch (newCell.Type)
                    {
                        case CellType.Bridge:
                            newCell = _Cells[newCell.NextPosition(dice1 + dice2).Value];
                            res += playerName + " jumps to " + newCell.Name + ". ";
                            break;
                        case CellType.Goose:
                            newCell = _Cells[newCell.NextPosition(dice1 + dice2).Value];
                            res += playerName + " moves again and goes to " + newCell.Name + ". ";
                            break;
                        default: break;
                    }
                }
            }

            // "prank"
            var othPlayer = _GamePlayers.FirstOrDefault(x => x.Cell.Position == newCell.Position && string.Compare(x.Name, playerName, true) != 0);
            if (othPlayer != null)
            {
                res += "On " + newCell.Name + " there is " + othPlayer.Name + ", who returns to " + player.Cell.Name;
                othPlayer.Cell = player.Cell;
            }
            
            // Attribuzione della nuova posizione al giocatore
            player.Cell = newCell;

            return res;
        }
    }
}
