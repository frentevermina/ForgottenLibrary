using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [Tooltip("Velocidad de movimiento del enemigo")]
    [SerializeField] private float speed = 1;

    private bool isMoving;

    private Rigidbody2D _rigidbody2D;
    [Tooltip("Tiempo que tarda el npc entre pasos sucesivos (durante cuánto tiempo se detiene)")]
    [SerializeField]private float timeBetweenSteps;
    private float timeBetweenStepsCounter;
    [Tooltip("Tiempo que tarda el npc en dar un paso (cuánto tiempo se estará moviendo)")]
    [SerializeField] private float timeToMakeStep;
    private float timeToMakeStepCounter;

    public Vector2 directionToMove;

    private Animator _animator;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0f, 2.0f); 
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0f, 2.0f);

    }

    void Update()
    {
        if (isMoving)
        {
            timeToMakeStepCounter -= Time.deltaTime;
            _rigidbody2D.velocity = directionToMove * speed;
            if(timeToMakeStepCounter < 0)
            {
                isMoving = false;
                timeBetweenStepsCounter = timeBetweenSteps;
                _rigidbody2D.velocity = Vector2.zero;

            }
        }
        else
        {
            timeBetweenStepsCounter -= Time.deltaTime;
            if(timeBetweenStepsCounter < 0)
            {
                isMoving = true;
                timeToMakeStepCounter = timeToMakeStep;
                directionToMove = new Vector2(Random.Range(-1, 2),
                                              Random.Range(-1, 2));
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isMoving)
        {

            _animator.SetBool("IsMoving", false);
            _animator.SetFloat("Last_H", directionToMove.x);
            _animator.SetFloat("Last_V", directionToMove.y);
        }



        if (isMoving)
        {

            _animator.SetBool("IsMoving", true);
            _animator.SetFloat("Horizontal", directionToMove.x);
            _animator.SetFloat("Vertical", directionToMove.y);
        }

        if (isMoving && directionToMove.x.Equals(0) && directionToMove.y.Equals(0))
        {
            _animator.SetBool("IsMoving", false);
            _animator.SetFloat("Last_H", directionToMove.x);
            _animator.SetFloat("Last_V", directionToMove.y);
        }
    }





}
