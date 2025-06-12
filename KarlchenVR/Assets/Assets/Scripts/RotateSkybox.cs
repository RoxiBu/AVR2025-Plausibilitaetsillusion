using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    [Tooltip("Rotationsgeschwindigkeit in Grad pro Sekunde")]
    public float rotationSpeed = 0.3f;

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
