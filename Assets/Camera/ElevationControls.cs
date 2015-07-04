using UnityEngine;

public class ElevationControls : MonoBehaviour
{
    public float KeySpeed;
    public float ScrollSpeed;

    void Update()
    {
        transform.Translate((GetKeysMovement() + GetScrollMovement()) * Time.deltaTime, Space.World);
    }

    private Vector3 GetScrollMovement()
    {
        return Vector3.up*Input.mouseScrollDelta.y*ScrollSpeed;
    }

    private Vector3 GetKeysMovement()
    {
        var upMove = Input.GetKey(KeyCode.LeftShift) ? Vector3.down : Vector3.zero;
        var downMove = Input.GetKey(KeyCode.Space) ? Vector3.up : Vector3.zero;

        var keyMove = (upMove + downMove)*KeySpeed;
        return keyMove;
    }
}
