using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Mainmenu1 : MonoBehaviour
{
    public float delay = 0f; 

    public void Playgame()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(delay);

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("Ha salido del juego");
        Application.Quit();
    }
}