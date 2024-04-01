using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapLayerInFront : MonoBehaviour
{
    public const string CAPA_DINAMICADELANTE = "DetailsSuperior";
    public const string CAPA_DINAMICA = "MiddleGround";

    [SerializeField] private GameObject TriggerDetras;
    [SerializeField] private GameObject teleport;
    [SerializeField] private TilemapRenderer tilemapRenderer;



    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(teleport != null)
            {
                tilemapRenderer.sortingLayerName = CAPA_DINAMICA;
                TriggerDetras.SetActive(true);
                teleport.SetActive(false);
                this.gameObject.SetActive(false);
            }
            else
            {
                tilemapRenderer.sortingLayerName = CAPA_DINAMICA;
                TriggerDetras.SetActive(true);
                this.gameObject.SetActive(false);
            }
          
        }
    }
}
