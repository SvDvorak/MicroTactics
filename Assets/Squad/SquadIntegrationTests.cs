using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions.Must;

//[IntegrationTest.DynamicTest("IntegrationTests")]
//[IntegrationTest.SucceedWithAssertions]
//[IntegrationTest.Timeout(1)]
public class SquadIntegrationTests : MonoBehaviour
{
	private void Awake ()
	{
	    var squad = GameObject.Find("Squad");
	    var selectionIndicators = squad.GetChildren("SelectionIndicator");

        selectionIndicators.Select(x => x.activeSelf).All(x => x == false).MustBeTrue();
	}
}