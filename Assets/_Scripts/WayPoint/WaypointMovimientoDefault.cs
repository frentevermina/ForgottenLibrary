using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DireccionMovimiento
{
    Horizontal,
    Vertical
}

public class WaypointMovimientoDefault : MonoBehaviour
{

    [SerializeField] protected float speed;

    protected Waypoint _waypoint;
    protected int puntoActualIndex;
    protected Vector3 ultimaPosicion;
    protected Animator _animator;
    public Vector3 PuntoPorMoverse => _waypoint.ObtenerPosicionMovimiento(puntoActualIndex);



    void Start()
    {
        puntoActualIndex = 0;
        _waypoint = GetComponent<Waypoint>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoverPersonaje();
        RotarPersonaje();
        RotarVertical();
        if (ComprobarPuntoActualAlcanzado())
        {
            ActualizarIndexMovimiento();
        }
    }
      

    private bool ComprobarPuntoActualAlcanzado()
    {
        float distanciaHaciaPuntoActual = (transform.position - PuntoPorMoverse).magnitude;
        if (distanciaHaciaPuntoActual < 0.1f)
        {
            ultimaPosicion = transform.position;
            return true;
        }

        return false;
    }

    private void ActualizarIndexMovimiento()
    {
        //para moverse eternamente

        if(puntoActualIndex==_waypoint.Puntos.Length - 1)
        {
            puntoActualIndex = 0;
        }
        else
        {
            if (puntoActualIndex < _waypoint.Puntos.Length - 1)
            {
                puntoActualIndex ++;
            }
        }
    }
    private void MoverPersonaje()
    {
        transform.position = Vector3.MoveTowards(transform.position, PuntoPorMoverse,
                             speed * Time.deltaTime);
    }

    protected virtual void RotarPersonaje()
    {
        
    
    }
    
    protected virtual void RotarVertical()
    {

    }

}
