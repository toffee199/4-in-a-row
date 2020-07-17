using UnityEngine;

public class SlotView : BaseView
{

    public GameObject redSlot, whiteSlot;

    public void SetState(SlotState state)
    {

        redSlot.SetActive(false);
        whiteSlot.SetActive(false);

        switch (state)
        {
            case SlotState.WHITE:
                whiteSlot.SetActive(true);
                break;
            case SlotState.RED:
                redSlot.SetActive(true);
                break;
            case SlotState.EMPTY:
            default:
                break;
        }
    }
}
