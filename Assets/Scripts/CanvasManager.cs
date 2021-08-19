using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public delegate void CanvasManagerDelegate();

    public static CanvasManagerDelegate LevelWordCompletedSetActiveDelegate;
    public static CanvasManagerDelegate SetWordCountRemainingDelegate;
    

    public GameObject wordCompletedPanel;
    public TextMeshProUGUI wordCountRemaining;

    private void Start()
    {
        LevelWordCompletedSetActiveDelegate += SetActiveWordCompletedPanel;
        WordManager.NextWordDelegate += SetWordCountRemaining;
        SetWordCountRemainingDelegate += SetWordCountRemaining;
        SetWordCountRemaining();
    }

    private void SetActiveWordCompletedPanel()
    {
        wordCompletedPanel.SetActive(true);
    }

    private void SetPassiveWordCompletedPanel()
    {
        wordCompletedPanel.SetActive(false);
    }

    private void SetWordCountRemaining()
    {
        wordCountRemaining.text = "WORD COUNTDOWN  " + GameManager.instance.levelWordNumber;
    }

    public void NextLevelButton()
    {
        SetPassiveWordCompletedPanel();
        GameManager.NextLevelDelegate();
    }
}