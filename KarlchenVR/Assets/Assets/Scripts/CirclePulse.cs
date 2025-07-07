using UnityEngine;

public class CirclePulse : MonoBehaviour
{
    public float pulseSpeed = 1f;         // Geschwindigkeit der Skalierung
    public float pulseAmount = 0.1f;      // Stärke der Skalierung (z. B. 0.1 = 10%)
    public float offset = 0f;             // Phasenverschiebung (z. B. für unteren Kreis)

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        float scaleFactor = 1 + Mathf.Sin(Time.time * pulseSpeed + offset) * pulseAmount;
        transform.localScale = initialScale * scaleFactor;
    }
}
