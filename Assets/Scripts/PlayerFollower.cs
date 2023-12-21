using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    
   
    void Update()
    {
        var pos = transform.position;
        pos.x = player.transform.position.x;
        transform.position = pos;
    }
}
