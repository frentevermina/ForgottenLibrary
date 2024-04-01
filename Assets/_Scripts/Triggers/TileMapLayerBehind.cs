using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapLayerBehind : MonoBehaviour
{
    public const string CAPA_DINAMICA = "MiddleGround";
    public const string CAPA_DINAMICADELANTE = "DetailsSuperior";

    [SerializeField] private GameObject TriggerDelante;
    [SerializeField] private GameObject teleport;
    [SerializeField] private TilemapRenderer tilemapRenderer;


    public bool capaDelante;
    // Start is called before the first frame update
    void Start()
    {
        if(teleport != null)
        {
            teleport.SetActive(false);
        }
        if (capaDelante)
        {
            tilemapRenderer.sortingLayerName = CAPA_DINAMICA;
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(teleport != null)
            {
                tilemapRenderer.sortingLayerName = CAPA_DINAMICADELANTE;
                teleport.SetActive(true);
                this.gameObject.SetActive(false);
                TriggerDelante.SetActive(true);
            }
            else
            {
                tilemapRenderer.sortingLayerName = CAPA_DINAMICADELANTE;
                this.gameObject.SetActive(false);
                TriggerDelante.SetActive(true);

            }
        }
    }
}
