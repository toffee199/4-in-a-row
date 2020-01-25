using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    //this is our singelton
    public static GameController gameInstance;

    //views
    public StartView startView;
    public GameView gameView;

    //models
    GameModel gameModel;
    
    private void Awake()
    {
        gameModel = new GameModel();
        InitializeWindows();

        if (gameInstance == null)
        {
            gameInstance = this;
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
}
