using Assets.Scripts.Scene_Manager;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void ResetBtn()
    {
        StartCoroutine(SceneManagerObject.PrototypeSceneOpen());
    }
}
