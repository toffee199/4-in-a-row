using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    //this is our singelton
    public static GameController gameInstance;

    //views
    public StartView startView;
    public GameView gameView;

    private SlotState currentPlayer;

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
        int currentPlayerIndex = Random.Range(0,2);
        currentPlayer = (currentPlayerIndex == (int)SlotState.RED) ? SlotState.WHITE : SlotState.RED;

    }

    private void SetCallbacks()
    {
        startView.onStartGame += StartGame; //assiging StartGame function to be called onStartGame
        gameView.board.onColumnClicked += HandleColumnClicked;
        gameView.board.onColumnHovered += HandleColumnHovered;
    }

    private void OnDestroy()
    {
        startView.onStartGame -= StartGame; 
        gameView.board.onColumnClicked -= HandleColumnClicked;
        gameView.board.onColumnHovered -= HandleColumnHovered;
        
    }

    private void TogglePlayerTurn(int colIndex)
    {
        int hoverSlotIndex = gameView.board.columns.Length - 1;
        int nextAvilableSlot = gameModel.GetNextSlotInColumn(colIndex);

        currentPlayer = (currentPlayer == SlotState.RED) ? SlotState.WHITE : SlotState.RED;

        if(nextAvilableSlot >= 0)
        {
            gameView.board.SetSlotState(colIndex, hoverSlotIndex, currentPlayer);
        } else
        {
            gameView.board.SetSlotState(colIndex, hoverSlotIndex, SlotState.EMPTY);
        }
        
    }

    
    public void HandleColumnClicked(int colIndex)
    {
        
        int nextAvilableSlot = gameModel.GetNextSlotInColumn(colIndex);

        if (nextAvilableSlot >= 0)
        {
            gameModel.SetUsedSlot(colIndex, nextAvilableSlot, currentPlayer);
            gameView.board.SetSlotState(colIndex, nextAvilableSlot, currentPlayer);
            TogglePlayerTurn(colIndex);
        }
    }

    public void HandleColumnHovered(int colIndex)
    {
        int nextAvilableSlot = gameModel.GetNextSlotInColumn(colIndex);
        int hoverSlotIndex = gameView.board.columns.Length - 1;

        //if column is full
        if (nextAvilableSlot < 0)
        {
            gameView.board.SetSlotState(colIndex, hoverSlotIndex, SlotState.EMPTY);
        }
        else {
            gameView.board.SetSlotState(colIndex, hoverSlotIndex, currentPlayer);
        }

        
    }
}
