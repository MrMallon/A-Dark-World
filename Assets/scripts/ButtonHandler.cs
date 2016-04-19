using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ButtonHandler : MonoBehaviour
{
    private Canvas canvas;
    private Canvas[] canvasArray;
    private Texture2D imageNumber;
    private float fireSize;
    private double timeRemaining = 00;
    private float trapTimeRemaining = 00;
    private bool trigger = false;
    private bool traptrigger = false;

    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    void Update()
    {
        var trapsLeftText = GameObject.Find("Traps Set").GetComponent<Text>();
        var checktrapBtn = GameObject.Find("Check Trap Btn").GetComponent<Image>();
        var notifier = GameObject.Find("Camp Canvas").GetComponent<Notifier>();
        // Notifier notifier = new Notifier();
        if (trigger == true)
        {
            timeRemaining += Time.deltaTime;
            if (timeRemaining >= 5)
            {
                trapsLeftText.text = "";
                notifier.trapNotification("", "");
                timeRemaining = 0;
                setTrigger(false);
            }
        }
        if (traptrigger == true)
        {
            trapTimeRemaining += Time.deltaTime;

            if (trapTimeRemaining >= 10)
            {
                setInteractable();
                trapTimeRemaining = 0;
                setTrapTrigger(false);
            }
            else { checktrapBtn.fillAmount = trapTimeRemaining / 10f; }

        }
    }

    public void demoDayItems(string action)
    {
        var inventoryComponent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var inventory = inventoryComponent.inventory;
        for (int i =0; i < inventory.Count; i++)
        {
            if (action == "Add" && inventory[i].itemName == "Hut") { inventory[i].itemQuantity = inventory[i].itemQuantity; }
            else if (action == "Add" && inventory[i].itemName == "Wood") { inventory[i].itemQuantity += 1000; }
            else if(action == "Add") { inventory[i].itemQuantity += 10; }
            if (action == "Remove" && inventory[i].itemQuantity >= 100) { inventory[i].itemQuantity -= 100; }
        }
    }

    public void setTrigger(bool trig)
    {
        trigger = trig;
    }

    public void setTrapTrigger(bool trig)
    {
        traptrigger = trig;
    }

    public void setinputfeildActive()
    {
        var inputField = GameObject.Find("WoodInputField").GetComponent<InputField>();
        var inputFieldText = GameObject.Find("Wood Input Text").GetComponent<Text>();
        inputField.enabled = true;
        inputField.interactable = true;
        inputField.image.enabled = true;
        inputFieldText.enabled = true;
        inputField.Select();
    }

    public void setInteractable()
    {
        var settrapBtn = GameObject.Find("Set Trap Btn").GetComponent<Button>();
        var checktrapBtn = GameObject.Find("Check Trap Btn").GetComponent<Button>();
        var inventoryComponent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var items = inventoryComponent.inventory;

        if (items[4].itemQuantity > 0)
        {
            // Debug.Log("ACTIVE");
            settrapBtn.interactable = true;
        }
        else if (items[4].itemQuantity == 0)
        {
            // Debug.Log("INACTIVE");
            settrapBtn.interactable = false;
        }
        if (items[19].itemQuantity > 0)
        {
            checktrapBtn.interactable = true;
        }
        else if (items[19].itemQuantity == 0)
        {
            checktrapBtn.interactable = false;
        }
    }

    public void loadScene(string sceneName)
    {
        Save save = new Save();
        save.saveInventory();
        save.savePlayerInfo();
        save.saveBackpack();
        //Application.LoadLevel(sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void populateCanvasList()
    {
        canvasArray = new Canvas[3];
        canvasArray[0] = GameObject.Find("Craft Canvas").GetComponent<Canvas>();
        canvasArray[1] = GameObject.Find("Inventory Canvas").GetComponent<Canvas>();
        canvasArray[2] = GameObject.Find("Backpack Canvas").GetComponent<Canvas>();
    }

    public void showCanvas()
    {
        Debug.Log("SHOW CANVAS");
        var fireOBJ = GameObject.Find("FireParticle").GetComponent<ParticleSystem>();
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
#pragma warning disable CS0618 // Type or member is obsolete
                    fireOBJ.enableEmission = false;
#pragma warning restore CS0618 // Type or member is obsolete

                }
                else
                    canvasArray[i].enabled = false;

            }
        }
        else if (canvas.enabled == true)
        {
            Debug.Log("btn not pressed");
            canvas.enabled = false;
#pragma warning disable CS0618 // Type or member is obsolete
            fireOBJ.enableEmission = true;
#pragma warning restore CS0618 // Type or member is obsolete

        }
    }

    public void cratfItems(string buttonVars)
    {
        string itemName;
        int reqAmount1;
        int reqAmount2;
        var inventoryComponent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var items = inventoryComponent.inventory;
        var hutTextField = GameObject.Find("Hut Count").GetComponent<Text>();
        var populationTextField = GameObject.Find("Population Count").GetComponent<Text>();
        int populationCount;
        var hutWoodReq = GameObject.Find("Hut Requirement 1").GetComponent<Text>();
        Load resetText = new Load();
        var notify = GameObject.Find("Camp Canvas").GetComponent<Notifier>();
        //  Notifier notify = new Notifier();
        char[] delimiter = { ';' };
        string[] input = buttonVars.Split(delimiter);

        itemName = input[0];
        reqAmount1 = int.Parse(input[1]);
        reqAmount2 = int.Parse(input[2]);
        // items[1].itemQuantity = 200;

        if (itemName == "Wooden Armour")
        {
            if (items[1].itemQuantity >= reqAmount1 && items[6].itemQuantity >= reqAmount2)
            {
                items[14].itemQuantity += 1;
                items[1].itemQuantity -= reqAmount1;
                items[6].itemQuantity -= reqAmount2;
                resetText.loadInventoryText();
                resetText.loadCraftText();
                notify.inventoryNotification("Crafted");
            }
        }

        Debug.Log(itemName + " " + reqAmount1);
        if (itemName == "Cured Meat")
        {                         //raw Meat
            if (items[0].itemQuantity >= reqAmount1)
            {
                items[5].itemQuantity += 1;
                items[0].itemQuantity -= reqAmount1;
                resetText.loadInventoryText();
                resetText.loadCraftText();
                notify.inventoryNotification("Crafted");

                var foodTextField = GameObject.Find("Food Count").GetComponent<Text>();
                foodTextField.text = items[5].itemQuantity.ToString();
            }
        }
        if (itemName == "Hut" || itemName == "Trap")
        {
            //wood
            if (items[1].itemQuantity >= reqAmount1)
            {
                if (itemName == "Hut")
                {
                    reqAmount1 = reqAmount1 * (items[3].itemQuantity + 1);
                    if (items[1].itemQuantity >= reqAmount1)
                    {
                        items[3].itemQuantity += 1;
                        populationCount = items[3].itemQuantity * 4;
                        hutTextField.text = items[3].itemQuantity.ToString();
                        populationTextField.text = PlayerInformation.PeopleInCamp + "/" + populationCount.ToString();
                        items[1].itemQuantity -= reqAmount1;
                        reqAmount1 = reqAmount1 * items[3].itemQuantity;
                        hutWoodReq.text = "/" + reqAmount1 + " Wood";
                    }
                }
                if (itemName == "Trap")
                {
                    items[4].itemQuantity += 1;
                    notify.inventoryNotification("Crafted");
                    setInteractable();
                    items[1].itemQuantity -= reqAmount1;
                }

                resetText.loadInventoryText();
                resetText.loadCraftText();
            }
        }
        if (itemName == "Water Canister" || itemName == "Wooden Sword")
        {                       //wood                                  Fur
            if (items[1].itemQuantity >= reqAmount1 && items[6].itemQuantity >= reqAmount2)
            {
                if (itemName == "Water Canister")
                {
                    items[17].itemQuantity += 1;
                    notify.inventoryNotification("Crafted");
                }
                if (itemName == "Wooden Sword")
                {
                    items[18].itemQuantity += 1;
                    notify.inventoryNotification("Crafted");
                }

                items[1].itemQuantity -= reqAmount1;
                items[6].itemQuantity -= reqAmount2;

                resetText.loadInventoryText();
                resetText.loadCraftText();
            }
        }
        if (itemName == "Torch")
        {                       //wood                                  Cloth
            if (items[1].itemQuantity >= reqAmount1 && items[7].itemQuantity >= reqAmount2)
            {
                items[2].itemQuantity += 1;
                items[1].itemQuantity -= reqAmount1;
                items[7].itemQuantity -= reqAmount2;
                resetText.loadInventoryText();
                resetText.loadCraftText();
                notify.inventoryNotification("Crafted");
            }
        }
        if (itemName == "Cloth")
        {                  //Fur
            if (items[6].itemQuantity >= reqAmount1)
            {
                items[7].itemQuantity += 1;
                items[6].itemQuantity -= reqAmount1;
                resetText.loadInventoryText();
                resetText.loadCraftText();
                notify.inventoryNotification("Crafted");
            }
        }
    }

    public void setTrap()
    {
        var inventoryComponent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var checkTrapBtn = GameObject.Find("Check Trap Btn").GetComponent<Button>();
        var checkTrapImg = GameObject.Find("Check Trap Btn").GetComponent<Image>();
        var trapsLeftText = GameObject.Find("Traps Set").GetComponent<Text>();
        var items = inventoryComponent.inventory;


        if (items[4].itemQuantity > 0)
        {
            items[4].itemQuantity -= 1;
            items[19].itemQuantity += 1;

            var totalTrapRemain = items[19].itemQuantity;
            trapsLeftText.text = "+" + totalTrapRemain.ToString();

            timeRemaining = 0;
            setTrigger(true);

            trapTimeRemaining = 0;
            setTrapTrigger(true);
            setInteractable();
            checkTrapImg.fillAmount = 0.0f;
            checkTrapBtn.interactable = false;
        }

    }
    public void checkTrap()
    {
        var inventoryComponent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var items = inventoryComponent.inventory;
        int number = Random.Range(1, 11);
        int initMeat = items[0].itemQuantity;
        int initFur = items[6].itemQuantity;
        int NumSettraps = items[19].itemQuantity;
        int newMeat, newFur;
        string noticeMeat, noticeFur;
        Notifier notify = new Notifier();
        Debug.Log(number);

        if (NumSettraps > 0)
        {
            if (NumSettraps > 0 && NumSettraps <= 2)
            {
                if (number >= 0 && number < 3) { items[0].itemQuantity += 1; }
                if (number >= 3 && number <= 6) { items[0].itemQuantity += 2; items[6].itemQuantity += 1; }
                if (number > 6 && number <= 11) { items[0].itemQuantity += 3; items[6].itemQuantity += 2; }
                newMeat = items[0].itemQuantity - initMeat;
                newFur = items[6].itemQuantity - initFur;
                noticeMeat = "+" + newMeat + " Raw Meat";
                noticeFur = "+" + newFur + " Fur";
                notify.trapNotification(noticeMeat, noticeFur);
            }

            else if (NumSettraps > 2 && NumSettraps <= 5)
            {
                if (number >= 0 && number < 4) { items[0].itemQuantity += 2; items[6].itemQuantity += 2; }
                if (number >= 4 && number <= 7) { items[0].itemQuantity += 4; items[6].itemQuantity += 3; }
                if (number > 7 && number <= 11) { items[0].itemQuantity += 5; items[6].itemQuantity += 5; }
                newMeat = items[0].itemQuantity - initMeat;
                newFur = items[6].itemQuantity - initFur;
                noticeMeat = "+" + newMeat + " Raw Meat";
                noticeFur = "+" + newFur + " Fur";
                notify.trapNotification(noticeMeat, noticeFur);
            }

            else if (NumSettraps > 5 && NumSettraps <= 10)
            {
                if (number >= 0 && number < 4) { items[0].itemQuantity += 3; items[6].itemQuantity += 3; }
                if (number >= 4 && number <= 7) { items[0].itemQuantity += 6; items[6].itemQuantity += 5; }
                if (number > 7 && number <= 11) { items[0].itemQuantity += 9; items[6].itemQuantity += 8; }
                newMeat = items[0].itemQuantity - initMeat;
                newFur = items[6].itemQuantity - initFur;
                noticeMeat = "+" + newMeat + " Raw Meat";
                noticeFur = "+" + newFur + " Fur";
                notify.trapNotification(noticeMeat, noticeFur);
            }

            else if (NumSettraps > 10)
            {
                if (number >= 0 && number < 5) { items[0].itemQuantity += NumSettraps - 6; items[6].itemQuantity += NumSettraps - 6; }
                if (number >= 5 && number <= 8) { items[0].itemQuantity += NumSettraps - 4; items[6].itemQuantity += NumSettraps - 4; }
                if (number > 8 && number <= 11) { items[0].itemQuantity += NumSettraps - 2; items[6].itemQuantity += NumSettraps - 3; }
                newMeat = items[0].itemQuantity - initMeat;
                newFur = items[6].itemQuantity - initFur;
                noticeMeat = "+" + newMeat + " Raw Meat";
                noticeFur = "+" + newFur + " Fur";
                notify.trapNotification(noticeMeat, noticeFur);
            }
        }
        timeRemaining = 0;
        setTrigger(true);
        items[19].itemQuantity -= items[19].itemQuantity;
        setInteractable();
    }
}