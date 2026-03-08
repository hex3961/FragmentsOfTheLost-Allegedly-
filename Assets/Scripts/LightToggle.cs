using UnityEngine;
using System.Linq;  

public class LightToggle : MonoBehaviour, IInteractable  // Add interface
{
    public Light yellowPointLight;
    public Light blacklightSpot;

    public BloodStain[] bloodStains;

    private bool blacklightActive = false;
    private bool playerInRange = false;

    void Start()
    {
        yellowPointLight.gameObject.SetActive(true);
        blacklightSpot.gameObject.SetActive(false);
        
        if (bloodStains.Length == 0)
            bloodStains = FindObjectsOfType<BloodStain>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleBlacklight();
        }
    }
    
    public string promptText => blacklightActive ? "Deactivate Blacklight [Normal Light]" : "Activate Blacklight [Reveal Blood]";

    public void OnHover() { /* Optional: Outline glow */ }

    public void OnHoverEnd() { /* Reset outline */ }

    public void Interact()  // Called by raycast
    {
        ToggleBlacklight();
    }

    public void ToggleBlacklight()  // Keep private or public
    {
        blacklightActive = !blacklightActive;
        yellowPointLight.gameObject.SetActive(!blacklightActive);
        blacklightSpot.gameObject.SetActive(blacklightActive);

        foreach (var stain in bloodStains)
            stain.ToggleReveal(blacklightActive);
    }
}
