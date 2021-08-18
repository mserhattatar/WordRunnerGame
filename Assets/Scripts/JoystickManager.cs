using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    private static Joystick _joystick;
    protected static float JoystickHorizontal;

    private void Awake()
    {
        _joystick = FindObjectOfType<Joystick>();
    }

    private void FixedUpdate()
    {
        JoystickHorizontal = _joystick.Horizontal;
    }

    protected static bool CheckJoystickHorizontal()
    {
        return (JoystickHorizontal > 0.3 || JoystickHorizontal < -0.3);
    }
}