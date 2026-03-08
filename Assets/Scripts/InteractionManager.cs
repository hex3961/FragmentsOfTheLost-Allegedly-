using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class InteractionManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI dotCrosshair;
    public TextMeshProUGUI interactPrompt;

    [Header("Detection")]
    public float interactRange = 1f;
    public LayerMask interactLayer = -1;
    
    private Camera cam;
    private IInteractable nearestTarget;
    private readonly List<IInteractable> inRangeTargets = new();

    void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateTargets();
        if (Input.GetKeyDown(KeyCode.E) && nearestTarget != null)
            nearestTarget.Interact();
    }

    void UpdateTargets()
    {
        // Clear previous
        if (nearestTarget != null)
        {
            nearestTarget.OnHoverEnd();
            nearestTarget = null;
        }
        inRangeTargets.Clear();

        // Overlap Sphere finds ALL in range
        Collider[] hits = Physics.OverlapSphere(transform.position, interactRange, interactLayer);
        
        // Check for lost targets
        var newTargets = new HashSet<IInteractable>();
        foreach (var hit in hits)
        {
            var target = hit.GetComponentInParent<IInteractable>();
            if (target != null) newTargets.Add(target);
        }
        
        // Call OnHoverEnd only on targets that left range
        foreach (var target in inRangeTargets)
        {
            if (!newTargets.Contains(target))
                target.OnHoverEnd();
        }
        
        // Call OnHover only on NEW targets
        foreach (var target in newTargets)
        {
            if (!inRangeTargets.Contains(target))
                target.OnHover();
        }
        
        inRangeTargets.Clear();
        inRangeTargets.AddRange(newTargets);

        // Pick closest
        nearestTarget = null;
        float closestDist = float.MaxValue;
        foreach (var t in inRangeTargets)
        {
            float dist = Vector3.Distance(transform.position, ((MonoBehaviour)t).transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                nearestTarget = t;
            }
        }

        // UI Update
        if (nearestTarget != null)
        {
            interactPrompt.text = $"E {nearestTarget.promptText}";
            interactPrompt.alpha = 1;
            dotCrosshair.color = Color.green;
        }
        else
        {
            interactPrompt.alpha = 0;
            dotCrosshair.color = Color.white;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
