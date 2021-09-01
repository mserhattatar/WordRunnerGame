using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void GameManagerDelegate();

    public static GameManagerDelegate NextLevelDelegate;
    public static GameManagerDelegate ResetLevelDelegate;
    public static GameManagerDelegate GameOverDelegate;
    public static GameManagerDelegate StartGameDelegate;
    public static GameManagerDelegate LevelCompletedDelegate;
    public int levelNumber;
    public int language;
    public int levelWordNumber;
    public int playerLife;

    private void Awake()
    {
        Instance = this;
        levelNumber = 1;
        playerLife = 5;
        GameDataScript.GetLevelDataFromJson();
    }

    private void OnEnable()
    {
        NextLevelDelegate += SetLevelWordNumber;
        NextLevelDelegate += LevelUp;
        NextLevelDelegate += SetLevelData;
        NextLevelDelegate += ResetPlayerLifeCount;
        
        GameOverDelegate += ResetPlayerLifeCount;
        GameOverDelegate += SetLevelWordNumber;
    }

    private void Start()
    {
        SetLevelWordNumber();
    }


    private void SetLevelWordNumber()
    {
        levelWordNumber = levelNumber * 2;

        if (levelNumber > 5)
            levelWordNumber = 6;
        else
            levelWordNumber = levelNumber + 1;
    }

    public void SubtractLevelWordNumber(int subtractionNumber)
    {
        levelWordNumber -= subtractionNumber;
        CanvasManager.SetWordCountRemainingDelegate();
    }
    private void LevelUp()
    {
        levelNumber += 1;
    }

    private void ResetPlayerLifeCount()
    {
        playerLife = 5;
    }

    public bool SubtractPlayerLife()
    {
        playerLife -= 1;
        return playerLife == 0;
    }

    public static void GameTime(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void SetLevelData()
    {
        GameDataScript.SetLevelDataAsJson();
    }
}