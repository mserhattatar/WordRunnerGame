using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;


[Serializable]
public class SelectedWordChar
{
    public int Index { get; set; }
    public string Letter { get; set; }
    public bool IsHidden { get; set; }

    public SelectedWordChar(int index, string letter, bool isHidden)
    {
        Index = index;
        Letter = letter;
        IsHidden = isHidden;
    }
}

public class WordManager : MonoBehaviour
{
    private static readonly Random Rng = new Random();
    private List<PanelLetterController> _lettersInPanelChar = new List<PanelLetterController>();
    [SerializeField] private CanvasWordUIManager canvasWordUIManager;
    [SerializeField] private WordsScript wordsScript;

    public static WordManager İnstance;
    public int hiddenLetterAmount = 2; //TODO: set it automatically
    [HideInInspector] public List<SelectedWordChar> selectedWordCharList = new List<SelectedWordChar>();

    public delegate void WordManagerDelegate();

    public static WordManagerDelegate NextWordDelegate;

    private void Awake()
    {
        İnstance = this;
    }

    private void OnEnable()
    {
        NextWordDelegate += SetSelectedWordWithDelay;
        GameManager.NextLevelDelegate += SetSelectedWord;
        GameManager.ResetLevelDelegate += SetSelectedWord;
    }

    private void Start()
    {
        SetSelectedWord();
    }

    private void SetSelectedWordWithDelay()
    {
        StartCoroutine(SetSelectedWordDelay());
    }

    private IEnumerator SetSelectedWordDelay()
    {
        yield return new WaitForSeconds(1.3f);
        SetSelectedWord();
    }

    private void SetSelectedWord()
    {
        selectedWordCharList.Clear();
        var selectedWord = wordsScript.SelectWord();
        _lettersInPanelChar = canvasWordUIManager.ShowLetters(selectedWord.Length);
        InitSelectedWord(selectedWord);
    }

    private void InitSelectedWord(string selectedWord)
    {
        var lenght = selectedWord.Length;
        for (var i = 0; i < lenght; i++)
        {
            _lettersInPanelChar[i].SetLetterVisibility(true);
            selectedWordCharList.Add(new SelectedWordChar(i, selectedWord[i].ToString(), false));
            _lettersInPanelChar[i].UpdateLetter(selectedWord[i].ToString());
        }

        HideRandomLetter(hiddenLetterAmount, lenght);
    }

    private void HideRandomLetter(int amount, int maxNumber)
    {
        var numbers = 0;

        while (numbers < amount)
        {
            int num = Rng.Next(maxNumber);
            if (!selectedWordCharList[num].IsHidden)
            {
                selectedWordCharList[num].IsHidden = true;
                _lettersInPanelChar[num].SetLetterVisibility(false);
                numbers += 1;
            }
        }
    }

    // Check if letter is one of the hidden letters. If yes; show the letter in box
    public void FindLetterAndShow(string letter, Vector3 dorPos)
    {
        var firstFound = selectedWordCharList.FirstOrDefault(h => h.Letter.ToString() == letter && h.IsHidden);

        if (firstFound != null)
        {
            //TODO: set true visibilty after the doorpanelimage movement
            CanvasDoorManager.instance.CanvasDoorUIMovement(dorPos, _lettersInPanelChar[firstFound.Index], letter);
            selectedWordCharList[firstFound.Index].IsHidden = false;

            var hiddenFirstFound = selectedWordCharList.FirstOrDefault(h => h.IsHidden);
            if (hiddenFirstFound == null)
            {
                GameManager.instance.SubtractLevelWordNumber(1);

                if (GameManager.instance.levelWordNumber == 0)
                {
                    CanvasManager.SetWordCountRemainingDelegate();
                    GameManager.LevelCompletedDelegate();
                }
                else
                    NextWordDelegate();
            }
        }
        else
        {
            if (GameManager.instance.SubtractPlayerLife())
                GameManager.GameOverDelegate();

            PlayerScript.PlayerAnimatorController.PlayerStumbleAnimationDelegate();
            CineMachineManager.CineMachineShakeDelegate();
            CanvasManager.SetPlayerLifeDelegate();
        }
    }
}