using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public TextMeshPro doorTextMeshPro;
    private string _doorLetter;

    public DoorController(string doorLetter)
    {
        _doorLetter = doorLetter;
    }
}