using UnityEngine;

public class TalkingMouth : MonoBehaviour
{
    public AudioSource audioSource;          // Die AudioSource, die Sprache abspielt
    public Transform mouthTransform;         // Das Transform des Mund-Modells
    public float maxMovement = 0.5f;         // Wie weit der Mund sich maximal nach oben bewegt (Y-Achse)
    public float sensitivity = 10f;          // Multiplikator für Lautstärke-Empfindlichkeit
    public float smoothSpeed = 33f;          // Wie schnell sich der Mund bewegt

    private Vector3 initialLocalPosition;

    void Start()
    {
        if (mouthTransform == null)
            mouthTransform = transform;

        initialLocalPosition = mouthTransform.localPosition;
    }

    void Update()
    {
        float[] samples = new float[256];
        audioSource.GetOutputData(samples, 0);

        float sum = 0f;
        foreach (var sample in samples)
        {
            sum += sample * sample;
        }
        float rmsValue = Mathf.Sqrt(sum / samples.Length);
        float volume = rmsValue * sensitivity;

        float yOffset = Mathf.Clamp(volume, 0f, maxMovement);

        Vector3 targetPosition = initialLocalPosition + new Vector3(0f, yOffset, 0f);
        mouthTransform.localPosition = Vector3.Lerp(mouthTransform.localPosition, targetPosition, Time.deltaTime * smoothSpeed);
    }
}