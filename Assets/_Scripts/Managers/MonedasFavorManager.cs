using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedasFavorManager : Sigleton<MonedasFavorManager>
{

    [SerializeField] private int monedasTest;


    public int MonedasTotales { get; set; }

    private string KEY_MONEDAS = "MYGAME_MONEDAS";



    private void Start()
    {
        //chetado:
        PlayerPrefs.DeleteKey(KEY_MONEDAS); // para probar las monedas, quitar
        CargarMonedas();
    }

    private void CargarMonedas()
    {
        MonedasTotales = PlayerPrefs.GetInt(KEY_MONEDAS, monedasTest);
    }


    public void AņadirMonedas(int cantidad)
    {
        MonedasTotales += cantidad;
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales);
        PlayerPrefs.Save();
    }

    public void RemoverMonedas(int cantidad)
    {
        if (cantidad > MonedasTotales)
        {
            return;
        }

        MonedasTotales -= cantidad;
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales);
        PlayerPrefs.Save();

    }





}
