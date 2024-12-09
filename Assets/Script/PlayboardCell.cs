using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayboardCell : MonoBehaviour
{
    /// <summary>
    /// 셀 위치(XZ) : 오른쪽 위부터 기준
    /// (-3.6, 3)       (-1.8, 3)       (0, 3)      (1.8, 3)    (3.6, 3)
    /// (-3.6, 1)       (-1.8, 1)       (0, 1)      (1.8, 1)    (3.6, 1)
    /// (-3.6, -1)      (-1.8, -1)      (0, -1)     (1.8, -1)   (3.6, -1)
    /// (-3.6, -3)      (-1.8, -3)      (0, -3)     (1.8, -3)   (3.6, -3)
    /// </summary>

    [SerializeField] Material[] MaterialColors;
    [SerializeField] Material[] MaterialNumber;
    [SerializeField] Material defaultMat;

    [Serializable]
    public struct PlayCell
    {
        public DiceColor diceColor;
        public int Num;
        public bool isFull;
        public bool canHere;
        public List<int> BanNum;
        public List<DiceColor> BanColor;
    }

    public PlayCell cell;


    public void FullDice() => cell.isFull = true;

    public void ApplyBoard()
    {
        if (cell.diceColor != DiceColor.Empty)
            GetComponent<MeshRenderer>().material = MaterialColors[(int)cell.diceColor];
        else if (cell.Num != 0)
            GetComponent<MeshRenderer>().material = MaterialNumber[cell.Num - 1];
        else
            GetComponent<MeshRenderer>().material = defaultMat;
    }

    
}
