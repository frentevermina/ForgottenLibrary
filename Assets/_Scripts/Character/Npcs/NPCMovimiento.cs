using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovimiento : WaypointMovimientoDefault
{

    [SerializeField] private DireccionMovimiento direction;

    protected override void RotarPersonaje()
    {
        if (direction != DireccionMovimiento.Horizontal)
        {
            return;
        }
        if (PuntoPorMoverse.x > ultimaPosicion.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void RotarVertical()
    {
        if (direction != DireccionMovimiento.Vertical)
        {
            return;
        }
        
        if (PuntoPorMoverse.y > ultimaPosicion.y)
        {
          //  _animator.SetBool(false);
        }
    
    
    }

    private void AnimatorStart()
    {
        _animator.SetBool("IsMoving", true);
        _animator.SetFloat("Horizontal", ((float)direction));
        _animator.SetFloat("Vertical", ((float)direction));
    }

    private void AnimatorStop()
    {
        _animator.SetBool("IsMoving", false);
        _animator.SetFloat("Last_H", ((float)direction));
        _animator.SetFloat("Last_V", ((float)direction));
    }

}
