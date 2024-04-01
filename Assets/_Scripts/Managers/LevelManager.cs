using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  
  [SerializeField] private Player personaje;
  [SerializeField] private Transform respawn;
  [SerializeField] private UIManager uIManager;
  [SerializeField] private PlayerMovement playerMovement;
  [SerializeField] private PersonajeAtaque playerAttack;
  [SerializeField] private Animator playerAnimation;
  //[SerializeField] private GameObject PausaTMP;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        { 
            if (personaje.PersonajeVida.Derrotado)
            {
                personaje.transform.localPosition = respawn.position;
                personaje.RestaurarPersonaje();

            }
    
        }

        if (Input.GetButtonDown("Pause"))
        {
            PauseUnPauseGame();
        }

        if (Input.GetButtonDown("Inventory") && uIManager.InventarioAbierto == true)
        {

            uIManager.CerrarInventario();
            return;
        }

        if (Input.GetButtonDown("Inventory") && uIManager.InventarioAbierto == false)
        {

            uIManager.AbrirInventario();
            return;
        }

    }
    
    public void PauseNormal()
    {
        Time.timeScale = 0;
        //playerMovement.enabled = false;
        //playerAttack.enabled = false;

    }

    public void UnPauseNormal()
    {
        Time.timeScale = 1;
       // playerMovement.enabled = true;
        //playerAttack.enabled = true;
    }

    public void PauseUnPauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            playerMovement.enabled = false;
          //  PausaTMP.SetActive(true);

        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            playerMovement.enabled = true;
            //PausaTMP.SetActive(false);
        }
    }

    public void ShowTextWithoutPause()
    {
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        playerAnimation.enabled = false;
    }
    public void EndShowTextWithoutPause()
    {
        playerMovement.enabled = true;
        playerAttack.enabled = true;
        playerAnimation.enabled = true;
    }

    public void ShowTextWithPause()
    {
        Time.timeScale = 0;
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        playerAnimation.enabled = false;
    }
    public void EndShowTextWithPause()
    {
        Time.timeScale = 1;
        playerMovement.enabled = true;
        playerAttack.enabled = true;
        playerAnimation.enabled = true;
    }

}
