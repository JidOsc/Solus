using UnityEngine;
using Unity.Collections;
using TMPro;
using System.Collections;

public class Text : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public bool changeTextIndex = false;

    public int textIndex; //numret på texten som visas

    void Start()
    {
        textComponent.text = string.Empty;
        StartText();
    }

    void Update()
    {
        if (Input.GetKeyDown(key: KeyCode.E) && textIndex < 8) //siffran behöver ändras till max antalet i arrayn om man ändrar det, alltså max-1 för den börjar på 0
        {
            if (textComponent.text == lines[textIndex])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[textIndex];
            }
        }
        else if (Input.GetKeyDown(key: KeyCode.Q) && textIndex > 0)
        {
            if (textComponent.text == lines[textIndex])
            {
                PastLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[textIndex];
            }
        }
        else if (changeTextIndex == true)
        {
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());

            changeTextIndex = false;
        }
    }

    void StartText()
    {
        textIndex = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[textIndex].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (textIndex < lines.Length - 1)
        {
            textIndex++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void PastLine()
    {
        if (textIndex < lines.Length)
        {
            textIndex--;
            textComponent.text = string.Empty;
            StartCoroutine (TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
