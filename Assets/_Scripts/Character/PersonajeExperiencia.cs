using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersonajeExperiencia : MonoBehaviour
{
    public static Action EventoLevelUp;

    [Header("Stat")]
    [SerializeField] private PersonajeStats stats;

    [Header("Config")]

    //Variables Necesarias para construir la experiencia
    [SerializeField] private int nivelMax;          //Nivel m�ximo al cual puedes llegar
    [SerializeField] private int expBase ;          //cuanta exp necesaria para nuevo nivel
    [SerializeField] private int valorIncremental;  //incrementar la exp para los niveles siguientes
    [SerializeField] private TextoAnimacionNivelUp _texto;

    //END

    //Variables para controlar la EXP
    private float expActual;
    private float expActualTemp;
    private float expRequeridaSiguienteNivel;


    //END
    void Start()
    {
        stats.Nivel = 1;
        expRequeridaSiguienteNivel = expBase;
        stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
        ActualizarBarraExp();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            A�adirExperiencia(20f);
        }
    }
    public void A�adirExperiencia(float expObtenida)    //En el par�metro definimos cu�nta experiencia
                                                    //vamos a obtener - float expObtenida-
    {
        if (expObtenida > 0f)
        {
            float expRestanteNuevoNivel = expRequeridaSiguienteNivel - expActualTemp; //cu�nto nos falta para llegar al siguiente nivel
            if(expObtenida>= expRestanteNuevoNivel)
            {
                expObtenida -= expRestanteNuevoNivel;//Restar la experiencia obtenida
                expActual += expObtenida;

                ActualizarNivel();
                A�adirExperiencia(expObtenida);
            }
            else
            {
                expActual += expObtenida;
                expActualTemp += expObtenida;
                if (expActualTemp == expRequeridaSiguienteNivel)
                {
                    ActualizarNivel();
                }
            }
            
        }
        
        stats.ExpActual = expActual;
        ActualizarBarraExp();
    }
    
    private void ActualizarNivel() //Para subir el nivel
    //Actualizaremos la exp para el siguiente nivel el nivel y la exp actual temporal
    {
        if (stats.Nivel < nivelMax)
        {
            stats.Nivel++; //Para a�adir uun nivel
            expActualTemp = 0f;
            expRequeridaSiguienteNivel *= valorIncremental;
            stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
            stats.PuntosDisponibles += 3;
            expActual = 0;
            _texto.RespuestaLevelUp();
        }
    }

    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActualTemp, expRequeridaSiguienteNivel);
   
    }

    private void RespuestaEnemigoDerrotado(float exp)
    {
        A�adirExperiencia(exp);
    }


    private void OnEnable()
    {
        EnemigoVida.EventoEnemigoDerrotado += RespuestaEnemigoDerrotado;
    }


    private void OnDisable()
    {
        EnemigoVida.EventoEnemigoDerrotado -= RespuestaEnemigoDerrotado;
    }
}
