using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPickup : MonoBehaviour
{
    public enum PickupObject {COIN, GEM};
    public PickupObject currentObject;
    public int pickupQuantity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (currentObject == PickupObject.COIN)
            {
                player.PickupCoin(pickupQuantity);
            }
            else
            {
                player.PickupGem(pickupQuantity);
            }
            Destroy(gameObject);
        }
    }
}
