using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    
    public bool Interaccion { get; set; }
  
    //public Vector2 Direction
    // {
    //     get
    //     {
    //         return _direction;
    //     }
    // }

    public bool walking => _direction.magnitude > 0f;
    public Vector2 Direction => _direction;
    public bool pickingUp;
    public bool running { get; set; }


    private Rigidbody2D _rigidbody2D;
    private Vector2  _direction;
    private Vector2 _input;

    private PersonajeVida _personajeVida;
    private PersonajeTemperatura _personajeTemperatura;

    private void Awake()
    {

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _personajeVida = GetComponent < PersonajeVida>();
        _personajeTemperatura = GetComponent<PersonajeTemperatura>();
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

      // Update is called once per frame
    void Update()
    {
    

        if (Input.GetButtonDown("Interact"))
        {
            StartCoroutine(IEEstablecerCondicionInteraccion());           
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            running = true;
            speed = 4.5f;


        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
            speed = 3;
        }

        if (_personajeVida.Derrotado)
        {
            _direction = Vector2.zero;
            return;
        }

        if (_personajeTemperatura.Congelado)
        {
            _direction = Vector2.zero;
            return;
        }
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //X
        if (_input.x > 0.1f)
        {
            _direction.x = 1f;
        }
        else if (_input.x <0f)
        {
            _direction.x = -1f;
        }
        else
        {
            _direction.x = 0f;
        }


        //Y

        if (_input.y > 0.1f)
        {
            _direction.y = 1f;
        }
        else if (_input.y < 0f)
        {
            _direction.y = -1f;
        }
        else
        {
            _direction.y = 0f;
        }

    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _direction.normalized * speed * Time.fixedDeltaTime);
    }

    private IEnumerator IEEstablecerCondicionInteraccion()
    {
        Interaccion = true;
        yield return new WaitForSeconds(0.3f);
        Interaccion = false;
    }

    
  
    public void PararAlAtacar()
    {
        _rigidbody2D.velocity = Vector2.zero;
        Debug.Log("me paro al atacar");
        
    }


}
