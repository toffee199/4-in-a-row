using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    //this is our singelton
    public static GameController gameInstance;

    //views
    public StartView startView;
    public GameView gameView;

    private int currentPlayer;

    //models
    GameModel gameModel;
    
    private void Awake()
    {
        gameModel = new GameModel();
        InitializeWindows();

        if (gameInstance == null)
        {
            gameInstance = this;
            SetCallbacks();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame(string player1Name, string player2Name)
    {
        
        if (!string.IsNullOrEmpty(player1Name) && !string.IsNullOrEmpty(player2Name)) {

            startView.Hide();

            GetPlayerTurn();
            gameModel.SetPlayerNames(player1Name, player2Name);
            gameView.StartNewGame(player1Name, player2Name);
            gameView.Show();

        }
        else
        {
            //show error
        }
    }

    private void InitializeWindows()
    {
        startView.Show();
        gameView.Hide();
        
    }

    private void GetPlayerTurn()
    {
        currentPlayer = Random.Range(0,2);

    }

    private void SetCallbacks()
    {
        startView.onStartGame += StartGame; //assiging StartGame function to be called onStartGame
        gameView.board.onColumnClicked += HandleColumnClicked;
    }

    public SlotState GetCurrentPlayerColor()
    {
        return currentPlayer == 1 ? SlotState.RED : SlotState.WHITE;
    }

    public void HandleColumnClicked(int colIndex)
    {
        Debug.Log("chene " + colIndex);
    }
}
