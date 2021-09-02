using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private List<DoorController> doorsList;
    [SerializeField] private Transform player;
    private const string AlphabetChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private float _playerOldPosY;
    private bool _isDoorCreated;
    private static readonly Random Rng = new Random();


    private void OnEnable()
    {
        GameManager.ResetLevelDelegate += ResetDoorManager;

        GameManager.NextLevelDelegate += ResetDoorManager;
    }

    private void Update()
    {
        if (!_isDoorCreated && player.transform.position.z > _playerOldPosY + 35f)
        {
            _isDoorCreated = true;
            CreateDoorLetters(Rng.Next(2, 4));
        }
    }

    private void CreateDoorLetters(int doorCount)
    {
        // Get a random hidden word
        var selectedChars = GetSelectedWordList();
        if (selectedChars == null || selectedChars.Count == 0)
        {
            Debug.LogWarning("Selected char list is empty or null");
            _isDoorCreated = false;
            return;
        }

        // Create a object list of hidden letters
        var hiddenList = selectedChars.Where(c => c.IsHidden).ToList();
        if (hiddenList.Count == 0)
        {
            _isDoorCreated = false;
            return;
        }

        // Select a random hidden letter to show in one door
        var index = Rng.Next(0, hiddenList.Count());
        var hiddenLetter = hiddenList[index].Letter;
        // Create random letters to show in other doors
        var doorLetters = CreateRandomLetter(doorCount - 1, hiddenList);
        doorLetters.Add(hiddenLetter);

        SetDoorPassive(player.transform.position.z);
        SetDoorsPositions(doorCount, doorLetters);
        SetPlayerOldPos();
        _isDoorCreated = false;
    }

    private static List<SelectedWordChar> GetSelectedWordList()
    {
        return WordManager.İnstance.selectedWordCharList;
    }

    private static List<string> CreateRandomLetter(int amount, List<SelectedWordChar> hiddens)
    {
        // Create a string list of hidden letters
        var hiddenLetters = hiddens.Select(x => x.Letter).ToList();
        var ran = new Random();
        var letters = new List<string>();

        while (letters.Count < amount)
        {
            var num = ran.Next(AlphabetChars.Length);
            var letter = AlphabetChars[num];
            // Check if random letter is already in letters list or if it is a hidden letter
            if (letters.IndexOf(letter.ToString()) < 0 && hiddenLetters.IndexOf(letter.ToString()) < 0)
                letters.Add(letter.ToString());
        }

        return letters;
    }

    private void SetDoorsPositions(int doorCount, IReadOnlyList<string> doorLetters)
    {
        var posX = -1.4f;
        if (doorCount == 3)
            posX = -2.5f;

        var passiveDoors = doorsList.Where(d => !d.isSetActive).ToList();
        var randomizeDoorLetters = doorLetters.OrderBy(a => Guid.NewGuid()).ToList();
        for (var i = 0; i < doorCount; i++)
        {
            var pos = new Vector3(posX, 1.52f, player.transform.position.z + 35f);
            posX += 2.5f;
            passiveDoors[i].GetComponent<DoorController>().SetDoorPosWriteLetter(randomizeDoorLetters[i], pos);
        }
    }

    private void SetDoorPassive(float playerPosZ, bool all = false)
    {
        var activeDoors = doorsList.Where(d => d.isSetActive).ToList();

        foreach (var a in activeDoors)
        {
            if (all || playerPosZ + 5f > a.gameObject.transform.position.z)
            {
                a.GetComponent<DoorController>().SetDoor(false);
            }
        }
    }

    private void SetPlayerOldPos()
    {
        _playerOldPosY = player.transform.position.z;
    }

    private void ResetDoorManager()
    {
        SetDoorPassive(0, true);
        SetPlayerOldPos();
        _isDoorCreated = false;
        CreateDoorLetters(2);
    }
}