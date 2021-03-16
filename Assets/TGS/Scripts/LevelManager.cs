using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu()]
public class LevelManager : ScriptableObject
{
    public List<Level> Levels;
    public GameObject Prefab;
    [System.Serializable]
    public struct Level
    {
        public string Name;
        public string info;
        public int  Reward;
        public Sprite icon;
    }
}
