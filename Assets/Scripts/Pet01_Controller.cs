using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpiritPetMaster;
using TMPro;

public class Pet01_Controller : Pet {
    [SerializeField]
    private Animator animator;
    private Rigidbody2D rb;
    private float timerJump = 0.6f;
    private int Dir = 1;
    private float timerRecover = 0;
    private float timerAttackfire = 0, timerAttackwind = 0, timerAttackwater = 0, timerBetweenAttacks = 0;
    private float timerAttack = 0;
    public float HP, MP;
    private bool isJump = false;
    private bool isDoubleJump = false;
    private bool isFalling = false;
    private GameObject Plane;
    private float PetSpeedInScenes;
    private int GameFinish = 0;

    public GameObject Attackfire, Attackwind, Attackwater, SlimeAttack, OwlAttack, MuchroomAttack;
    public Slider PlayerHP, PlayerMP;
    public GameStageController gamestage;
    public float force;
    public GameObject HurtText;
    public int EnvironmentType;
    public float SpeedValue;
    public float JumpForceINWater = 5f;
    public float JumpDownSpeed = 1.0f;
    public RuntimeAnimatorController Slime, Owl, Muchroom; 


    void Start () {
        //change to read file here
        //NewPet(524, 1, "01");
        //LoadPet(524);
        //Speed = 2;

        if(PlayerData.instance != null)
        {
            LoadPet(PlayerData.instance.GetPlayerFocusPetId());
            Debug.Log(PlayerData.instance.GetPlayerFocusPetId());

            SaveData();
        }

        /*Speed = 5;
        MaxHP = 200;
        MaxMP = 200;
        MPRecover = 0.01f;
        HPRecover = 0.01f;
        PetfireAttack = 100;
        PetwaterAttack = 100;
        PetwindAttack = 100;*/

        rb = GetComponent<Rigidbody2D>();
        Dir = 1;
        MP = MaxMP;
        HP = MaxHP;
        isJump = false;
        isDoubleJump = false;
        PetSpeedInScenes = Speed * SpeedValue;
        Random.seed = System.Guid.NewGuid().GetHashCode();
        GameFinish = 0;
        //Kind = "Muchroom";
        if (Kind == "Slime") this.GetComponent<Animator>().runtimeAnimatorController = Slime as RuntimeAnimatorController;
        else if(Kind == "Owl") this.GetComponent<Animator>().runtimeAnimatorController = Owl as RuntimeAnimatorController;
        else if(Kind == "Muchroom") this.GetComponent<Animator>().runtimeAnimatorController = Muchroom as RuntimeAnimatorController;
    }

    void Update()
    {
        if (gamestage.Gameover == 1)//win
        {
            // if (GameFinish == 0)
            // {
            //     Exp = gamestage.Killnum * 10 + 200;
            //     SaveData();
            //     GameFinish = 1;
            // }
            return;
        }
        if (gamestage.Gameover == 2 || gamestage.stop == 1)//lose
        {
            return;
        }

        timerJump += Time.deltaTime;
        timerRecover += Time.deltaTime;
        timerAttackfire += Time.deltaTime;
        timerAttackwind += Time.deltaTime;
        timerAttackwater += Time.deltaTime;
        timerAttack += Time.deltaTime;
        timerBetweenAttacks += Time.deltaTime;

        if (HP <= 0)
        {
            HP = 0;
            animator.SetInteger("Dead", 0);
            gamestage.Gameover = 2;//lose
            return;
        }

        //move
        float moveHorizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        float moveZ = moveHorizontal * PetSpeedInScenes;
        moveZ *= Time.deltaTime;
        transform.Translate(moveZ, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && timerJump > 0.2f)
        {
            if (!isJump)//如果还在跳跃中，则不重复执行 
            {
                if (EnvironmentType == 1) rb.AddForce(Vector3.up * force * JumpForceINWater);
                else rb.AddForce(Vector3.up * force);
                isJump = true;
                animator.SetBool("isJumping", true);
                Debug.Log("Jump");
                timerJump = 0;
            }
            else
            {
                //for water only (can always jump)
                if (EnvironmentType == 1)
                {
                    if (timerJump > 0.2f)
                    {
                        isDoubleJump = true;
                        rb.velocity = Vector2.zero;
                        rb.angularVelocity = 0;
                        rb.AddForce(Vector3.up * force * JumpForceINWater);
                        animator.SetBool("isJumping", true);
                        Debug.Log("SwimJump");
                        timerJump = 0;
                    }
                }
                //for water only (can always jump)

                if (isDoubleJump)//判断是否在二段跳  
                {
                    return;//否则不能二段跳  
                }
                else if(timerJump > 0.2f)
                {
                    isDoubleJump = true;
                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0;
                    rb.AddForce(Vector3.up * force);
                    animator.SetBool("isJumping", true);
                    Debug.Log("DoubleJump");
                    timerJump = 0;
                }

                
            }
            /*rb.AddForce(Vector3.up * 350.0f);
            //transform.Translate(Vector3.up * 10.0f);
            timerJump = 0;
            animator.SetBool("isJumping", true);*/
        }
        //else animator.SetBool("isJumping", false);

        if(timerJump > 0.2f) animator.SetBool("isJumping", false);

        //JumpDown
        if(EnvironmentType == 0) {//land jump down
            if ( (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && Plane.transform.parent.name == "level")
            {
                Plane.layer = LayerMask.NameToLayer("JumpDownPlane");
                Plane.GetComponent<BoxCollider2D>().usedByEffector = false;
                Debug.Log("JumpDown");
            }
        }
        else
        {
            float moveY = Input.GetAxis("Vertical") * PetSpeedInScenes * JumpDownSpeed;
            if (moveY > 0) moveY = 0;
            moveY *= Time.deltaTime;
            transform.Translate(0, moveY, 0);
        }

        //animation
        Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        if(moveHorizontal * transform.localScale.x < 0)
        {
            transform.localScale = new Vector2 ( -transform.localScale.x, transform.localScale.y);
        }
        if (moveHorizontal < 0 && currentVelocity.x <= 0)
        {
            // animator.SetInteger("DirectionX", -1);
            Dir = -1;
            //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocity.x - 0.1f, currentVelocity.y);// for ice
        }
        else if (moveHorizontal > 0 && currentVelocity.x >= 0)
        {
            Dir = 1;
            // animator.SetInteger("DirectionX", 1);
            //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocity.x + 0.1f, currentVelocity.y);// for ice
        }
        else
        {
            // animator.SetInteger("DirectionX", 0);
        }
        isFalling = currentVelocity.y < -0.2f ? true : false;//jump falling
        if(isFalling) animator.SetInteger("isFalling", animator.GetInteger("isFalling")+1);
        else animator.SetInteger("isFalling", 0);

        //MPHP Recover
        if (timerRecover > 1)
        {
            if (MP + MP * MPRecover < MaxMP) MP +=  MP * MPRecover;
            if (HP + HP * HPRecover < MaxHP) HP += HP * HPRecover;
            timerRecover = 0;
        }

        //MPHP UI
        PlayerHP.value = HP / MaxHP;
        PlayerMP.value = MP / MaxMP;

        //attack
        if (Input.GetKeyDown(KeyCode.Q) && MP - 10 > 0 && timerAttackfire > 0.7f && timerBetweenAttacks > 0.2f)
        {
            MP -= 10;
            Quaternion rot;
            if (Dir == 1) rot = Quaternion.Euler(0, 0, 0);//Quaternion.Euler(0, 0, 125);
            else rot = Quaternion.Euler(0, 0, 180);//Quaternion.Euler(0, 0, -45);
            GameObject fires =  Instantiate(Attackfire, transform.position, rot);
            fires.transform.localScale = new Vector3(1, 1, 1);
            fires.GetComponent<Attack_far>().far = 1;
            fires.GetComponent<Attack_far>().fire = 1;
            fires.GetComponent<Attack_far>().Attacknum = PetAttack * PetfireAttack/100 * 0.1f;
            fires.GetComponent<Attack_far>().AttackDir = Dir;
            animator.SetBool("isAttacking", true);
            timerAttackfire = 0;
            timerBetweenAttacks = 0;
        }
        if(timerAttackfire > 0.2f) animator.SetBool("isAttacking", false);

        if (Input.GetKeyDown(KeyCode.W) && MP - 10 > 0 && timerAttackwind > 0.7f && timerBetweenAttacks > 0.2f)
        {
            MP -= 10;
            Quaternion rot;
            if (Dir == 1) rot = Quaternion.Euler(0, 0, 0);//Quaternion.Euler(0, 0, 125);
            else rot = Quaternion.Euler(0, 0, 180);//Quaternion.Euler(0, 0, -45);
            GameObject winds = Instantiate(Attackwind, transform.position, rot);
            winds.transform.localScale = new Vector3(1, 1, 1);
            winds.GetComponent<Attack_far>().far = 1;
            winds.GetComponent<Attack_far>().wind = 1;
            winds.GetComponent<Attack_far>().Attacknum = PetAttack * PetwindAttack/100 * 0.1f;
            winds.GetComponent<Attack_far>().AttackDir = Dir;
            animator.SetBool("isAttacking", true);
            timerAttackwind = 0;
            timerBetweenAttacks = 0;
        }
        if (timerAttackwind > 0.2f) animator.SetBool("isAttacking", false);

        if (Input.GetKeyDown(KeyCode.E) && MP - 10 > 0 && timerAttackwater > 0.7f && timerBetweenAttacks > 0.2f)
        {
            MP -= 10;
            Quaternion rot;
            if (Dir == 1) rot = Quaternion.Euler(0, 0, 0);//Quaternion.Euler(0, 0, 125);
            else rot = Quaternion.Euler(0, 0, 180);//Quaternion.Euler(0, 0, -45);
            GameObject waters = Instantiate(Attackwater, transform.position, rot);
            waters.transform.localScale = new Vector3(1, 1, 1);
            waters.GetComponent<Attack_far>().far = 1;
            waters.GetComponent<Attack_far>().water = 1;
            waters.GetComponent<Attack_far>().Attacknum = PetAttack * PetwaterAttack/100 * 0.1f;
            waters.GetComponent<Attack_far>().AttackDir = Dir;
            animator.SetBool("isAttacking", true);
            timerAttackwater = 0;
            timerBetweenAttacks = 0;
        }
        if (timerAttackwater > 0.2f) animator.SetBool("isAttacking", false);

        if (Input.GetKeyDown(KeyCode.Z) && timerAttack > 0.2f && timerBetweenAttacks > 0.2f)
        {
            GameObject attacks = null;
            if (Kind == "Slime") attacks = Instantiate(SlimeAttack);
            else if (Kind == "Owl") attacks = Instantiate(OwlAttack);
            else if (Kind == "Muchroom") attacks = Instantiate(MuchroomAttack);

            attacks.transform.SetParent(this.transform);
            attacks.transform.localScale = new Vector3(2, 1, 1);
            attacks.transform.localPosition = new Vector3(2f, 0, 0);
            attacks.transform.SetParent(this.transform.parent);
            attacks.GetComponent<Attack_far>().far = 0;
            attacks.GetComponent<Attack_far>().Attacknum = PetAttack * 0.06f;
            attacks.GetComponent<Attack_far>().AttackDir = Dir;
            animator.SetBool("isAttacking", true);
            timerAttack = 0;
            timerBetweenAttacks = 0;
        }
        if(timerAttack > 0.2f) animator.SetBool("isAttacking", false);


    }

    //Hitted
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gamestage.stop == 1) return;

        if (other.gameObject.CompareTag("Plane"))
        {//碰撞的是Plane  
            isJump = false;
            isDoubleJump = false;
            if (EnvironmentType  == 0) {//land jump down
                if (Plane == null) Plane = other.gameObject;
                if (Plane != other.gameObject)
                {
                    Plane.layer = LayerMask.NameToLayer("Default");
                    Plane.GetComponent<BoxCollider2D>().usedByEffector = true;
                    Plane = other.gameObject;
                }
            }
        }
        else if (other.gameObject.CompareTag("Monster"))
        {
            int HurtNum;
            if (EnvironmentType == 1 || EnvironmentType == 2) HurtNum = (int)other.gameObject.GetComponent<Monster02_Controller>().Attacknum + Random.Range(0, (int)(other.gameObject.GetComponent<Monster02_Controller>().Attacknum * 0.5f));
            else HurtNum = (int)other.gameObject.GetComponent<Monster01_Controller>().Attacknum + Random.Range(0, (int)(other.gameObject.GetComponent<Monster01_Controller>().Attacknum * 0.5f));
            //HurtNum = (HurtNum<PetDefence) ? 1 : (int)(HurtNum-PetDefence);
            HurtNum = (int)(HurtNum - PetDefence * 0.1f);
            HP -= HurtNum;
            GameObject text = GameObject.Instantiate(HurtText);
            text.transform.parent = GameObject.Find("Canvas").transform;
            text.transform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(80, 30, 0);
            text.GetComponent<TextMeshProUGUI>().text = HurtNum.ToString();
            //back
            float Dist = Mathf.Abs(gameObject.transform.position.x - other.transform.position.x);
            float moveHorizontal = (gameObject.transform.position.x - other.transform.position.x) / Dist;
            rb.velocity = (new Vector2(1, 0) * moveHorizontal * 3);
            animator.SetBool("isDamaged", true);
            animator.SetBool("Damaging", false);
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
            StartCoroutine("Damage");
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            int HurtNum = (int)other.gameObject.GetComponent<Boss01_Controller>().Attacknum + Random.Range(0, (int)(other.gameObject.GetComponent<Boss01_Controller>().Attacknum * 0.5f));
            //HurtNum = (HurtNum<PetDefence) ? 1 : (int)(HurtNum-PetDefence);
            HurtNum = (int)(HurtNum - PetDefence * 0.1f);
            HP -= HurtNum;
            GameObject text = GameObject.Instantiate(HurtText);
            text.transform.parent = GameObject.Find("Canvas").transform;
            text.transform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(80, 30, 0);
            text.GetComponent<TextMeshProUGUI>().text = HurtNum.ToString();
            //back
            float Dist = Mathf.Abs(gameObject.transform.position.x - other.transform.position.x);
            float moveHorizontal = (gameObject.transform.position.x - other.transform.position.x) / Dist;
            rb.velocity = (new Vector2(1, 0) * moveHorizontal * 3);
            animator.SetBool("isDamaged", true);
            animator.SetBool("Damaging", false);
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
            StartCoroutine("Damage");
        }
 //    else animator.SetInteger("Hitted", 0);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Plane")){//碰撞的是Plane  
            //isJump = false;
            //isDoubleJump = false;
            if (EnvironmentType == 0)//land jump down
            {
                if (Plane == null) Plane = other.gameObject;
                if (Plane != other.gameObject)
                {
                    Plane.layer = LayerMask.NameToLayer("Default");
                    Plane.GetComponent<BoxCollider2D>().usedByEffector = true;
                    Plane = other.gameObject;
                }
            }
        }
    }

    IEnumerator Damage()//無敵
    {
        //animator.SetBool("Damaging", true);
        gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        int count = 15;
        while (count > 0)
        {
            GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.05f);
            GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.05f);
            count--;
        }
        animator.SetBool("Damaging", false);
        animator.SetBool("isDamaged", false);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }


}


