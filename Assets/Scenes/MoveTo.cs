using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MoveTo : MonoBehaviour
{
    public VideoPlayer videoPlayer; // El VideoPlayer que estás usando
    public string nextSceneName = "NextScene"; // El nombre de la siguiente escena a cargar

    void Start()
    {
        // Asegúrate de que el VideoPlayer esté asignado
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Cuando el video termine, llamamos a la función LoadNextScene
        videoPlayer.loopPointReached += LoadNextScene;

        // Iniciar el video
        videoPlayer.Play();
    }

    // Este método se llama cuando el video llega al final
    void LoadNextScene(VideoPlayer vp)
    {
        // Cargar la siguiente escena
        SceneManager.LoadScene("NIvel1");
    }
}
