using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersonajeMunicion : MonoBehaviour
{

    [SerializeField] private int municionInicial;
    [SerializeField] private int municionMax;
 

    public int MunicionActual { get; set; }

    private PersonajeVida _personajeVida;

    private void Awake()
    {
        _personajeVida = GetComponent <PersonajeVida>() ;
    }
    void Start()
    {
        MunicionActual = municionInicial;
        ActualizarMunicion();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UsarMunicion(1);
        }
    }

    public void UsarMunicion(int cantidad)
    {
        if(MunicionActual >= cantidad)
        {
            MunicionActual -= cantidad;
            ActualizarMunicion();
        }
    }

    private void ActualizarMunicion()
    {
        UIManager.Instance.ActualizarMunicionPersonaje(MunicionActual, municionMax);
    }

}
