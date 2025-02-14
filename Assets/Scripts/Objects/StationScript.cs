using UnityEngine;
using UnityEngine.SceneManagement;

public class StationScript : MonoBehaviour
{
    public AudioClip radio;
    public Transform player;
    public float maxDistance = 5f;

    private AudioSource audioSource;

    const short AMOUNT_OF_STAGES = 1; //när stage når AMOUNT_OF_STAGES vinner spelaren
    
    int[] stage_cost = new int[] { 7 }; //stage i innebär att kostnaden är stage_cost[i], stage_cost.length ska alltid vara lika stor som AMOUNT_OF_STAGES
    short stage = 0;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= maxDistance)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(radio, 0.5f);
                }
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }
    }

    public int CurrentCost()
    {
        return stage_cost[stage];
    }

    public void Repair()
    {
        stage++;

        if(stage >= AMOUNT_OF_STAGES)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
        }
    }
}
