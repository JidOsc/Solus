using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class VerticalBox : MonoBehaviour
{
    public int MARGIN = 20;


    void Update()
    {
        short ApplicableItems = 0;

        foreach (Transform t in transform)
        {
            Vector2 pos = new Vector2();
            pos.x = transform.position.x;
            pos.y = transform.position.y - ApplicableItems * (MARGIN + 60);

            t.position = pos;

            if (t.gameObject.activeSelf)
            {
                ApplicableItems++;
            }
        }
    }

    public void GameButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void SettingsButtonPressed()
    {
        
    }

    public void CreditsButtonPressed()
    {

    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    public void ToggledFullscreen(bool state)
    {
        Screen.fullScreen = state;
    }

    public void ChangedSensitivity(string value)
    {
        int new_value = int.Parse(value);

        PlayerPrefs.SetFloat("SENSITIVITY", new_value);
        PlayerPrefs.Save();
    }

    public void ChangedVolume(float value)
    {
        PlayerPrefs.SetFloat("VOLUME", value);
        PlayerPrefs.Save();
    }
}
