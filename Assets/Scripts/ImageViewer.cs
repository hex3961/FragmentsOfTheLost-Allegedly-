using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageViewer : MonoBehaviour, IInteractable
{
    [Header("Page Images")]
    public Sprite[] pageImages = {};  
    
    [Header("UI (Assign Once)")]
    public GameObject viewerPanel;
    public Image pageImage; 
    public Button closeBtn, prevBtn, nextBtn;
    
    private int currentPage = 0;
    private CanvasGroup overlay;
    private bool isViewerOpen = false;

    public string promptText => "View Pages";

    public void OnHover() { }
    public void OnHoverEnd() { }

    public void Interact()
    {
        OpenViewer();
    }

    void Start()
    {
        if (viewerPanel == null)
        {
            Debug.LogError("ImageViewer: viewerPanel not assigned!");
            return;
        }

        overlay = viewerPanel.GetComponent<CanvasGroup>();
        if (overlay == null)
        {
            Debug.LogError("ImageViewer: viewerPanel missing CanvasGroup component!");
            return;
        }

        if (closeBtn) closeBtn.onClick.AddListener(CloseViewer);
        if (prevBtn) prevBtn.onClick.AddListener(PrevPage);
        if (nextBtn) nextBtn.onClick.AddListener(NextPage);

        if (pageImages.Length == 0)
        {
            Debug.LogWarning("ImageViewer: No page images assigned!");
        }

        // Start with viewer hidden
        viewerPanel.SetActive(false);
    }

    void OpenViewer()
    {
        if (isViewerOpen || pageImages.Length == 0) return;

        isViewerOpen = true;
        viewerPanel.SetActive(true);
        overlay.alpha = 1;
        Time.timeScale = 0;  // Pause
        Cursor.lockState = CursorLockMode.None;
        ShowPage(0);
    }

    void CloseViewer()
    {
        if (!isViewerOpen) return;

        StartCoroutine(FadeOut(() => {
            isViewerOpen = false;
            viewerPanel.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }));
    }

    void ShowPage(int page)
    {
        if (pageImages.Length == 0) return;

        currentPage = Mathf.Clamp(page, 0, pageImages.Length - 1);
        if (pageImage && pageImages.Length > 0)
            pageImage.sprite = pageImages[currentPage];
        
        if (prevBtn) prevBtn.interactable = currentPage > 0;
        if (nextBtn) nextBtn.interactable = currentPage < pageImages.Length - 1;
    }

    void PrevPage() { ShowPage(currentPage - 1); }
    void NextPage() { ShowPage(currentPage + 1); }

    IEnumerator FadeOut(System.Action onComplete)
    {
        float duration = 0.3f;
        float elapsed = 0;
        while (elapsed < duration)
        {
            overlay.alpha = Mathf.Lerp(1, 0, elapsed / duration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        onComplete?.Invoke();
    }
}
