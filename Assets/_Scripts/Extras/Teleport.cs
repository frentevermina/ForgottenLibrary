using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{

    public string NewSceneName = "EL nombre de la escena";
    public string ID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<Player>().nextID = ID;
            SceneManager.LoadScene(NewSceneName);
        }
    }
}
