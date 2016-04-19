using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10f;
    bool moveUp = false;
    bool moveDown = false;
    bool moveRight = false;
    bool moveLeft = false;
    bool interact = false;
    string directionFaceing = "Up";
    private double swordTimmer = 0;
    public bool spotted = false;
    public float floatHeight;
    public float liftForce;
    public float damping;

    private bool attacking;
    public float attackingTime;
    private float attackTimeCounter;

    private Transform myTransform;
    public Rigidbody2D rb2D;
   // Rigidbody2D rBody;
    Animator animation;
    BoxCollider2D swordCollision, interactCollision;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Sword").GetComponent<BoxCollider2D>())
        {
            Debug.Log("Here Sword");
            swordCollision = GameObject.FindGameObjectWithTag("Sword").GetComponent<BoxCollider2D>();
            interactCollision = GameObject.Find("Hit Point").GetComponent<BoxCollider2D>();
        }

        animation = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Player");
       // rBody = player.GetComponent<Rigidbody2D>();
        myTransform = player.transform;
        // var sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<SpriteRenderer>(); 
    }

    void FixedUpdate()
    {
        Vector2 movmentVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Button Up = GameObject.Find("Up").GetComponent<Button>();
        Button Down = GameObject.Find("Down").GetComponent<Button>();
        Button Right = GameObject.Find("Right").GetComponent<Button>();
        Button Left = GameObject.Find("Left").GetComponent<Button>();

        if (!attacking)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || moveLeft == true)
            {
                movingLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow) || moveRight == true)
            {
                movingRight();
            }
            else if (Input.GetKey(KeyCode.UpArrow) || moveUp == true)
            {
                movingUp();
            }
            else if (Input.GetKey(KeyCode.DownArrow) || moveDown == true)
            {
                movingDown();
            }
            else
            {
                Down.image.overrideSprite = Resources.Load<Sprite>("Dpad Icons/Down");
                Up.image.overrideSprite = Resources.Load<Sprite>("Dpad Icons/Up");
                Right.image.overrideSprite = Resources.Load<Sprite>("Dpad Icons/Right");
                Left.image.overrideSprite = Resources.Load<Sprite>("Dpad Icons/Left");
                animation.SetBool("IsWalking", false);
            }


            if (Input.GetKeyDown(KeyCode.Space) || interact == true)
            {
                swordCollision.enabled = true;
                interactCollision.enabled = true;
                attackTimeCounter = attackingTime;
                attacking = true;
                Attack();
            }
            else
            {
                swordCollision.enabled = false;
                interactCollision.enabled = false;
            }
                
        }

        if(attackingTime > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }
        if (attackTimeCounter <= 0)
        {
            attacking = false;
            animation.SetBool("Attack", false);
        }
    }
    public string getDirFaceing()
    {
        return directionFaceing;
    }

    public void setMoveUp(bool input)
    {
        moveUp = input;
    }
    public void setMoveDown(bool input)
    {
        moveDown = input;
    }
    public void setMoveRight(bool input)
    {
        moveRight = input;
    }
    public void setMoveLeft(bool input)
    {
        moveLeft = input;
    }
    public void setInteract(bool input)
    {
        interact = input;
    }

    private void movingUp()
    {
        Button Up = GameObject.Find("Up").GetComponent<Button>();

        Up.image.overrideSprite = Resources.Load<Sprite>("Dpad Icons/PressedUp");
        transform.position += Vector3.up * speed * Time.deltaTime;
        animation.SetBool("IsWalking", true);
        animation.SetFloat("input_x", 0);
        animation.SetFloat("input_y", 1f);
        directionFaceing = "Up";
    }
    private void movingDown()
    {
        Button Down = GameObject.Find("Down").GetComponent<Button>();

        Down.image.overrideSprite = Resources.Load<Sprite>("Dpad Icons/PressedDown");
        transform.position += Vector3.down * speed * Time.deltaTime;
        animation.SetBool("IsWalking", true);
        animation.SetFloat("input_x", 0);
        animation.SetFloat("input_y", -1f);
        directionFaceing = "Down";
    }
    private void movingLeft()
    {
        Button Left = GameObject.Find("Left").GetComponent<Button>();

        Left.image.overrideSprite = Resources.Load<Sprite>("Dpad Icons/PressedLeft");
        transform.position += Vector3.left * speed * Time.deltaTime;
        animation.SetBool("IsWalking", true);
        animation.SetFloat("input_x", -1f);
        animation.SetFloat("input_y", 0);
        directionFaceing = "Left";
    }
    private void movingRight()
    {
        Button Right = GameObject.Find("Right").GetComponent<Button>();

        Right.image.overrideSprite = Resources.Load<Sprite>("Dpad Icons/PressedRight");
        transform.position += Vector3.right * speed * Time.deltaTime;
        animation.SetBool("IsWalking", true);
        animation.SetFloat("input_x", 1f);
        animation.SetFloat("input_y", 0);
        directionFaceing = "Right";
    }

    private void Attack()
    {

        if (directionFaceing == "Up")
        {
            animation.SetBool("Attack", true);
            animation.SetFloat("input_x", 0);
            animation.SetFloat("input_y", 1f);
        }
        else if (directionFaceing == "Down")
        {
            animation.SetBool("Attack", true);
            animation.SetFloat("input_x", 0);
            animation.SetFloat("input_y", -1f);
        }
        else if (directionFaceing == "Right")
        {
            animation.SetBool("Attack", true);
            animation.SetFloat("input_x", 1f);
            animation.SetFloat("input_y", 0);
        }
        else if (directionFaceing == "Left")
        {
            animation.SetBool("Attack", true);
            animation.SetFloat("input_x", -1f);
            animation.SetFloat("input_y", 0);
        }

    }
}
