using UnityEngine;
using System.Collections;

public class NPCHurtPlayer : MonoBehaviour
{
    public int damageToGive;
    public GameObject damageBurst;
    public Transform hitPoint;
    public GameObject damageNumber;
    public bool PlayerFound;
    public float attackingTime;
    private float attackTimeCounter;
    GameObject player;
    public int damageMultiplier;

    void Start()
    {
        player = GameObject.Find("Player");
        var multiplier = PlayerPrefs.GetInt("LevelPiacked");
        setMultiplier(multiplier);
    }

    void Update()
    {
        var range = Vector2.Distance(transform.position, player.transform.position);
        if (range <= 1.7f)
        {
            if (attackingTime > 0)
            {
                attackTimeCounter -= Time.deltaTime;
            }
            if (attackTimeCounter <= 0)
            {
                attackTimeCounter = attackingTime;
                Debug.Log("FOUND PLAYER");
                PlayerFound = true;
                player.GetComponent<PlayerStats>().HurtPlayer(damageToGive);
                Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
                var clone = (GameObject)Instantiate(damageNumber, hitPoint.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingNumbers>().damageNumber = -damageToGive;
            }
        }
    }

    public void setMultiplier(int multiplier)
    {
        damageMultiplier = multiplier;
        setDamageToGive();
    }

    private void setDamageToGive()
    {
        damageToGive = damageToGive * damageMultiplier;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") { PlayerFound = true; }
        else { PlayerFound = false; }
    }
}
