using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class WordManager : MonoBehaviour
{
    
    private readonly string[] _words = new[] { "SEVNUR", "SERHAT", "DENEME" }; //TODO: add more words
    [SerializeField] private List<PanelLetterController> lettersInPanelChar;
    private List<int> _hiddenLettersIndex = new List<int>();

    
    public List<SelectedWordChar> selectedWordCharList = new List<SelectedWordChar>();
    public string selectedWord;
    public int hiddenLetterAmount = 2; //TODO: set it automatically
    public static WordManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        selectedWord = _words[2]; //TODO: set it automatically
        InitLetter(selectedWord);
    }

    private void InitLetter(string word)
    {
        _hiddenLettersIndex = CreateRandomNumber(hiddenLetterAmount, word.Length);
        for (int i = 0; i < word.Length; i++)
        {
            lettersInPanelChar[i].UpdateLetter(word[i].ToString());
            // Hide random letters of the word
            if (_hiddenLettersIndex.IndexOf(i) >= 0)
                lettersInPanelChar[i].SetLetterVisibility(false);
        }
    }


    // Create different random numbers in wordLength
    public static List<int> CreateRandomNumber(int amount, int maxNumber)
    {
        var ran = new Random();
        var numbers = new List<int>();

        while (numbers.Count < amount)
        {
            int num = ran.Next(maxNumber);
            if (numbers.IndexOf(num) < 0)
                numbers.Add(num);
        }

        return numbers;
    }

    public static List<string> GetHiddenLetters(string selectedWord, List<int> _hiddenLettersIndex)
    {
        var hiddenLettersList = new List<string>();
        for (int i = 0; i < _hiddenLettersIndex.Count; i++)
        {
            hiddenLettersList.Add(selectedWord[_hiddenLettersIndex[i]].ToString());
        }

        return hiddenLettersList;
    }


    public List<int> GetHiddenLetterIndex()
    {
        return _hiddenLettersIndex;
    }
    // Check if letter is one of the hidden letters. If yes; show the letter in box
    public void FindLetterAndShow(string letter)
    {
        var hiddenElement = _hiddenLettersIndex.First(h => selectedWord[h].ToString() == letter);

        if (hiddenElement >= 0)
        {
            lettersInPanelChar[hiddenElement].SetLetterVisibility(true);
            _hiddenLettersIndex.Remove(hiddenElement);
            //UpdateHiddenLetters();
        }
    }

    public void SetSelectedWord()
    {
        // Select a random word from words
        // Set this word as selected word
        // Remove this word from words list
    }

    public string GetSelectedWord()
    {
        return selectedWord;
    }

    public void setHiddenAmount(int levelNumber)
    {
        hiddenLetterAmount = levelNumber / 3 + 2;
    }

}

[Serializable]
public class SelectedWordChar
{
    public int Index { get; set; }
    public char Letter { get; set; }
    public bool IsHidden { get; set; }
    
    public SelectedWordChar(int index, char letter, bool isHidden)
    {
        Index = index;
        Letter = letter;
        IsHidden = isHidden;
    }
}
