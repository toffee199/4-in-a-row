using System.Collections;
using System.Collections.Generic;


public class GameModel
{
    private string player1Name;
    private string player2Name;


    public string GetPlayer1Name()
    {    
        return player1Name;   
    }

    public string GetPlayer2Name()
    {
        return player2Name;
    }

    public void SetPlayer1Name(string playerName)
    {
        player1Name = playerName;
    }

    public void SetPlayer2Name(string playerName)
    {
        player2Name = playerName;
    }

    public void SetPlayerNames(string player1Name, string player2Name)
    {
        SetPlayer1Name(player1Name);
        SetPlayer2Name(player2Name);
    }

}
