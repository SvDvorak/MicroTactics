using UnityEngine;
using System.Collections;

public class RotateTestCode : MonoBehaviour
{
    private Quaternion _startRotation;
    private Quaternion _targetRotation;
    private float _time;
    private float _totalTime;

    void Start()
    {
        _startRotation = transform.rotation;
        _targetRotation = _startRotation;
        _time = 0;
        _totalTime = 1;
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(_startRotation, _targetRotation, _time/_totalTime);

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var toHitDirection = (hit.point - transform.position).normalized;
                var toHitRotation = Quaternion.LookRotation(toHitDirection, Vector3.up);
                _startRotation = transform.rotation;
                _targetRotation = toHitRotation;
                Vector3 axis;
                float angle;
                Quaternion.FromToRotation(transform.rotation*Vector3.forward, toHitDirection).ToAngleAxis(out angle, out axis);
                Debug.Log("Angle: " + angle);
                Debug.DrawLine(Vector3.zero, hit.point, Color.red, 5);
                Debug.DrawLine(Vector3.zero, transform.rotation*transform.forward*10, Color.blue, 5);
                _time = 0;
                _totalTime = angle/80;
            }
        }

        _time += Time.deltaTime;
    }
}