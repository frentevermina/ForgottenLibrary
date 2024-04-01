using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController2 : MonoBehaviour
{
    [Tooltip("Velocidad de movimiento del enemigo")]
    [SerializeField] private float speed = 1.0f;

    private bool isMoving;

    private Rigidbody2D _rigidbody2D;
    [Tooltip("Tiempo que tarda el npc entre pasos sucesivos (durante cuánto tiempo se detiene)")]
    [SerializeField] private float walkTime = 1.5f;
    private float walkCounter;
    [Tooltip("Tiempo que tarda el npc en dar un paso (cuánto tiempo se estará moviendo)")]
    [SerializeField] private float waitTime = 4.0f;
    private float waitCounter;
    [SerializeField] private int currentDirection;
    [SerializeField] private BoxCollider2D villagerZone;
    [SerializeField] private int forbiddenDirection= -1;
    [SerializeField] private Vector2 lastDirection;


    public List<Vector2> walkingDirections;

    private Animator _animator;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        walkCounter = walkTime; 
        waitCounter = waitTime;
        
    }

    private void Start()
    {                
            walkingDirections.Add(Vector2.up);
            walkingDirections.Add(Vector2.down);
            walkingDirections.Add(Vector2.left);
            walkingDirections.Add(Vector2.right);
        
    }
 
    private void FixedUpdate()
    {
        if (isMoving)
        {
            forbiddenDirection = currentDirection;

            if (this.transform.position.x < villagerZone.bounds.min.x ||
               this.transform.position.x > villagerZone.bounds.max.x ||
               this.transform.position.y < villagerZone.bounds.min.y ||
               this.transform.position.y > villagerZone.bounds.max.y)
            {
                if (isMoving)
                {
                    StopWalking();
                }
                else
                {
                    _rigidbody2D.velocity = -walkingDirections[currentDirection] * speed;
                }
            }
            walkCounter -= Time.fixedDeltaTime;
            _rigidbody2D.velocity = walkingDirections[currentDirection] * speed;
            if (walkCounter < 0)
            {
                StopWalking();
            }            
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
            waitCounter -= Time.fixedDeltaTime;
            if(waitCounter < 0)
            {
                StartWalking();
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMoving)
        {
            StopWalking();
        }
        else
        {
            _rigidbody2D.velocity = -walkingDirections[currentDirection] * speed;
        }
    }

    public void StartWalking()
    {
        if(forbiddenDirection == currentDirection)
        {

            currentDirection = Random.Range(0, walkingDirections.Count);
            if(currentDirection == forbiddenDirection)
            {
                StopWalking();
                return;
            }
            else 
            {
                isMoving = true;
                walkCounter = walkTime;
                AnimatorStart();
                forbiddenDirection = -1;
            }
            
            
        }

       

        if (forbiddenDirection != currentDirection)
        {
            
            currentDirection = Random.Range(0, walkingDirections.Count);
            isMoving = true;
            walkCounter = walkTime;
            AnimatorStart();
        }      
    }

    public void StopWalking()
    {
        isMoving = false;
        waitCounter = waitTime;

        AnimatorStop();

        _rigidbody2D.velocity = Vector2.zero;
    
    }



    private void AnimatorStart()
    {
        _animator.SetBool("IsMoving", true);
        _animator.SetFloat("Horizontal", walkingDirections[currentDirection].x);
        _animator.SetFloat("Vertical", walkingDirections[currentDirection].y);
    }

    private void AnimatorStop()
    {
        _animator.SetBool("IsMoving", false);
        _animator.SetFloat("Last_H", walkingDirections[currentDirection].x);
        _animator.SetFloat("Last_V", walkingDirections[currentDirection].y);
    }

}
