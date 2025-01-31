using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "IA/Acciones/Atacar Personaje")]
public class AccionAtacarPJ : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        Atacar(controller);
    }

    private void Atacar (IAController controller)
    {
        if(controller.PersonajeReferencia == null)
        {
            return;
        }
    
        if(controller.EsTiempoDeAtacar() == false)
        {
            return;
        }

        if (controller.PersonajeEnRangoAtaque(controller.RangoDeAtaqueDeterminado))
        {
            if(controller.TipoAtaque == TiposDeAtaque.Embestida)
            {
                controller.AtaqueEmbestida(controller.Da�o);
            }
            else
            {
                controller.AtaqueMelee(controller.Da�o);
            }

            controller.ActualizarTiempoEntreAtaques();
        }
    
    }


}
