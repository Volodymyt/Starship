using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIOptions : MonoBehaviour
{
    [SerializeField] private Slider _speedSlider;

    public float ReturnSpeedPercentage()
    {
        return _speedSlider.value;
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1.0f;
    }    
    
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }
}
