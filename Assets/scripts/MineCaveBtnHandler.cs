using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MineCaveBtnHandler : MonoBehaviour {

    Canvas canvas;
    Canvas[] canvasArray;
    Backpack backpackComponent;
    Image slot, slot1;
    Text slotText, slotText1;
    // Use this for initialization
    void Start () {
        backpackComponent = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void populateCanvasList()
    {
        canvasArray = new Canvas[1];
        canvasArray[0] = GameObject.Find("Backpack Canvas").GetComponent<Canvas>();
    }

    public void showCanvas()
    {
        canvas = GetComponent<Canvas>();
        populateCanvasList();

        if (canvas.enabled == false)
        {
            Debug.Log("btn pressed");

            for (int i = 0; i < canvasArray.Length; i++)
            {
                if (canvas == canvasArray[i])
                {
                    canvasArray[i].enabled = true;
                }
                else
                    canvasArray[i].enabled = false;

            }
        }
        else if (canvas.enabled == true)
        {
            Debug.Log("btn not pressed");
            canvas.enabled = false;
        }
    }

    public void backpackBtnActions(int slot)
    {
        var refreshBackpack = GameObject.Find("Backpack Canvas").GetComponent<InventoryToBackpack>();
        var buttonPressed = GameObject.Find("Slot " + slot).GetComponent<Image>();
        var player = GameObject.Find("Player").GetComponent<PlayerStats>();
        var backpack = backpackComponent.backpack;
        string itemName = buttonPressed.sprite.name;

        if (itemName == "Cured Meat")
        {
            player.HealPlayer(5);
            backpack[slot].itemQuantity -= 1;
        }
        else if (itemName == "Medicine")
        {
            player.HealPlayer(10);
            backpack[slot].itemQuantity -= 1; 
        }
        if (backpack[slot].itemQuantity <= 0) { backpack.Remove(backpack[slot]); }
        refreshBackpack.refreshBackpackPanel();
    }
}
