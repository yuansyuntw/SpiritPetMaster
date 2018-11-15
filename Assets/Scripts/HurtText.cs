using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HurtText : MonoBehaviour {
    // Use this for initialization
    void Start () {
        Random.seed = System.Guid.NewGuid().GetHashCode();
        int i = Random.Range(0, 150);
        int y = Random.Range(30, 80);
        Destroy(gameObject, 0.5f);
        Sequence mySequence = DOTween.Sequence();
        Tweener move1 = transform.DOMoveY(transform.position.y + y, 0.3f);
        Tweener movex = transform.DOMoveX(transform.position.x + i, 0.3f);
        Tweener scale1 = transform.DOScale(2f, 0.5f);
        //Tweener move2 = transform.DOMoveY(transform.position.y + 80, 0.3f);
        //Color c = gameObject.GetComponent<TextMeshProUGUI>().color;
        //Tweener alpha1 = gameObject.GetComponent<TextMeshProUGUI>().DOFade(0, 0.5f);

        mySequence.Append(move1);
        mySequence.Join(scale1);
        mySequence.Join(movex);
        //mySequence.Append(alpha1);
        // mySequence.AppendInterval(0.01f);

    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

}
