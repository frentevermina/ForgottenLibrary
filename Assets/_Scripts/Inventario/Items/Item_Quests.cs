using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Objeto para usar")]


public class Item_Quests : InventarioItem
{
    
    public override bool UsarItem()
    {
        for (int i = 0; i < DoorManager.Instance.GetAllDoors().Count; i++)
        {
            if (DoorManager.Instance.GetAllDoors()[i].GetComponent<DoorType>().inside && DoorManager.Instance.GetAllDoors()[i].GetComponent<DoorType>().ID == ID)
            {
                Debug.Log("for bien hecho");
                DoorManager.Instance.GetPlayerUsedKey(ID);
                return true;
            }
            
            if ((DoorManager.Instance.GetAllDoors()[i].GetComponent<DoorType>().inside == false) && DoorManager.Instance.GetAllDoors()[i].GetComponent<DoorType>().ID == ID)
            {
                Debug.Log("inside correcto");
                DoorManager.Instance.NoSirveAqui(ID);
                return false;
            }
            
                
            
        }
        
        
        return false;
        
        






        /*for (int i = 0; i < doorManager.GetAllDoors().Count; i++)
        {
            Debug.Log("here1");
            if (doorManager.GetAllDoors()[i].GetComponent<DoorType>().ID == ID)
            {
                Debug.Log("here2");
                if (doorManager.GetAllDoors()[i].GetComponent<DoorType>().needKey && doorManager.GetAllDoors()[i].GetComponent<DoorType>().inside == false)
                {
                    doorManager.GetAllDoors()[i].GetComponent<DoorType>().StartEnumerator();
                    return false;
                }
                if (doorManager.GetAllDoors()[i].GetComponent<DoorType>().needKey && doorManager.GetAllDoors()[i].GetComponent<DoorType>().inside == true && doorManager.GetAllDoors()[i].GetComponent<DoorType>().playerHasKey)
                {
                    doorManager.GetPlayerUsedKey(ID);
                    doorManager.GetAllDoors()[i].GetComponent<DoorType>().StartSonidoPuerta();
                    return true;
                }

            }
        }
        Debug.Log("herefinal");*/
       
        
    }

}
