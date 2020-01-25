using TMPro;

public class GameView : BaseView
{

    public TMP_Text player1NameText, player2NameText;

    public void StartNewGame(string player1Name, string player2Name)
    {
        player1NameText.text = player1Name;
        player2NameText.text = player2Name;
    }

}
