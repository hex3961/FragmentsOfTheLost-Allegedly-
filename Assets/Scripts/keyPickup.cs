using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteractable
{
    private PlayerInventory playerInventory;
    
    public string promptText => "Pick Up Key";
    
    public void OnHover() { }
    public void OnHoverEnd() { }
    public void Interact() 
    { 
        if (playerInventory != null)
        {
            playerInventory.hasKey = true;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<PlayerInventory>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = null;
        }
    }
}
