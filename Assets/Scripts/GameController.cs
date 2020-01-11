using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject startWindow, gameWindow;
    public TMP_Text playerNameText;
    public TMP_InputField playerNameInput;

    //this is our singelton
    static GameController gameInstance;

    
    private void Awake()
    {
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

    public void StartGameClicked()
    {
        string playerName = playerNameInput.text;

        if (!string.IsNullOrEmpty(playerName)) {
            startWindow.SetActive(false);
            gameWindow.SetActive(true);
            playerNameText.text = playerName;
        }
        else
        {
            //show error
        }
    }

    private void InitializeWindows()
    {
        startWindow.SetActive(true);
        //hide all other windows
        gameWindow.SetActive(false);

    }
}
