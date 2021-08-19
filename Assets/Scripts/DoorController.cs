using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public TextMeshPro doorTextMeshPro;
    [HideInInspector]public string doorLetter;

    public void UpdateDoorLetter(string doorLetter)
    {
        this.doorLetter = doorLetter;
        doorTextMeshPro.text = this.doorLetter;
    }

    public void SetDoor(bool SetActive)
    {
        gameObject.SetActive(SetActive);
    }
}