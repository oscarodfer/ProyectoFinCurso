using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    public GameObject player;
    public int score;
    public int level;

    private void Start()
    {
        score = player.GetComponent<CharacterStats>().totalScore;
        level = player.GetComponent<CharacterStats>().level;
    }
    private void Update()
    {
        score = player.GetComponent<CharacterStats>().totalScore;
        level = player.GetComponent<CharacterStats>().level;
    }

    [ContextMenu("Escribir Varios Sin Json")]
    public void EscribirVariosSinJson()
    {
        StartCoroutine(CorrutinaEscribirVariosSinJson());
    }

    [System.Obsolete]
    IEnumerator CorrutinaEscribirVariosSinJson()
    {
        WWWForm form = new WWWForm();
        form.AddField("archivo", "pruebaVariosSinJson.txt");
        form.AddField("texto", score.ToString() + "$" + level.ToString() + "$");


        UnityWebRequest web = UnityWebRequest.Post("https://pipasjourney.com/compartido/escribir.php", form);
        yield return web.SendWebRequest();
        if (!web.isNetworkError && !web.isHttpError)
        {
            Debug.Log(web.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error de conexión. ");
        }
    }

    [ContextMenu("Leer Varios Sin Json")]
    public void LeerVariosSinJson()
    {
        StartCoroutine(CorrutinaLeerVariosSinJson());
    }

    IEnumerator CorrutinaLeerVariosSinJson()
    {
        UnityWebRequest web = UnityWebRequest.Get("https://pipasjourney.com/compartido/pruebaVariosSinJson.txt");
        yield return web.SendWebRequest();
        if (!web.isNetworkError && !web.isHttpError)
        {
            string textoOriginal = web.downloadHandler.text;
            string[] partes = textoOriginal.Split('$');

            GameObject.Find("Player").GetComponent<CharacterStats>().totalScore = int.Parse(partes[0]);
            GameObject.Find("Player").GetComponent<CharacterStats>().level = int.Parse(partes[1]);
        }
        else
        {
            Debug.Log("Error de conexión.");
        }
    }
}
