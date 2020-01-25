using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColumnView : BaseView, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public SlotView[] slots;

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //SlotState playerColor = GameController.gameInstance.GetCurrentSlotState();
        slots[slots.Length - 1].SetState(SlotState.RED);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slots[slots.Length - 1].SetState(SlotState.EMPTY);
    }

    public void Reset()
    {
        foreach (SlotView slot in slots)
        {
            slot.SetState(SlotState.EMPTY);
        }
    }

    public void SetSlotState(int row, SlotState state)
    {
        slots[row].SetState(state);
    }

}
