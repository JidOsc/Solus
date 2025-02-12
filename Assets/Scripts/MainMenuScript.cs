using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

[ExecuteInEditMode]
public class VerticalBox : MonoBehaviour
{
    public int MARGIN = 20;
    public GameObject sensitivity_field;

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
        MoveMenuBackground moveMenuBackground = GetComponent<MoveMenuBackground>();
        moveMenuBackground.menuActive = true;
        StartCoroutine(startDelay());
        SceneManager.LoadScene(1);
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
        int new_value = int.Parse(sensitivity_field.GetComponent<TMP_InputField>().text);
        PlayerPrefs.SetFloat("SENSITIVITY", new_value);
        PlayerPrefs.Save();
    }

    public void ChangedVolume(float value)
    {
        PlayerPrefs.SetFloat("VOLUME", value);
        PlayerPrefs.Save();
    }

    private IEnumerator startDelay() //Delay till när man har tryckt play och bakgrunden ändras
    {
        yield return new WaitForSeconds(5);
    }
}
