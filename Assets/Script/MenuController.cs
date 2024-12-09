using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class MenuController : MonoBehaviour
{
    BoardScriptable[] boards;

    [SerializeField] GameObject _pannelIntro;
    [SerializeField] GameObject _pannelSelectLv;
    [SerializeField] GameObject _pannelBtns;
    [SerializeField] GameObject _pannelScore;
    [SerializeField] GameObject _pannelAlert;
    [SerializeField] GameObject _pannelWarnning;

    [SerializeField] GameObject _btnMakeDice;
    [SerializeField] GameObject _btnReMake;
    [SerializeField] TextMeshProUGUI _tmpRound;

    [SerializeField] TextMeshProUGUI _tmpScore;

    [SerializeField] TextMeshProUGUI _tmpAlert;

    float roundTime = 0;

    int nowLevel;

    CancellationTokenSource _cancelToken;

    private void Awake()
    {
        boards = Resources.LoadAll<BoardScriptable>("ScriptableObj");
        
    }

    public void OnBtn_PlayGame()
    {
        //_pannelIntro.SetActive(false);
        _pannelSelectLv.SetActive(true);
        OpenAlert("게임 시작시 주사위는 보드의 \n모서리에만 놓을 수 있습니다.");
    }

    public void OnBtn_Quit() => Application.Quit();


    public void OnBtn_Level(int level)
    {
        nowLevel = level;
        GameManager.GetInstance.PlayBoard.SettingPlayCells(boards[nowLevel]);
        _pannelIntro.SetActive(false);
        _pannelSelectLv.SetActive(false);
        _pannelScore.SetActive(false);
        _pannelBtns.SetActive(true);
        _btnMakeDice.SetActive(true);
        _btnReMake.SetActive(true);

        RoundAppear().Forget();
    }

    public void OnBtn_Roll()
    {
        if (_cancelToken == null)
            GameManager.GetInstance.Dice.OnBtn_Roll();
    }

    public void OnBtn_Exit()
    {
        _pannelIntro.SetActive(true);
        _pannelBtns.SetActive(false);
        _pannelScore.SetActive(false);

        GameManager.GetInstance.ResetRound();
        GameManager.GetInstance.Dice.ClearDice();
        GameManager.GetInstance.PlayBoard.SettingPlayCells(boards[0]);
        GameManager.GetInstance.PlayBoard.ResetCell();
    }

    public void OnBtn_MakeDice()
    {
        GameManager.GetInstance.Dice.InstaniateDice();
        _btnMakeDice.SetActive(false);
    }

    public void DiceOn() => _btnMakeDice.SetActive(true);

    public void OnBtn_Skip() => GameManager.GetInstance.Dice.SkipTurn();

    public async UniTask RoundAppear()
    {
        if (_cancelToken == null)
            _cancelToken = new CancellationTokenSource();

        _tmpRound.text = "Round " + GameManager.GetInstance.GetNowRound().ToString();

        _tmpRound.gameObject.SetActive(true);
        while(roundTime < 2.5f)
        {
            roundTime += Time.deltaTime;
            await UniTask.NextFrame(_cancelToken.Token);
        }

        roundTime = 0;
        _tmpRound.gameObject.SetActive(false);

        _cancelToken.Cancel();
        _cancelToken.Dispose();
        _cancelToken = null;
    }

    public void OpenScoreBoard() => _pannelScore.SetActive(true);

    public void SetScore(int score) => _tmpScore.text = score.ToString();

    public void OnBtn_Replay()
    {
        OnBtn_Exit();
        OnBtn_Level(nowLevel);
    }

    public void OpenAlert(string msg)
    {
        _tmpAlert.text = msg;
        _pannelAlert.SetActive(true);
    }

    public void OnBtn_Alert() => _pannelAlert.SetActive(false);



    public void OnBtn_RemakeDice() => _pannelWarnning.SetActive(true);

    public void OnBtn_Yes()
    {
        _btnReMake.SetActive(false);
        GameManager.GetInstance.Dice.RemakeDice();
        _pannelWarnning.SetActive(false);
    }

    public void OnBtn_No() => _pannelWarnning.SetActive(false);
}
