using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private Player player;
    public string ID;

    // Start is called before the first frame update
    void Start()
    {
        
        player = FindObjectOfType<Player>();
        if (!player.nextID.Equals(ID))
        {
            return;
        }

        player.transform.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
