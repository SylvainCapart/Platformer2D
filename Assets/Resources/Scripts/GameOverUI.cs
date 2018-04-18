using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

    private AudioManager audioManager;

    [SerializeField]
    public string mouseHoverSound = "ButtonHover";

    [SerializeField]
    public string buttonPressSound = "ButtonPress";

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audioManager found in GameOverUI");
        }
    }

    public void Quit()
    {
        audioManager.PlaySound(buttonPressSound);
        Debug.Log("APPLCIATION QUIT!");
        Application.Quit();
    }

    public void Retry()
    {
        audioManager.PlaySound(buttonPressSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(mouseHoverSound);
    }
}
