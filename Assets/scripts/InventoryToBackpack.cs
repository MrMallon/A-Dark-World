using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryToBackpack : MonoBehaviour
{
    Inventory inventoryComponent;
    Backpack backpackComponent;
    Image slot, slot1;
    Text slotText, slotText1;
    int mapUnlock;

    void Start()
    {
        inventoryComponent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        backpackComponent = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
    }

    void Update()
    {

    }

    public void moveItemToBackpack(int itemNo)
    {
        var itemToMove = GameObject.Find("ISlot " + itemNo).GetComponent<Image>();
        var inventory = inventoryComponent.inventory;
        var backpack = backpackComponent.backpack;
        bool foundBackpack = false;
        bool foundInventory = false;
        bool hasSword = false;
        bool hasArmour = false;

        Debug.Log(itemToMove.sprite.name);
        string itemName = itemToMove.sprite.name;
        int i = 0;
        int j = 0;
        Debug.Log(i);

        if (backpack.Count > 0)
        {
            while (!foundBackpack)
            {

                Debug.Log("I = " + i);
                Debug.Log("Backpack Loop");
                if (backpack[i].itemName.Contains("Armour") && itemName.Contains("Armour")) {
                    foundBackpack = true;
                    foundInventory = true;
                    i = 0;
                    j = 0;
                }
                else if (backpack[i].itemName.Contains("Sword") && itemName.Contains("Sword"))
                {
                    foundBackpack = true;
                    foundInventory = true;
                    i = 0;
                    j = 0;
                }
                else if (backpack[i].itemName == itemName)
                {
                        Debug.Log("FOUND ITEM i = " + i);
                        backpack[i].itemQuantity += 1;

                        for (j = 0; j < inventory.Count; j++)
                        {
                            if (inventory[j].itemName == itemName)
                                inventory[j].itemQuantity -= 1;
                        }

                        refreshInventoryPanel();
                        refreshBackpackPanel();
                        foundBackpack = true;
                        foundInventory = true;
                        i = 0;
                        j = 0;  
                }

                if (i < backpack.Count-1) i++;
                else
                {
                    Debug.Log("DID NOT FIND ITEM i = " + i + " j = " + j);
                    i = 0;
                    j = 0;
                    foundBackpack = true;
                }

            }


        }
        while (!foundInventory)
        {         
            if (inventory[j].itemName == itemName)
            {
                Debug.Log("Inventory Loop");                                                                    ////Item Quantity = 0
                backpack.Add(new Item(inventory[j].itemName, inventory[j].itemID, inventory[j].itemDesc, 0, inventory[j].itemPower, inventory[j].itemWeight, inventory[j].itemRequirement1, inventory[j].itemRequirement2, inventory[j].itemType));
                inventory[j].itemQuantity = inventory[j].itemQuantity - 1;
                backpack[backpack.Count - 1].itemQuantity += 1;
                refreshInventoryPanel();
                refreshBackpackPanel();
                foundInventory = true;
                j = 0;
            }
            else if (inventory.Count - 1 > j) j++;
            else
            {
                j = 0;
                foundInventory = true;
            }
        }

    }

    public void removeItemFromBackpack(int itemNo)
    {
        var itemToMove = GameObject.Find("Slot " + itemNo).GetComponent<Image>();
        var inventory = inventoryComponent.inventory;
        var backpack = backpackComponent.backpack;
        bool found = false;
        Debug.Log("Removing" + itemToMove.sprite.name);
        string itemName = itemToMove.sprite.name;
        int i = 0;

        while (!found)
        {
            if (inventory[i].itemName == itemName)
            {
                Debug.Log("i = " + i + " item NO = " + itemNo);
                Debug.Log(inventory[i].itemName + " = " + itemName);
                inventory[i].itemQuantity += 1;
                backpack[itemNo].itemQuantity -= 1;
                Debug.Log("Quantity " + backpack[itemNo].itemQuantity);
                refreshBackpackPanel();
                refreshInventoryPanel();
                if (backpack[itemNo].itemQuantity <= 0)
                {
                    Debug.Log("Removing " + backpack[itemNo].itemName + " Number " + itemNo);
                    backpack.Remove(backpack[itemNo]);
                    refreshBackpackPanel();
                }
                found = true;
                i = 0;
            }
            else if (inventory.Count - 1 != i) i++;
            else
            {
                i = 0;
                found = true;
            }
        }
    }

    public void refreshInventoryPanel()
    {
        var inventory = inventoryComponent.inventory;
        int itemTypecount = 0;
        for (int i = 0; i < inventory.Count; i++)
        {
            slotText = GameObject.Find("ISlot " + itemTypecount + " txt").GetComponent<Text>();
            slotText.text = "";
            slot = GameObject.Find("ISlot " + itemTypecount).GetComponent<Image>();
            slot.sprite = Resources.Load<Sprite>("Item Icons/UIMask");

            if (inventory[i].itemType != Item.Itemtype.normal && inventory[i].itemQuantity > 0 && inventory[i].itemName != "Set Trap")
            {
                slotText.text = inventory[i].itemQuantity.ToString();
                slot.sprite = Resources.Load<Sprite>("Item Icons/" + inventory[i].itemName);
                itemTypecount++;
            }
        }
    }

    public void refreshBackpackPanel()
    {
        var backpack = backpackComponent.backpack;
        int itemTypecount = 0;
        Debug.Log("REFRESHING");
        if (backpack.Count > 0)
        {
            for (int i = 0; i < backpack.Count; i++)
            {
                int nextSlot = backpack.Count;
                slotText1 = GameObject.Find("Slot " + nextSlot + " txt").GetComponent<Text>();
                slotText1.text = "";
                slot1 = GameObject.Find("Slot " + nextSlot).GetComponent<Image>();
                slot1.sprite = Resources.Load<Sprite>("Item Icons/UIMask");

                Debug.Log("Backpack");
                slotText = GameObject.Find("Slot " + itemTypecount + " txt").GetComponent<Text>();
                slot = GameObject.Find("Slot " + itemTypecount).GetComponent<Image>();

                slotText.text = "";
                slot.sprite = Resources.Load<Sprite>("Item Icons/UIMask");
                if (backpack[i].itemType != Item.Itemtype.normal && backpack[i].itemQuantity > 0)
                {
                    slotText.text = backpack[i].itemQuantity.ToString();
                    slot.sprite = Resources.Load<Sprite>("Item Icons/" + backpack[i].itemName);
                    itemTypecount++;
                }
            }
        }
        //call method to activate map if required items are in backpack
        if (Application.loadedLevelName == "Camp") { activateMap(); }
    }

    private void activateMap()
    {
        var backpack = backpackComponent.backpack;
        var mapBtn = GameObject.Find("MapBtn").GetComponent<Button>();
        Debug.Log(mapBtn.name);
        mapUnlock = 0;
        for (int i = 0; i < backpack.Count; i++)
        {
            if (backpack[i].itemName.Contains("Sword") || backpack[i].itemName.Contains("Armour") || backpack[i].itemName.Contains("Cured"))
            {
                setMapUnlock();
                Debug.Log("map Unlock = " + mapUnlock);
            }
            if (mapUnlock >= 3)
            {
                mapBtn.interactable = true;
            }
            else
                mapBtn.interactable = false;
        }
    }

    private void setMapUnlock()
    {
        mapUnlock++;
    }
}
