using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu1 : MonoBehaviour
{
    public void Playgame()
    {

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("Ha salido del juego");
        Application.Quit();
    }
   
}