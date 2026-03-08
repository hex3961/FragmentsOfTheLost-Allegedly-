using UnityEngine;

public class BloodStain : MonoBehaviour
{
    [Header("Colors")]
    public Color normalTint = new Color(1, 0.95f, 0.9f, 0.05f);  // Subtle red-brown blend (low alpha)
    public Color glowColor = Color.green;
    public float glowPower = 5f;
    
    [Header("Textures (Optional)")]
    public Texture2D bloodTexture;  // Splatter PNG, assign if desired

    private Renderer rend;
    private Material mat;
    private MaterialPropertyBlock propBlock;  // Efficient per-object props

    void Start()
    {
        rend = GetComponent<Renderer>();
        propBlock = new MaterialPropertyBlock();
        
        // Use wall-like base + subtle blood
        mat = rend.material;
        if (bloodTexture != null)
            mat.mainTexture = bloodTexture;  // Splatter over surroundings
        
        mat.SetFloat("_Mode", 3);  // Transparent mode
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
        mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;  // Transparent queue
        
        SetNormalState();
    }

    public void ToggleReveal(bool blacklightOn)
    {
        if (blacklightOn)
        {
            propBlock.SetColor("_BaseColor", new Color(glowColor.r, glowColor.g, glowColor.b, 0.8f));  // Opaque green
            propBlock.SetColor("_EmissionColor", glowColor * glowPower);
            rend.SetPropertyBlock(propBlock);
        }
        else
        {
            SetNormalState();
            rend.SetPropertyBlock(null);  // Reset to material defaults
        }
    }

    void SetNormalState()
    {
        // Subtle tint blends with wall texture/color
        propBlock.SetColor("_BaseColor", normalTint);
        propBlock.SetColor("_EmissionColor", Color.black);
    }
}
