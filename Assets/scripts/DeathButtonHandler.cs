using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathButtonHandler : MonoBehaviour {

    private string str;
    private float textSpeed = 0.09f;

    // Use this for initialization
    void Start () {
        string NPCreply = "Thank God I found you in time!!\nUnfortunately all of your items were stolen before I could get to you.\nHurry we must leave before they come back!!";
        StartCoroutine(convoText(NPCreply, ""));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void loadScene(string sceneName)
    {
        //Loads scene
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator convoText(string strComplete, string buttonPressed)
    {
        var replybtn2 = GameObject.Find("Reply 1").GetComponent<Button>();

        replybtn2.interactable = false;

        int i = 0;
        str = "";
        while (i < strComplete.Length)
        {
            Debug.Log("Animateing Text");
            str += strComplete[i++];
            yield return new WaitForSeconds(textSpeed);
        }

        replybtn2.interactable = true;
    }

    void OnGUI()
    {
        var convoBox = GameObject.Find("CampConvoText").GetComponent<Text>();
        convoBox.text = str;
    }

    public void setTextSpeed(float speed)
    {
        textSpeed = speed;
    }

}
