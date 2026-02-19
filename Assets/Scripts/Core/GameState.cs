using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState I;

    public bool keyA;
    public bool keyB;
    public bool keyC;

    void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public int KeyCount()
    {
        int c = 0;
        if (keyA) c++;
        if (keyB) c++;
        if (keyC) c++;
        return c;
    }

    public bool HasAllKeys()
    {
        return keyA && keyB && keyC;
    }

    public void ResetKeys()
    {
        keyA = false;
        keyB = false;
        keyC = false;
    }
}
