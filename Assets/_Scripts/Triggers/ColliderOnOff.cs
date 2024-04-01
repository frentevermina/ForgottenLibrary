using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderOnOff : MonoBehaviour
{
    [SerializeField] private BoxCollider2D colisionPjDelante;
    [SerializeField] private BoxCollider2D colisionPjDetras;
    [SerializeField] private bool pjDelante;
    private SpriteRenderer layer;


    private void Start()
    {
        layer = GetComponent<SpriteRenderer>();
        if (pjDelante)
        {
            colisionPjDelante.enabled = true;
            colisionPjDetras.enabled = false;
        }
        else
        {
            colisionPjDelante.enabled = false;
            colisionPjDetras.enabled = true;
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (pjDelante)
            {
                layer.sortingLayerName = "Objetos de Hierarchy";
                colisionPjDetras.enabled = true;
                colisionPjDelante.enabled = false;
            }
            else
            {
                layer.sortingLayerName = "Details";
                colisionPjDetras.enabled = false;
                colisionPjDelante.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (pjDelante)
            {
                layer.sortingLayerName = "Details";
                colisionPjDetras.enabled = false;
                colisionPjDelante.enabled = true;
            }
            else
            {
                layer.sortingLayerName = "Objetos de Hierarchy";
                colisionPjDetras.enabled = true;
                colisionPjDelante.enabled = false;

            }
        }
            
    }
}
