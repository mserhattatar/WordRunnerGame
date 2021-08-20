using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    private readonly List<string> _words = new List<string>
    {
        "ASTRAL", "ATOMIC", "BENIGN", "BRAINY", "BRIGHT", "CHALKY", "CHEEKY", "CHUMMY", "CLAMMY", "CLUMPY", "COGENT",
        "COMELY", "CRAVEN", "DECENT", "DEMURE", "DIRECT", "DREAMY", "EARTHY", "EFFETE", "EROTIC", "FILTHY", "FLABBY",
        "FLIRTY", "FOLIAR", "GLASSY", "GLOOMY", "GRUBBY", "GRUMPY", "HEARTY", "HEATED", "HECTIC", "HUNGRY", "IRENIC",
        "IRONIC", "KARMIC", "KINGLY", "LATENT", "MATURE", "MIGHTY", "MULISH", "NATIVE", "PALLID", "PATCHY", "POROUS",
        "PRETTY", "PUTRID", "RAGGED", "RANCID", "RECENT", "RUSTIC", "SECRET", "SERENE", "SLOPPY", "SPONGY", "STUPID",
        "TENDER", "TORPID", "TOUCHY", "TRENDY", "UPPITY", "URSINE", "VESTAL", "WORTHY"
    }; //TODO: add more words

    [SerializeField] private List<PanelLetterController> lettersInPanelChar;
    private static readonly Random Rng = new Random();

    public static WordManager İnstance;
    public int hiddenLetterAmount = 2; //TODO: set it automatically
    [HideInInspector] public string selectedWord;
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
        SelectWord();
        InitSelectedWord(selectedWord);
    }
    
    private void SelectWord()
    {
        var wordIndex = Rng.Next(_words.Count);
        selectedWord = _words[wordIndex];
        _words.Remove(_words[wordIndex]);
    }

    private void InitSelectedWord(string _selectedWord)
    {
        var lenght = _selectedWord.Length;
        for (int i = 0; i < lenght; i++)
        {
            lettersInPanelChar[i].SetLetterVisibility(true);
            selectedWordCharList.Add(new SelectedWordChar(i, _selectedWord[i].ToString(), false));
            lettersInPanelChar[i].UpdateLetter(_selectedWord[i].ToString());
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
                lettersInPanelChar[num].SetLetterVisibility(false);
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
            CanvasDoorManager.instance.CanvasDoorUIMovement(dorPos, lettersInPanelChar[firstFound.Index], letter);
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