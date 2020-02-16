using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardView : BaseView
{
    public ColumnView[] columns;
    public Action<int> onColumnClicked;

    public void Start()
    {
        foreach (ColumnView column in columns)
        {
            column.onColumnClicked += HandleColumnClicked;
        }
    }

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

    private void HandleColumnClicked(ColumnView column)
    {
        for(int i =0; i < columns.Length; i++)
        {
            if(columns[i] == column) {
                 onColumnClicked?.Invoke(i);
                break;
            }
        }
    }
}
