using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //this is our singelton
    public static GameController gameInstance;

    //views
    public StartView startView;
    public GameView gameView;
    public GameOverView gameOverView;

    private SlotState currentPlayer;

    bool isVsAi = false;
    //models
    private GameModel gameModel;
    
    private void Awake()
    {
        if (gameInstance == null)
        {
            gameInstance = this;
            gameModel = new GameModel();
            InitializeWindows();
            SetCallbacks();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame(string player1Name, string player2Name)
    {
        bool err = true;
        if (!string.IsNullOrEmpty(player1Name))
        {
            if (isVsAi)
            {
                startView.Hide();
                GetPlayerTurn();
                gameModel.SetPlayerNames(player1Name, "Computer");
                gameView.StartNewGame(player1Name, "Computer");
                gameView.Show();
                err = false;
                if (currentPlayer == SlotState.WHITE) HandleColumnClicked(Random.Range(0, 7));
            }
            else if(!string.IsNullOrEmpty(player2Name))
            {
                startView.Hide();
                GetPlayerTurn();
                gameModel.SetPlayerNames(player1Name, player2Name);
                gameView.StartNewGame(player1Name, player2Name);
                gameView.Show();
                err = false;
            }
        }

        if (err)
        {
            startView.SetErrorMessage("Must enter name.");
        }
    }

    private void InitializeWindows()
    {
        startView.Show();
        gameView.Hide();
        gameOverView.Hide();
        
    }

    private void GetPlayerTurn()
    {
        int currentPlayerIndex = Random.Range(0,2);
        currentPlayer = (currentPlayerIndex == (int)SlotState.RED) ? SlotState.WHITE : SlotState.RED;

    }

    private void SetCallbacks()
    {
        startView.onStartGame += StartGame; //assiging StartGame function to be called onStartGame
        startView.onAiToggled += HandleAiToggled;
        gameView.board.onColumnClicked += HandleColumnClicked;
        gameView.board.onColumnHovered += HandleColumnHovered;
        gameOverView.onNewGame += InitializeWindows;
        gameOverView.onExit += Application.Quit;
    }

    private void OnDestroy()
    {
        startView.onStartGame -= StartGame;
        startView.onAiToggled -= HandleAiToggled;
        gameView.board.onColumnClicked -= HandleColumnClicked;
        gameView.board.onColumnHovered -= HandleColumnHovered;
        gameOverView.onNewGame += InitializeWindows;
        gameOverView.onExit += Application.Quit;
    }

    private void TogglePlayerTurn(int colIndex)
    {
        int nextAvilableSlot = gameModel.GetNextAvilableSlot(colIndex);
        
        currentPlayer = (currentPlayer == SlotState.RED) ? SlotState.WHITE : SlotState.RED;
        if (!isVsAi)
        {
            if (nextAvilableSlot >= 0)
            {
                gameView.board.SetHoverSlotState(colIndex, currentPlayer);
            }
            else
            {
                gameView.board.SetHoverSlotState(colIndex, SlotState.EMPTY);
            }
        }

        if (isVsAi && currentPlayer == SlotState.WHITE) {
            HandleColumnClicked(Random.Range(0, 7));
        }
    }

    private void HandleGameOver(bool isTie)
    {
        gameView.Hide();
        gameModel.ResetBoard();
        string winningPlayerName = isTie ? null : gameModel.GetPlayerName(currentPlayer);
        SlotState winningPlayer = isTie ? SlotState.EMPTY : currentPlayer;
        gameOverView.ShowGameOverView(winningPlayer, winningPlayerName);
    }

    
    public void HandleColumnClicked(int colIndex)
    {
        int nextAvilableSlot = gameModel.GetNextAvilableSlot(colIndex);

        if (nextAvilableSlot >= 0)
        {
            gameModel.SetUsedSlot(colIndex, nextAvilableSlot, currentPlayer);
            gameView.board.SetSlotState(colIndex, nextAvilableSlot, currentPlayer);

            bool isWin = gameModel.GetGameWinState(colIndex, nextAvilableSlot, currentPlayer);
            bool isTie = gameModel.GetIsBoardFull();

            if (!isWin && !isTie)
            {
                TogglePlayerTurn(colIndex);
            }
            else
            {
                HandleGameOver(isTie);
            }
        }
    }

    public void HandleColumnHovered(int colIndex)
    {
        int nextAvilableSlot = gameModel.GetNextAvilableSlot(colIndex);

        //if column is full
        if (nextAvilableSlot < 0)
        {
            gameView.board.SetHoverSlotState(colIndex, SlotState.EMPTY);
        }
        else {
            gameView.board.SetHoverSlotState(colIndex, currentPlayer);
        }
    }

    public void HandleAiToggled(bool isAi)
    {
        isVsAi = isAi;
    }
}
