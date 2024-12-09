using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public int speed = 10;
    public bool isJumpping = false;

    public DiceColor color;

    private int Count = 1;
    [SerializeField] GameObject[] diceValues;
    private Rigidbody rb;
    // Use this for initialization

    private Vector3 originPos;

    private int diceEye;

    private bool isRoll;

    public delegate void GetValueEvent(int value, string name);

    public event GetValueEvent ReturnDiceValue;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isRoll = false;
        ReturnDiceValue += GameManager.GetInstance.Dice.GetDiceValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity == Vector3.zero && Count > 0)
        {
            isJumpping = false;
            Count--;

            ReturnDiceValue?.Invoke(DiceHigh(), this.gameObject.name);
        }
    }

    public void moveCube(Vector3 position)
    {
        if (!isRoll)
            isRoll = true;

        if (originPos == Vector3.zero)
            originPos = position;

        if (!isJumpping)
        {
            Count++;
            this.isJumpping = true;
            transform.position = position;
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * 100);
            rb.AddTorque(UnityEngine.Random.Range(0, 300), UnityEngine.Random.Range(0, 300), UnityEngine.Random.Range(0, 300));
            this.GetComponent<Rigidbody>().velocity = Vector3.up * this.speed;
        }
    }

    private int DiceHigh()
    {
        for(int i = 0; i < diceValues.Length; i++)
        {
            if (diceValues[i].transform.position.y > 3)
            {
                diceEye = int.Parse(diceValues[i].name);
                return diceEye;
            }
        }

        return 0;
    }

    public bool GetRollState() => isRoll;

    public int GetDiceEye() => diceEye;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == this.transform.parent)
            return;

        if (other.GetComponent<PlayboardCell>().cell.diceColor != DiceColor.Empty)
        {
            if (other.GetComponent<PlayboardCell>().cell.diceColor != color)
            {
                GameManager.GetInstance.Menu.OpenAlert("보드의 색상과 주사위의 색상이 일치하지 않습니다.");

                this.transform.position = originPos;
                rb.freezeRotation = true;
                return;
            }
        }

        if (other.GetComponent<PlayboardCell>().cell.Num != 0)
        {
            if (other.GetComponent<PlayboardCell>().cell.Num != diceEye)
            {
                GameManager.GetInstance.Menu.OpenAlert("보드의 지정된 숫자와 주사위의 눈이 일치하지 않습니다.");

                this.transform.position = originPos;
                rb.freezeRotation = true;
                return;
            }
        }

        if(!other.GetComponent<PlayboardCell>().cell.canHere)
        {
            GameManager.GetInstance.Menu.OpenAlert("이곳에는 주사위를 \n둘 수 없습니다.");

            this.transform.position = originPos;
            rb.freezeRotation = true;
            return;
        }

        if (BanCheck(other.GetComponent<PlayboardCell>()))
        {
            this.transform.position = originPos;
            rb.freezeRotation = true;
            return;
        }

        transform.position = new Vector3(other.transform.position.x, 3f, other.transform.position.z);
        other.GetComponent<BoxCollider>().isTrigger = false;
        other.GetComponent<PlayboardCell>().FullDice();
        rb.freezeRotation = true;
        GetComponent<DragDice>().isSeat = true;
        if (GameManager.GetInstance.Dice.isFirst)
        {
            GameManager.GetInstance.PlayBoard.LogicFirst(other.GetComponent<PlayboardCell>(), diceEye, color);
            GameManager.GetInstance.Dice.isFirst = false;
        }
        else
            GameManager.GetInstance.PlayBoard.LogicBoard(other.GetComponent<PlayboardCell>(), diceEye, color);


        GameManager.GetInstance.Dice.SetDiceInPlayboard(this.gameObject);

    }

    private bool BanCheck(PlayboardCell checkCell)
    {
        foreach (int num in checkCell.cell.BanNum)
        {
            if (num == diceEye)
            {
                GameManager.GetInstance.Menu.OpenAlert("연속된 눈의 주사위는 \n사용 할 수 없습니다.");
                //Debug.Log("연속된 눈의 주사위는 사용할수없습니다");
                return true;
            }
        }

        foreach (DiceColor col in checkCell.cell.BanColor)
        {
            if (col == color)
            {
                GameManager.GetInstance.Menu.OpenAlert("연속된 색의 주사위는 \n사용 할 수 없습니다.");
                //Debug.Log("연속된 색의 주사위는 사용할수없습니다");
                return true;
            }
        }

        return false;
    }
}
