using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoardData")]
public class BoardScriptable : ScriptableObject
{
    /// <summary>
    /// element0~4 : 첫번째줄
    /// element5~9 : 두번째줄
    /// element10~14 : 세번째줄
    /// element15~19 : 네번째줄
    /// </summary>


    public PlayboardCell.PlayCell[] CellData;

}
