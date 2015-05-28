using UnityEngine;
using System.Collections;

public class ContextTesting : MonoBehaviour
{
    /// Add a context menu named "Do Something" in the inspector
    /// of the attached script.
    [ContextMenu("Do Something")]
    private void DoSomething()
    {
        Debug.Log("Perform operation");
    }
}
