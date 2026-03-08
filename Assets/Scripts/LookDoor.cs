using UnityEngine;

public class LockDoor : MonoBehaviour
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
            Debug.Log("Lock interactable");  // Test range
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
        if (!playerInRange || playerInventory == null) return;

        if (Input.GetKeyDown(interactKey))
        {
            if (playerInventory.hasKey)
            {
                UnlockAndDisappear();
            }
            else
            {
                Debug.Log("Need key to unlock.");
            }
        }
    }

    void UnlockAndDisappear()
    {
        Debug.Log("Lock unlocked and removed!");
        gameObject.SetActive(false);  // Lock disappears instantly
        // Later: Open door animation here
    }
}
