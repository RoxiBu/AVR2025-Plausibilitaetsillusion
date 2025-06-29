using UnityEngine;

public class MovingGradient : MonoBehaviour
{
    public Color colorA = Color.magenta; // Pink
    public Color colorB = Color.blue;    // Blau
    public float speed = 1f;
    public float emissionIntensity = 1f; // Emissionsstärke im Inspector

    private Material _material;
    private float _t = 0f;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("Kein Renderer gefunden!");
            enabled = false;
            return;
        }

        _material = renderer.material;

        // Emission aktivieren
        _material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if (_material == null) return;

        _t += Time.deltaTime * speed;

        // Weicher, symmetrischer Farbverlauf mit Sinus
        float lerp = (Mathf.Sin(_t * Mathf.PI * 2f - Mathf.PI / 2f) + 1f) / 2f;

        Color currentColor = Color.Lerp(colorA, colorB, lerp);

        _material.color = currentColor;
        _material.SetColor("_EmissionColor", currentColor * emissionIntensity);
    }

}
