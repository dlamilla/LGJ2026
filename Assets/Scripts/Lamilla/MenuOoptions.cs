using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOoptions : MonoBehaviour
{
    public Slider volumeSlider;
    public float sliderValue;
    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volumeAudio", 0.5f);
        AudioListener.volume = volumeSlider.value;
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumeAudio", sliderValue);
        AudioListener.volume = sliderValue;
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

 
}
