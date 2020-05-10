using UnityEngine;
using System.Collections;

public class RotateSample : MonoBehaviour
{	
	void Start(){
		iTween.RotateBy(gameObject, iTween.Hash("z", .01, "easeType", "easeInOutBack", "loopType", "pingPong", "delay", .4));
	}
}

