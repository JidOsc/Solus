using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class VerticalBox : MonoBehaviour
{
    public int MARGIN = 20;
    public GameObject sensitivity_field;
    [SerializeField] GameObject background;


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
        StartCoroutine(BackgroundFade());
        Invoke("StartGame", 3.0f);
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

    //När man trycker play så fadar bilden av planeten med ögonen in, sedan kommer man in i spelet
    public IEnumerator BackgroundFade() 
    { 
        Color color = new UnityEngine.Color();

        color = background.GetComponent<Image>().color;
        color.a = 1f;

        while (color.a > 0 && color.a < 255)
        {
            yield return new WaitForSeconds(0.1f);
            color.a += 0.1f;
            background.GetComponent<Image>().color = color;
        }
        //SceneManager.LoadScene(1);

        yield return new WaitForSeconds(1);
    }

    public void  StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
