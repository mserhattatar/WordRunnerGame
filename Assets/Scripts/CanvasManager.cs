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
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject wordsPanel;
    [SerializeField] private GameObject pauseButton;
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
        GameManager.LevelCompletedDelegate += PauseButtonSetPassive;

        GameManager.StartGameDelegate += WordsPanelSetActive;
        GameManager.StartGameDelegate += WordCountDownUISetActive;
        GameManager.StartGameDelegate += LevelNumberSetActive;
        GameManager.StartGameDelegate += PlayerLifeUISetActive;
        GameManager.StartGameDelegate += SetPlayerLifeText;
        GameManager.StartGameDelegate += SetWordCountRemaining;

        GameManager.GameOverDelegate += GameOverPanelSetActive;
        GameManager.GameOverDelegate += WordsPanelSetPassive;
        GameManager.GameOverDelegate += PlayerLifeUISetPassive;
        GameManager.GameOverDelegate += WordCountDownUISetPassive;
        GameManager.GameOverDelegate += PauseButtonSetPassive;

        GameManager.NextLevelDelegate += LoadingPanelPanelSetActive;
        GameManager.NextLevelDelegate += WordCompletedPanelSetPassive;
        GameManager.NextLevelDelegate += WordCountDownUISetActive;
        GameManager.NextLevelDelegate += PlayerLifeUISetPassive;
        GameManager.NextLevelDelegate += PauseButtonSetActive;
        GameManager.NextLevelDelegate += SetWordCountRemaining;

        GameManager.ResetLevelDelegate += LoadingPanelPanelSetActive;
        GameManager.ResetLevelDelegate += GameOverPanelSetPassive;
        GameManager.ResetLevelDelegate += WordCountDownUISetActive;
        GameManager.ResetLevelDelegate += WordsPanelSetActive;
        GameManager.ResetLevelDelegate += PlayerLifeUISetActive;
        GameManager.ResetLevelDelegate += PauseButtonSetActive;
        GameManager.ResetLevelDelegate += SetWordCountRemaining;
    }

    private void Start()
    {
        MainMenuPanelSetVisibility(true);
    }

    private void PlayerLifeUISetActive()
    {
        playerLifeUI.SetActive(true);
    }

    private void SetPlayerLifeText()
    {
        playerLifeText.text = GameManager.Instance.playerLife.ToString();
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
        GameManager.GameTime(setActive ? 0f : 1.0f);
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

    private void PauseButtonSetActive()
    {
        pauseButton.SetActive(true);
    }

    private void PauseButtonSetPassive()
    {
        pauseButton.SetActive(false);
    }

    private void SetWordCountRemaining()
    {
        wordCountRemaining.text = "WORD COUNTDOWN  " + GameManager.Instance.levelWordNumber;
    }

    private void LevelNumberSetActive()
    {
        StartCoroutine(LevelNumberSetActiveWithDelay());
    }

    private IEnumerator LevelNumberSetActiveWithDelay()
    {
        levelNumber.SetActive(true);
        levelNumberText.text = GameManager.Instance.levelNumber.ToString();
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
        GameManager.ResetLevelDelegate();
        MainMenuPanelSetVisibility(false);
    }

    public void SettingsButton()
    {
        MainMenuPanelSetVisibility(true);
        pausePanel.SetActive(false);
    }

    public void PauseButton()
    {
        pausePanel.SetActive(true);
        GameManager.GameTime(0f);
    }

    public void PausePanelBackButton()
    {
        pausePanel.SetActive(false);
        GameManager.GameTime(1.0f);
    }
}