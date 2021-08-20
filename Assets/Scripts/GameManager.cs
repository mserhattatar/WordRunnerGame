using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public delegate void GameManagerDelegate();

    public static GameManagerDelegate NextLevelDelegate;
    public static GameManagerDelegate ResetLevelDelegate;
    public static GameManagerDelegate GameOverDelegate;
    public static GameManagerDelegate StartGameDelegate;
    public static GameManagerDelegate LevelCompletedDelegate;
    public int levelNumber;
    public int levelWordNumber;
    public int playerLife;

    private void Awake()
    {
        instance = this;
        levelNumber = 1;
        playerLife = 5;
        SetLevelWordNumber();
    }

    private void OnEnable()
    {
        GameDataScript.GetLevelDataFromJson();
        NextLevelDelegate += NextLevel;
        GameOverDelegate += ResetPlayerLifeCount;
        GameOverDelegate += SetLevelWordNumber;
    }

    
    private void NextLevel()
    {
        ResetPlayerLifeCount();
        levelNumber += 1;
        GameDataScript.SetLevelDataAsJson();
        SetLevelWordNumber();
    }

    private void SetLevelWordNumber()
    {
        levelWordNumber = levelNumber * 2;
    }

    public void SubtractLevelWordNumber(int subtractionNumber)
    {
        levelWordNumber -= subtractionNumber;
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
}