using UnityEngine;
using System.Collections;

public class AudioEx : MonoBehaviour
{
	public static AudioEx Instance = null;

	public AudioSource Music;
	public AudioSource Sound;

	static private float musicVolume;
	static private float soundVolume;

	public int position = 0;
	public int samplerate = 44100;
	public float frequency = 440;

	void Awake()
	{
		if (Instance == null)
			Instance = this;

		soundVolume = 0.5f;
		musicVolume = 0.5f;
	}

    void Start()
    {
		//m_Audio.clip = background_music;
		//m_Audio.loop = true;
		//m_Audio.Play ();

    }

	public float getMusicVolume()
	{
		return musicVolume;
	}

	public float getSoundVolume()
	{
		return soundVolume;
	}

	public void setMusicSound(float num)
	{
		Debug.Log ("play music:" + num);
		Music.volume = musicVolume = num;
	}

	public void setSoundVolume(float num)
	{
		Debug.Log ("play sound:" + num);
		soundVolume = num;

		this.playSound ("button2");
	}

	public void playSound(string filename)
	{
		AudioClip myClip = Resources.Load ("Sound/button", typeof(AudioClip)) as AudioClip;
		Sound.clip = myClip;
		Sound.volume = soundVolume;
		Sound.Play ();
	}
}
