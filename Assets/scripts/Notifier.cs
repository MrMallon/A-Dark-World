using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Notifier : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var notifyImg = GameObject.Find("Something new in Inventory").GetComponent<Image>();
        notifyImg.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void inventoryNotification(string identifier)
    {
        var notifyImg = GameObject.Find("Something new in Inventory").GetComponent<Image>();
        
        if (identifier == "inventory Btn") { notifyImg.enabled = false; }
        else if (identifier == "Crafted" && notifyImg.enabled == true) { notifyImg.enabled = true; }
        else if (identifier == "Crafted") { notifyImg.enabled = true; }
        else if (notifyImg.enabled == true) { Debug.Log("Disabled"); notifyImg.enabled = false; }
        else if (notifyImg.enabled == false) { Debug.Log("Enabled"); notifyImg.enabled = true; }
    }

    public void trapNotification(string meatLoot, string furLoot)
    {

        var trapedMeat = GameObject.Find("Check Trap Result Meat").GetComponent<Text>();
        var trapedFur = GameObject.Find("Check Trap Result Fur").GetComponent<Text>();

        trapedMeat.text = meatLoot;
        trapedFur.text = furLoot;
       
    }
}
