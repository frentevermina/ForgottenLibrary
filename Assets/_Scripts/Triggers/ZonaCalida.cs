using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaCalida : MonoBehaviour
{
    [SerializeField] private PersonajeTemperatura _personajeTemperatura;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("dentro");
            _personajeTemperatura.ZonaSegura();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("saliste");
            _personajeTemperatura.ZonaInsegura();
        }
    }


}
