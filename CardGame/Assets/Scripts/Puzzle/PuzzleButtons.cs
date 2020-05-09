using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleButtons : MonoBehaviour
{
    public LoadScene loadScene;

    public void onRestartPress()
    {
        loadScene.LoadNewScene("Puzzle");
    }
}
