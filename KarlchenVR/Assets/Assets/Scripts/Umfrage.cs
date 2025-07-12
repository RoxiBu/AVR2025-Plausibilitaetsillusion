using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.Networking;

public class Umfrage : MonoBehaviour
{
    public TMP_InputField frage1Input;
    public TMP_Dropdown frage2Dropdown;

    public void Absenden()
    {
        string antwort1 = frage1Input.text;
        string antwort2 = frage2Dropdown.options[frage2Dropdown.value].text;

        StartCoroutine(SendeDaten(antwort1, antwort2));
    }

    IEnumerator SendeDaten(string a1, string a2)
    {
        WWWForm form = new WWWForm();
        form.AddField("antwort1", a1);
        form.AddField("antwort2", a2);

        using (UnityWebRequest www = UnityWebRequest.Post("url", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log("Fehler: " + www.error);
            else
                Debug.Log("Antwort gesendet!");
        }
    }
}
