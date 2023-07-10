using UnityEngine;

public class SkipPrologue : MonoBehaviour
{
    public GameObject sceneLoaderScript;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            sceneLoaderScript.SetActive(true);
    }
}
