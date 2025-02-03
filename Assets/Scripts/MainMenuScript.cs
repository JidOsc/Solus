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
        GameObject credits = transform.Find("CreditsText").gameObject;

        credits.SetActive(!credits.activeSelf);
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }
}
