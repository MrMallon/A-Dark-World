using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadMinCaveData : MonoBehaviour {

    Backpack backpackComponent;

    void Start () {
        backpackComponent = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
        loadBackpackData();
        loadInventory();
        loadPlayerStats();
    }
	
	void Update () {
	
	}

    private void loadBackpackData()
    {
        var backpack = backpackComponent.backpack;
        var database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        var databaseItems = database.items;
        int backpackCount = PlayerPrefs.GetInt("Backpack Count");
        if (backpackCount > 0 && backpack.Count == 0)
        {
            for (int i = 0; i < backpackCount; i++)
            {
                string itemName = PlayerPrefs.GetString("ItemName" + i);
                int itemQuantity = PlayerPrefs.GetInt("ItemQuantity" + i);
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

    public void loadPlayerStats()
    {
        
        var playerHealth = GameObject.Find("Player").GetComponent<PlayerStats>();
        var playerStrength = GameObject.Find("Sword").GetComponent<PlayerHurtNPC>();
        var backpack = backpackComponent.backpack;

        for (int i = 0; i < backpack.Count; i++)
        {
            Debug.Log("NAME = " + backpack[i].itemName);
            if (backpack[i].itemName.Contains("Armour"))
            {
                playerHealth.setArmour(backpack[i].itemPower);
                playerHealth.setArmourIcon(backpack[i].itemName);
            }
            else if (backpack[i].itemName.Contains("Sword"))
            {
                playerStrength.setDamageToGive(backpack[i].itemPower);
                playerStrength.setSwordIcon(backpack[i].itemName);
            }
        } 
    }
}
