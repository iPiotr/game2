using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int pineapples = 0;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Text PineapplesText;
    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pineapple"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            pineapples++;
            PineapplesText.text = "Pineapples: " + pineapples;
            // transform.localScale = transform.localScale * 1.25f;
        }

        if (collision.gameObject.CompareTag("apple"))
        {
            playerMovement.jumpForce = 5f;
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
        }
    }
}