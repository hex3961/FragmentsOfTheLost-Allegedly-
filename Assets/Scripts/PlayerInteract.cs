using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    [Header("UI Refs")]
    public TextMeshProUGUI dotCrosshair;    // "+" text component
    public TextMeshProUGUI interactPrompt;  // "E Interact" text

    [Header("Raycast")]
    public float interactRange = 4f;
    public LayerMask interactLayer = -1;  // All layers, or "Interact"

    private Camera cam;
    private IInteractable currentTarget;

    void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        if (dotCrosshair) dotCrosshair.color = Color.white;
        if (interactPrompt) interactPrompt.alpha = 0;
    }

    void Update()
    {
        Raycast();
        if (Input.GetKeyDown(KeyCode.E) && currentTarget != null)
            currentTarget.Interact();
    }

    void Raycast()
    {
        // Clear previous
        if (currentTarget != null)
        {
            currentTarget.OnHoverEnd();
            currentTarget = null;
            if (interactPrompt) interactPrompt.alpha = 0;
            if (dotCrosshair) dotCrosshair.color = Color.white;
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, interactRange, interactLayer))
        {
            currentTarget = hit.collider.GetComponent<IInteractable>();
            if (currentTarget != null)
            {
                currentTarget.OnHover();
                if (interactPrompt)
                {
                    interactPrompt.text = $"E {currentTarget.promptText}";
                    interactPrompt.alpha = 1;
                }
                if (dotCrosshair) dotCrosshair.color = Color.green;  // Hit feedback
            }
        }
    }
}
