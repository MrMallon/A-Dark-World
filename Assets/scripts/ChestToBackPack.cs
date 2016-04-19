using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChestToBackPack : MonoBehaviour {

    Inventory inventoryComponent;
    Backpack backpackComponent;
    ItemDatabase database;
    Chest chestComponent;
    Canvas chestCanvas;
    Button backpackBtn;

    // Use this for initialization
    void Start () {
        backpackComponent = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
        chestComponent = GameObject.FindGameObjectWithTag("Chest").GetComponent<Chest>();
        inventoryComponent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        chestCanvas = GameObject.Find("Chest Canvas").GetComponent<Canvas>();
        backpackBtn = GameObject.Find("Bakpack Btn").GetComponent<Button>();
        populateChest();
    }

    public void moveItemToBackpack(int itemNo)
    {
        var itemToMove = GameObject.Find("ISlot " + itemNo).GetComponent<Image>();
        //var inventory = inventoryComponent.inventory;
        var chest = chestComponent.chest;
        var backpack = backpackComponent.backpack;
        bool foundBackpack = false;
        bool foundChest = false;
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
                if (backpack[i].itemName == itemName)
                {
                    Debug.Log("FOUND ITEM i = " + i);
                    backpack[i].itemQuantity += 1;

                    for (j = 0; j < chest.Count; j++)
                    {
                        if (chest[j].itemName == itemName)
                            chest[j].itemQuantity -= 1;
                    }
                    if (chest[itemNo].itemQuantity <= 0)
                    {
                        chest.Remove(chest[itemNo]);
                        refreshChestPanel();
                    }
                    refreshChestPanel();
                    refreshChestBackpackPanel();
                    foundBackpack = true;
                    foundChest = true;
                    i = 0;
                    j = 0;
                }

                if (i < backpack.Count - 1) i++;
                else
                {
                    Debug.Log("DID NOT FIND ITEM i = " + i + " j = " + j);
                    i = 0;
                    j = 0;
                    foundBackpack = true;
                }
            }
        }
        if (chest.Count > 0)
        {
            while (!foundChest)
            {
                if (chest[j].itemName == itemName)
                {
                    Debug.Log("Inventory Loop");                                                                    ////Item Quantity = 0
                    backpack.Add(new Item(chest[j].itemName, chest[j].itemID, chest[j].itemDesc, 0, chest[j].itemPower, chest[j].itemWeight, chest[j].itemRequirement1, chest[j].itemRequirement2, chest[j].itemType));
                    chest[j].itemQuantity = chest[j].itemQuantity - 1;
                    backpack[backpack.Count - 1].itemQuantity += 1;

                    if (chest[itemNo].itemQuantity <= 0)
                    {
                        chest.Remove(chest[itemNo]);
                        refreshChestPanel();
                    }

                    refreshChestPanel();
                    refreshChestBackpackPanel();
                    foundChest = true;
                    j = 0;
                }
                else if (chest.Count - 1 > j) j++;
                else
                {
                    j = 0;
                    foundChest = true;
                }
            }
        }
    }

    public void removeItemFromBackpack(int itemNo)
    {
        var itemToMove = GameObject.Find("BSlot " + itemNo).GetComponent<Image>();
        var chest = chestComponent.chest;
        var backpack = backpackComponent.backpack;
        bool found = false;
        Debug.Log("Removing" + itemToMove.sprite.name);
        string itemName = itemToMove.sprite.name;
        int i = 0;
        if (chest.Count > 0)
        {
            while (!found)
            {
                if (chest[i].itemName == itemName)
                {
                    Debug.Log("i = " + i + " item NO = " + itemNo);
                    Debug.Log(chest[i].itemName + " = " + itemName);
                    chest[i].itemQuantity += 1;
                    backpack[itemNo].itemQuantity -= 1;
                    Debug.Log("Quantity " + backpack[itemNo].itemQuantity);
                    refreshChestBackpackPanel();
                    refreshChestPanel();
                    if (backpack[itemNo].itemQuantity <= 0)
                    {
                        Debug.Log("Removing " + backpack[itemNo].itemName + " Number " + itemNo);
                        backpack.Remove(backpack[itemNo]);
                        refreshChestBackpackPanel();
                    }
                    found = true;
                    i = 0;
                }
                else if (chest.Count - 1 != i) i++;
                else
                {
                    chest.Add(new Item(backpack[itemNo].itemName, backpack[itemNo].itemID, backpack[itemNo].itemDesc, 0, backpack[itemNo].itemPower, backpack[itemNo].itemWeight, backpack[itemNo].itemRequirement1, backpack[itemNo].itemRequirement2, backpack[itemNo].itemType));
                    refreshChestBackpackPanel();
                    refreshChestPanel();
                    i = 0;
                    found = true;
                }
            }
        }
        else
        {
            chest.Add(new Item(backpack[itemNo].itemName, backpack[itemNo].itemID, backpack[itemNo].itemDesc, 0, backpack[itemNo].itemPower, backpack[itemNo].itemWeight, backpack[itemNo].itemRequirement1, backpack[itemNo].itemRequirement2, backpack[itemNo].itemType));
            refreshChestBackpackPanel();
            refreshChestPanel();
        }
    }

    public void refreshChestPanel()
    {
        var chest = chestComponent.chest;
        int itemTypecount1 = 0;

        if (chest.Count > 0)
        {
            for (int i = 0; i < chest.Count; i++)
            {
                int nextSlot = chest.Count;
                var slotText1 = GameObject.Find("ISlot " + nextSlot + " txt").GetComponent<Text>();
                slotText1.text = "";
                var slot1 = GameObject.Find("ISlot " + nextSlot).GetComponent<Image>();
                slot1.sprite = Resources.Load<Sprite>("Item Icons/UIMask");

                var slotText = GameObject.Find("ISlot " + itemTypecount1 + " txt").GetComponent<Text>();
                var slot = GameObject.Find("ISlot " + itemTypecount1).GetComponent<Image>();

                slotText.text = "";
                slot.sprite = Resources.Load<Sprite>("Item Icons/UIMask");
                if (chest[i].itemQuantity > 0)
                {
                    slotText.text = chest[i].itemQuantity.ToString();
                    slot.sprite = Resources.Load<Sprite>("Item Icons/" + chest[i].itemName);
                    itemTypecount1++;
                }
            }
        }
        else
        {
            var slotText1 = GameObject.Find("ISlot " + 0 + " txt").GetComponent<Text>();
            slotText1.text = "";
            var slot1 = GameObject.Find("ISlot " + 0).GetComponent<Image>();
            slot1.sprite = Resources.Load<Sprite>("Item Icons/UIMask");
        }
    }

    public void refreshChestBackpackPanel()
    {
        var backpack = backpackComponent.backpack;
        int itemTypecount = 0;
        Debug.Log("BACKPACK COUNT = " + backpack.Count);
        if (backpack.Count > 0)
        {
            for (int i = 0; i < backpack.Count; i++)
            {
                int nextSlot = backpack.Count;
                var slotText1 = GameObject.Find("BSlot " + nextSlot + " txt").GetComponent<Text>();
                slotText1.text = "";
                var slot1 = GameObject.Find("BSlot " + nextSlot).GetComponent<Image>();
                slot1.sprite = Resources.Load<Sprite>("Item Icons/UIMask");

                var slotText = GameObject.Find("BSlot " + itemTypecount + " txt").GetComponent<Text>();
                var slot = GameObject.Find("BSlot " + itemTypecount).GetComponent<Image>();

                slotText.text = "";
                slot.sprite = Resources.Load<Sprite>("Item Icons/UIMask");
                if (backpack[i].itemQuantity > 0) { 
                
                    slotText.text = backpack[i].itemQuantity.ToString();
                    slot.sprite = Resources.Load<Sprite>("Item Icons/" + backpack[i].itemName);
                    itemTypecount++;
                }
            }
        }
    }
    
    public void hideChestCanvas()
    {
        chestCanvas.enabled = false;
        backpackBtn.enabled = true;
        var saveBackpack = GameObject.Find("Chest Canvas").GetComponent<ChestToBackPack>();
        saveBackpack.saveBackpack();
    }

    private void populateChest()
    {
        var chest = chestComponent.chest;
        var databaseItems = database.items;
        if (PlayerPrefs.GetInt("LevelPiacked") == 2)
        {
            chest.Add(new Item(databaseItems[16].itemName, databaseItems[16].itemID, databaseItems[16].itemDesc, 0, databaseItems[16].itemPower, databaseItems[16].itemWeight, databaseItems[16].itemRequirement1, databaseItems[16].itemRequirement2, databaseItems[16].itemType));
            chest.Add(new Item(databaseItems[12].itemName, databaseItems[12].itemID, databaseItems[12].itemDesc, 0, databaseItems[12].itemPower, databaseItems[12].itemWeight, databaseItems[12].itemRequirement1, databaseItems[12].itemRequirement2, databaseItems[12].itemType));
            chest.Add(new Item(databaseItems[2].itemName, databaseItems[2].itemID, databaseItems[2].itemDesc, 0, databaseItems[2].itemPower, databaseItems[2].itemWeight, databaseItems[2].itemRequirement1, databaseItems[2].itemRequirement2, databaseItems[2].itemType));
            chest.Add(new Item(databaseItems[15].itemName, databaseItems[15].itemID, databaseItems[15].itemDesc, 0, databaseItems[15].itemPower, databaseItems[15].itemWeight, databaseItems[15].itemRequirement1, databaseItems[15].itemRequirement2, databaseItems[15].itemType));
        }
        else
        {
            chest.Add(new Item(databaseItems[16].itemName, databaseItems[16].itemID, databaseItems[16].itemDesc, 0, databaseItems[16].itemPower, databaseItems[16].itemWeight, databaseItems[16].itemRequirement1, databaseItems[16].itemRequirement2, databaseItems[16].itemType));
            chest.Add(new Item(databaseItems[13].itemName, databaseItems[13].itemID, databaseItems[13].itemDesc, 0, databaseItems[13].itemPower, databaseItems[13].itemWeight, databaseItems[13].itemRequirement1, databaseItems[13].itemRequirement2, databaseItems[13].itemType));
            chest.Add(new Item(databaseItems[2].itemName, databaseItems[2].itemID, databaseItems[2].itemDesc, 0, databaseItems[2].itemPower, databaseItems[2].itemWeight, databaseItems[2].itemRequirement1, databaseItems[2].itemRequirement2, databaseItems[2].itemType));
            chest.Add(new Item(databaseItems[15].itemName, databaseItems[15].itemID, databaseItems[15].itemDesc, 0, databaseItems[15].itemPower, databaseItems[15].itemWeight, databaseItems[15].itemRequirement1, databaseItems[15].itemRequirement2, databaseItems[15].itemType));
        }
        chest[0].itemQuantity = 4;
        chest[1].itemQuantity = 1;
        chest[2].itemQuantity = 2;
        chest[3].itemQuantity = 6;
    }

    public void emptyBackpack()
    {
        var backpack = backpackComponent.backpack;

        if(backpack.Count > 0)
        {
            for(int i = 0; i < backpack.Count+1; i++)
            {
                backpack.Remove(backpack[i]);
                PlayerPrefs.DeleteKey("ItemQuantity" + i);
                PlayerPrefs.DeleteKey("ItemName" + i);
            }
        }
        PlayerPrefs.SetInt("Backpack Count", 0);
    }

    public void saveBackpack()
    {
        // Debug.Log("SAVING BACKPACK");
        var backpackDb = GameObject.Find("Backpack").GetComponent<Backpack>();
        Debug.Log("SAVING BACKPACK");
        var backpack = backpackDb.backpack;
        PlayerPrefs.SetInt("Backpack Count", backpack.Count);
        if (backpack.Count > 0)
        {
            for (int i = 0; i < backpack.Count; i++)
            {
                //   Debug.Log("SAVEING");
                PlayerPrefs.SetInt("ItemQuantity" + i, backpack[i].itemQuantity);
                PlayerPrefs.SetString("ItemName" + i, backpack[i].itemName);
            }
        }
    }
}
