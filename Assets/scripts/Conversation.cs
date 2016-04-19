using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Conversation : MonoBehaviour {

    private int conversationStage = 0;
    private int futureConvoState = 1;
    private string str;
    private float textSpeed = 0.09f;
    private bool fireLite = false;
    int characterGender;
    int characterType;
    int counter = 0;
    Image character;

    void Start()
    {
        var inputField = GameObject.Find("InputField").GetComponent<InputField>();
        var submitBtn = GameObject.Find("submit btn").GetComponent<Button>();
        var submitTxt = GameObject.Find("Submit Text").GetComponent<Text>();

        character = GameObject.Find("Character").GetComponent<Image>();
        inputField.enabled = false;
        inputField.image.enabled = false;
        submitBtn.enabled = false;
        submitBtn.image.enabled = false;
        submitTxt.text = "";
        characterType = Random.Range(1, 11);
        characterGender = Random.Range(1, 11);

    }

    void Update()
    {
        if(PlayerInformation.EventConvo == 0)
        {
            setConvoStage(1);
            string NPCreply = "Welcome to your camp!\nIf you press the gather wood button you will start to gather wood.\nOnce you have wood you can begin to craft Items by opening the craft item menu.\nDon't forget to keep your fire alive!!!";
            StartCoroutine(convoText(NPCreply, ""));
        }

    }

    public IEnumerator convoText(string strComplete, string buttonPressed)
    {
        var replybtn1 = GameObject.Find("Reply 1").GetComponent<Button>();
        var replybtn2 = GameObject.Find("Reply 2").GetComponent<Button>();
        var replybtn3 = GameObject.Find("Reply 3").GetComponent<Button>();
        var submitBtn = GameObject.Find("submit btn").GetComponent<Button>();

        replybtn1.interactable = false;
        replybtn2.interactable = false;
        replybtn3.interactable = false;
        submitBtn.interactable = false;

        int i = 0;
        str = "";
        while (i < strComplete.Length)
        {
            Debug.Log("Animateing Text");
            str += strComplete[i++];
            yield return new WaitForSeconds(textSpeed);
        }

        replybtn1.interactable = true;
        replybtn2.interactable = true;
        replybtn3.interactable = true;

        if (buttonPressed == "Reply1") { replybtn1.interactable = false; }
        else if (buttonPressed == "Reply2") { replybtn2.interactable = false; }
        else if (buttonPressed == "Reply3") { replybtn3.interactable = false; }

        if (futureConvoState == conversationStage)
        {
            futureConvoState++;
            replybtn1.interactable = true;
            replybtn2.interactable = true;
            replybtn3.interactable = true;
        }
        submitBtn.interactable = true;

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
    public void setFireLit(bool lite)
    {
        fireLite = lite;
    }

    private void setConvoStage(int nextStage)
    {
        PlayerInformation.EventConvo = nextStage;
        PlayerPrefs.SetInt("EventConvo", PlayerInformation.EventConvo);
        PlayerPrefs.Save();
    }

    public void eventConvo(string playerReply)
    {
        Canvas eventCanvas = GameObject.Find("Event Canvas").GetComponent<Canvas>();
        var replybtn1 = GameObject.Find("Reply 1 text").GetComponent<Text>();
        var replybtn2 = GameObject.Find("Reply 2 text").GetComponent<Text>();
        var replybtn3 = GameObject.Find("Reply 3 text").GetComponent<Text>();

        setTextSpeed(0.08f);

        if (PlayerInformation.EventConvo == 1)
        {
            eventCanvas.enabled = false;
            setConvoStage(2);
        }
        else if (PlayerInformation.EventConvo == 2)
        {           
            string NPCreply = PlayerInformation.PalyerName + "!!\nSomeone is approaching your camp...";
            StartCoroutine(convoText(NPCreply, "")); 
              
            replybtn1.text = "WHERE?!";
            replybtn2.text = "WHAT?!";
            replybtn3.text = "WHO?!";

            if (playerReply == "Reply1")
            {
                setConvoStage(3);
            }
            if (playerReply == "Reply2")
            {
                setConvoStage(3);
            }
            if (playerReply == "Reply3")
            {
                setConvoStage(3);
            }
            Debug.Log(PlayerInformation.EventConvo + "2");
        }
        else if (PlayerInformation.EventConvo == 3)
        {

            character.sprite = Resources.Load<Sprite>("Sprite/female stranger");
            
            string NPCreply = "Please HELP ME!!\nPeople have invaded the cave I was living in...\nHere you'll need this...";
            StartCoroutine(convoText(NPCreply, ""));

            if (playerReply == "Reply1")
            {
                setConvoStage(4);
            }
            if (playerReply == "Reply2")
            {
                setConvoStage(4);
            }
            if (playerReply == "Reply3")
            {
                setConvoStage(4);
            }

            replybtn1.text = "I'll Try";
            replybtn2.text = "What's this?";
            replybtn3.text = "Thank you";

            Debug.Log(PlayerInformation.EventConvo + "3");
        }
        else if (PlayerInformation.EventConvo == 4)
        {
            var mapBtn = GameObject.Find("MapBtn").GetComponent<Button>();
            mapBtn.interactable = true;

            PlayerInformation.Exploration = 1;
            PlayerPrefs.SetInt("Exploration", PlayerInformation.Exploration);
            PlayerPrefs.Save();

            if (PlayerInformation.Engineer == "Female") { character.sprite = Resources.Load<Sprite>("Sprite/FemaleEngineer"); }
            else { character.sprite = Resources.Load<Sprite>("Sprite/MaleEngineer"); }

            string NPCreply = PlayerInformation.PalyerName + " you've aquired map!!!\nYou are now able to venture out into the world!!!";
            StartCoroutine(convoText(NPCreply, ""));
            setConvoStage(5);
            replybtn1.text = "ADEVNTURE TIME!!";
            replybtn2.text = "LET'S DO THIS";
            replybtn3.text = "FOR THE STRANGER!!";
            Debug.Log(PlayerInformation.EventConvo + "4");
        }
        else if (PlayerInformation.EventConvo == 5)
        {
            string NPCreply = "";
            StartCoroutine(convoText(NPCreply, ""));
            setConvoStage(6);

            if (playerReply == "Reply1" || playerReply == "Reply2" || playerReply == "Reply3")
            {
                eventCanvas.enabled = false;
            }
            Debug.Log(PlayerInformation.EventConvo + "5");
        }
    }
}

