using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadTutData : MonoBehaviour {

    int PlayedTutBefore;
    bool splash, firstGame;
    double timeRemaining = 00;
    Save save;
    Load loadData;
    // Use this for initialization
    void Start()
    {
        PlayedTutBefore = PlayerPrefs.GetInt("PlayedBeforeTut");
        loadData = new Load();
        save = new Save();
       
        PlayedTutBefore = 0;
        Debug.Log("PlayedBefore Tut = " + PlayedTutBefore);
   }
	
	// Update is called once per frame
	void Update ()
    {
        var splashScreen = GameObject.Find("ADarkWorld").GetComponent<Image>();
        Color Alpha = splashScreen.color;
        timeRemaining += Time.deltaTime;

        if (!splash)
        {
            if (timeRemaining > 0.01)
            {
                if (Alpha.a > 0)
                {
                    Alpha.a -= 0.01f;
                    splashScreen.color = Alpha;
                    timeRemaining = 0;
                }
                if (Alpha.a <= 0)
                {
                    splash = true;
                }
            }
        }

        if (PlayedTutBefore > 0 && splash == true)
        {
            SceneManager.LoadScene("Camp");
        }

        else if (PlayedTutBefore == 0 && splash == true && !firstGame)
        {
            //clearData();
            var blackScreen = GameObject.Find("Canvas").GetComponent<Canvas>();
            blackScreen.enabled = false;
            save.createData();
            loadData.loadInventory();
            Debug.Log("I should not be here " + PlayedTutBefore);
            loadPlayerInfo();
            firstGame = true;
        }
    }


    private void clearData()
    {
        PlayerPrefs.SetString("PalyerName", "");
        PlayerPrefs.SetString("PalyerColour", "");
        PlayerPrefs.SetString("Engineer", "");
        PlayerPrefs.SetInt("PeopleInCamp", 0);
        PlayerPrefs.SetFloat("FireSize", 0f);
        PlayerPrefs.SetFloat("FireLight", 0f);
        PlayerPrefs.SetInt("Tutorial", 0);
        PlayerPrefs.SetInt("Exploration", 0);
        PlayerPrefs.SetInt("MapPostion", 0);
    }

    private void loadPlayerInfo()
    {
        PlayerInformation.PalyerName = PlayerPrefs.GetString("PalyerName");
        PlayerInformation.PalyerColour = PlayerPrefs.GetString("PalyerColour");
        PlayerInformation.Engineer = PlayerPrefs.GetString("Engineer");
        PlayerInformation.PeopleInCamp = PlayerPrefs.GetInt("PeopleInCamp");
        PlayerInformation.FireSize = PlayerPrefs.GetFloat("FireSize");
        PlayerInformation.FireLight = PlayerPrefs.GetFloat("FireLight");
        PlayerInformation.Tutorial = PlayerPrefs.GetInt("Tutorial");
        PlayerInformation.Exploration = PlayerPrefs.GetInt("Exploration");
        PlayerInformation.MapPostion = PlayerPrefs.GetInt("MapPostion");
    }
}
