using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjetivoManager : Sigleton<ObjetivoManager>
{
    [SerializeField] private string SinObjetivo;
    public string ObjetivoActual { get; set; }

    private string KEY_OBJETIVO = "MYJUEGO_OBJETIVO";

    private void Start()
    {
        PlayerPrefs.DeleteKey(KEY_OBJETIVO);
        CargarRemoverObjetivo();
    }

    private void CargarRemoverObjetivo()
    {
        ObjetivoActual = PlayerPrefs.GetString(KEY_OBJETIVO, SinObjetivo);
    }

    public void AñadirObjetivo(string objetivo)
    {
        ObjetivoActual = objetivo;
        PlayerPrefs.SetString(KEY_OBJETIVO, ObjetivoActual);
        PlayerPrefs.Save();
    }


}
