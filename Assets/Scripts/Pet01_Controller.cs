﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpiritPetMaster;

public class Pet01_Controller : Pet {
    [SerializeField]
    private Animator animator;
    private Rigidbody2D rb;
    private float timerJump = 0.6f;
    private int Dir = 1;
    private float timerRecover = 0;
    private float HP, MP;

    public GameObject Attackfire;
    public Slider PlayerHP, PlayerMP;
    

    public Pet01_Controller(int _id) : base(_id)
    {
        
    }

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        Dir = 1;
        MP = maxMP;
        HP = maxHP;
    }

    void FixedUpdate()
    {
        timerJump += Time.deltaTime;
        timerRecover += Time.deltaTime;

        //move
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveZ = moveHorizontal * speed;
        moveZ *= Time.deltaTime;
        transform.Translate(moveZ, 0, 0);

        if (Input.GetKeyDown(KeyCode.J) && timerJump > 0.25f)
        {
            rb.AddForce(Vector3.up * 150.0f);
            //transform.Translate(Vector3.up * 10.0f);
            timerJump = 0;
            animator.SetInteger("Jump", 1);
        }
        else animator.SetInteger("Jump", 0);

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

        //MPHP Recover
        if(timerRecover > 1)
        {
            if (MP + MP * MPRecover < maxMP) MP +=  MP * MPRecover;
            if (HP + HP * HPRecover < maxHP) HP += HP * HPRecover;
            timerRecover = 0;
        }

        //MPHP UI
        PlayerHP.value = HP / maxHP;
        PlayerMP.value = MP / maxMP;

        //attack
        if (Input.GetKeyDown(KeyCode.Q) && MP - 10 >= 0)
        {
            MP -= 10;
            Quaternion rot;
            if (Dir == 1) rot = Quaternion.Euler(0, 0, 125);
            else rot = Quaternion.Euler(0, 0, -45);
            GameObject fires =  Instantiate(Attackfire, transform.position, rot);
            fires.GetComponent<Attack_far>().fire = 1;
            fires.GetComponent<Attack_far>().Attacknum = PetfireAttack * 0.1f;
            fires.GetComponent<Attack_far>().AttackDir = Dir;
            animator.SetInteger("Fire", 1);
        }
        else animator.SetInteger("Fire", 0);




    }

    //Hitted
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            HP -= 10;
            animator.SetInteger("Hitted", 1);
        }
       else animator.SetInteger("Hitted", 0);

        Debug.Log(other.collider.name);
    }

}


