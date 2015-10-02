using UnityEngine;
using UnityEngine.Assertions;

public class RaiseExceptions : MonoBehaviour
{
    void Start ()
    {
        Assert.raiseExceptions = true;
    }
}
