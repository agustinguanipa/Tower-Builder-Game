using UnityEngine;
using System.Collections;

public class LoginScene : MonoBehaviour {

	public GameObject earth;
	public GameObject background;
	public GameObject music;

	void Awake () {
		DontDestroyOnLoad(music);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		earth.transform.Rotate(Vector3.forward, 3 * Time.deltaTime);
		background.transform.localScale += new Vector3 (0.02f, 0.02f, 0);
	}

	public void SingleClick(){
		Application.LoadLevel(2);
		AudioEx.Instance.playSound ("button");
	}

	public void MultiplayerClick(){
		Application.LoadLevel(2);
		AudioEx.Instance.playSound ("button");
	}

	public void SettingClick(){
		Application.LoadLevel(1);
		AudioEx.Instance.playSound ("button");
	}

	public void ExitClick(){
		AudioEx.Instance.playSound ("button2");
	}
}
