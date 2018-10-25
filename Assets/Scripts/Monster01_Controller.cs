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
    private float HP, Distx, timer;
    private int hitted = 0;

    public GameObject Player;
    public Slider MonsterHP;

    public Monster01_Controller(int _id) : base(_id)
    {

    }

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        Dir = -1;
        HP = maxHP;
        timer = 2.5f;
    }
	
	void Update () {
        Distx = Mathf.Abs(Player.transform.position.x - gameObject.transform.position.x);
        float moveHorizontal = (Player.transform.position.x - gameObject.transform.position.x) / Distx;
        //move
        if (Distx < 5 || hitted == 1) {  
            float moveZ = moveHorizontal * speed;
            moveZ *= Time.deltaTime;
            transform.Translate(moveZ, 0, 0);
        }
        else
        {
            if(timer > 0)
            {
                float moveZ = -1 * speed;
                moveZ *= Time.deltaTime;
                transform.Translate(moveZ, 0, 0);
            }
            else 
            {
                float moveZ = 1 * speed;
                moveZ *= Time.deltaTime;
                transform.Translate(moveZ, 0, 0);
                
            }
            if (timer < -2.5f)timer = 2.5f;
            timer -= Time.deltaTime;
        }

        //animation
        Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (moveHorizontal < 0 && currentVelocity.x <= 0)
        {
            animator.SetInteger("DirectionX", -1);
            Dir = -1;
            //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocity.x - 0.1f, currentVelocity.y);// for ice
        }
        else if (moveHorizontal > 0 && currentVelocity.x >= 0)
        {
            Dir = 1;
            animator.SetInteger("DirectionX", 1);
            //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocity.x + 0.1f, currentVelocity.y);// for ice
        }
        else
        {
            animator.SetInteger("DirectionX", 0);
        }

        //HP UI
        MonsterHP.value = HP / maxHP;

    }

    //Hitted
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PetAttack"))
        {
            //屬性相剋
            if (Monsterfire == 1)
            {
                if(other.GetComponent<Attack_far>().fire == 1) HP -= other.GetComponent<Attack_far>().Attacknum * 1;
                else if (other.GetComponent<Attack_far>().water == 1) HP -= other.GetComponent<Attack_far>().Attacknum * 0.8f;
                else if (other.GetComponent<Attack_far>().wind == 1) HP -= other.GetComponent<Attack_far>().Attacknum * 1.2f;
            }
            else if (Monsterwater == 1)
            {
                if (other.GetComponent<Attack_far>().fire == 1) HP -= other.GetComponent<Attack_far>().Attacknum * 1.2f;
                else if (other.GetComponent<Attack_far>().water == 1) HP -= other.GetComponent<Attack_far>().Attacknum * 1;
                else if (other.GetComponent<Attack_far>().wind == 1) HP -= other.GetComponent<Attack_far>().Attacknum * 0.8f;
            }
            else if (Monsterwind == 1)
            {
                if (other.GetComponent<Attack_far>().fire == 1) HP -= other.GetComponent<Attack_far>().Attacknum * 0.8f;
                else if (other.GetComponent<Attack_far>().water == 1) HP -= other.GetComponent<Attack_far>().Attacknum * 1.2f;
                else if (other.GetComponent<Attack_far>().wind == 1) HP -= other.GetComponent<Attack_far>().Attacknum * 1f;
            }

            animator.SetInteger("Hitted", 1);
            hitted = 1;
        }
        else animator.SetInteger("Hitted", 0);

    }
}
