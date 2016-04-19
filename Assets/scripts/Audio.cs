using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Audio : MonoBehaviour {

    AudioSource track;
    Image VolumeBtn;

	// Use this for initialization
	void Start () {
        track = GameObject.Find("Music").GetComponent<AudioSource>();
        VolumeBtn = GameObject.Find("Play Pause Audio Btn").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void muteAudio()
    {
        if (track.mute == true)
        {
            track.mute = false;
            VolumeBtn.sprite = Resources.Load<Sprite>("Sprite/volume");
        }
        else
        {
            VolumeBtn.sprite = Resources.Load<Sprite>("Sprite/volumeMute");
            track.mute = true;
        }
    }
}
