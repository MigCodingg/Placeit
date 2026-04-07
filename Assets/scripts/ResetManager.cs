using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    void Update()
    {
        // Check if the player presses the R key to restart the game
        if (Keyboard.current.rKey.wasPressedThisFrame) 
        {
            RestartCurrentScene();
        }
    }

    // Method to restart the current scene
    public void RestartCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene(); // Get the current scene
        SceneManager.LoadScene(currentScene.name); // Reload the scene by its name
    }
}