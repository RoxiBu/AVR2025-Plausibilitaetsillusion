using UnityEngine;

public class ArmIdleAnimation : MonoBehaviour
{
    public Transform robot;          // Referenz auf den Roboter
    public Transform leftArm;        // Linker Arm
    public Transform rightArm;       // Rechter Arm

    public float rotationStrength = 10f;  // Maximaler Rotationswinkel in Grad
    public float smoothSpeed = 5f;        // Wie weich die Bewegung sein soll

    private float previousY;
    private float currentRotation = 0f;

    void Start()
    {
        if (robot != null)
            previousY = robot.position.y;
    }

    void Update()
    {
        if (robot == null) return;

        float currentY = robot.position.y;
        float deltaY = currentY - previousY;

        // Berechne Zielrotation basierend auf Y-Änderung (Steigen oder Fallen)
        float targetRotation = Mathf.Clamp(deltaY * rotationStrength * 100f, -rotationStrength, rotationStrength);

        // Weiches Interpolieren der Rotation
        currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * smoothSpeed);

        // Wende Rotation auf Arme an (z-Achse kann je nach Modell variieren!)
        leftArm.localRotation = Quaternion.Euler(0, 0, -currentRotation);
        rightArm.localRotation = Quaternion.Euler(0, 0, currentRotation);

        previousY = currentY;
    }
}
