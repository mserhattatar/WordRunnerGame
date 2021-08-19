using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private List<DoorController> doorsList;
    [SerializeField] private Transform player;
    private readonly string alphabetChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private float _doorPosDistance = 40f;
    private static readonly Random Rng = new Random();

    private void Start()
    {
        CreateDoorLetters(3);
    }

    private void Update()
    {
        if (player.transform.position.z + 30f > _doorPosDistance)
        {
            CreateDoorLetters(Rng.Next(2, 4));
        }
    }

    private List<SelectedWordChar> GetSelectedWordList()
    {
        return WordManager.İnstance.selectedWordCharList;
    }

    private void CreateDoorLetters(int doorNumber)
    {
        // Get a random hidden word
        var selectedChars = GetSelectedWordList();
        if (selectedChars == null || selectedChars.Count == 0)
        {
            Debug.LogWarning("Selected char list is empty or null");
            return;
        }

        var ran = new Random();
        // Create a object list of hidden letters
        var hiddens = selectedChars.Where(c => c.IsHidden).ToList();
        if (hiddens.Count == 0)
        {
            Debug.LogWarning("hiddens char list is empty or null");
            return;
        }

        // Select a random hidden letter to show in one door
        var index = ran.Next(0, hiddens.Count());
        var hiddenLetter = hiddens[index].Letter;
        // Create random letters to show in other doors
        var doorLetters = CreateRandomLetter(doorNumber - 1, hiddens);
        doorLetters.Add(hiddenLetter);

        SetDoorPassive(player.transform.position.z);
        SetDoorsPositions(doorNumber, doorLetters);
    }

    private List<string> CreateRandomLetter(int amount, List<SelectedWordChar> hiddens)
    {
        // Create a string list of hidden letters
        var hiddenLetters = hiddens.Select(x => x.Letter).ToList();
        var ran = new Random();
        var letters = new List<string>();

        while (letters.Count < amount)
        {
            var num = ran.Next(alphabetChars.Length);
            var letter = alphabetChars[num];
            // Check if random letter is already in letters list or if it is a hidden letter
            if (letters.IndexOf(letter.ToString()) < 0 && hiddenLetters.IndexOf(letter.ToString()) < 0)
                letters.Add(letter.ToString());
        }

        return letters;
    }

    private void SetDoorsPositions(int doorNumber, IReadOnlyList<string> doorLetters)
    {
        var posX = -1.4f;
        if (doorNumber == 3)
            posX = -2.5f;

        var passiveDoors = doorsList.Where(d => !d.isSetActive).ToList();
        var randomizeDoorLetters = doorLetters.OrderBy(a => Guid.NewGuid()).ToList();
        for (int i = 0; i < doorNumber; i++)
        {
            var pos = new Vector3(posX, 1.52f, _doorPosDistance);
            posX += 2.5f;
            passiveDoors[i].GetComponent<DoorController>().SetDoorPosWriteLetter(randomizeDoorLetters[i], pos);
        }

        _doorPosDistance += 40f;
    }

    private void SetDoorPassive(float playerPosZ)
    {
        var activeDoors = doorsList.Where(d => d.isSetActive).ToList();

        foreach (var t in activeDoors.Where(t => playerPosZ > t.gameObject.transform.position.z))
        {
            t.GetComponent<DoorController>().SetDoor(false);
        }
    }
}