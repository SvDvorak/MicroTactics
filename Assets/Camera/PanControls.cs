using UnityEngine;

public class PanControls : MonoBehaviour
{
    public AnimationCurve MouseCurve;
    public float PanSpeed;

    void Start()
    {
    }

    void Update()
    {
        var panX = MouseCurve.Evaluate(Input.mousePosition.x / Screen.width);
        var panY = MouseCurve.Evaluate(Input.mousePosition.y / Screen.height);

        var pan = Vector3.ClampMagnitude(new Vector3(panX, 0, panY), 1);
        transform.Translate(pan * PanSpeed * Time.deltaTime, Space.World);
    }
}
