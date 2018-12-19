using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpiritMonsterMaster;
using TMPro;

public class Boss01_Controller : Monster
{
    [SerializeField]
    private Animator animator;
    private Rigidbody2D rb;
    private int Dir = 1;
    private float HP, Distx, Disty, timer, timerJump;
    private int hitted = 0;

    public GameObject Player;
    public Slider MonsterHP;
    public GameStageController gamestage;
    public GameObject HurtText, BoomHurtText;
    public float force;
    public int EnvironmentType;


    public Boss01_Controller(int _id) : base(_id)
    {

    }

    void Start()
    {
        //change to read file here 
        //warning = 5f;
        //Attacknum = 10;
        //speed = 1.5f;
        //maxHP = 100;
        //Monsterwind = 1;

        rb = GetComponent<Rigidbody2D>();
        Dir = -1;
        HP = maxHP;
        timer = 2.5f;
        MonsterHP.gameObject.SetActive(false);
        Random.seed = System.Guid.NewGuid().GetHashCode();
    }

    void Update()
    {
        if(gamestage.Gameover == 1 || gamestage.stop == 1) return;
        if (gamestage.Gameover == 2)
        {
            animator.SetInteger("BossWin", 1);
            return;
        }

        if (Mathf.Abs(rb.velocity.y) < 0.05f) timerJump += Time.deltaTime;

        Distx = Mathf.Abs(Player.transform.position.x - gameObject.transform.position.x);
        Disty = Mathf.Abs(Player.transform.position.y - gameObject.transform.position.y);
        float moveHorizontal = (Player.transform.position.x - gameObject.transform.position.x) / Distx;
        float moveVer = (Player.transform.position.y - gameObject.transform.position.y) / Disty;
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        //move
        float moveZ, moveY;
        if ((Distx < warning || hitted == 1) && (Disty < warning))
        {  //follow Player
            moveZ = moveHorizontal * Random.Range(speed - 1, speed + 1); ;
            moveY = moveVer * Random.Range(speed - 1, speed + 1);
            moveZ *= Time.deltaTime;
            moveY *= Time.deltaTime;
            if(EnvironmentType == 0)transform.Translate(moveZ, 0, 0);
            else if (EnvironmentType == 1) transform.Translate(moveZ, moveY, 0);//water
        }
        else
        {
            if (timer > 0)
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
            if (timer < -2.5f) timer = 2.5f;
            timer -= Time.deltaTime;
        }

        //jump
        if ((rb.velocity.y < -0.5f && timerJump > 0.5f) || (Player.transform.position.y - gameObject.transform.position.y > warning / 2 && Distx < warning && timerJump > 0.5f))
        {
            rb.AddForce(Vector3.up * force);
            animator.SetBool("isJumping", true);
            timerJump = 0;
        }
        else animator.SetBool("isJumping", false);

        //animation
        Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (moveZ * transform.localScale.x < 0)
        {
            if (Distx > 1f || Distx < -1f) transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
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
        if (hitted == 1 )
        {
            MonsterHP.gameObject.SetActive(true);
            MonsterHP.value = HP / maxHP;
        }


        if (HP <= 0)
        {
            // animator.SetInteger("Dead", 1);
            gamestage.Gameover = 1;//win
            Destroy(gameObject);
        }
    }

    //Hitted
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PetAttack"))
        {
            //屬性相剋
            float HurtNum = 0;
            int i = Random.Range(0, (int)(other.GetComponent<Attack_far>().Attacknum * 0.5f));
            if (Monsterfire == 1)
            {
                if (other.GetComponent<Attack_far>().fire == 1) HurtNum = (other.GetComponent<Attack_far>().Attacknum + i) * 1;
                else if (other.GetComponent<Attack_far>().water == 1) HurtNum = (other.GetComponent<Attack_far>().Attacknum + i) * 1.2f;
                else if (other.GetComponent<Attack_far>().wind == 1) HurtNum = (other.GetComponent<Attack_far>().Attacknum + i) * 0.8f;
                else HurtNum = other.GetComponent<Attack_far>().Attacknum + i;
                HP -= HurtNum;
            }
            else if (Monsterwater == 1)
            {
                if (other.GetComponent<Attack_far>().fire == 1) HurtNum = (other.GetComponent<Attack_far>().Attacknum + i) * 0.8f;
                else if (other.GetComponent<Attack_far>().water == 1) HurtNum = (other.GetComponent<Attack_far>().Attacknum + i) * 1;
                else if (other.GetComponent<Attack_far>().wind == 1) HurtNum = (other.GetComponent<Attack_far>().Attacknum + i) * 1.2f;
                else HurtNum = other.GetComponent<Attack_far>().Attacknum + i;
                HP -= HurtNum;
            }
            else if (Monsterwind == 1)
            {
                if (other.GetComponent<Attack_far>().fire == 1) HurtNum = (other.GetComponent<Attack_far>().Attacknum + i) * 1.2f;
                else if (other.GetComponent<Attack_far>().water == 1) HurtNum = (other.GetComponent<Attack_far>().Attacknum + i) * 0.8f;
                else if (other.GetComponent<Attack_far>().wind == 1) HurtNum = (other.GetComponent<Attack_far>().Attacknum + i) * 1f;
                else HurtNum = other.GetComponent<Attack_far>().Attacknum + i;
                HP -= HurtNum;
            }
            else
            {
                HurtNum = other.GetComponent<Attack_far>().Attacknum + i;
                HP -= HurtNum;
            }

            GameObject text = GameObject.Instantiate(HurtText);
            text.transform.parent = GameObject.Find("Canvas").transform;
            text.transform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(50, 150, 0);
            text.GetComponent<TextMeshProUGUI>().text = ((int)HurtNum).ToString();

            //back
            /*float Dist = Mathf.Abs(gameObject.transform.position.x - other.transform.position.x);
            float moveHorizontal = (gameObject.transform.position.x - other.transform.position.x) / Dist;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.AddForce(new Vector3(1, 0, 0) * moveHorizontal * 250);*/

            // animator.SetInteger("Hitted", 1);
            hitted = 1;
            other.GetComponent<Attack_far>().hitted = 1;
            Destroy(other);
        }
        // else animator.SetInteger("Hitted", 0);
       
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //water
        if (other.gameObject.GetComponent<Boom>().BoomHitted == 0 && other.gameObject.CompareTag("Boom") && other.gameObject.GetComponent<Boom>().Booming == true)
        {
            other.gameObject.GetComponent<Boom>().BoomHitted = 1;
            int HurtNum;
            if (other.gameObject.GetComponent<Boom>().Type == 1) HurtNum = 50;
            else HurtNum = 20;
            HP -= HurtNum;
            GameObject text = GameObject.Instantiate(BoomHurtText);
            text.transform.parent = GameObject.Find("Canvas").transform;
            text.transform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(50, 30, 0);
            text.GetComponent<TextMeshProUGUI>().text = (HurtNum).ToString();
        }
    }
}
