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
        SetLevelWordNumber();
        NextLevelDelegate += NextLevel;
        GameOverDelegate += ResetPlayerLifeCount;
        GameOverDelegate += SetLevelWordNumber;
    }


    private void NextLevel()
    {
        ResetPlayerLifeCount();
        levelNumber += 1;
        SetLevelData();
        SetLevelWordNumber();
    }

    private void SetLevelWordNumber()
    {
        levelWordNumber = levelNumber * 2;

        if (levelNumber > 5)
            levelWordNumber = 8;
        else if (levelNumber > 3)
            levelWordNumber = levelNumber + 2;
        else
            levelWordNumber = 2;
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

    public static void GameTime(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void SetLevelData()
    {
        GameDataScript.SetLevelDataAsJson();
    }
}