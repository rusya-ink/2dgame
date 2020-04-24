using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour
{	
    //fire attack variables
	private Rigidbody2D rb;
    private int dir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = GetComponent<Transform>().localScale.x>0 ? -1 : 1;
    }
    

    // Update is called once per frame (move)
    void Update()
    {
        rb.velocity = Vector2.left*2*dir;
    }

    //destroy then out of border
    private void OnTriggerEnter2D(Collider2D other){

    	if(other.tag=="DeathLine"){
    		Destroy(gameObject);
    	}
    }
}
