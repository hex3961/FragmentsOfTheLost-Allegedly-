using UnityEngine;

public class LockDoor : MonoBehaviour, IInteractable
{
    private PlayerInventory playerInventory;
    
    public string promptText { get; private set; }
    
    public void OnHover() { }
    public void OnHoverEnd() { }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<PlayerInventory>();
            UpdatePrompt();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = null;
        }
    }
    
    void UpdatePrompt()
    {
        if (playerInventory != null)
        {
            promptText = playerInventory.hasKey ? "Unlock Door [OPEN]" : "Unlock Door [NEED KEY]";
        }
    }
    
    public void Interact()
    {
        if (playerInventory == null) return;

        if (playerInventory.hasKey)
        {
            UnlockAndDisappear();
        }
        else
        {
            Debug.Log("Need key to unlock this door.");
        }
    }

    void UnlockAndDisappear()
    {
        Debug.Log("Lock unlocked and removed!");
        gameObject.SetActive(false);  // Lock disappears instantly
        // Later: Open door animation here
    }
}
