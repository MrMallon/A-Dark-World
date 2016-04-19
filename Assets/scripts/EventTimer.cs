using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventTimer : MonoBehaviour
{
    private bool mapEvent = false;
    private float nextEvent = 0;
    private float timer = 0;
    private float foodTimer = 0;
    private Inventory inventoryComponent;
    private Text populationText;
    private Canvas eventCanvas;

    void Start()
    {
        eventCanvas = GameObject.Find("Event Canvas").GetComponent<Canvas>();
        inventoryComponent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        populationText = GameObject.Find("Population Count").GetComponent<Text>();
        nextEvent = Random.Range(2, 4);
        nextEvent = nextEvent * 60f;
        //nextEvent = 2;
    }

    void Update()
    {

        var items = inventoryComponent.inventory;
        int campCapacity = items[3].itemQuantity * 4;
       // Debug.Log(PlayerInformation.PeopleInCamp);

        if (PlayerInformation.PeopleInCamp > 0)
        {
            foodTimer += Time.deltaTime;
            Debug.Log("FOOD TIMER   " + foodTimer);
            if (foodTimer >= 60f)
            {
                
                //cured meat
                if (items[5].itemQuantity > 0)
                {
                    //hut amount
                    if (items[3].itemQuantity > 0)
                    {
                        int foodEaten = PlayerInformation.PeopleInCamp / 4;
                        items[5].itemQuantity = items[5].itemQuantity - foodEaten;

                        if (items[5].itemQuantity < 0) { items[5].itemQuantity = 0; }

                        var foodTextField = GameObject.Find("Food Count").GetComponent<Text>();
                        foodTextField.text = items[5].itemQuantity.ToString();
                        foodTimer = 0;
                    }
                }
                else if (foodTimer >= 70f && PlayerInformation.PeopleInCamp > 0)
                {
                    var populationTextField = GameObject.Find("Population Count").GetComponent<Text>();
                    int populationCount;
                    PlayerInformation.PeopleInCamp -= 1;

                    populationCount = items[3].itemQuantity * 4;
                    populationTextField.text = PlayerInformation.PeopleInCamp + "/" + populationCount.ToString();

                    foodTimer = 0;
                }
            }
        }

        if (PlayerInformation.PeopleInCamp < campCapacity)
        {
            timer += Time.deltaTime;

            if (timer >= nextEvent)
            {
                PlayerInformation.PeopleInCamp += 1;
                populationText.text = PlayerInformation.PeopleInCamp + "/" + campCapacity;
                PlayerPrefs.SetInt("PeopleInCamp", PlayerInformation.PeopleInCamp);
                PlayerPrefs.Save();
                timer = 0f;
                nextEvent = Random.Range(2, 4);
                nextEvent = nextEvent * 60f;
            }
        }

        if (items[3].itemQuantity > 0 && items[14].itemQuantity > 0 && items[18].itemQuantity > 0)
        {
            setMapEvent(true);
        }
        if (mapEvent == true && PlayerInformation.EventConvo < 2)
        {
            eventCanvas.enabled = true;
            PlayerInformation.EventConvo = 2;
            eventCanvas.GetComponent<Conversation>().eventConvo("Reply1");
            mapEvent = false;
        }
    }

    public void setMapEvent(bool trig)
    {
        mapEvent = trig;
    }
}
