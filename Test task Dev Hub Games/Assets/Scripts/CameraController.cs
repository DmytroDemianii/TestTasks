using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float joystickSensitivity = 0.1f;
    public float cameraSensitivity = 1.0f;
    [SerializeField] private Joystick joystick;


    void Update ()
    {
        float joystickX = joystick.Horizontal;
        float joystickY = joystick.Vertical;

        Vector3 cameraRotation = new Vector3(-joystickY, joystickX, 0.0f);
        Quaternion rotation = Quaternion.Euler(cameraRotation * cameraSensitivity);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, joystickSensitivity);
    }
}
