using UnityEngine;
using UnityEngine.SceneManagement;

public class StationScript : MonoBehaviour
{
    const short AMOUNT_OF_STAGES = 1; //n�r stage n�r AMOUNT_OF_STAGES vinner spelaren
    
    int[] stage_cost = new int[] { 7 }; //stage i inneb�r att kostnaden �r stage_cost[i], stage_cost.length ska alltid vara lika stor som AMOUNT_OF_STAGES
    short stage = 0;

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
