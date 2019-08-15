using System;
using System.Linq;

namespace GooseGame
{
    class Program
    {
        private static Game _Instance = new Game();

        /// <summary>
        /// entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Esecuzione automatica con lista di giocatori prestabilita
            if (System.Configuration.ConfigurationManager.AppSettings["AutoExec"] == "1")
            {
                // aggiunta dei giocatori
                var players = System.Configuration.ConfigurationManager.AppSettings["AutoPlayers"]
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                foreach (var name in players)
                    Console.WriteLine(AddPlayer(name));

                // esecuzione del gioco
                while (!_Instance.Stop)
                {
                    foreach(var player in _Instance.Players)
                    {
                        // "lancio dei dadi"
                        var seed = DateTime.Now.Millisecond * DateTime.Now.Second + player.GetHashCode();
                        var random = new Random(seed);
                        var dice1Pts = random.Next(1, 6);
                        var dice2Pts = random.Next(1, 6);

                        Console.WriteLine(MovePlayer(player.Name, dice1Pts, dice2Pts));
                        // interruzione in caso di vittoria
                        if (_Instance.Stop) break;
                    }
                }
            }
            else // Esecuzione manuale
            {
                while (!_Instance.Stop)
                {
                    var command = Console.ReadLine();
                    ParseCommand(command.Trim());

                }
            }
            Console.WriteLine("Game ended.");
            Console.ReadKey();
        }

        /// <summary>
        /// Esegue il parsing di un comando passato all'applicazione
        /// </summary>
        /// <param name="typedCommand">Comando in input</param>
        private static void ParseCommand(string typedCommand)
        {
            if (typedCommand.ToLowerInvariant().StartsWith("add player")) // aggiunta di un giocatore
            {
                // legge il nome del giocatore da aggiungere...
                var playerName = typedCommand.Substring("add player".Length).Trim();
                // ...e lo aggiunge alla lista
                Console.WriteLine(AddPlayer(playerName));
            }

            else if (typedCommand.ToLowerInvariant().StartsWith("move")) // esecuzione di una mossa
            {
                // suddivide il comando in input nelle singole parti: nome del comando (che verrà ignorato)/nome del giocatore/punteggio di due dadi (optional)
                string[] cmdParts = typedCommand.Split(new char[] { ' ' }, 3);
                if (cmdParts.Length <= 1)
                    Console.WriteLine("Incorrect command");
                else
                {
                    string playerName = cmdParts[1].Trim();
                    int dice1Pts = 0;
                    int dice2Pts = 0;
                    if (cmdParts.Length == 2)
                    {
                        // "lancio dei dadi" da parte dell'applicazione
                        var seed = DateTime.Now.Millisecond * DateTime.Now.Second + playerName.GetHashCode();
                        var random = new Random(seed);
                        dice1Pts = random.Next(1, 6);
                        dice2Pts = random.Next(1, 6);
                    }
                    else
                    {
                        // legge il punteggio dei due dadi inserito in input
                        string[] dice = cmdParts[2].Split(new char[] { ',' }).Select(x => x.Trim()).ToArray();
                        if (!int.TryParse(dice[0], out dice1Pts) || !int.TryParse(dice[1], out dice2Pts))
                            Console.WriteLine("Error in specifying dice points");
                        if (dice1Pts > 6 || dice1Pts < 1 || dice2Pts > 6 || dice2Pts < 1)
                            Console.WriteLine("Dice points must be between 1 and 6");
                    }
                    // esecuzione della mossa
                    Console.WriteLine(MovePlayer(playerName, dice1Pts, dice2Pts));
                }

            }
            else
                Console.WriteLine("Unknown command");

        }

        /// <summary>
        /// Aggiunge un giocatore all'elenco dei partecipanti
        /// </summary>
        /// <param name="playerName">Nome del giocatore da aggiungere</param>
        /// <returns>Descrizione dell'esito dell'operazione</returns>
        private static string AddPlayer(string playerName)
        {
            if (playerName.Length == 0)
                return "Player name not specified";
            else
            {
                if (_Instance.AddPlayer(playerName))
                    return "players: " + string.Join(", ", _Instance.Players.Select(x => x.Name).ToArray());
                else
                    return playerName + ": already existing player";
            }
        }

        /// <summary>
        /// Esegue una mossa per il giocatore specificato, dopo aver verificato che sia tra i partecipanti
        /// </summary>
        /// <param name="playerName">Nome del giocatore</param>
        /// <param name="dice1">Punteggio del primo dado</param>
        /// <param name="dice2">Punteggio del secondo dado</param>
        /// <returns>Descrizione dell'esito dell'operazione</returns>
        private static string MovePlayer(string playerName, int dice1, int dice2)
        {

            if (!_Instance.PlayerExists(playerName))
                return "Player " + playerName + " does not exist";

            return _Instance.MovePlayer(playerName, dice1, dice2);
           
        }
    }
}
