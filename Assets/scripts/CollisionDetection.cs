using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour {

    Rigidbody2D rBody;
    Animator animation;
    int LevelAccess;
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        LevelAccess = PlayerPrefs.GetInt("LevelPiacked");
        Debug.Log("LEVEL ACCESS == " + LevelAccess);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Palyer" || collision.gameObject.name.Contains("NPC")) 
        {
            rBody.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Chest Icon")
        {
            var chest = GameObject.Find("Chest Canvas").GetComponent<Canvas>();
            var backpackBtn = GameObject.Find("Bakpack Btn").GetComponent<Button>();
            var refreshChest = chest.GetComponent<ChestToBackPack>();
            backpackBtn.enabled = false;
            chest.enabled = true;
            refreshChest.refreshChestPanel();
            refreshChest.refreshChestBackpackPanel();
            int nextLevel = LevelAccess + 1;  
            if (LevelAccess == PlayerPrefs.GetInt("CurrentLevel")) { PlayerPrefs.SetInt("CurrentLevel", nextLevel); }
            PlayerPrefs.Save();
            PlayerInformation.CurrentMapLevel = PlayerPrefs.GetInt("CurrentLevel");
            Debug.Log("LEVEL ACCESS NOW == " + PlayerPrefs.GetInt("CurrentLevel"));
        }
    }
}