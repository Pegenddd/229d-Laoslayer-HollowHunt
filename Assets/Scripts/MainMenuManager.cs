using UnityEngine;
using UnityEngine.SceneManagement; // ต้องมีบรรทัดนี้เพื่อเปลี่ยนฉาก

public class MainMenuManager : MonoBehaviour
{
    // Function for Play button
    public void PlayGame()
    {
        // Load the main gameplay scene (Replace "YourGameSceneName" with your actual scene name)
        SceneManager.LoadScene("SampleScene"); 
    }

    // Function for Credits button
    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    // Function for Back to Menu button
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Function to Quit Game (Optional)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
}