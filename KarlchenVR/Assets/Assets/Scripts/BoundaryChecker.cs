using UnityEngine;
using UnityEngine.UI;

public class BoundaryChecker : MonoBehaviour
{
    public GameObject player;                
    public GameObject warningCanvas;         
    public Image[] screenFaders;             
    public float fadeSpeed = 2f;             

    private float targetAlpha = 0f;

    void Start()
    {
        if (warningCanvas != null)
            warningCanvas.SetActive(false);

        SetAlpha(0f); 
    }

    void Update()
    {
        foreach (Image img in screenFaders)
        {
            if (img == null) continue;

            Color currentColor = img.color;
            float newAlpha = Mathf.MoveTowards(currentColor.a, targetAlpha, Time.deltaTime * fadeSpeed);
            currentColor.a = newAlpha;
            img.color = currentColor;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Spieler hat den Bereich verlassen");
            targetAlpha = 1f;

            if (warningCanvas != null)
                warningCanvas.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Spieler ist zurück im Bereich");
            targetAlpha = 0f;

            if (warningCanvas != null)
                warningCanvas.SetActive(false);
        }
    }

    void SetAlpha(float alpha)
    {
        foreach (Image img in screenFaders)
        {
            if (img == null) continue;

            Color color = img.color;
            color.a = alpha;
            img.color = color;
        }
    }
}
