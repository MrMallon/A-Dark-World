using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public Transform startPoint, upDir, downDir, leftDir, rightDir;
    public bool spotted = false;

    public Transform target;
    public float speed = 5;
    Vector3[] path;
    int targetIndex;

    Animator animation;
    Transform player;


    private bool attacking;
    public float attackingTime;
    private float attackTimeCounter;

    void Start()
    {
        animation = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Transform>();
     //   PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    // Update is called once per frame
    void Update()
    {

        var step = speed * Time.deltaTime;
        var range = Vector2.Distance(transform.position, player.transform.position);
        float xAxis = transform.position.x - player.position.x;
        float yAxis = transform.position.y - player.position.y;
        Raycasting();

        if (spotted)
        {
            if (!attacking)
            {
                if (xAxis < yAxis)
                {
                    if (transform.position.y < player.position.y)
                    {
                        movingUp();
                    }
                    else if (transform.position.y > player.position.y)
                    {
                        movingDown();
                    }

                }
                else
                {
                    if (transform.position.x > player.position.x)
                    {
                        movingLeft();
                    }
                    else if (transform.position.x < player.position.x)
                    {
                        movingRight();
                    }
                }

                if (range <= 1.7f)
                {
                    attackTimeCounter = attackingTime;
                    attacking = true;
                   
                }
            }
        }

        if (attackingTime > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }
        if (attackTimeCounter <= 0)
        {
            attacking = false;
        }
    }

    void Raycasting()
    {
      //  Debug.DrawLine(startPoint.position, upDir.position, Color.green, 2, false);
      //  Debug.DrawLine(startPoint.position, downDir.position, Color.green, 2, false);
      //  Debug.DrawLine(startPoint.position, leftDir.position, Color.green, 2, false);
      //  Debug.DrawLine(startPoint.position, rightDir.position, Color.green, 2, false);

        var currentNPC = this.gameObject;
        char num;
        string npcName = currentNPC.name;
        num = npcName[npcName.Length - 1];

        var spotNotiferImg = GameObject.Find("Player Spotted " + num).GetComponent<SpriteRenderer>();
        Color Alpha = spotNotiferImg.color;

        if (Physics2D.Linecast(startPoint.position, upDir.position, 1 << LayerMask.NameToLayer("Player"))) { spotted = Physics2D.Linecast(startPoint.position, upDir.position, 1 << LayerMask.NameToLayer("Player")); }
        else if (Physics2D.Linecast(startPoint.position, downDir.position, 1 << LayerMask.NameToLayer("Player"))) { spotted = Physics2D.Linecast(startPoint.position, downDir.position, 1 << LayerMask.NameToLayer("Player")); }
        else if (Physics2D.Linecast(startPoint.position, leftDir.position, 1 << LayerMask.NameToLayer("Player"))) { spotted = Physics2D.Linecast(startPoint.position, leftDir.position, 1 << LayerMask.NameToLayer("Player")); }
        else if (Physics2D.Linecast(startPoint.position, rightDir.position, 1 << LayerMask.NameToLayer("Player"))) { spotted = Physics2D.Linecast(startPoint.position, rightDir.position, 1 << LayerMask.NameToLayer("Player")); }

        if (spotted == true)
        {

            Alpha.a = 255;
            spotNotiferImg.color = Alpha;

            if (this.gameObject == GameObject.Find("Enemy_NPC 3"))
            {
                if (GameObject.Find("Enemy_NPC 3"))
                {
                    Debug.Log("Here");
                    PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                }
            }
            else
                playerSpotted();
        }
        else
        {
            Alpha.a = 0;
            spotNotiferImg.color = Alpha;
        }
    }

    void playerSpotted()
    {
        string directionFaceing = player.GetComponent<PlayerMovement>().getDirFaceing();
        var currentNPC = this.gameObject;

        var step = speed * Time.deltaTime;
        var range = Vector2.Distance(transform.position, player.transform.position);

        if (range >= 1.7f)
        {
            attackTimeCounter = attackingTime;
            attacking = true;
            
            transform.position = Vector2.MoveTowards(transform.position, player.position, step);
        }
    }

   

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {

        if (pathSuccessful)
        {
            if (GameObject.Find("Enemy_NPC 3"))
            {
                path = newPath;
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }
    }

    IEnumerator FollowPath()
    {
            Vector3 currentWaypoint = path[0];

            while (true)
            {

                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;

            }
      
    }

    private void movingUp()
    {
        //   transform.position += Vector3.up * speed * Time.deltaTime;
        animation.SetBool("IsWalking", true);
        animation.SetFloat("input_x", 0);
        animation.SetFloat("input_y", 1f);

    }
    private void movingDown()
    {
        // transform.position += Vector3.down * speed * Time.deltaTime;
        animation.SetBool("IsWalking", true);
        animation.SetFloat("input_x", 0);
        animation.SetFloat("input_y", -1f);

    }
    private void movingLeft()
    {
        // transform.position += Vector3.left * speed * Time.deltaTime;
        animation.SetBool("IsWalking", true);
        animation.SetFloat("input_x", -1f);
        animation.SetFloat("input_y", 0);

    }
    private void movingRight()
    {
        // transform.position += Vector3.right * speed * Time.deltaTime;
        animation.SetBool("IsWalking", true);
        animation.SetFloat("input_x", 1f);
        animation.SetFloat("input_y", 0);

    }
}
