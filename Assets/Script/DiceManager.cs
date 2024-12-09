using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiceColor
{
    Red,
    Blue,
    Yellow,
    Green,
    Prurle,
    Empty
}

public class DiceManager : MonoBehaviour
{
    //[SerializeField] GameObject[] Dices;
    [SerializeField] GameObject OriginDice;

    [SerializeField] Material[] diceMat;

    List<GameObject> _list_Dices = new List<GameObject>();
    List<GameObject> _list_InBoardDice = new List<GameObject>();


    Vector3[] Positions = {new Vector3(-20,5,0), new Vector3(-15, 5, 0), new Vector3(-10, 5, 0), new Vector3(-5, 5, 0), new Vector3(-3, 5, 0), };

    int setCount = 2;

    int rollCount = 1;

    public bool isFirst = true;

    private void Update()
    {
        Roll();

    }

    private void Roll()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_list_Dices.Count == 0)
            {
                InstaniateDice();
                return;
            }

            for (int i = 0; i < _list_Dices.Count; i++)
                _list_Dices[i].GetComponent<DiceRoll>().moveCube(Positions[i]);
        }
    }

    public void OnBtn_Roll()
    {
        if (_list_Dices.Count == 0)
        {
            GameManager.GetInstance.Menu.OpenAlert("주사위가 없습니다.");
            //Debug.Log("주사위가 없습니다");
            return;
        }

        if(rollCount < 1)
        {
            GameManager.GetInstance.Menu.OpenAlert("주사위를 굴리는 \n기회가 없습니다.");
            return;
        }

        rollCount--;

        for (int i = 0; i < _list_Dices.Count; i++)
            _list_Dices[i].GetComponent<DiceRoll>().moveCube(Positions[i]);
    }

    public void GetDiceValue(int Value , string Name)
    {
        Debug.Log(Name + " Dice Value is " + Value);
    }

    public void InstaniateDice()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject go = Instantiate(OriginDice, transform);

            int rand = UnityEngine.Random.Range(0, (int)DiceColor.Empty);

            go.GetComponent<DiceRoll>().color = (DiceColor)rand;
            go.GetComponent<MeshRenderer>().material = diceMat[rand];
            go.transform.position = Positions[i];

            _list_Dices.Add(go);
        }
    }

    public void SetDiceInPlayboard(GameObject gameObject)
    {
        int index = _list_Dices.FindIndex(x => x == gameObject);
        _list_InBoardDice.Add(_list_Dices[index]);
        _list_Dices.RemoveAt(index);
        setCount--;

        if(setCount == 0)
        {
            for (int i = 0; i < _list_Dices.Count; i++)
                Destroy(_list_Dices[i]);

            _list_Dices.Clear();
            setCount = 2;
            rollCount = 1;
            GameManager.GetInstance.NextRound();

            if (GameManager.GetInstance.GetNowRound() > 10)
                GameEnd();
            else
            {
                GameManager.GetInstance.Menu.DiceOn();
                GameManager.GetInstance.Menu.RoundAppear().Forget();
            }
        }
    }

    public void ClearDice()
    {
        for (int i = 0; i < _list_Dices.Count; i++)
            Destroy(_list_Dices[i]);

        for (int i = 0; i < _list_InBoardDice.Count; i++)
            Destroy(_list_InBoardDice[i]);

        _list_Dices.Clear();
        _list_InBoardDice.Clear();
        setCount = 2;
        rollCount = 1;
    }

    public void RemakeDice()
    {
        for (int i = 0; i < _list_Dices.Count; i++)
            Destroy(_list_Dices[i]);

        _list_Dices.Clear();

        rollCount = 1;

        InstaniateDice();
    }

    public void SkipTurn()
    {
        for (int i = 0; i < _list_Dices.Count; i++)
            Destroy(_list_Dices[i]);

        _list_Dices.Clear();
        setCount = 2;
        rollCount = 1;

        GameManager.GetInstance.NextRound();

        if (GameManager.GetInstance.GetNowRound() > 10)
            GameEnd();
        else
        {
            GameManager.GetInstance.Menu.DiceOn();
            GameManager.GetInstance.Menu.RoundAppear().Forget();
        }
    }


    private void GameEnd()
    {
        //게임종료안내
        int score = 0;

        for(int i = 0; i < _list_InBoardDice.Count; i++)
            score += _list_InBoardDice[i].GetComponent<DiceRoll>().GetDiceEye();

        score += GameManager.GetInstance.PlayBoard.VerticalCells();
        score += GameManager.GetInstance.PlayBoard.BlankCell();


        GameManager.GetInstance.Menu.SetScore(score);
        GameManager.GetInstance.Menu.OpenScoreBoard();

    }
}
