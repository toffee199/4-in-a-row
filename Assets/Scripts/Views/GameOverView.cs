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

    public Action onNewGame, onExit;

    public void ShowGameOverView(SlotState winingPlayer, string playerName = null)
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
