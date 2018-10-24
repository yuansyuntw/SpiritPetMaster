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
    private float HP, Dist;

    public GameObject Player;
    public Slider MonsterHP;

    public Monster01_Controller(int _id) : base(_id)
    {

    }

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        Dir = -1;
        HP = maxHP;
    }
	
	void Update () {
        Dist = Mathf.Abs(Player.transform.position.x - gameObject.transform.position.x);
        float moveHorizontal = (Player.transform.position.x - gameObject.transform.position.x) / Dist;
        //move
        if (Dist < 3) {  
            float moveZ = moveHorizontal * speed;
            moveZ *= Time.deltaTime;
            transform.Translate(moveZ, 0, 0);
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
            HP -= 50;
            animator.SetInteger("Hitted", 1);
            Debug.Log(other.tag + HP);
        }
        else animator.SetInteger("Hitted", 0);

        Debug.Log(other.name);
    }
}
