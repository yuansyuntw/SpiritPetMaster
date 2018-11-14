using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpiritMonsterMaster;

public class Monster01_Controller : Monster {
    [SerializeField]
    private Animator animator;
    private Rigidbody2D rb;
    private int Dir = 1;
    private float HP, Distx, timer, timerJump;
    private int hitted = 0;
    private GameObject HPBar;

    public GameObject Player;
    public GameStageController gamestage;
    public GameObject HurtText;

    public Monster01_Controller(int _id) : base(_id)
    {

    }

    void Start () {
        //change to read file here 
        //warning = 5f;
        //Attacknum = 10;
        speed = 1.5f;
        //maxHP = 100;
        Monsterwind = 1;

        rb = GetComponent<Rigidbody2D>();
        Dir = -1;
        HP = maxHP;
        timer = 2.5f;

        //GameObject NewHP = Instantiate(HPBar, transform.position, Quaternion.identity);
        //NewHP.transform.SetParent(gameObject.transform);
        //MonsterHP = NewHP.GetComponent<Slider>();
        HPBar = gameObject.transform.GetChild(0).gameObject;
    }
	
	void Update () {
        if (gamestage.Gameover == 2)
        {
            return;
        }

        if (Mathf.Abs(rb.velocity.y) < 0.05f)timerJump += Time.deltaTime;

        Distx = Mathf.Abs(Player.transform.position.x - gameObject.transform.position.x);
        float moveHorizontal = (Player.transform.position.x - gameObject.transform.position.x) / Distx;
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        //move
        float moveZ;
        if ((Distx < warning || hitted == 1) && (Player.transform.position.y - gameObject.transform.position.y < 1f)) {  //follow Player
            moveZ = moveHorizontal * speed;
            moveZ *= Time.deltaTime;
            transform.Translate(moveZ, 0, 0);
        }
        else
        {
            if(timer > 0)
            {
                moveZ = -1 * speed;
                moveZ *= Time.deltaTime;
                transform.Translate(moveZ, 0, 0);
            }
            else 
            {
                moveZ = 1 * speed;
                moveZ *= Time.deltaTime;
                transform.Translate(moveZ, 0, 0);
                
            }
            if (timer < -2.5f)timer = 2.5f;
            timer -= Time.deltaTime;
        }

        //jump
        if ((rb.velocity.y < -0.5f && timerJump > 0.5f) || (Player.transform.position.y - gameObject.transform.position.y > warning/2 && Distx < warning && timerJump > 0.5f))
        {
            rb.AddForce(Vector3.up * 850.0f);
            animator.SetBool("isJumping", true);
            timerJump = 0;
            Debug.Log("jump");
        }
        else animator.SetBool("isJumping", false);

        //animation
        Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (moveZ * transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        if (moveZ < 0 && currentVelocity.x <= 0)
        {
            // animator.SetInteger("DirectionX", -1);
            Dir = -1;
            //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocity.x - 0.1f, currentVelocity.y);// for ice
        }
        else if (moveZ > 0 && currentVelocity.x >= 0)
        {
            Dir = 1;
            // animator.SetInteger("DirectionX", 1);
            //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocity.x + 0.1f, currentVelocity.y);// for ice
        }
        else
        {
            // animator.SetInteger("DirectionX", 0);
        }

        //HP UI
        if(hitted == 1)
        {
            //MonsterHP.gameObject.SetActive(true);
            //MonsterHP.value = HP / maxHP;
            HPBar.transform.GetChild(0).gameObject.transform.localPosition = new Vector3( (1 - (HP / maxHP)) * -18.4f, 0, 0);
        }
        
        
        if(HP <= 0) {
            // animator.SetInteger("Dead", 1);
            Destroy(gameObject);
        }
    }

    //Hitted
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PetAttack"))
        {
            //屬性相剋
            float HurtNum;
            if (Monsterfire == 1)
            {
                if (other.GetComponent<Attack_far>().fire == 1) HurtNum = other.GetComponent<Attack_far>().Attacknum * 1;
                else if (other.GetComponent<Attack_far>().water == 1) HurtNum = other.GetComponent<Attack_far>().Attacknum * 1.2f;
                else if (other.GetComponent<Attack_far>().wind == 1) HurtNum = other.GetComponent<Attack_far>().Attacknum * 0.8f;
                else HurtNum = other.GetComponent<Attack_far>().Attacknum;
                HP -= HurtNum;

                GameObject text = GameObject.Instantiate(HurtText);
                text.transform.parent = GameObject.Find("Canvas").transform;
                text.transform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(0, 20, 0);
                text.GetComponent<Text>().text = other.GetComponent<Attack_far>().Attacknum.ToString();

            }
            else if (Monsterwater == 1)
            {
                if (other.GetComponent<Attack_far>().fire == 1) HurtNum = other.GetComponent<Attack_far>().Attacknum * 0.8f;
                else if (other.GetComponent<Attack_far>().water == 1) HurtNum = other.GetComponent<Attack_far>().Attacknum * 1;
                else if (other.GetComponent<Attack_far>().wind == 1) HurtNum = other.GetComponent<Attack_far>().Attacknum * 1.2f;
                else HurtNum = other.GetComponent<Attack_far>().Attacknum;
                HP -= HurtNum;

                GameObject text = GameObject.Instantiate(HurtText);
                text.transform.parent = GameObject.Find("Canvas").transform;
                text.transform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(0, 20, 0);
                text.GetComponent<Text>().text = other.GetComponent<Attack_far>().Attacknum.ToString();
            }
            else if (Monsterwind == 1)
            {
                if (other.GetComponent<Attack_far>().fire == 1) HurtNum = other.GetComponent<Attack_far>().Attacknum * 1.2f;
                else if (other.GetComponent<Attack_far>().water == 1) HurtNum = other.GetComponent<Attack_far>().Attacknum * 0.8f;
                else if (other.GetComponent<Attack_far>().wind == 1) HurtNum = other.GetComponent<Attack_far>().Attacknum * 1f;
                else HurtNum = other.GetComponent<Attack_far>().Attacknum;
                HP -= HurtNum;

                GameObject text = GameObject.Instantiate(HurtText);
                text.transform.parent = GameObject.Find("Canvas").transform;
                text.transform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(0, 20, 0);
                text.GetComponent<Text>().text = other.GetComponent<Attack_far>().Attacknum.ToString();
            }
            // animator.SetInteger("Hitted", 1);
            hitted = 1;
            other.GetComponent<Attack_far>().hitted = 1;
            Destroy(other);
        }
        // else animator.SetInteger("Hitted", 0);

    }
}
