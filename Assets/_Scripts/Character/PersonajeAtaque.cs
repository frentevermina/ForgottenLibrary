using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;




public class PersonajeAtaque : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Header("Disparo")]
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private Transform[] posicionesDisparo;

    public Arma ArmaEquipada { get; private set; }
    public EnemigoInteraccion EnemigoObjetivo { get; private set; }
    public bool Atacando { get; set; }

    private PersonajeTemperatura _personajeMana;
    private PlayerMovement _playerMovement;
    private int indexDireccionDisparo;
    private float tiempoParaSiguienteAtaque;

    private void Awake()
    {
        _personajeMana = GetComponent<PersonajeTemperatura>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        ObtenerDireccionDisparo();

        if (Time.time > tiempoParaSiguienteAtaque)
        {
            if (Input.GetButtonDown("Attack"))
            {
                StartCoroutine(IEEstablecerCondicionAtaque());

                

                //Para atacar solo con enemigo, etc 
                if (ArmaEquipada == null || EnemigoObjetivo == null)
                {
                    return;
                }

                UsarArma();
                tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;
              //  StartCoroutine(IEEstablecerCondicionAtaque());
            }
        }
    }

    private void UsarArma()
    {
        if (ArmaEquipada.Tipo == TipoArma.Invocacion)
        {
            if (_personajeMana.TemperaturaActual < ArmaEquipada.ManaRequerido)
            {
                return;
            }

            GameObject nuevoProyectil = pooler.ObetenerInstancia();
            nuevoProyectil.transform.localPosition = posicionesDisparo[indexDireccionDisparo].position;

            Proyectil proyectil = nuevoProyectil.GetComponent<Proyectil>();
            proyectil.InicializarProyectil(this);

            nuevoProyectil.SetActive(true);
            _personajeMana.UsarMana(ArmaEquipada.ManaRequerido);
        }
        else
        {
            EnemigoVida enemigoVida = EnemigoObjetivo.GetComponent<EnemigoVida>();
            enemigoVida.RecibirDaño(ObtenerDaño());
        }
    }

    public float ObtenerDaño()
    {
        float cantidad = stats.Daño;
        if(UnityEngine.Random.value < stats.PorcentajeCritico / 100)
        {
            cantidad *= 2;
        }

        return cantidad;
    }


    private IEnumerator IEEstablecerCondicionAtaque()
    {
        Atacando = true;
        _playerMovement.enabled = false;

        yield return new WaitForSeconds(0.2f);
        Atacando = false;
        _playerMovement.enabled = true;

    }

    public void EquiparArma(Item_Arma armaPorEquipar)
    {
        ArmaEquipada = armaPorEquipar.Arma;
        if (ArmaEquipada.Tipo == TipoArma.Invocacion)
        {
            pooler.CrearPool(ArmaEquipada.ProyectilPrefab.gameObject);
        }

        stats.AñadirBonusPorArma(ArmaEquipada);

    }

   

    public void RemoverArma()
    {
        if(ArmaEquipada == null)
        {
            return;
        }

        if (ArmaEquipada.Tipo == TipoArma.Invocacion)
        {
            pooler.DestruirPool();
        }

        stats.QuitarBonusPorArma(ArmaEquipada);
        ArmaEquipada = null; 
    }

    private void ObtenerDireccionDisparo()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.x >0.1f)
        {
            indexDireccionDisparo = 1;
        }
        else if (input.x < 0f)
        {
            indexDireccionDisparo = 3;
        }
        else if (input.y > 0.1f)
        {
            indexDireccionDisparo = 0;
        }
        else if (input.y < 0f)
        {
            indexDireccionDisparo = 2;
        }
    }


    private void EnemigoRangoSeleccionado(EnemigoInteraccion enemigoSeleccionado)
    {
        if (ArmaEquipada == null)
        {
            return;
        }

        if (ArmaEquipada.Tipo != TipoArma.Invocacion)
        {
            return;
        }

        if (EnemigoObjetivo == enemigoSeleccionado)
        {
            return;
        }

        EnemigoObjetivo = enemigoSeleccionado;
        EnemigoObjetivo.MostarEnemigoSeleccionado(true, TipoDeteccion.Rango);
    }

    private void EnemigoNoSeleccionado()
    {
        if (EnemigoObjetivo == null)
        {
            return;
        }

        EnemigoObjetivo.MostarEnemigoSeleccionado(false, TipoDeteccion.Rango);
        EnemigoObjetivo = null;

    }

    private void EnemigoMeleeDetectado(EnemigoInteraccion enemigoDetectado)
    {
        if(ArmaEquipada == null)
        {
            return;
        }
        if(ArmaEquipada.Tipo != TipoArma.Melee)
        {
            return;
        }

        EnemigoObjetivo = enemigoDetectado;
        EnemigoObjetivo.MostarEnemigoSeleccionado(true, TipoDeteccion.Melee);
    }

    private void EnemigoMeleePerdido()
    {
        if (ArmaEquipada == null)
        {
            return;
        }

        if (EnemigoObjetivo == null)
        {
            return;
        }

        if (ArmaEquipada.Tipo != TipoArma.Melee)
        {
            return;
        }

        EnemigoObjetivo.MostarEnemigoSeleccionado(false, TipoDeteccion.Melee);
        EnemigoObjetivo = null;

    }

    private void OnEnable()
    {
        SeleccionManager.EventoEnemigoSeleccionado += EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado += EnemigoNoSeleccionado;
        PersonajeDetector.EventoEnemigoDetectado += EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido += EnemigoMeleePerdido;
    }

    private void OnDisable()
    {
        SeleccionManager.EventoEnemigoSeleccionado -= EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado -= EnemigoNoSeleccionado;
        PersonajeDetector.EventoEnemigoDetectado -= EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido -= EnemigoMeleePerdido;
    }

}
