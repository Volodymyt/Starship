using UnityEngine;
using UnityEngine.UI;

public class SoundsSave : MonoBehaviour
{
    [SerializeField] private Slider _soundsSlider, _musicSlider, _UISlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MS"))
        {
            PlayerPrefs.SetFloat("MS", 0.2f);
        }
        
        if (!PlayerPrefs.HasKey("UIS"))
        {
            PlayerPrefs.SetFloat("UIS", 0.7f);
        }
        
        if (!PlayerPrefs.HasKey("SS"))
        {
            PlayerPrefs.SetFloat("SS", 0.3f);
        }

        _soundsSlider.value = PlayerPrefs.GetFloat("SS");
        _musicSlider.value = PlayerPrefs.GetFloat("MS");
        _UISlider.value = PlayerPrefs.GetFloat("UIS");
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("MS", _musicSlider.value);
        PlayerPrefs.SetFloat("SS", _soundsSlider.value);
        PlayerPrefs.SetFloat("UIS", _UISlider.value);
    }
}
