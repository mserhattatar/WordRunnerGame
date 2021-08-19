using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public delegate void GameManagerDelegate();

    public static GameManagerDelegate NextLevelDelegate;
    public static GameManagerDelegate ResetLevelDelegate;
    public int levelNumber;
    public int levelWordNumber;

    private void Awake()
    {
        instance = this;
        levelNumber = 1;
        SetLevelWordNumber();
    }

    private void Start()
    {
        GameDataScript.GetLevelDataFromJson();
        NextLevelDelegate += NextLevel;
    }

    private void NextLevel()
    {
        GameDataScript.SetLevelDataAsJson();
        levelNumber += 1;
        SetLevelWordNumber();
    }

    private void SetLevelWordNumber()
    {
        levelWordNumber = levelNumber * 2;
    }

    public void SubtractLevelWordNumber(int substractionNumber)
    {
        levelWordNumber -= substractionNumber;
    }
}