using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    private bool gameHasEnded = false;
    public void GameOver()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");

            //Invoke("Restart", 4f);
            Invoke("LoadMainMenu", 4f);
        } 
    }

    void LoadMainMenu ()
    {
        var objects = GameObject.FindObjectsOfType<GameObject>();

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }

        SceneManager.LoadScene(0);
    }

    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
