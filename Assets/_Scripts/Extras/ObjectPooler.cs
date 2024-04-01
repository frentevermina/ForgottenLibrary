using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private int CantidadPorCrear;

    private List<GameObject> lista;
    public GameObject listaContenedor { get; private set; }
   

    public void CrearPool(GameObject objetoPorCrear)
    {
        lista = new List<GameObject> ();
        listaContenedor = new GameObject($"Pool - {objetoPorCrear.name}");

        for (int i = 0; i < CantidadPorCrear; i++)
        {
            lista.Add(AñadirInstancia(objetoPorCrear));
        }
    }

    private GameObject AñadirInstancia(GameObject objetoPorCrear)
    {
        GameObject nuevoObjeto = Instantiate(objetoPorCrear, listaContenedor.transform);
        nuevoObjeto.SetActive(false);
        return nuevoObjeto;
    }

    public GameObject ObetenerInstancia()
    {
        for (int i = 0; i < lista.Count; i++)
        {
            if (lista[i].activeSelf == false)
            {
                return lista[i];
            }
        }

        return null;
    }

    public void DestruirPool()
    {
        Destroy(listaContenedor);
        lista.Clear();
    }

}
