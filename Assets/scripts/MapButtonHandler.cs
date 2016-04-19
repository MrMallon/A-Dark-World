using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapButtonHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void loadScene(string sceneName)
    {
        //Loads scene
        SceneManager.LoadScene(sceneName);
    }

    public void levelPicked(int level)
    {
        PlayerPrefs.SetInt("LevelPiacked", level);
        PlayerPrefs.Save();
    }
}
