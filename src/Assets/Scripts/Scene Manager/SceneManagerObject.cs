using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scene_Manager
{
    public class SceneManagerObject : MonoBehaviour
    {
        public static IEnumerator GameOverSceneOpen()
        {
            yield return new WaitForSeconds(1.5f);
            
            var sceneLoading = SceneManager.LoadSceneAsync("GameOver");
            
            while(!sceneLoading.isDone)
            {
                yield return null;
            }
        }

        public static IEnumerator PrototypeSceneOpen()
        {
            var sceneLoading = SceneManager.LoadSceneAsync("Launching Prototype");

            while (!sceneLoading.isDone)
            {
                yield return null;
            }
        }
    }
}
