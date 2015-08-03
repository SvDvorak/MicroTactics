using UnityEngine;
using System.Collections;

public class SetFrameRate : MonoBehaviour
{
	public void Awake ()
	{
	    Application.targetFrameRate = 60;
	}
}