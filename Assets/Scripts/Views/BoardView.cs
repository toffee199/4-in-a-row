using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : BaseView
{
    public ColumnView[] columns;

    public void Reset()
    {
        foreach(ColumnView column in columns)
        {
            column.Reset();
        }
    }

    public void SetSlotState(int col, int row, SlotState state)
    {
        columns[col].SetSlotState(row, state);
    }
}
