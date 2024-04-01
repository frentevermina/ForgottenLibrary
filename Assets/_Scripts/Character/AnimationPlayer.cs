using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerWalking;
    [SerializeField] private string layerRunning;
    [SerializeField] private string layerAttacking;

    private Animator _animator;
    private PlayerMovement _playerMovement;
    private PersonajeAtaque _personajeAtaque;


    private readonly int xDirection = Animator.StringToHash("X");
    private readonly int yDirection = Animator.StringToHash("Y");
    private readonly int derrotado = Animator.StringToHash("Derrotado");
    private readonly int congelado = Animator.StringToHash("Congelado");



    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _personajeAtaque = GetComponent <PersonajeAtaque>();
    }
    
    // Update is called once per frame
    void Update()
    {
        ActualizarLayer();
       if (_playerMovement.walking == false)
        {
            return;
        }
        _animator.SetFloat(xDirection, _playerMovement.Direction.x);
        _animator.SetFloat(yDirection, _playerMovement.Direction.y);
    }

    public void ActivateLayer(string nameLayer)
    {
        for (int i = 0; i < _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i, 0); //desactivando todos los layer del animator para
                                            //poder mostrar la animación de caminar o idle según corresopnda
        }

        _animator.SetLayerWeight(_animator.GetLayerIndex(nameLayer), 1);
    }

    private void ActualizarLayer()
    {
        if (_personajeAtaque.Atacando)
        {
            ActivateLayer(layerAttacking);
        }
        else if (_playerMovement.running && _playerMovement.Direction.magnitude > 0f)
        {
            ActivateLayer(layerRunning);
        }
        else if (_playerMovement.walking)
        {
            ActivateLayer(layerWalking);
        }
        
        else
        {
            ActivateLayer(layerIdle);
        }
    }

    public void RevivirPersonaje()
    {
        ActivateLayer(layerIdle);
        _animator.SetBool(derrotado, false);
    }

    private void PersonajeDerrotadoRespuesta()
    {
        if (_animator.GetLayerWeight(_animator.GetLayerIndex(layerIdle))== 1)
        {
            _animator.SetBool(derrotado, true);
        }
        else
        {
            ActivateLayer(layerIdle);
            _animator.SetBool(derrotado, true);
        }
           
    }

    private void PersonajeCongeladoRespuesta()
    {
        if (_animator.GetLayerWeight(_animator.GetLayerIndex(layerIdle)) == 1)
        {
            _animator.SetBool(congelado, true);
        }
        else
        {
            ActivateLayer(layerIdle);
            _animator.SetBool(congelado, true);
        }
    }
    private void OnEnable()
    {
        PersonajeVida.EventoPersonajeDerrotado += PersonajeDerrotadoRespuesta;
        PersonajeTemperatura.EventoPersonajeCongelado += PersonajeCongeladoRespuesta;
    
    }

    private void OnDisable()
    {
        PersonajeVida.EventoPersonajeDerrotado -= PersonajeDerrotadoRespuesta;
        PersonajeTemperatura.EventoPersonajeCongelado -= PersonajeCongeladoRespuesta;


    }

}
