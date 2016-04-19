using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

    public int playerMaxHealth;
    public int playerCurrentHelth;
    int playerArmour;
    int armour = 0;
    float armourDamage;
    Image healthBar, armourBar, showArmourBar, armourIcon;
    Backpack backpackComponent;
    Text HealthText;

    void Awake()
    {
        HealthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthBar = GameObject.Find("Health Status").GetComponent<Image>();
        armourBar = GameObject.Find("Armour Status").GetComponent<Image>();
        showArmourBar = GameObject.Find("Armour Damaged").GetComponent<Image>();
        armourIcon  = GameObject.Find("Armour Icon").GetComponent<Image>();
        backpackComponent = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
        SetMaxHealth();
    }
    void Update()
    {
        if (playerCurrentHelth <= 0)
        {
            var emptyBackpack = GameObject.Find("Chest Canvas").GetComponent<ChestToBackPack>();
            emptyBackpack.emptyBackpack();
            SceneManager.LoadScene("DeathScreen");
        }  
    }

    public void HurtPlayer(int damageGiven)
    {   
        if (playerArmour > 0) {
            playerArmour -= damageGiven;
            float durability = (playerArmour / 100f) * armourDamage;
            Debug.Log("durability" + durability);
            armourBar.fillAmount = durability;           
        }
        else {
            playerCurrentHelth -= damageGiven;
            float health = (playerCurrentHelth / 100f) * 2f;
            Debug.Log("HEALTH" + health);
            armourBar.fillAmount = 0;
            armourIcon.sprite = Resources.Load<Sprite>("Item Icons/UIMask");
            setArmour(0);
            armour = 1;
            HealthText.text = playerCurrentHelth.ToString();
            healthBar.fillAmount = health;
        }

        if (armour == 1)
        {
            var backpack = backpackComponent.backpack;
            for(int i = 0; i < backpack.Count; i++)
            {
                if (backpack[i].itemName.Contains("Armour"))
                {
                    backpack.Remove(backpack[i]);
                }
                else
                    armour = 0;
            }           
        }
    }

    public void SetMaxHealth()
    {
        playerCurrentHelth = playerMaxHealth;
        float health = (playerCurrentHelth / 100f) * 2f;
        healthBar.fillAmount = health;
        HealthText.text = playerCurrentHelth.ToString();
    }

    public void HealPlayer(int healAmount)
    {
        if (playerCurrentHelth < playerMaxHealth) { playerCurrentHelth += healAmount; }
        if(playerCurrentHelth > playerMaxHealth){ playerCurrentHelth = playerMaxHealth; }

        float health = (playerCurrentHelth / 100f) * 2f;
        HealthText.text = playerCurrentHelth.ToString();
        Debug.Log("HEALTH" + health);
        healthBar.fillAmount = health;
    }

    public void setArmour(int armour)
    {
        playerArmour = armour;

        if (playerArmour == 20) { armourDamage = 5f; }
        else if(playerArmour == 50) { armourDamage = 2f; }
        else if (playerArmour == 70) { armourDamage = 1.4f; }

        if (playerArmour > 0)
            showArmourBar.enabled = true;
        else
            showArmourBar.enabled = false;
    }

    public void setArmourIcon(string armourType)
    {
        armourIcon.sprite = Resources.Load<Sprite>("Item Icons/" + armourType);
    }
}
