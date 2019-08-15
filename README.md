# GooseGame 
Implementazione in C# del Goose Game Kata (https://github.com/xpeppers/goose-game-kata)

L'applicazione può essere compilata in VisualStudio per ottenere una console application.
Lanciando l'eseguibile viene aperta una finestra DOS che, a seconda della configurazione
- permette di "giocare una partita" di gioco dell'oca fornendo dei comandi manuali;
- permette di eseguire automaticamente una partita di gioco dell'oca.

## Configurazione
L'applicazione ha un file di configurazione (GooseGame.exe.config), che prevede i seguenti parametri:
- LastCell (numerico), stabilisce il numero di "caselle" del gioco dell'oca;
- Bridge (numerico), stabilisce la posizione della casella "Bridge", che consente al giocatore che vi giunge con il tiro dei dadi, di avanzare ulteriormente di un numero stabilito di caselle;
- BridgeSteps (numerico), che stabilisce di quante caselle avanti si può spostare il giocatore che al suo turno muove sulla casella Bridge
- GooseCells (lista di numeri separati da virgola), che indica in quali posizioni si trovano le caselle Goose, che permettono al giocatore che vi giunge di muovere nuovamente un numero di passi pari all'ultimo punteggio ottenuto con il lancio dei dadi
- AutoExec (numerico, 1/0), che stabilisce se al lancio l'applicazione deve automaticamente eseguire una partita con l'elenco di giocatori prestabilito (vedere parametro successivo), oppure se l'esecuzione della partita deve essere "manuale", specificando da parte dell'operatore i comandi di aggiunta giocatori o esecuzione di mossa degli stessi. Se AutoExec = 1 il programma giocherà una partita automatica; altrimenti sarà necessario specificare ogni singolo comando
- AutoPlayers, è la lista dei nomi dei giocatori, separati da virgola, da considerare per far giocare una partita in automatico al programma.

La configurazione va definita attentamente per evitare che il posizionamento delle caselle "Goose", o della casella Bridge sia tale da evitare situazioni senza uscita (per es.: casella "Goose" in 61, ultima cella 63, e il giocatore totalizza 4 con i dadi).

## Esecuzione manuale
Per eseguire una partita in modalità "manuale" si dovrà porre in configurazione AutoExec = 0. 
Lanciando l'applicazione verrà aperta una finestra DOS in cui l'operatore dovrà digitare i comandi desiderati.
I comandi previsti sono:
- add player {player_name} per aggiungere un giocatore alla lista dei partecipanti. Se il nome è già in uso, l'operazione non viene eseguita. L'operatore in ogni caso ha un riscontro dell'esito -positivo o meno- dell'operazione
- move {player_name} [dice1_points, dice2_points] per muovere un giocatore secondo il punteggio, eventualmente specificato dall'operatore: se il comando non comprende la parte "dice1_points, dice2_points" il sistema fornirà due valori casuali compresi tra 1 e 6. Anche in questo caso, l'esecuzione del comando darà un riscontro dell'esito all'operatore.

## Esecuzione automatica
Ponendo AutoExec = 1 e specificando una lista di nomi con il parametro "AutoPlayers" il sistema gioca una partita senza che sia necessario specificare dei comandi: lanciando l'eseguibile i giocatori verranno aggiunti uno ad uno, e verranno eseguite a turno delle mosse fino al termine della partita.

