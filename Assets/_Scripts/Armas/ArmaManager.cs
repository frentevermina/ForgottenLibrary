using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaManager : Sigleton<ArmaManager>
{

    [SerializeField] private List<GameObject> weapons;



    //
    //
    public List<GameObject> GetAllWeapons()
    {
        return weapons;
    }



    void Start()
    {
        //
        //
        weapons = new List<GameObject>();
        foreach (Transform weapon in transform)
        {
            weapons.Add(weapon.gameObject);
        }

        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(false);
            
            
            
            
        }
    
    }
   
    public void ChangeWeapon()
    {

        ContenedorArma contenedorArma = FindObjectOfType<ContenedorArma>();
        


        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].GetComponent<WeaponType>().Equiped)
            {
                weapons[i].SetActive(false);
            }

            if(contenedorArma.ID == weapons[i].GetComponent<WeaponType>().ID)
            {
                weapons[i].GetComponent<WeaponType>().Equiped = true;
                weapons[i].SetActive(true);
                weapons[i].SetActive(true);

            }
        }
               
    }

    public void UnequipWeapon()
    {

        ContenedorArma contenedorArma = FindObjectOfType<ContenedorArma>();

        for (int i = 0; i < weapons.Count; i++)
        {
           
            if (contenedorArma.ID == weapons[i].GetComponent<WeaponType>().ID)
            {
                
                weapons[i].GetComponent<WeaponType>().Equiped = false;
                weapons[i].SetActive(false);

            }
        }




    }
}
