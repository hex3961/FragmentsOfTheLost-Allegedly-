using UnityEngine;

public interface IInteractable
{
    string promptText { get; }
    void OnHover();
    void OnHoverEnd();
    void Interact();
}
