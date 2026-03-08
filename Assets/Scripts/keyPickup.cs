using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;

    private bool playerInRange = false;
    private PlayerInventory playerInventory;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerInventory = other.GetComponent<PlayerInventory>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerInventory = null;
        }
    }

    void Update()
    {
        if (playerInRange && playerInventory != null && Input.GetKeyDown(interactKey))
        {
            playerInventory.hasKey = true;
            gameObject.SetActive(false);  // key disappears
        }
    }
}
