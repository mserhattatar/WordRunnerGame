using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    private readonly string[] _words = new[]
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

    public delegate void WordManagerDelegate();

    public static WordManagerDelegate NextWordDelegate;

    [HideInInspector] public List<SelectedWordChar> selectedWordCharList = new List<SelectedWordChar>();
    [HideInInspector] public string selectedWord;
    public int hiddenLetterAmount = 2; //TODO: set it automatically
    public static WordManager İnstance;

    private void Awake()
    {
        İnstance = this;
    }

    private void Start()
    {
        NextWordDelegate += SetSelectedWord;
        GameManager.NextLevelDelegate += SetSelectedWord;
        GameManager.ResetLevelDelegate += SetSelectedWord;
        SetSelectedWord();
    }

    private void SetSelectedWord()
    {
        var wordIndex = Rng.Next(_words.Length);
        selectedWord = _words[wordIndex];
        selectedWordCharList.Clear();
        InitSelectedWord(selectedWord);
    }

    private void InitSelectedWord(string _selectedWord)
    {
        var lenght = _selectedWord.Length;
        for (int i = 0; i < lenght; i++)
        {
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
        var found = selectedWordCharList.FirstOrDefault(h => h.Letter.ToString() == letter && h.IsHidden);


        if (found != null)
        {
            //TODO: set true visibilty after the doorpanelimage movement
            //lettersInPanelChar[found.Index].SetLetterVisibility(true);
            selectedWordCharList[found.Index].IsHidden = false;

            CanvasDoorManager.instance.CanvasDoorUIMovement(dorPos, lettersInPanelChar[found.Index], letter);
        }
        else
        {
            PlayerScript.PlayerAnimatorController.PlayerStumbleAnimationDelegate();
            CineMachineManager.CineMachineShakeDelegate();
        }
    }
}