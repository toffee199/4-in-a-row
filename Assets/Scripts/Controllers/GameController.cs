using System.Collections.Generic;
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
    int player1Score = 210, player2Score = 210;

    //models
    private GameModel gameModel;
    private List<HighScoreItemData> highScores;

    private void Awake()
    {
        if (gameInstance == null)
        {
            gameInstance = this;
            gameModel = new GameModel();
            InitializeWindows();
            SetCallbacks();
            var highScoreJson = PlayerPrefs.GetString("High Scores", null);
            if (highScoreJson != null)
            {
                try
                {
                    highScores = JsonHelper.FromJson<HighScoreItemData>(highScoreJson);
                }
                catch
                {

                }
            }
            if (highScores == null || highScores.Count == 0)
            {
                highScores = new List<HighScoreItemData>();
            }
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
            player1Score = 210;
            player2Score = 210;
            if (isVsAi)
            {
                startView.Hide();
                GetPlayerTurn();
                gameModel.SetPlayerNames(player1Name, "Computer");
                gameView.StartNewGame(player1Name, "Computer");
                gameView.SetScores(player1Score, player2Score);
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
                gameView.SetScores(player1Score, player2Score);
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
        if(currentPlayer == SlotState.RED)
        {
            currentPlayer = SlotState.WHITE;
            player1Score -= 10;
        }
        else
        {
            currentPlayer = SlotState.RED;
            player2Score -= 10;
        }
        gameView.SetScores(player1Score, player2Score);
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
        int winningScore = winningPlayer == SlotState.RED ? player1Score : player2Score;
        if (!isTie) { SaveHighScore(winningScore, winningPlayerName); }
        gameOverView.ShowGameOverView(winningPlayer, GetTopHighScores(), winningPlayerName);
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

    private void SaveHighScore(int score, string name)
    {
        var newHighScore = new HighScoreItemData(name, score);
        highScores.Add(newHighScore);
        var highScoreJson = JsonHelper.ToJson(highScores);
        Debug.Log("Saving High Scores: " + highScoreJson);
        PlayerPrefs.SetString("High Scores", highScoreJson);
    }

    private List<HighScoreItemData> GetTopHighScores()
    {
        List<HighScoreItemData> retval = new List<HighScoreItemData>();
        highScores.Sort((x, y) => y.score.CompareTo(x.score));
        int i = 0;
        while (i < 5 && i < highScores.Count)
        {
            retval.Add(highScores[i]);
            i++;
        }
        return retval;
    }
}
