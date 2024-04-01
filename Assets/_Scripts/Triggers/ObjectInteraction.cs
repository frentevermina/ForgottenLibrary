using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private bool isOpen;
    
    private bool interact;
    private bool inside;

    private CapsuleCollider2D _capsuleCollider2D;
    private Animator _animator;


    private readonly int OPEN = Animator.StringToHash("Open");
    private readonly int INTERACTEDTOOPEN = Animator.StringToHash("InteractedToOpen");
    private readonly int INTERACTEDTOCLOSE = Animator.StringToHash("InteracedToClose");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && inside)
        {
            StartCoroutine(IEInteraccionPuerta());
        }
        if (interact)
        {
            
            if (!isOpen)
            {
                StartCoroutine(OpenDoor());
            }
            if (isOpen)
            {
               StartCoroutine(CloseDoor());
            }
        }
    }



    private IEnumerator IEInteraccionPuerta()
    {
        interact = true;
        yield return new WaitForSeconds(0.3f);
        interact = false;
    }

    private IEnumerator OpenDoor()
    {
        _animator.SetBool(OPEN, false);
        _animator.SetBool(INTERACTEDTOOPEN, true);
        _animator.SetBool(INTERACTEDTOCLOSE, false);
        yield return new WaitForSeconds(0.3f);
        isOpen = true;
    }

    private IEnumerator CloseDoor()
    {
        _animator.SetBool(OPEN, true);
        _animator.SetBool(INTERACTEDTOOPEN, false);
        _animator.SetBool(INTERACTEDTOCLOSE, true);
        yield return new WaitForSeconds(0.3f);
        isOpen = false;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inside = false;
        }

    }
}
