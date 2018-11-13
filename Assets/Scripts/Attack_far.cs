using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_far : MonoBehaviour {

    public int fire, water, wind;
    public float Attacknum = 0;
    public int AttackDir = 1;
    public float maxDis = 5;
    public int hitted = 0;
    public int far = 0;

    private float Orix, Disx = 0;
    private float timer = 0;
    void Start () {
        Orix = gameObject.transform.position.x;
        maxDis = 5;
    }

    void Update() {
        if (far == 1) { 
            gameObject.transform.position += new Vector3(0.2f * AttackDir, 0, 0);
            Disx = Mathf.Abs(Orix - gameObject.transform.position.x);
            if (Disx > maxDis || hitted ==1) Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > 1f) Destroy(gameObject);
        }
    }
}
