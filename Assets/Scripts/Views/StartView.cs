using TMPro;
using System;
using UnityEngine;

public class StartView : BaseView { 

    [SerializeField]
    private TMP_InputField playerName1Input, playerName2Input;

    [SerializeField]
    private TMP_Text errorText;

    public Action<string, string> onStartGame;
    public Action<bool> onAiToggled;

    public void StartGameClicked()
    {
        onStartGame?.Invoke(playerName1Input.text, playerName2Input.text);
    }

    public void OnToggle(bool isAi)
    {
        playerName2Input.transform.parent.gameObject.SetActive(!isAi);
        onAiToggled?.Invoke(isAi);
    }

    public void SetErrorMessage(string msg)
    {
        errorText.text = msg;
    }

    public override void Reset()
    {
        base.Reset();
        playerName1Input.text = "";
        playerName2Input.text = "";
    }
}
