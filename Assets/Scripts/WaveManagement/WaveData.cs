using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Waves/Wave Data", fileName = "New Wave")]
public class WaveData : ScriptableObject
{
    [System.Serializable]
    public struct Entry
    {
        public Enemy prefab;
        public int amount;
    }

    public Entry[] enemies;
    public float spawnInterval = 1f;
    public float duration = 30f;
}

