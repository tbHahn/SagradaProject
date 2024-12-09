using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayboardManager : MonoBehaviour
{
    public List<PlayboardCell> playboardCells;

    public void SettingPlayCells(BoardScriptable board)
    {
        for (int i = 0; i < playboardCells.Count; i++)
        {
            playboardCells[i].cell = board.CellData[i];
            playboardCells[i].cell.BanNum.Clear();
            playboardCells[i].cell.BanColor.Clear();
            playboardCells[i].ApplyBoard();

            if (i == 0 || i == 4 || i == 15 || i == 19)
                playboardCells[i].cell.canHere = true;
        }    
    }

    public void ResetCell()
    {
        for(int i = 0; i < playboardCells.Count; i++)
        {
            playboardCells[i].transform.GetComponent<BoxCollider>().isTrigger = true;
        }
    }


    public void LogicBoard(PlayboardCell cell, int banNum, DiceColor banColor)
    {
        int idx = playboardCells.FindIndex(x => x == cell);

        #region ¡÷ºÆ
        /*switch (stDir)
        {
            case startDir.LT:
                if (idx + 1 < playboardCells.Count && !playboardCells[idx + 1].cell.isFull)
                {
                    playboardCells[idx + 1].cell.canHere = true;
                    playboardCells[idx + 1].cell.BanNum.Add(banNum);
                }

                if (idx + 5 < playboardCells.Count && !playboardCells[idx + 5].cell.isFull)
                {
                    playboardCells[idx + 5].cell.canHere = true;
                    playboardCells[idx + 5].cell.BanNum.Add(banNum);
                }

                break;
            case startDir.RT:
                if (idx - 1 >= 0 && !playboardCells[idx - 1].cell.isFull)
                {
                    playboardCells[idx - 1].cell.canHere = true;
                    playboardCells[idx - 1].cell.BanNum.Add(banNum);
                }

                if (idx + 5 < playboardCells.Count && !playboardCells[idx + 5].cell.isFull)
                {
                    playboardCells[idx + 5].cell.canHere = true;
                    playboardCells[idx + 5].cell.BanNum.Add(banNum);
                }
                break;
            case startDir.LB:
                if (idx + 1 < playboardCells.Count && !playboardCells[idx + 1].cell.isFull)
                {
                    playboardCells[idx + 1].cell.canHere = true;
                    playboardCells[idx + 1].cell.BanNum.Add(banNum);
                }

                if (idx - 5 >= 0 && !playboardCells[idx - 5].cell.isFull)
                {
                    playboardCells[idx - 5].cell.canHere = true;
                    playboardCells[idx - 5].cell.BanNum.Add(banNum);
                }
                break;
            case startDir.RB:
                if (idx - 1 >= 0 && !playboardCells[idx - 1].cell.isFull)
                {
                    playboardCells[idx - 1].cell.canHere = true;
                    playboardCells[idx - 1].cell.BanNum.Add(banNum);
                }

                if (idx - 5 >= 0 && !playboardCells[idx - 5].cell.isFull)
                {
                    playboardCells[idx - 5].cell.canHere = true;
                    playboardCells[idx - 5].cell.BanNum.Add(banNum);
                }
                break;
        }*/
        #endregion

        if (idx + 1 < playboardCells.Count && !playboardCells[idx + 1].cell.isFull)
        {
            if (idx == 4 || idx == 9 || idx == 14 || idx == 19)
            {

            }
            else
            {
                playboardCells[idx + 1].cell.canHere = true;
                playboardCells[idx + 1].cell.BanNum.Add(banNum);
                playboardCells[idx + 1].cell.BanColor.Add(banColor);
            }
        }

        if (idx + 5 < playboardCells.Count && !playboardCells[idx + 5].cell.isFull)
        {
            playboardCells[idx + 5].cell.canHere = true;
            playboardCells[idx + 5].cell.BanNum.Add(banNum);
            playboardCells[idx + 5].cell.BanColor.Add(banColor);
        }

        if (idx - 1 >= 0 && !playboardCells[idx - 1].cell.isFull)
        {
            if (idx == 0 || idx == 5 || idx == 10 || idx == 15)
            {
                //playboardCells[idx - 1].cell.BanNum.Add(0);
            }
            else
            {
                playboardCells[idx - 1].cell.canHere = true;
                playboardCells[idx - 1].cell.BanNum.Add(banNum);
                playboardCells[idx - 1].cell.BanColor.Add(banColor);
            }
        }

        if (idx - 5 >= 0 && !playboardCells[idx - 5].cell.isFull)
        {
            playboardCells[idx - 5].cell.canHere = true;
            playboardCells[idx - 5].cell.BanNum.Add(banNum);
            playboardCells[idx - 5].cell.BanColor.Add(banColor);
        }

        if (idx - 6 >= 0 && !playboardCells[idx - 6].cell.isFull)
            playboardCells[idx - 6].cell.canHere = true;

        if (idx - 4 >= 0 && !playboardCells[idx - 4].cell.isFull)
            playboardCells[idx - 4].cell.canHere = true;

        if (idx + 4 < playboardCells.Count && !playboardCells[idx + 4].cell.isFull)
            playboardCells[idx + 4].cell.canHere = true;

        if (idx + 6 < playboardCells.Count && !playboardCells[idx + 6].cell.isFull)
            playboardCells[idx + 6].cell.canHere = true;


    }

    public void LogicFirst(PlayboardCell cell , int banNum, DiceColor banColor)
    {
        for (int i = 0; i < playboardCells.Count; i++)
        {
            if (playboardCells[i].cell.canHere)
                playboardCells[i].cell.canHere = false;
        }

        int idx = playboardCells.FindIndex(x => x == cell);

        if (idx == 0)
        {
            playboardCells[idx + 1].cell.canHere = true;
            playboardCells[idx + 1].cell.BanNum.Add(banNum);
            playboardCells[idx + 1].cell.BanColor.Add(banColor);

            playboardCells[idx + 5].cell.canHere = true;
            playboardCells[idx + 5].cell.BanNum.Add(banNum);
            playboardCells[idx + 5].cell.BanColor.Add(banColor);

            playboardCells[idx + 6].cell.canHere = true;

            //stDir = startDir.LT;
        }
        else if (idx == 4)
        {
            playboardCells[idx - 1].cell.canHere = true;
            playboardCells[idx - 1].cell.BanNum.Add(banNum);
            playboardCells[idx - 1].cell.BanColor.Add(banColor);

            playboardCells[idx + 5].cell.canHere = true;
            playboardCells[idx + 5].cell.BanNum.Add(banNum);
            playboardCells[idx + 5].cell.BanColor.Add(banColor);

            playboardCells[idx + 4].cell.canHere = true;

            //stDir = startDir.RT;
        }
        else if (idx == 15)
        {
            playboardCells[idx + 1].cell.canHere = true;
            playboardCells[idx + 1].cell.BanNum.Add(banNum);
            playboardCells[idx + 1].cell.BanColor.Add(banColor);

            playboardCells[idx - 5].cell.canHere = true;
            playboardCells[idx - 5].cell.BanNum.Add(banNum);
            playboardCells[idx - 5].cell.BanColor.Add(banColor);

            playboardCells[idx - 4].cell.canHere = true;
            //stDir = startDir.LB;
        }
        else if (idx == 19)
        {
            playboardCells[idx - 1].cell.canHere = true;
            playboardCells[idx - 1].cell.BanNum.Add(banNum);
            playboardCells[idx - 1].cell.BanColor.Add(banColor);

            playboardCells[idx - 5].cell.canHere = true;
            playboardCells[idx - 5].cell.BanNum.Add(banNum);
            playboardCells[idx - 5].cell.BanColor.Add(banColor);

            playboardCells[idx - 6].cell.canHere = true;
            //stDir = startDir.RB;
        }
    }

    public int VerticalCells()
    {
        int sum = 0;

        if (playboardCells[0].cell.isFull && playboardCells[5].cell.isFull && playboardCells[10].cell.isFull && playboardCells[15].cell.isFull)
            sum += 5;

        if (playboardCells[1].cell.isFull && playboardCells[6].cell.isFull && playboardCells[11].cell.isFull && playboardCells[16].cell.isFull)
            sum += 5;

        if (playboardCells[2].cell.isFull && playboardCells[7].cell.isFull && playboardCells[12].cell.isFull && playboardCells[17].cell.isFull)
            sum += 5;

        if (playboardCells[3].cell.isFull && playboardCells[8].cell.isFull && playboardCells[13].cell.isFull && playboardCells[18].cell.isFull)
            sum += 5;

        if (playboardCells[4].cell.isFull && playboardCells[9].cell.isFull && playboardCells[14].cell.isFull && playboardCells[19].cell.isFull)
            sum += 5;

        return sum;
    }

    public int BlankCell()
    {
        int sum = 0;

        foreach(PlayboardCell cell in playboardCells)
        {
            if (!cell.cell.isFull)
                sum += 3;
        }

        return -sum;
    }
}
