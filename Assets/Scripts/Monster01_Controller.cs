using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpiritMonsterMaster;
using TMPro;

public class Monster01_Controller : Monster {
    [SerializeField]
    private Animator animator;
    private Rigidbody2D rb;
    private int Dir = 1;
    private float HP, Distx, Disty, timer, timerJump;
    private int hitted = 0;
    private GameObject HPBar;

    public GameObject Player;
    public GameStageController gamestage;
    public GameObject HurtText;
    public GameObject NewHPBar;
    public float force;
    public float size;

    public Monster01_Controller(int _id) : base(_id)
    {

    }

    void Start () {
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

        //GameObject NewHP = Instantiate(HPBar, transform.position, Quaternion.identity);
        //NewHP.transform.SetParent(gameObject.transform);
        //MonsterHP = NewHP.GetComponent<Slider>();
        HPBar = Instantiate(NewHPBar);
        HPBar.transform.SetParent(gameObject.transform);
        HPBar.transform.localScale = new Vector3(size, size, 1);
        HPBar.transform.localPosition = new Vector3(0, 0.15f, 0);
        //HPBar = gameObject.transform.GetChild(0).gameObject;
        animator = this.GetComponent<Animator>();
        Random.seed = System.Guid.NewGuid().GetHashCode();
    }
	
	void Update () {
        if (gamestage.Gameover == 2 || gamestage.Gameover == 1 || gamestage.stop == 1)
        {
            return;
        }

        if (Mathf.Abs(rb.velocity.y) < 0.05f)timerJump += Time.deltaTime;

        Distx = Mathf.Abs(Player.transform.position.x - gameObject.transform.position.x);
        Disty = Mathf.Abs(Player.transform.position.y - gameObject.transform.position.y);
        float moveHorizontal = (Player.transform.position.x - gameObject.transform.position.x) / Distx;
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        //move
        float moveZ;
        if ((Distx < warning || hitted == 1) && (Disty < warning/2)) {  //follow Player
            moveZ = moveHorizontal * Random.Range(speed - 1, speed + 1);;
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
        if (timerJump > 1f ||(rb.velocity.y < -0.5f && timerJump > 0.5f) || (Player.transform.position.y - gameObject.transform.position.y > warning/2 && Distx < warning && timerJump > 0.5f))
        {
            if(timerJump > 1f) rb.AddForce(Vector3.up * force/2);
            else rb.AddForce(Vector3.up * force);
            animator.SetBool("isJumping", true);
            timerJump = 0;
        }
        else animator.SetBool("isJumping", false);

        //animation
        Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (moveZ * transform.localScale.x < 0)
        {
            if (Distx > 0.5f || Distx < -0.5f) transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            HPBar.transform.localScale = new Vector3(-HPBar.transform.localScale.x, HPBar.transform.localScale.y, 1);
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
            gamestage.Killnum++;
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
            text.transform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(50, 30, 0);
            text.GetComponent<TextMeshProUGUI>().text = ((int)HurtNum).ToString();

            //back
            float Dist = Mathf.Abs(gameObject.transform.position.x - other.transform.position.x);
            float moveHorizontal = (gameObject.transform.position.x - other.transform.position.x) / Dist;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.AddForce(new Vector3(1, 0, 0) * moveHorizontal * 250);

            // animator.SetInteger("Hitted", 1);
            hitted = 1;
            other.GetComponent<Attack_far>().hitted = 1;
            Destroy(other);
        }
        // else animator.SetInteger("Hitted", 0);

    }
}
