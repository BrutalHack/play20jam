using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class MenuController : MonoBehaviour
    {
        private FMOD.Studio.Bus _masterBus;
        [SerializeField] private string _masterBusString = "Bus:/";

        private void Start()
        {
            _masterBus = FMODUnity.RuntimeManager.GetBus(_masterBusString);
        }

        public void StartGame()
        {
            SceneManager.LoadScene(UnitySceneIndices.Game);
        }

        public void SetVolume(float volume)
        {
            _masterBus.setVolume(volume);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}