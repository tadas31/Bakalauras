using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Animator transition;     // Animator for transition.

    public float time = 1;  // Time that animation plays.

    /// <summary>
    ///  Loads scene.
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadNewScene(string sceneName)
    {
        StartCoroutine( Load(sceneName) );
    }

    /// <summary>
    /// Plays animation and loads scene.
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    IEnumerator Load(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(sceneName);
    }
}
