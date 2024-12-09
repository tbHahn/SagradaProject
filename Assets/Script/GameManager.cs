using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject goBoard;

    public PlayboardManager PlayBoard { get; set; }
    public DiceManager Dice { get; set; }

    public MenuController Menu { get; set; }


    static GameManager Instance;

    int GameRound = 1;

    public static GameManager GetInstance
    {
        get
        {
            if (Instance == null)
            {
                GameObject go = GameObject.Find("GameManager");
                // 한번도 인스턴스화 한적 없었을 경우
                if (go == null)
                {
                    go = new GameObject() { name = "GameManager" };
                    Instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
                // GameObject가 존재하는 경우
                Instance = go.GetComponent<GameManager>();
            }
            return Instance;
        }
    }


    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (Instance == null)
        {
            GameObject go = GameObject.Find("GameManager");
            // 한번도 인스턴스화 한적 없었을 경우
            if (go == null)
            {
                go = new GameObject() { name = "GameManager" };
                Instance = go.AddComponent<GameManager>();
            }
                DontDestroyOnLoad(go);
        }
    }

    private void Start()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        PlayBoard = FindObjectOfType<PlayboardManager>();
        Dice = FindObjectOfType<DiceManager>();
        Menu = FindObjectOfType<MenuController>();
    }

    public void NextRound() => GameRound++;

    public int GetNowRound() => GameRound;

    public void ResetRound() => GameRound = 1;
}
