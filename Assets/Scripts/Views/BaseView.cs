using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour
{

    protected void ToggleVisibility(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }

    public void Show()
    {
        ToggleVisibility(true);
    }

    public void Hide()
    {
        ToggleVisibility(false);
    }
}
