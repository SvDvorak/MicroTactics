using UnityEngine;
using System.Collections;

public class SetFrameRate : MonoBehaviour
{
	public void Awake ()
	{
	    Application.targetFrameRate = Simulation.FrameRate;
	}
}

public static class Simulation
{
    public static readonly int FrameRate = 60;
    public static readonly float Gravity = 9.81f;
}