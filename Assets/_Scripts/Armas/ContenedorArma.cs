using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContenedorArma : Sigleton<ContenedorArma>
{

    [SerializeField] private Image armaIcono;
    [SerializeField] private Image armaSkillIcono;
    //
    public string ID;
    //
    public Item_Arma ArmaEquipada { get; set; }

    public Item_Arma itemArma;
    private void Start()
    {
        
    }
    public void EquiparArma(Item_Arma itemArma)
    {
       // RemoverArma();

        ArmaEquipada = itemArma;
        ID = itemArma.ID;

        armaIcono.sprite = itemArma.Arma.IconoArma;
        armaIcono.gameObject.SetActive(true);
        
            
        
        
        ArmaManager manager = FindObjectOfType<ArmaManager>();
        
        if(itemArma.Arma.Tipo == TipoArma.Melee)
        {
            manager.ChangeWeapon();
            
        }
        
        if(itemArma.Arma.Tipo == TipoArma.Distancia)
        {
            manager.ChangeWeapon();

            armaSkillIcono.sprite = itemArma.Arma.IconoSkill;
            armaSkillIcono.gameObject.SetActive(true);
        }
       
        if (itemArma.Arma.Tipo == TipoArma.Invocacion)
        {
            armaSkillIcono.sprite = itemArma.Arma.IconoSkill;
            armaSkillIcono.gameObject.SetActive(true);

        }

        Inventario.Instance.Personaje.PersonajeAtaque.EquiparArma(itemArma);
    }
    
    public void RemoverArma()
    {
        armaIcono.gameObject.SetActive(false);
        armaSkillIcono.gameObject.SetActive(false);
        
        //ArmaManager manager = FindObjectOfType<ArmaManager>();

        ArmaManager.Instance.UnequipWeapon();
        
        
        ArmaEquipada = null;
        Inventario.Instance.Personaje.PersonajeAtaque.RemoverArma();
    }

}
