using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    //dragon variables
	private Animator _animator;
	private Rigidbody2D rb;

    //attack time
	private float minTime=0;
	private float maxTime=3.0f;
	private float cooldown=5.0f;
	private float timeBeforeAttack;

    //fire attack variables
	private int parentRotation;
	private GameObject firePrefab;
    [SerializeField]GameObject fireLeftPrefab; 
    [SerializeField]GameObject fireRightPrefab; 

    // Start is called before the first frame update
    void Start()
    {
    	rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        timeBeforeAttack = Random.Range(minTime, maxTime);
        parentRotation = GetComponent<Transform>().localScale.x<0 ? -1 : 1;
        firePrefab  = GetComponent<Transform>().localScale.x<0 ? fireLeftPrefab : fireRightPrefab;

    }

    // Update is called once per frame
    void Update()
    {
    	timeBeforeAttack-=Time.deltaTime;
        if(timeBeforeAttack<=0){
        	Attack();
        	timeBeforeAttack=cooldown+Random.Range(minTime, maxTime);
        }
    }
    
    void Attack(){
    	_animator.Play("Attack");

    	StartCoroutine(createFire());
    }

    //create fire object after animation end
    IEnumerator createFire()
	{
	    //Wait for 2 seconds

	    yield return new WaitForSecondsRealtime(0.85f);

	    Instantiate(firePrefab, rb.position+Vector2.left*(-parentRotation)*1.2f, Quaternion.identity);
 
	}
	
}
