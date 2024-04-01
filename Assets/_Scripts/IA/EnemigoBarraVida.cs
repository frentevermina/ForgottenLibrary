using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemigoBarraVida : MonoBehaviour
{

    [SerializeField] private Image barraVida;

    private float SaludActual;
    private float SaludMax;

    void Update()
    {
        barraVida.fillAmount = Mathf.Lerp(barraVida.fillAmount, SaludActual / SaludMax, 10f * Time.deltaTime);
    }

    public void ModificarSalud(float pSaludActual, float pSaludMax)
    {
        SaludActual = pSaludActual;
        SaludMax = pSaludMax;
    }


}
