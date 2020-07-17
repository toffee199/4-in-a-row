using System.Collections;
using System.Collections.Generic;


public class GameModel
{
    private string player1Name;
    private string player2Name;
    private readonly SlotState[,] board = new SlotState[7,6];

    public GameModel ()
    {
        ResetBoard();
    }


    public string GetPlayer1Name()
    {    
        return player1Name;   
    }

    public string GetPlayer2Name()
    {
        return player2Name;
    }

    public string GetPlayerName(SlotState currentPlayer)
    {
        if (currentPlayer == SlotState.RED)
            return player1Name;
        if (currentPlayer == SlotState.WHITE)
            return player2Name;
        return "";
    }

    public void SetPlayer1Name(string playerName)
    {
        player1Name = playerName;
    }

    public void SetPlayer2Name(string playerName)
    {
        player2Name = playerName;
    }

    public void SetPlayerNames(string player1Name, string player2Name)
    {
        SetPlayer1Name(player1Name);
        SetPlayer2Name(player2Name);
    }

    public void SetUsedSlot(int col, int row, SlotState slotState)
    {
        board[col, row] = slotState;
    }

    public void ResetBoard()
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                board[i, j] = SlotState.EMPTY;
            }
        } 
    }

    public int GetNextAvilableSlot(int col)
    {
        for(int i = 0; i < board.GetLength(1); i++)
        {
            if(board[col, i] == SlotState.EMPTY)
            {
                return i;
            }        
        }
        return -1;
    }

    public bool GetGameWinState(int col, int row, SlotState slotColor) 
    {
        return SequenceFinder.IsAWin(board, col, row, slotColor);
    }

    public bool GetIsBoardFull()
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if(board[i, j] == SlotState.EMPTY)
                {
                    return false;
                }
            }
        }
        return true;
    }

}
