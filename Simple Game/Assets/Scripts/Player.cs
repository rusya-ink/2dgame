using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player variables
	[SerializeField]private float maxSpeed = 5f;
	private float curSpeed;
	[SerializeField]float jumpForce = 450f;
	private Direction direction = Direction.Right;
	private Transform _transform;
	private Rigidbody2D _rigidbody2D;
	private CapsuleCollider2D _collider;
	private Animator _animator;

    //Gems like score
	private Color checkedFalse = new Color(29.0f/255.0f, 39.0f/255.0f, 58.0f/255.0f);
	private Color checkedTrue = Color.white;
	[SerializeField]SpriteRenderer gem1;
	[SerializeField]SpriteRenderer gem2;
	[SerializeField]SpriteRenderer gem3;

    //Key and door animations
	[SerializeField]Animator doorAnimator;
	[SerializeField]Animator keyAnimator;

    [SerializeField]Transform respawn;

    //For checking availability of jumps
	[SerializeField]LayerMask groundLayer;
	[SerializeField]float groundTriggerRadius = 0.23f;
	[SerializeField]Transform groundTrigger; 

    //state variables
    private bool doorOpened=false;
    private bool win = false;
    private bool die = false;
    enum Direction{Right, Left}

    // Start is called before the first frame update
    void Start()
    {
    	_collider = GetComponent<CapsuleCollider2D>();
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        direction = transform.localScale.x > 0 ? Direction.Right : Direction.Left;
        gem1.color = checkedFalse;
        gem2.color = checkedFalse;
        gem3.color = checkedFalse;
    }

    // Update is called once per frame
    void Update()
    {
    	if(!win&&!die){
	    	curSpeed = Input.GetAxis("Horizontal")*maxSpeed;
	    	if(!Physics2D.OverlapCircle(groundTrigger.position, groundTriggerRadius, groundLayer)){
	    		_animator.Play("Jump");
	    	}else if(curSpeed==0){
	    		_animator.Play("Idle");
	    	}else{
	    		_animator.Play("Walk");
	    	}
	    	if (Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W)){
	        	Jump();
	        }
	    }else{
	    	curSpeed=0;
	    }
    }

    void FixedUpdate()
    {
    	Move();
    }

    //Respawn with movement lock
    IEnumerator respawnAfterTime(float time)
    {
    	_rigidbody2D.constraints = 
    	RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        //Wait for 2 seconds
        yield return new WaitForSecondsRealtime(time);
        _rigidbody2D.velocity=Vector2.zero;
        _rigidbody2D.MovePosition(respawn.position);
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        die=false;

    }

    //game end
    IEnumerator endAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Application.Quit();
    }

    //All player triggers
    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag=="DeathLine"){
        	die=true;
        	_animator.Play("Die");
        	StartCoroutine(respawnAfterTime(2.0f));
        }else if(other.tag=="Gem"){
        	if(gem1.color==checkedFalse){
        		gem1.color = checkedTrue;
        	}else if(gem2.color==checkedFalse){
				gem2.color = checkedTrue;
    		}else if(gem3.color==checkedFalse){
    			gem3.color = checkedTrue;
    		}
    		Destroy (other.gameObject);
        }else if(other.tag=="Potion"){
        	_transform.localScale = new Vector3(_transform.localScale.x*0.75f, _transform.localScale.y*0.75f, 
        		_transform.localScale.z);
        	Destroy (other.gameObject);
        }else if(other.tag=="Key"){
        	keyAnimator.Play("LevelOpened");
        	doorAnimator.Play("OpenDoor");
        	doorOpened = true;
        }else if(other.tag=="Door"){
        	if(doorOpened){
        		win=true;
        		_animator.Play("Win");
                StartCoroutine(endAfterTime(3.0f));
				Time.timeScale = 0;
        	}
        }else if(other.tag=="Enemy"){
        	die=true;
        	_animator.Play("Die");
        	StartCoroutine(respawnAfterTime(2.0f));
        }
        
    }
    
    //jump
    public void Jump(){
    	if(Physics2D.OverlapCircle(groundTrigger.position, groundTriggerRadius, groundLayer)){
    		_rigidbody2D.AddForce(Vector2.up*jumpForce);
    	}
    }

    //moving and direction control
    private void Move(){
    	if(curSpeed<0&&direction==Direction.Right){
    		_transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
			direction = Direction.Left;
    	}else if(curSpeed>0&&direction==Direction.Left){
    		_transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
			direction = Direction.Right;
    	}
    	_rigidbody2D.velocity = new Vector2(curSpeed, _rigidbody2D.velocity.y);
    }


    
  
}
