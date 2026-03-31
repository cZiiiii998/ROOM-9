using UnityEngine;

public class KeyInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject keyIcon1;
    [SerializeField] private GameObject keyIcon2;
    [SerializeField] private GameObject keyIcon3;

    private void Update()
    {
        if (GameState.I == null) return;

        int keyCount = 0;

        if (GameState.I.keyA) keyCount++;
        if (GameState.I.keyB) keyCount++;
        if (GameState.I.keyC) keyCount++;

        keyIcon1.SetActive(keyCount >= 1);
        keyIcon2.SetActive(keyCount >= 2);
        keyIcon3.SetActive(keyCount >= 3);
    }
}