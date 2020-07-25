using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : BaseView
{
    [SerializeField]
    private TMP_Text player1NameText, player2NameText, player1ScoreText, player2ScoreText;

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

    public void SetScores(int p1Score, int p2Score)
    {
        player1ScoreText.text = p1Score.ToString();
        player2ScoreText.text = p2Score.ToString();
    }

}
