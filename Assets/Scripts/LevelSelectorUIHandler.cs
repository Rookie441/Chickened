using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorUIHandler : MonoBehaviour
{
    public void NewLevelSelected(int level)
    {
        MainManager.Instance.currentLevel = level;
        MainManager.Instance.LoadLevel();
    }
}
