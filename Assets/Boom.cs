using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour {
    private Animator animator;
    public Environment envi;
    private float timer;
    public bool Booming;
    public int Type, BoomHitted;

    // Use this for initialization
    void Start () {
        timer = 0;
        BoomHitted = 0;
        Booming = false;
        animator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 1f && Booming == true) {
            animator.SetBool("Boom", false);  
            Destroy(gameObject);
        } 

        /*if(Type == 1 && envi.BoomNum == 15)
        {
            animator.SetBool("Boom", true);
            GetComponent<BoxCollider2D>().size = new Vector2(0.7f, 0.7f);
            timer = 0;
            Booming = true;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Booming == false && Type == 0 && other.gameObject.CompareTag("PetAttack"))
        {
            animator.SetBool("Boom", true);
            GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.5f);
            envi.BoomNum++;
            timer = 0;
            Booming = true;
        }

        if (Booming == false && envi.BoomNum >= 15 && Type == 1 && other.gameObject.CompareTag("PetAttack"))
        {
            animator.SetBool("Boom", true);
            GetComponent<BoxCollider2D>().size = new Vector2(0.7f, 0.7f);
            timer = 0;
            Booming = true;
        }
    }
}
