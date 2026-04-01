using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState I;

    public enum KeyType
    {
        None,
        A,
        B,
        C
    }

    public bool keyA;
    public bool keyB;
    public bool keyC;

    public KeyType nextRewardKey = KeyType.None;

    public string returnSceneName;
    public string returnSpawnPointId;

    void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GiveKey(KeyType type)
    {
        if (type == KeyType.A) keyA = true;
        if (type == KeyType.B) keyB = true;
        if (type == KeyType.C) keyC = true;
    }

    public bool HasKey(KeyType type)
    {
        if (type == KeyType.A) return keyA;
        if (type == KeyType.B) return keyB;
        if (type == KeyType.C) return keyC;
        return false;
    }

    public bool HasAllKeys()
    {
        return keyA && keyB && keyC;
    }

    public void ResetRun()
    {
        keyA = false;
        keyB = false;
        keyC = false;
        nextRewardKey = KeyType.None;
        returnSceneName = "";
        returnSpawnPointId = "";
    }
}