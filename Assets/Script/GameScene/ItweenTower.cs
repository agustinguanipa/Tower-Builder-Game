using UnityEngine;
using System.Collections;

public class ItweenTower : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveTo (this.gameObject, iTween.Hash ("path", iTweenPath.GetPath ("Towerin"), 
		                                             "time", 3, 
		                                             "looptype", iTween.LoopType.loop,
		                                             "easetype", iTween.EaseType.linear));
	}
}
