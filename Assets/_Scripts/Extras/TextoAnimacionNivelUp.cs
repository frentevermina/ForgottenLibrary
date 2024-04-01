using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextoAnimacionNivelUp : MonoBehaviour
{

    public int activaranimacion = Animator.StringToHash("ActivarAnimacion");


    [SerializeField] private GameObject subidaNivel;
    [SerializeField] private GameObject itemGanado;
    private Animator _animator;

    private bool sameTime;
   

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        itemGanado.SetActive(false);
        subidaNivel.SetActive(false);
        sameTime = false;
    }
    public void GetItemSound()
    {
        SFXManager.Instance.PlaySFX(SFXType.SoundType.PICK_ITEM);

    }

    private IEnumerator AnimacionItemGanado()
    {
        _animator.SetBool(activaranimacion, true);
        itemGanado.SetActive(true);
        GetItemSound();
        sameTime = true;
        

        yield return new WaitForSeconds(2f);

        itemGanado.SetActive(false);
        _animator.SetBool(activaranimacion, false);
        sameTime = false;
        
    }

    private IEnumerator AnimacionLevelUP()
    {

        yield return new WaitForSeconds(0.3f);
        if (sameTime)
        {
            yield return new WaitForSeconds(3f);

            _animator.SetBool(activaranimacion, true);
            subidaNivel.SetActive(true);
            SFXManager.Instance.PlaySFX(SFXType.SoundType.LEVELUP);



            yield return new WaitForSeconds(2f);

            subidaNivel.SetActive(false);
            _animator.SetBool(activaranimacion, false);
            sameTime = false;
        }
        else
        {
            _animator.SetBool(activaranimacion, true);
            subidaNivel.SetActive(true);
            SFXManager.Instance.PlaySFX(SFXType.SoundType.LEVELUP);



            yield return new WaitForSeconds(2f);

            subidaNivel.SetActive(false);
            _animator.SetBool(activaranimacion, false);
        }
        
    }

    public void SonidoPapelCogido()
    {
        SFXManager.Instance.PlaySFX(SFXType.SoundType.PAPER_SHOWING);

    }

    public void SonidoDesbloqueo()
    {
        SFXManager.Instance.PlaySFX(SFXType.SoundType.UNLOCK_DOOR);
    }

    public void SonidoBloqueado()
    {
        SFXManager.Instance.PlaySFX(SFXType.SoundType.LOCKED_DOOR);
    }

    public void SonidoAbrir()
    {
        SFXManager.Instance.PlaySFX(SFXType.SoundType.DOOR_OPENING);
    }

    public void SonidoAbrirCorredera()
    {
        SFXManager.Instance.PlaySFX(SFXType.SoundType.DOOR_OPENING_CORREDERA);
    }

    public void SonidoMobiliario()
    {
        SFXManager.Instance.PlaySFX(SFXType.SoundType.TABLE_OPENCLOSE);
    }

    public void RespuestaItemGanado()
    {
        StartCoroutine(AnimacionItemGanado());
    }

    public void RespuestaLevelUp()
    {

        StartCoroutine(AnimacionLevelUP());
    }


    private void OnEnable()
    {
        PersonajeExperiencia.EventoLevelUp += RespuestaLevelUp;
    }

    private void OnDisable()
    {
        PersonajeExperiencia.EventoLevelUp -= RespuestaLevelUp;
    }
    /* 
    
    



    public IEnumerator AnimacionLevelUp()
    {
        
        
       

        Debug.Log("empecé");
    }


    private void RespuestaLevelUp()
    {

        StartCoroutine(AnimacionLevelUp);
    }

    private void StartCoroutine(Func<IEnumerator> animacionLevelUp)
    {
        throw new NotImplementedException();
    }

    private void OnEnable()
    {
        PersonajeExperiencia.EventoLevelUp += RespuestaLevelUp;
    }

    private void OnDisable()
    {
        PersonajeExperiencia.EventoLevelUp -= RespuestaLevelUp;
    }
    */


}
