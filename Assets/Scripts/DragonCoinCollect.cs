using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCoinCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {            
            var coin = collision.GetComponent<Coin>();
            coin.Collected();
        }
    }
}
