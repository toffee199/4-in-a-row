using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnView : BaseView
{
    public SlotView[] slots;

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
