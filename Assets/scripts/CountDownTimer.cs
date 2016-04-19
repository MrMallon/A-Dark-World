using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{

    private bool triggerCounter = false;
    private bool stopTimer = false;
    private string counterType;
    float timeRemaining = 0;
    double increaseWood = 1.00;
    private Canvas inventoryCanvas;
    private Canvas craftCanvas;
    // Use this for initialization
    void Start()
    {
        inventoryCanvas = GameObject.Find("Camp Canvas").GetComponent<Canvas>();
        craftCanvas = GameObject.Find("Camp Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerCounter == true)
        {
            var inventoryComponent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
            var items = inventoryComponent.inventory;
            var inputFieldtext = GameObject.Find("Wood Input Text").GetComponent<Text>();
            if (counterType == "gatherWood")
            {
                var woodbtn = GameObject.Find("Gather Wood Btn").GetComponent<Button>();
                var woodImg = GameObject.Find("Gather Wood Btn").GetComponent<Image>();
                var uiCounter = GameObject.Find("GatherCount").GetComponent<Text>();
                var inputField = GameObject.Find("WoodInputField").GetComponent<InputField>();

                if (inputFieldtext.text == "" || inputFieldtext.text == " " || inputFieldtext.text == null) { PlayerInformation.AssignedGatherWood = 1; }
                else if(int.Parse(inputFieldtext.text) > PlayerInformation.PeopleInCamp) { PlayerInformation.AssignedGatherWood = PlayerInformation.PeopleInCamp; }
                else { PlayerInformation.AssignedGatherWood = int.Parse(inputFieldtext.text); }
                if(PlayerInformation.PeopleInCamp == 0) { PlayerInformation.AssignedGatherWood = 1; }

              //  Debug.Log("HHHHEEEEEEEEEEEEERRRRRRRRREEEEEE" + PlayerInformation.AssignedGatherWood);

                inputField.enabled = false;
                inputField.interactable = false;
                inputField.image.enabled = false;
                inputFieldtext.enabled = false;
                woodbtn.interactable = false;

                timeRemaining += Time.deltaTime;
                woodImg.fillAmount = (timeRemaining / 3) / 10f;

                if (craftCanvas.enabled == true) { craftCanvas.GetComponent<Load>().loadCraftText(); }
                if (inventoryCanvas.enabled == true) { craftCanvas.GetComponent<Load>().loadInventoryText(); }

                if (timeRemaining >= increaseWood)
                {
                    Debug.Log(timeRemaining);
                    items[1].itemQuantity += PlayerInformation.AssignedGatherWood;
                    uiCounter.text = items[1].itemQuantity.ToString();
                    increaseWood += 1.00;
                }
                else if (timeRemaining >= 30)
                {
                    Debug.Log("if statment" + " Wood");
                    uiCounter.text = "";
                    woodbtn.interactable = true;
                    setTrigger(stopTimer);
                    setCounterType("");
                    timeRemaining = 0;
                    increaseWood = 2.0;
                }
            } 
        }
    }

    public void setTrigger(bool trig)
    {
        Debug.Log("SETTER BOOL");
        triggerCounter = trig;
    }

    public void setCounterType(string type)
    {
        Debug.Log("SETTER string trap");
        counterType = type;
    }
}
