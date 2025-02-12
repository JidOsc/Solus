using UnityEngine;

public class MoveMenuBackground : MonoBehaviour
{
    public bool menuActive;

    void Start()
    {
        
    }

    void Update()
    {
        RectTransform backgroundActive = GetComponent<RectTransform>();

        if (menuActive == true)
        {
            backgroundActive.anchoredPosition = new Vector3(0, 0, 0);
        }
        else
        {
            backgroundActive.anchoredPosition = new Vector3(3000, 0, 0);
        }
    }
}
