using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOverView : BaseView
{
    [SerializeField]
    private TMP_Text gameStateText;
    [SerializeField]
    private SlotView slotView;
    [SerializeField]
    private HighScoresView highScoresView;


    
    public Action onNewGame, onExit;

    public void ShowGameOverView(SlotState winingPlayer, List<HighScoreItemData> highScores, string playerName = null)
    {
        base.Show();
        if(winingPlayer == SlotState.EMPTY)
        {
            gameStateText.text = "It's a tie!!";
            slotView.gameObject.SetActive(false);
        } else
        {
            gameStateText.text = playerName + " won!!";
            slotView.gameObject.SetActive(true);
            slotView.SetState(winingPlayer);
        }
        highScoresView.Init(highScores);
    }

    public void OnNewGameButtonClicked()
    {
        onNewGame?.Invoke();
    }

    public void OnExitButtonClicked()
    {
        onExit?.Invoke();
    }
}
