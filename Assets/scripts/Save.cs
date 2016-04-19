using UnityEngine;
using System.Collections;
using System.IO;

public class Save : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("ONPAUSE");
            saveInventory();
            savePlayerInfo();
            saveBackpack();
        }
    }

    public void createData()
    {
        var itemDb = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var inventory = itemDb.inventory;

        var database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        var databaseItems = database.items;

        if (inventory.Count == 0)
        {
            for (int i = 0; i < databaseItems.Count; i++)
            {
                Debug.Log("Counting");
                if (i > databaseItems.Count) { i = databaseItems.Count; }
                else
                    inventory.Add(databaseItems[i]);
            }
        }

    }

    public void saveInventory()
    {
        var itemDb = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var inventory = itemDb.inventory;

        for (int i = 0; i < inventory.Count; i++)
        {
            PlayerPrefs.SetInt("InventoryQuantity " + i, inventory[i].itemQuantity);
        }
    }

    public void saveBackpack()
    {
       // Debug.Log("SAVING BACKPACK");
        var backpackDb = GameObject.Find("Backpack").GetComponent<Backpack>();
        var backpack = backpackDb.backpack;
        PlayerPrefs.SetInt("Backpack Count", backpack.Count);
        if (backpack.Count>0) {
            for (int i = 0; i < backpack.Count; i++)
            {
             //   Debug.Log("SAVEING");
                PlayerPrefs.SetInt("ItemQuantity" + i, backpack[i].itemQuantity);
                PlayerPrefs.SetString("ItemName" + i, backpack[i].itemName);
            }
        }
    }

    public void savePlayerInfo()
    {
        var fireOBJ = GameObject.Find("FireParticle").GetComponent<ParticleSystem>();
        var fireLight = GameObject.Find("fire light").GetComponent<Light>();

        PlayerInformation.FireSize = fireOBJ.startSize;
        PlayerInformation.FireLight = fireLight.range;

        PlayerPrefs.SetString("PalyerName", PlayerInformation.PalyerName);
        PlayerPrefs.SetString("PalyerColour", PlayerInformation.PalyerColour);
        PlayerPrefs.SetString("Engineer", PlayerInformation.Engineer);
        PlayerPrefs.SetInt("PeopleInCamp", PlayerInformation.PeopleInCamp);
        PlayerPrefs.SetFloat("FireSize", PlayerInformation.FireSize);
        PlayerPrefs.SetFloat("FireLight", PlayerInformation.FireLight);
        PlayerPrefs.SetInt("Tutorial", PlayerInformation.Tutorial);
        PlayerPrefs.SetInt("Exploration", PlayerInformation.Exploration);
        PlayerPrefs.SetInt("MapPostion", PlayerInformation.MapPostion);
        PlayerPrefs.SetInt("EventConvo", PlayerInformation.EventConvo);
        PlayerPrefs.SetInt("AssignedGatherWood", PlayerInformation.AssignedGatherWood);
        PlayerPrefs.SetInt("UnAssignedPeopleInCamp", PlayerInformation.UnAssignedPeopleInCamp);
        //PlayerPrefs.SetInt("CurrentLevel", PlayerInformation.CurrentMapLevel);

        PlayerPrefs.Save();
    }
}
