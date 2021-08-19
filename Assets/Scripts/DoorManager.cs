using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using Random = System.Random;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private List<DoorController> doorsList;
    private string alphabetChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private List<string> _hiddenLetters = new List<string>();
    private List<int> _hiddenLettersIndex = new List<int>();
    private string _selectedWord;

    private void Start()
    {
        _hiddenLettersIndex = WordManager.instance.GetHiddenLetterIndex();
        _selectedWord = WordManager.instance.GetSelectedWord();
        CreateDoorLetters(3);
    }

    private void CreateDoorLetters(int doorNumber)
    {
        // Get a random hidden word
        var ran = new Random();
        var index = ran.Next(0, _hiddenLettersIndex.Count);
        var hiddenLetter = _selectedWord[_hiddenLettersIndex[index]];
        var doorLetters = CreateRandomLetter(doorNumber - 1);
        doorLetters.Add(hiddenLetter);
        
        var posX = -1.4f;
        if (doorNumber == 3)
            posX = -2.5f;

        for (int i = 0; i < doorNumber; i++)
        {
            doorsList[i].transform.position = new Vector3(posX, 1.52f, 40f);
            posX += 2.5f;
            doorsList[i].GetComponent<DoorController>().UpdateDoorLetter(doorLetters[i].ToString());
        }
    }

    private List<char> CreateRandomLetter(int amount)
    {
        _hiddenLetters = WordManager.GetHiddenLetters(_selectedWord,_hiddenLettersIndex);
        var ran = new Random();
        var letters = new List<char>();
        
        while (letters.Count < amount)
        {
            var num = ran.Next(alphabetChars.Length);
            var letter = alphabetChars[num];
            if (letters.IndexOf(letter) < 0 && _hiddenLetters.IndexOf(letter.ToString()) < 0)
                letters.Add(letter);
        }

        return letters;
    }
}