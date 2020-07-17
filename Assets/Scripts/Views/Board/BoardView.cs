using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardView : BaseView
{
    [SerializeField]
    private ColumnView[] columns;

    public Action<int> onColumnClicked;
    public Action<int> onColumnHovered;

    public void Start()
    {
        foreach (ColumnView column in columns)
        {
            column.onColumnClicked += HandleColumnClicked;
            column.onColumnHovered += HandleColumnHovered;
        }
    }

    public void Reset()
    {
        foreach(ColumnView column in columns)
        {
            column.Reset();
        }
    }

    private void OnDestroy()
    {
        foreach (ColumnView column in columns)
        {
            column.onColumnClicked -= HandleColumnClicked;
            column.onColumnHovered -= HandleColumnHovered;
        }
    }

    public void SetSlotState(int col, int row, SlotState state)
    {
        columns[col].SetSlotState(row, state);
    }

    public void SetHoverSlotState(int col, SlotState state)
    {
        int hoverIndex = columns.Length - 1;
        SetSlotState(col, hoverIndex, state);

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

    private void HandleColumnHovered(ColumnView column)
    {
        for (int i = 0; i < columns.Length; i++)
        {
            if (columns[i] == column)
            {
                onColumnHovered?.Invoke(i);
                break;
            }
        }
    }
}
