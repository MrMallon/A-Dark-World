using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Item {

    public string itemName;
    public int itemID;
    public string itemDesc;
    public Texture2D itemIcon;
    public int itemQuantity;
    public int itemPower;
    public int itemWeight;
    public string itemRequirement1;
    public string itemRequirement2;
    public Itemtype itemType;

    public enum Itemtype
    {
        Weapon,
        Consumable,
        normal
    }

    public Item(string name, int ID, string desc, int quantity, int power, int weight, string requirement1, string requirement2, Itemtype type)
    {
       // var convertType = (Itemtype)Enum.Parse(typeof(Itemtype), type);

        itemName = name;
        itemID = ID;
        itemDesc = desc;
        itemIcon = Resources.Load<Texture2D>("Item Icons/" + name);
        itemQuantity = quantity;
        itemPower = power;
        itemWeight = weight;
        itemRequirement1 = requirement1;
        itemRequirement2 = requirement2;
        itemType = type;//convertType;
    }

    public Item()
    {
        itemID = -1;
    }
}
