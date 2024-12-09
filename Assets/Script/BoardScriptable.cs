using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoardData")]
public class BoardScriptable : ScriptableObject
{
    /// <summary>
    /// element0~4 : ù��°��
    /// element5~9 : �ι�°��
    /// element10~14 : ����°��
    /// element15~19 : �׹�°��
    /// </summary>


    public PlayboardCell.PlayCell[] CellData;

}
