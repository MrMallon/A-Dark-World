using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimateText : MonoBehaviour
{
    private string greeting = "Hey there ... are you okay? ...";
    private int conversationStage = 0;
    private int futureConvoState = 1;
    private string str;
    private float textSpeed = 0.09f;
    private bool fireLite = false;
    int counter = 0;

    public AnimateText() { }

    void Start()
    {
        var inputField = GameObject.Find("InputField").GetComponent<InputField>();
        var submitBtn = GameObject.Find("submit btn").GetComponent<Button>();
        var submitTxt = GameObject.Find("Submit Text").GetComponent<Text>();

        inputField.enabled = false;
        inputField.image.enabled = false;
        submitBtn.enabled = false;
        submitBtn.image.enabled = false;
        submitTxt.text = "";

        StartCoroutine(animateText(greeting, ""));
    }

    void Update()
    {
        var fireOBJ = GameObject.Find("Tut FireParticle").GetComponent<ParticleSystem>();
        var fireBtn = GameObject.Find("Tut fire button").GetComponent<Button>();
        var fireLight = GameObject.Find("Tut fire light").GetComponent<Light>();
        if (conversationStage >= 4)
        {
            fireBtn.interactable = true;
            if (fireOBJ.startSize > 0 && fireLite == false)
            {
                string NPCreply = "Good Job " + PlayerInformation.PalyerName + "!!! Make sure you don't let the fire go out. When you are at the camp you can gather wood and add it to the fire by tapping it.";
                StartCoroutine(animateText(NPCreply, ""));
                setFireLit(true);
                fireLight.range = 80;
            }
        }

    }

    public IEnumerator animateText(string strComplete, string buttonPressed)
    {
        var replybtn1 = GameObject.Find("Reply 1").GetComponent<Button>();
        var replybtn2 = GameObject.Find("Reply 2").GetComponent<Button>();
        var replybtn3 = GameObject.Find("Reply 3").GetComponent<Button>();
        var submitBtn = GameObject.Find("submit btn").GetComponent<Button>();
        var fireBtn = GameObject.Find("Tut fire button").GetComponent<Button>();

        replybtn1.interactable = false;
        replybtn2.interactable = false;
        replybtn3.interactable = false;
        submitBtn.interactable = false;
        fireBtn.interactable = false;

        int i = 0;
        str = "";
        while (i < strComplete.Length)
        {
         //   Debug.Log("Animateing Text");
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

        if (futureConvoState == 4)
        {
            replybtn1.interactable = false;
            replybtn2.interactable = false;
            replybtn3.interactable = false;
            fireBtn.interactable = true;
        }
        if (futureConvoState == 5 || futureConvoState == 6)
        {
            replybtn1.interactable = true;
            replybtn2.interactable = true;
            replybtn3.interactable = true;
        }
        submitBtn.interactable = true;

    }

    void OnGUI()
    {
        var convoBox = GameObject.Find("ConversationText").GetComponent<Text>();
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
        conversationStage = nextStage;
    }

    public void tutorialConversation(string playerReply)
    {
        var replybtn1 = GameObject.Find("Reply 1 text").GetComponent<Text>();
        var replybtn2 = GameObject.Find("Reply 2 text").GetComponent<Text>();
        var replybtn3 = GameObject.Find("Reply 3 text").GetComponent<Text>();

        var btn1 = GameObject.Find("Reply 1").GetComponent<Image>();
        var btn3 = GameObject.Find("Reply 3").GetComponent<Image>();

        var btnOne = GameObject.Find("Reply 1").GetComponent<Button>();
        var btnTwo = GameObject.Find("Reply 2").GetComponent<Button>();
        var btnThree = GameObject.Find("Reply 3").GetComponent<Button>();

        var engineer = GameObject.Find("Engineer").GetComponent<Image>();
        setTextSpeed(0.08f);

        if (conversationStage == 0)
        {

            if (playerReply == "Reply1")
            {
                counter++;
            //    Debug.Log("String Matched");
                string NPCreply = "I was an engineer before the world descended into chaos... I'm not quite sure who I am now... Do you like colours?";
                StartCoroutine(animateText(NPCreply, playerReply));
            }
            if (playerReply == "Reply2")
            {
                counter++;
                string NPCreply = "Not sure really, its always dark or really dull outside...... Do you like colours?";
                StartCoroutine(animateText(NPCreply, playerReply));
            }
            if (playerReply == "Reply3")
            {
                counter++;
                string NPCreply = "Not really sure to be honest I just found you here... Do you like colours?";
                StartCoroutine(animateText(NPCreply, playerReply));
            }
            replybtn1.text = "Yes";
            replybtn2.text = "No";
            replybtn3.text = "What?";

            setConvoStage(1);
            counter = 0;
        }

        else if (conversationStage == 1)
        {
            btn1.color = Color.red;
            btn3.color = Color.blue;
            if (playerReply == "Reply1")
            {
              //  Debug.Log("String Matched");
                string NPCreply = "ME TOO!!!! Which do you prefer, Red, Green, or Blue?";
                StartCoroutine(animateText(NPCreply, playerReply));
            }
            if (playerReply == "Reply2")
            {
                string NPCreply = "Well you're no fun.... well even so which Colour do you prefer, Red, Green or Blue?";
                StartCoroutine(animateText(NPCreply, playerReply));
            }
            if (playerReply == "Reply3")
            {
                string NPCreply = "You know ... colours.... the property possessed by an object of producing different sensations on the eye as a result of the way it reflects or emits light? ..... anyway.... Red, Green or Blue? ";
                StartCoroutine(animateText(NPCreply, playerReply));
            }
            replybtn1.text = "Red";
            replybtn2.text = "Green";
            replybtn3.text = "Blue";

            setConvoStage(2);
        }
        else if (conversationStage == 2)
        {
            int number = Random.Range(1, 5);

            if (playerReply == "Reply1")
            {
                PlayerInformation.PalyerColour = "Red";
                PlayerInformation.Engineer = "Female";
                string NPCreply = "Aaaaaahhh " + PlayerInformation.PalyerColour + ", the color of blood and fire, love and passion, strength and leadership, courage and danger, anger and DETERMINATION!!! .... sorry I get carried away sometimes .... what is you name anyway?";
                StartCoroutine(animateText(NPCreply, playerReply));
            }
            if (playerReply == "Reply2")
            {
                PlayerInformation.PalyerColour = "Green";
                string NPCreply = "Aaaaaahhh " + PlayerInformation.PalyerColour + ", the color of life, renewal, nature, and energy, BUT can also be associated with money, finances, banking, ambition, greed and JEALOUSY!!!!!.... sorry I get carried away sometimes .... what is you name anyway?";
                StartCoroutine(animateText(NPCreply, playerReply));
                if (number >= 0 && number <= 2) { PlayerInformation.Engineer = "Female"; }
                if (number >= 3 && number < 5) { PlayerInformation.Engineer = "Male"; }
            }
            if (playerReply == "Reply3")
            {
                PlayerInformation.PalyerColour = "Blue";
                PlayerInformation.Engineer = "Male";
                string NPCreply = "Aaaaaahhh " + PlayerInformation.PalyerColour + ", the color of the sky and sea..... well before the whole world decending into chaos thing..... BLUE!! symbolizes trust, loyalty, wisdom, confidence, intelligence, truth, and STABILITY!!!!.... sorry I get carried away sometimes .... what is you name anyway?";
                StartCoroutine(animateText(NPCreply, playerReply));
            }

            PlayerPrefs.SetString("Engineer", PlayerInformation.Engineer);
            PlayerPrefs.SetString("PalyerColour", PlayerInformation.PalyerColour);

            replybtn1.text = "";
            replybtn2.text = "";
            replybtn3.text = "";

            var inputField = GameObject.Find("InputField").GetComponent<InputField>();
            var submitBtn = GameObject.Find("submit btn").GetComponent<Button>();
            var submitTxt = GameObject.Find("Submit Text").GetComponent<Text>();

            inputField.enabled = true;
            inputField.image.enabled = true;
            submitBtn.enabled = true;
            submitBtn.image.enabled = true;
            submitTxt.text = "Submit";
            setConvoStage(3);
        }

        else if (conversationStage == 3)
        {
            setConvoStage(4);
            enteredName();
            var inputField = GameObject.Find("InputField").GetComponent<InputField>();
            var inputFieldtext = GameObject.Find("Input Text").GetComponent<Text>();
            var submitBtn = GameObject.Find("submit btn").GetComponent<Button>();
            var submitTxt = GameObject.Find("Submit Text").GetComponent<Text>();

            inputField.enabled = false;
            inputField.image.enabled = false;
            submitBtn.enabled = false;
            submitBtn.image.enabled = false;
            submitTxt.text = "";
            inputFieldtext.text = "";

            string NPCreply = PlayerInformation.PalyerName + " , I used to know someone by that name...... will you do me a favour and light a fire?.... Just tap the center of the screen a few times and one should start.";
            StartCoroutine(animateText(NPCreply, playerReply));
            replybtn1.text = "What is your name ?";
            replybtn2.text = "What happend to the world ?";
            replybtn3.text = "Why is it dark all the time ?";
        }

        else if (conversationStage == 4)
        {
            var fireOBJ = GameObject.Find("Tut FireParticle").GetComponent<ParticleSystem>();
            if (fireOBJ.startSize <= 0)
            {
                string NPCreply = "Can you light the fire first it's getting cold. Just tap the center of the screen a few times...";
                StartCoroutine(animateText(NPCreply, playerReply));

            }
            else
            {
                if (counter < 3)
                {
                    if (playerReply == "Reply1")
                    {
                        counter++;
                        string NPCreply;
                      //  Debug.Log(PlayerInformation.Engineer);
                        if (PlayerInformation.Engineer == "Female")
                        {
                            Debug.Log("Female if statment");
                            engineer.sprite = Resources.Load<Sprite>("Sprite/FemaleEngineer");
                            NPCreply = "People used to call me Dodger.";
                            StartCoroutine(animateText(NPCreply, playerReply));
                        }
                        else
                        {
                            engineer.sprite = Resources.Load<Sprite>("Sprite/MaleEngineer");
                            NPCreply = "People used to call me Dodger.";
                            StartCoroutine(animateText(NPCreply, playerReply));
                        }
                        replybtn1.text = "";
                        btnOne.interactable = false;
                    }
                    if (playerReply == "Reply2")
                    {
                        counter++;
                        string NPCreply = "Nuclear warfare, The people in power deployed their weaponry and so did everyone else sending us back to the dark ages.";
                        StartCoroutine(animateText(NPCreply, playerReply));
                        btnTwo.interactable = false;
                        replybtn2.text = "";
                    }
                    if (playerReply == "Reply3")
                    {
                        counter++;
                        string NPCreply = "Ever since the first bomb went off the sky has been covered in a dark chemical cloud leaving the world shrouded in darkness. Days and night have become one, forever in eternal darkness...";
                        StartCoroutine(animateText(NPCreply, playerReply));
                        btnThree.interactable = false;
                        replybtn3.text = "";
                    }
                    if (counter == 3)
                    {
                        replybtn1.text = "Cool";
                        replybtn2.text = "Okay";
                        replybtn3.text = "Alrighty Then";
                    }
                    setConvoStage(4);
                }
                else if (counter == 3)
                {
                    counter++;
                    string NPCreply = "People are going to approach you over time seeking help. You can turn them away or give them shelter but they can be useful.";
                    StartCoroutine(animateText(NPCreply, playerReply));
                }
                else if (counter == 4)
                {
                    string NPCreply = "You will need to build huts in order for them to stay with you. Huts require a lot of wood so best to start gathering. Are you ready to go?";
                    StartCoroutine(animateText(NPCreply, playerReply));

                    replybtn1.text = "Yes";
                    replybtn2.text = "No";
                    replybtn3.text = "...";

                    setConvoStage(5);
                }
            }

        }
        else if (conversationStage == 5)
        {
            if (playerReply == "Reply1")
            {
                var fireOBJ = GameObject.Find("Tut FireParticle").GetComponent<ParticleSystem>();
                var fireLight = GameObject.Find("Tut fire light").GetComponent<Light>();

                PlayerInformation.FireSize = fireOBJ.startSize;
                PlayerInformation.FireLight = fireLight.range;
                PlayerPrefs.SetFloat("FireSize", PlayerInformation.FireSize);
                PlayerPrefs.SetFloat("FireLight", PlayerInformation.FireLight);

                PlayerInformation.Tutorial = 1;
                PlayerPrefs.SetInt("Tutorial", PlayerInformation.Tutorial);
                int PlayedTutBefore = 1;
                PlayerPrefs.SetInt("PlayedBeforeTut", (PlayedTutBefore));
                PlayerPrefs.Save();
                SceneManager.LoadSceneAsync("Camp");
            }
            if (playerReply == "Reply2")
            {
                string NPCreply = "Okay we can wait here for a bit..";
                StartCoroutine(animateText(NPCreply, playerReply));
            }
            if (playerReply == "Reply3")
            {
                string NPCreply = "Ready to go?";
                StartCoroutine(animateText(NPCreply, playerReply));
            }
            setConvoStage(5);
        }
    }

    public void enteredName()
    {
        var inputField = GameObject.Find("InputField").GetComponent<InputField>();
        var playerName = new InputField.SubmitEvent();
        PlayerInformation.PalyerName = inputField.text;
        PlayerPrefs.SetString("PalyerName", PlayerInformation.PalyerName);
    }
}
