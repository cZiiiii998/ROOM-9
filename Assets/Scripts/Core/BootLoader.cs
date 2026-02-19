using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    public string firstSceneName = "S10_Corridor_A";

    void Start()
    {
        SceneManager.LoadScene(firstSceneName);
    }
}
