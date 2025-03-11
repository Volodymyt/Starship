using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIOptions : MonoBehaviour
{
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private AudioSource _musicSource, _UISoundsSource;
    [SerializeField] private AudioSource[] _soundsSources;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private SaveHandler _saveHandler;
    [SerializeField] private int _money = 0;

    private void Start()
    {
        _musicSource.volume = PlayerPrefs.GetFloat("MS");
        _UISoundsSource.volume = PlayerPrefs.GetFloat("UIS");

        foreach (AudioSource soundsSource in _soundsSources)
        {
            soundsSource.volume = PlayerPrefs.GetFloat("SS");
        }
    }

    private void Update()
    {
        _moneyText.text = _money.ToString();
    }

    public float ReturnSpeedPercentage()
    {
        return _speedSlider.value;
    }

    public void SaveProgres()
    {
        _saveHandler.Save();
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

    public void AddMoney()
    {
        _money++;
    }

    public int ReturnMoney()
    {
        return _money;
    }

    public void LoadRestOfMoney(int rest)
    {
        _money = rest;
    }
}
