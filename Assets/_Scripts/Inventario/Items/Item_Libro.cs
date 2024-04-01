using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Libros")]


public class Item_Libro : InventarioItem
{
    [Header("Bonificadores")]
    public float Esoterismo;
    public float Ocultismo;
    public float Medicina;
    public float Buscar;
  

     public override bool UsarItem()
     {
         if (Inventario.Instance.Personaje.PersonajeVida.Derrotado == false)
         {
            Inventario.Instance.PersonajeStats.AñadirBonusPorLibro(Esoterismo, Ocultismo, Medicina, Buscar);
             return true;
         }

        

         return false;
     }
}
