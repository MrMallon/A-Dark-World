using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHurtNPC : MonoBehaviour {
    public int damageToGive;
    public GameObject damageBurst;
    public Transform hitPoint;
    public GameObject damageNumber;
    SpriteRenderer swordIcon;

    void Start()
    {
        swordIcon = GameObject.Find("Sword").GetComponent<SpriteRenderer>();
    }

    public void setDamageToGive(int swordDamage)
    {
        damageToGive = swordDamage;
    }

    public void setSwordIcon(string swordType)
    {
        Debug.Log("SWORD PASSED = " + swordType);
        swordIcon.sprite = Resources.Load<Sprite>("Item Icons/" + swordType);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            collision.gameObject.GetComponent<NPCStats>().HurtNpc(damageToGive);
            Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
            var clone = (GameObject) Instantiate(damageNumber, hitPoint.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = damageToGive;
        }
    }
}
