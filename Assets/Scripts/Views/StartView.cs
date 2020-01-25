using TMPro;
using UnityEngine;

public class StartView : BaseView { 

    public TMP_InputField playerName1Input, playerName2Input;

    public void StartGameClicked()
    {
        string player1Name = playerName1Input.text;
        string player2Name = playerName2Input.text;

        GameController.gameInstance.StartGame(player1Name, player2Name);
        
    }

}
