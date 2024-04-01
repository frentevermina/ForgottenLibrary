using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BibliotecaLibros : MonoBehaviour
{

    private readonly int activaranimacion = Animator.StringToHash("ActivarAnimacion");

    private Animator _animation;

    // Start is called before the first frame update
    void Start()
    {
        _animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animation.SetBool(activaranimacion, true);
        }
    }
}
