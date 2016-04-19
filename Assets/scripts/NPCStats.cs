using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NPCStats : MonoBehaviour{
    public int npcMaxHealth;
    public int npcCurrentHelth;
    public int npcHealthMultiplier;
    BoxCollider2D chest;
    // Use this for initialization
    void Start () {
       // npcCurrentHelth = npcMaxHealth;
        var multiplier = PlayerPrefs.GetInt("LevelPiacked");
        chest = GameObject.Find("Chest Icon").GetComponent<BoxCollider2D>();
        chest.enabled = false;
        setMultiplier(multiplier);
    }
    void Update()
    {
        if (npcCurrentHelth <= 0 && gameObject.name.Contains("Boss"))
        {
            chest.enabled = true;
            Destroy(gameObject);
        }
        if (npcCurrentHelth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void setMultiplier(int multiplier)
    {
        npcHealthMultiplier = multiplier;
        SetMaxHealth();
    }

    public void HurtNpc(int damageGiven)
    {
        npcCurrentHelth -= damageGiven;
    }

    public void SetMaxHealth()
    {
        npcCurrentHelth = npcMaxHealth * npcHealthMultiplier;
    }
}
