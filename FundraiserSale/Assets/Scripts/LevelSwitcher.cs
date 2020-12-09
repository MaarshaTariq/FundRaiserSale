using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour {

    public int nextLevelIndex;
    public GameManager manager;
    public void nextLevel()
    {
        manager.LevelFinish(nextLevelIndex);
    }
}
