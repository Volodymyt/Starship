using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeinMenuOptions : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource, _musicSource;
    [SerializeField] private AudioClip _buttonSound;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Load"))
        {
            PlayerPrefs.SetInt("Load", 0);
        }
    }

    private void Update()
    {
        _musicSource.volume = PlayerPrefs.GetFloat("MS");
        _audioSource.volume = PlayerPrefs.GetFloat("UIS");
    }

    public void LoadGame(int Number)
    {
        PlayerPrefs.SetInt("Load", Number);
    }

    public void ClosePanel(GameObject Panel)
    {
        Panel.SetActive(false);
    }
    
    public void OpenPanel(GameObject Panel)
    {
        Panel.SetActive(true);
    }

    public void LoadScene(int Scene)
    {
        StartCoroutine(LoadSceneButton(Scene));
    }

    public void Quit()
    {
        StartCoroutine(QuitButton());
    }


    private IEnumerator LoadSceneButton(int Scene)
    {
        _audioSource.PlayOneShot(_buttonSound);

        yield return new WaitForSeconds(0.3f);

        SceneManager.LoadScene(Scene);
        Time.timeScale = 1.0f;
    }
    
    private IEnumerator QuitButton()
    {
        _audioSource.PlayOneShot(_buttonSound);

        yield return new WaitForSeconds(0.3f);

        Application.Quit();
    }
}
