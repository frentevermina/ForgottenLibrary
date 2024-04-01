using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVInteraccion : MonoBehaviour
{

    [SerializeField] private bool interact;
    public SpriteRenderer sprite;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interact = true;
        }
        
       
    }


    private void OnTriggerStay2D(Collider2D other)
    {
               
        if (other.CompareTag("Player") && interact == true)
        {

            gameObject.SetActive(false);            
        }
        
       

    }
}
