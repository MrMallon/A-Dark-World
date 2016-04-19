using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadMap : MonoBehaviour {
    private Image homeBtn;
    int LevelAccess;
 	// Use this for initialization
	void Start () {
        homeBtn = GameObject.Find("HomeBtn").GetComponent<Image>();
        string colour = PlayerInformation.PalyerColour;

        if (colour == "Red") { homeBtn.color = Color.red; }
        else if (colour == "Green") { homeBtn.color = Color.green; }
        else if (colour == "Blue") { homeBtn.color = Color.blue; }
        Debug.Log("LEVEL !! ==" + PlayerPrefs.GetInt("CurrentLevel"));
        LevelAccess = PlayerPrefs.GetInt("CurrentLevel");
        activateLevels();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void activateLevels()
    {
       // var textLevel = GameObject.Find("LEVELNo").GetComponent<Text>();
      //  textLevel.text = LevelAccess.ToString();
        for (int i = 1; i <= LevelAccess; i++)
        {
            var level = GameObject.Find("Level " + i).GetComponent<Button>();
            level.interactable = true;
        }
    }
}
