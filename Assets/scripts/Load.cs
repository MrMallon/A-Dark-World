using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Load : MonoBehaviour
{
    private string presentpath;

    int PlayedBefore;

    void Start()
    {
        var save = GameObject.Find("Camp Canvas").GetComponent<Save>();
        var setInteractiveState = GameObject.Find("Camp Canvas").GetComponent<ButtonHandler>();
        PlayedBefore = PlayerPrefs.GetInt("PlayedBeforeSave");
        var inventoryComponent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var items = inventoryComponent.inventory;
        var hutTextField = GameObject.Find("Hut Count").GetComponent<Text>();
        var populationTextField = GameObject.Find("Population Count").GetComponent<Text>();
        var foodTextField = GameObject.Find("Food Count").GetComponent<Text>();
        int populationCount;
        var inputField = GameObject.Find("WoodInputField").GetComponent<InputField>();

        //Checks if this is the first time you play
        //PlayedBefore = 0;
        if (PlayedBefore == 0)
        {
            //clearData();
            PlayerPrefs.Save();
            PlayedBefore = 4;
            int startLevel = 1;
            PlayerPrefs.SetInt("CurrentLevel", startLevel);
            PlayerPrefs.SetInt("PlayedBeforeSave", (PlayedBefore));
            PlayerPrefs.Save();
            save.createData();
            loadInventory();
            Debug.Log("PlayedBefore = " + PlayedBefore);
            loadPlayerInfo();
            loadBackpack();
        }
        else if (PlayedBefore == 1)
        {
            PlayedBefore = 4;
            int startLevel = 1;
            PlayerPrefs.SetInt("CurrentLevel", startLevel);
            PlayerPrefs.SetInt("PlayedBeforeSave", (PlayedBefore));
            PlayerPrefs.Save();
            loadData();
            save.saveInventory();
            save.savePlayerInfo();
            loadInventory();
            loadPlayerInfo();
            loadBackpack();
        }
        else if (PlayedBefore == 2)
        {
            PlayedBefore = 4;
            int startLevel = 1;
            PlayerPrefs.SetInt("CurrentLevel", startLevel);
            PlayerPrefs.SetInt("PlayedBeforeSave", (PlayedBefore));
            PlayerPrefs.Save();
            loadInventory();
            loadPlayerInfo();
            loadBackpack();
        }
        else if (PlayedBefore == 3)
        {
            PlayedBefore = 4;
            int startLevel = 1;
            PlayerPrefs.SetInt("CurrentLevel", startLevel);
            PlayerPrefs.SetInt("PlayedBeforeSave", (PlayedBefore));
            PlayerPrefs.Save();
            loadInventory();
            loadPlayerInfo();
            loadBackpack();
        }
        else if (PlayedBefore == 4)
        {
            loadInventory();
            loadPlayerInfo();
            loadBackpack();
            emptyBackpackToInventory();
        }

        setInteractiveState.setInteractable();
        populationCount = items[3].itemQuantity * 4;
        hutTextField.text = items[3].itemQuantity.ToString();
        populationTextField.text = PlayerInformation.PeopleInCamp + "/" + populationCount.ToString();
        foodTextField.text = items[5].itemQuantity.ToString();
        inputField.enabled = false;
        inputField.interactable = false;
        inputField.image.enabled = false;
    }

    public void emptyBackpackToInventory()
    {
        var backpackDb = GameObject.Find("Backpack").GetComponent<Backpack>();
        var backpack = backpackDb.backpack;
        var itemDb = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var inventory = itemDb.inventory;
        int backpackCount = backpack.Count;

        if (backpack.Count > 0)
        {
            for (int i = backpackCount - 1; i >= 0; i--)
            {
                for (int j = 0; j < inventory.Count; j++)
                {
                    if (inventory[j].itemName == backpack[i].itemName)
                    {
                        inventory[j].itemQuantity += backpack[i].itemQuantity;
                        backpack.Remove(backpack[i]);
                        break;
                    }
                }
            }
        }
    }

    public void loadCraftText()
    {
        var inventoryComponent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var items = inventoryComponent.inventory;

        foreach (GameObject textField in GameObject.FindGameObjectsWithTag("Items requirement"))
        {
            Text textFieldName = textField.GetComponent<Text>();
            if (textFieldName.name.Contains("Raw Meat"))
            {
                textFieldName.text = items[0].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Wood"))
            {
                textFieldName.text = items[1].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Fur"))
            {
                textFieldName.text = items[6].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Cloth"))
            {
                textFieldName.text = items[7].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Requirement 1"))
            {
                int totatl = (items[3].itemQuantity + 1) * 200;
                textFieldName.text = "/" + totatl.ToString() + " Wood";
            }
        }
    }

    public void loadInventoryText()
    {
        var inventoryComponent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var items = inventoryComponent.inventory;

        var trapedMeat = GameObject.Find("Check Trap Result Meat").GetComponent<Text>();
        var trapedFur = GameObject.Find("Check Trap Result Fur").GetComponent<Text>();

        trapedMeat.text = "";
        trapedFur.text = "";

        foreach (GameObject textField in GameObject.FindGameObjectsWithTag("InventoryItems"))
        {
            Text textFieldName = textField.GetComponent<Text>();
            if (textFieldName.name.Contains("Raw Meat"))
            {
                textFieldName.text = items[0].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Wood"))
            {
                textFieldName.text = items[1].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Fur"))
            {
                textFieldName.text = items[6].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Cloth"))
            {
                textFieldName.text = items[7].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Cured"))
            {
                textFieldName.text = items[5].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Torch"))
            {
                textFieldName.text = items[2].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Trap"))
            {
                textFieldName.text = items[4].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Medicine"))
            {
                textFieldName.text = items[16].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Water"))
            {
                textFieldName.text = items[17].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Wooden Sword"))
            {
                textFieldName.text = items[18].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Wooden Armour"))
            {
                textFieldName.text = items[14].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Coal"))
            {
                textFieldName.text = items[15].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Iron"))
            {
                textFieldName.text = items[11].itemQuantity.ToString();
            }
            if (textFieldName.name.Contains("Steel"))
            {
                textFieldName.text = items[8].itemQuantity.ToString();
            }
        }
    }


    public void clearData()
    {

        /*  PlayerPrefs.SetString("PalyerName", "");
          PlayerPrefs.SetString("PalyerColour", "");
          PlayerPrefs.SetString("Engineer", "");
          PlayerPrefs.SetInt("PeopleInCamp", 0);
          PlayerPrefs.SetFloat("FireSize", 0f);
          PlayerPrefs.SetFloat("FireLight", 0f);
          PlayerPrefs.SetInt("Tutorial", 0);
          PlayerPrefs.SetInt("Exploration", 0);
          PlayerPrefs.SetInt("MapPostion", 0);*/
        var backpackDb = GameObject.Find("Backpack").GetComponent<Backpack>();
        var backpack = backpackDb.backpack;
        PlayerPrefs.DeleteKey("Backpack Count");
        if (backpack.Count > 0)
        {
            for (int i = 0; i < backpack.Count; i++)
            {
                Debug.Log("SAVEING");
                PlayerPrefs.DeleteKey("ItemQuantity" + i);
                PlayerPrefs.DeleteKey("ItemName" + i);
                backpack[i].itemQuantity = 0;
                if (backpack[i].itemQuantity <= 0)
                {
                    backpack.Remove(backpack[i]);
                }
            }
        }
    }

    private void loadData()
    {
        Debug.Log("LOADING");
        StreamReader read = File.OpenText(Application.persistentDataPath + "/" + "inventory.txt");
        var inventoryComponent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var items = inventoryComponent.inventory;
        string readfile = read.ReadLine();

        while (readfile != null)
        {
            char[] delimiter2 = { ';' };
            string[] input = readfile.Split(delimiter2);

            var convertType = (Item.Itemtype)Enum.Parse(typeof(Item.Itemtype), input[8]);

            items.Add(new Item(input[0], int.Parse(input[1]), input[2], int.Parse(input[3]), int.Parse(input[4]), int.Parse(input[5]), input[6], input[7], convertType));
            readfile = read.ReadLine();
        }
        read.Close();
    }

    public void loadInventory()
    {
        var itemDb = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var inventory = itemDb.inventory;

        var database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        var databaseItems = database.items;

        if (inventory.Count == 0)
        {
            for (int i = 0; i < databaseItems.Count; i++)
            {
                inventory.Add(databaseItems[i]);
                inventory[i].itemQuantity = PlayerPrefs.GetInt("InventoryQuantity " + i);
            }
        }
    }

    public void loadBackpack()
    {
        //Debug.Log("LOADING BACKPACK");
        var backpackDb = GameObject.Find("Backpack").GetComponent<Backpack>();
        var backpack = backpackDb.backpack;
        var database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        var databaseItems = database.items;
        int backpackCount = PlayerPrefs.GetInt("Backpack Count");
        if (backpackCount > 0 && backpack.Count == 0)
        {
            for (int i = 0; i < backpackCount; i++)
            {
                string itemName = PlayerPrefs.GetString("ItemName" + i);
                int itemQuantity = PlayerPrefs.GetInt("ItemQuantity" + i);
              //  Debug.Log(itemName + " = " + itemQuantity);
                for (int j = 0; j < databaseItems.Count; j++)
                {  
                    if (itemName == databaseItems[j].itemName)
                    {
                        backpack.Add(new Item(databaseItems[j].itemName, databaseItems[j].itemID, databaseItems[j].itemDesc, 0, databaseItems[j].itemPower, databaseItems[j].itemWeight, databaseItems[j].itemRequirement1, databaseItems[j].itemRequirement2, databaseItems[j].itemType));
                        backpack[i].itemQuantity += itemQuantity;
                    }
                }
            }
        }
    }

    private void loadPlayerInfo()
    {
        var fireOBJ = GameObject.Find("FireParticle").GetComponent<ParticleSystem>();
        var fireLight = GameObject.Find("fire light").GetComponent<Light>();
        var itemDb = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var inventory = itemDb.inventory;
        int populationCount = inventory[3].itemQuantity * 4;
        // ButtonHandler loadNext = new ButtonHandler();
        var loadNext = GameObject.Find("Camp Canvas").GetComponent<ButtonHandler>();

        Canvas eventCanvas = GameObject.Find("Event Canvas").GetComponent<Canvas>();
        var engineer = GameObject.Find("Character").GetComponent<Image>();
        var replybtn1 = GameObject.Find("Reply 1 text").GetComponent<Text>();
        var replybtn2 = GameObject.Find("Reply 2 text").GetComponent<Text>();
        var replybtn3 = GameObject.Find("Reply 3 text").GetComponent<Text>();

        var btnOne = GameObject.Find("Reply 1").GetComponent<Button>();
        var btnTwo = GameObject.Find("Reply 2").GetComponent<Button>();
        var btnThree = GameObject.Find("Reply 3").GetComponent<Button>();

        PlayerInformation.PalyerName = PlayerPrefs.GetString("PalyerName");
        PlayerInformation.PalyerColour = PlayerPrefs.GetString("PalyerColour");
        PlayerInformation.Engineer = PlayerPrefs.GetString("Engineer");
        PlayerInformation.PeopleInCamp = PlayerPrefs.GetInt("PeopleInCamp");
        PlayerInformation.FireSize = PlayerPrefs.GetFloat("FireSize");
        PlayerInformation.FireLight = PlayerPrefs.GetFloat("FireLight");
        PlayerInformation.Tutorial = PlayerPrefs.GetInt("Tutorial");
        PlayerInformation.Exploration = PlayerPrefs.GetInt("Exploration");
        PlayerInformation.MapPostion = PlayerPrefs.GetInt("MapPostion");
        PlayerInformation.EventConvo = PlayerPrefs.GetInt("EventConvo");
        PlayerInformation.AssignedGatherWood = PlayerPrefs.GetInt("AssignedGatherWood");
        PlayerInformation.UnAssignedPeopleInCamp = PlayerPrefs.GetInt("UnAssignedPeopleInCamp");
        PlayerInformation.CurrentMapLevel = PlayerPrefs.GetInt("CurrentLevel");

        string currentPopulation = PlayerInformation.PeopleInCamp.ToString();

        if (PlayerInformation.Engineer == "Female") { engineer.sprite = Resources.Load<Sprite>("Sprite/FemaleEngineer"); }
        else { engineer.sprite = Resources.Load<Sprite>("Sprite/MaleEngineer"); }

        fireOBJ.startSize = PlayerInformation.FireSize;
        fireLight.range = PlayerInformation.FireLight;

        if (PlayedBefore == 0)
        {
            loadNext.loadScene("Tutorial");
        }
        // PlayerInformation.EventConvo = 0;
        if (PlayerInformation.EventConvo == 0)
        {
            eventCanvas.enabled = true;

            btnOne.interactable = true;
            btnTwo.interactable = true;
            btnThree.interactable = true;

            replybtn1.text = "Got it";
            replybtn2.text = "Cool";
            replybtn3.text = "Okay";
        }
    }
}
