using UnityEngine;
using UnityEngine.SceneManagement;

namespace TappyTale
{
    public class SceneBootstrap : MonoBehaviour
    {
        void Awake()
        {
            Application.targetFrameRate = 120;
            SaveService.Load();
        }
        void Start()
        {
            if (SceneManager.GetActiveScene().name == SceneNames.Bootstrap)
            {
                SceneManager.LoadScene(SceneNames.MainMenu);
            }
        }
    }
}
