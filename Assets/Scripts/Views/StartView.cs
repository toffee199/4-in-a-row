using TMPro;
using System;
using UnityEngine;

public class StartView : BaseView { 

    [SerializeField]
    private TMP_InputField playerName1Input, playerName2Input;

    public Action<string, string> onStartGame;

    public void StartGameClicked()
    {
        string player1Name = playerName1Input.text;
        string player2Name = playerName2Input.text;

        onStartGame?.Invoke(player1Name, player2Name);
        
    }

}
