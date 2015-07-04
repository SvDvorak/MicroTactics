using UnityEngine;

public class PanControls : MonoBehaviour
{
    public AnimationCurve MouseCurve;
    public float MousePanSpeed;
    public float KeysPanSpeed;

    void Update()
    {
        transform.Translate((GetMouseMove() + GetKeysMove()) * Time.deltaTime, Space.World);
    }

    private Vector3 GetMouseMove()
    {
        var panX = MouseCurve.Evaluate(Input.mousePosition.x/Screen.width);
        var panY = MouseCurve.Evaluate(Input.mousePosition.y/Screen.height);

        return Vector3.ClampMagnitude(new Vector3(panX, 0, panY), 1)*MousePanSpeed;
    }

    private Vector3 GetKeysMove()
    {
        var forwardMove = Input.GetKey(KeyCode.W) ? Vector3.forward : Vector3.zero;
        var backMove = Input.GetKey(KeyCode.S) ? Vector3.back : Vector3.zero;
        var leftMove = Input.GetKey(KeyCode.A) ? Vector3.left : Vector3.zero;
        var rightMove = Input.GetKey(KeyCode.D) ? Vector3.right : Vector3.zero;

        var keysMove = (forwardMove + backMove + leftMove + rightMove)*KeysPanSpeed;
        return keysMove;
    }
}
