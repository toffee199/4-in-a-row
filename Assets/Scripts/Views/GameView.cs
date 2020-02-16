using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : BaseView
{

    public TMP_Text player1NameText, player2NameText;
    public BoardView board;

    public override void Show()
    {
        base.Show();
        board.Reset();
    }

    public void StartNewGame(string player1Name, string player2Name)
    {
        player1NameText.text = player1Name;
        player2NameText.text = player2Name;
    }

}
