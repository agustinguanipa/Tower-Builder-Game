using UnityEngine;
using System.Collections;

public class SettingScene : MonoBehaviour {
	
	//public float num = 0.5;
	// Use this for initialization
	void Start () {
		float music = PlayerPrefs.GetFloat ("Music");
		float sound = PlayerPrefs.GetFloat ("Sound");
		
		GameObject object1 = GameObject.Find("MusicSlider");
		UISlider musicSlider = object1.GetComponent("UISlider") as UISlider;
		musicSlider.value = music;

		GameObject object2 = GameObject.Find("SoundSlider");
		UISlider soundSlider = object2.GetComponent("UISlider") as UISlider;
		soundSlider.value = sound;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnClick(){
		Application.LoadLevel(0);
	}
	
	public void OnValueChange()
	{
		if (AudioEx.Instance) {
			float num = UIProgressBar.current.value;
			AudioEx.Instance.setMusicSound (num);
			PlayerPrefs.SetFloat("Music", num);
			
			Debug.Log ("change music:" + UIProgressBar.current.value);
		}
	}
	
	public void OnValueChangeSound()
	{
		if (AudioEx.Instance) {
			float num = UIProgressBar.current.value;
			AudioEx.Instance.setSoundVolume (num);
			PlayerPrefs.SetFloat("Sound", num);
			
			Debug.Log ("change sound:" + UIProgressBar.current.value);
		}
	}
	
	public void OnValueChangeSoundEnd()
	{
		if (AudioEx.Instance) {
			
		}
	}
}
