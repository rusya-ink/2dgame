using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    //Demon system's variables
	[SerializeField]GameObject point1;
	[SerializeField]GameObject point2;
	private Transform t;
	private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(1.3f*(t.localScale.x>0 ? 1 : -1), rb.velocity.y);
    }

    //change direction
    private void OnTriggerEnter2D(Collider2D other){
    	if(other.gameObject==point1||other.gameObject==point2){
    		t.localScale = new Vector3(-t.localScale.x, t.localScale.y, t.localScale.z);
    	}
    }
}
