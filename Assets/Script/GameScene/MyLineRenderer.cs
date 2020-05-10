using UnityEngine;
using System.Collections;

public class MyLineRenderer : MonoBehaviour {
	
	[SerializeField]
	private GameObject rope1;

	[SerializeField]
	private GameObject target;

	// Update is called once per frame
	void LateUpdate () {
		rope1.GetComponent<LineRenderer>().SetPosition(1, target.transform.localPosition);
	}
}
