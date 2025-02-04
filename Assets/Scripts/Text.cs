using UnityEngine;
using Unity.Collections;
using TMPro;
using System.Collections;

public class Text : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    public int textIndex;

    void Start()
    {
        textComponent.text = string.Empty;
        StartText();
    }

    void Update()
    {
        if (Input.GetKeyDown(key: KeyCode.C) && textIndex < 7) //sifran behöver ändras till max antalet i arrayn om man ändrar det, alltså max-1 för den börjar på 0
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
        else if (Input.GetKeyDown(key: KeyCode.X) && textIndex > 0)
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
        else if (Input.GetKeyDown(key: KeyCode.X) && textIndex == 7) //samma sak med denna siffran som den översta
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
        else { }
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
