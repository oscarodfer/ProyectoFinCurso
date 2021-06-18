using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    public int score;
    public int level;

   [ContextMenu("Leer simple")]
   public void LeerSimple()
   {
        StartCoroutine(CorrutinaLeerSimple());
   }

    IEnumerator CorrutinaLeerSimple ()
    {
        UnityWebRequest web = UnityWebRequest.Get("https://pipasjourney.com/compartido/prueba.txt");
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

    [ContextMenu("Escribir simple")]
    public void EscribirSimple()
    {
        StartCoroutine(CorrutinaEscribirSimple());
    }

    IEnumerator CorrutinaEscribirSimple()
    {
        WWWForm form = new WWWForm();
        form.AddField("archivo", "prueba.txt");
        form.AddField("texto", "Hola Farola");


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

    [ContextMenu("Escribir Varios Sin Json")]
    public void EscribirVariosSinJson()
    {
        StartCoroutine(CorrutinaEscribirVariosSinJson());
    }

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

            score = int.Parse(partes[0]);
            level = int.Parse(partes[1]);
        }
        else
        {
            Debug.Log("Error de conexión. ");
        }
    }
}
