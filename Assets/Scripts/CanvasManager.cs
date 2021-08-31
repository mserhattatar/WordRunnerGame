using System.Collections;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public delegate void CanvasManagerDelegate();

    public static CanvasManagerDelegate SetWordCountRemainingDelegate;
    public static CanvasManagerDelegate SetPlayerLifeDelegate;


    [SerializeField] private GameObject wordCompletedPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject wordsPanel;
    [SerializeField] private GameObject wordCountDownUI;
    [SerializeField] private TextMeshProUGUI wordCountRemaining;
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private GameObject levelNumber;
    [SerializeField] private GameObject playerLifeUI;
    [SerializeField] private TextMeshProUGUI playerLifeText;


    private void OnEnable()
    {
        SetWordCountRemainingDelegate += SetWordCountRemaining;
        SetPlayerLifeDelegate += SetPlayerLifeText;

        WordManager.NextWordWithDelayDelegate += SetWordCountRemaining;

        GameManager.LevelCompletedDelegate += WordCompletedPanelSetActive;
        GameManager.LevelCompletedDelegate += WordCountDownUISetPassive;
        GameManager.LevelCompletedDelegate += PlayerLifeUISetPassive;

        GameManager.StartGameDelegate += WordsPanelSetActive;
        GameManager.StartGameDelegate += WordCountDownUISetActive;
        GameManager.StartGameDelegate += SetWordCountRemaining;
        GameManager.StartGameDelegate += LevelNumberSetActive;
        GameManager.StartGameDelegate += PlayerLifeUISetActive;
        GameManager.StartGameDelegate += SetPlayerLifeText;

        GameManager.GameOverDelegate += GameOverPanelSetActive;
        GameManager.GameOverDelegate += WordsPanelSetPassive;
        GameManager.GameOverDelegate += PlayerLifeUISetPassive;
        GameManager.GameOverDelegate += WordCountDownUISetPassive;

        GameManager.NextLevelDelegate += LoadingPanelPanelSetActive;
        GameManager.NextLevelDelegate += WordCompletedPanelSetPassive;
        GameManager.NextLevelDelegate += WordCountDownUISetActive;
        GameManager.NextLevelDelegate += PlayerLifeUISetPassive;

        GameManager.ResetLevelDelegate += LoadingPanelPanelSetActive;
        GameManager.ResetLevelDelegate += GameOverPanelSetPassive;
        GameManager.ResetLevelDelegate += WordCountDownUISetActive;
        GameManager.ResetLevelDelegate += WordsPanelSetActive;
        GameManager.ResetLevelDelegate += SetWordCountRemaining;
        GameManager.ResetLevelDelegate += PlayerLifeUISetActive;
    }

    private void PlayerLifeUISetActive()
    {
        playerLifeUI.SetActive(true);
    }

    private void SetPlayerLifeText()
    {
        playerLifeText.text = GameManager.instance.playerLife.ToString();
    }

    private void PlayerLifeUISetPassive()
    {
        playerLifeUI.SetActive(false);
    }

    private void GameOverPanelSetActive()
    {
        gameOverPanel.SetActive(true);
    }

    private void GameOverPanelSetPassive()
    {
        gameOverPanel.SetActive(false);
    }

    private void LoadingPanelPanelSetActive()
    {
        loadingPanel.SetActive(true);
        StartCoroutine(LoadingPanelSetPassiveDelay());
    }

    private IEnumerator LoadingPanelSetPassiveDelay()
    {
        yield return new WaitForSeconds(1.3f);
        loadingPanel.SetActive(false);
    }

    private void MainMenuPanelSetVisibility(bool setActive)
    {
        mainMenuPanel.SetActive(setActive);
    }

    private void WordsPanelSetActive()
    {
        wordsPanel.SetActive(true);
    }

    private void WordsPanelSetPassive()
    {
        wordsPanel.SetActive(false);
    }

    private void WordCountDownUISetActive()
    {
        wordCountDownUI.SetActive(true);
    }

    private void WordCountDownUISetPassive()
    {
        wordCountDownUI.SetActive(false);
    }

    private void WordCompletedPanelSetActive()
    {
        wordCompletedPanel.SetActive(true);
    }

    private void WordCompletedPanelSetPassive()
    {
        wordCompletedPanel.SetActive(false);
    }


    private void SetWordCountRemaining()
    {
        wordCountRemaining.text = "WORD COUNTDOWN  " + GameManager.instance.levelWordNumber;
    }

    private void LevelNumberSetActive()
    {
        StartCoroutine(LevelNumberSetActiveWithDelay());
    }

    private IEnumerator LevelNumberSetActiveWithDelay()
    {
        levelNumber.SetActive(true);
        levelNumberText.text = GameManager.instance.levelNumber.ToString();
        yield return new WaitForSeconds(3f);
        levelNumber.SetActive(false);
    }

    public void NextLevelButton()
    {
        GameManager.NextLevelDelegate();
    }

    public void TryAgainButton()
    {
        GameManager.ResetLevelDelegate();
    }

    public void PlayButton()
    {
        MainMenuPanelSetVisibility(false);
    }

    public void SettingsButton()
    {
        MainMenuPanelSetVisibility(true);
    }
}