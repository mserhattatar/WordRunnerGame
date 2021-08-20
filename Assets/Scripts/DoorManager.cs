using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private List<DoorController> doorsList;
    [SerializeField] private Transform player;
    private string _alphabetChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private float _playerOldPosY;
    private bool _doorCreated;
    private static readonly Random Rng = new Random();
    
    public delegate void DoorManagerDelegate();

    public static DoorManagerDelegate SetOldPlayerPosDelegate;

    private void OnEnable()
    {
        SetOldPlayerPosDelegate += SetPlayerOldPos;
        
        GameManager.ResetLevelDelegate += ResetDoorManager;

        GameManager.NextLevelDelegate += ResetDoorManager;
    }


    private void Start()
    {
        CreateDoorLetters(2);
        SetPlayerOldPos();
    }

    private void Update()
    {
        if (!_doorCreated && player.transform.position.z > _playerOldPosY + 40f)
        {
            _doorCreated = true;
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


        // Create a object list of hidden letters
        var hiddenList = selectedChars.Where(c => c.IsHidden).ToList();
        if (hiddenList.Count == 0)
        {
            _doorCreated = false;
            return;
        }

        // Select a random hidden letter to show in one door
        var index = Rng.Next(0, hiddenList.Count());
        var hiddenLetter = hiddenList[index].Letter;
        // Create random letters to show in other doors
        var doorLetters = CreateRandomLetter(doorNumber - 1, hiddenList);
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
            var num = ran.Next(_alphabetChars.Length);
            var letter = _alphabetChars[num];
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
            var pos = new Vector3(posX, 1.52f, player.transform.position.z + 35f);
            posX += 2.5f;
            passiveDoors[i].GetComponent<DoorController>().SetDoorPosWriteLetter(randomizeDoorLetters[i], pos);
        }

        SetPlayerOldPos();
        _doorCreated = false;
    }

    private void SetDoorPassive(float playerPosZ = 0, bool all = false)
    {
        var activeDoors = doorsList.Where(d => d.isSetActive).ToList();

        foreach (var a in activeDoors)
        {
            if (all || playerPosZ > a.gameObject.transform.position.z)
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
        _doorCreated = false;
        CanvasManager.SetWordCountRemainingDelegate();
    }
}