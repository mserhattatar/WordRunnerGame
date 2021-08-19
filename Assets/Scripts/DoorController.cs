using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public TextMeshPro doorTextMeshPro;
    public bool isSetActive;
    [HideInInspector]public string doorLetter;

    public void SetDoorPosWriteLetter(string letter, Vector3 pos)
    {
        SetDoor(true);
        doorLetter = letter;
        doorTextMeshPro.text = doorLetter;
        transform.position = pos;
    }

    public void SetDoor(bool setActive)
    {
        gameObject.SetActive(setActive);
        isSetActive = setActive;
    }
}