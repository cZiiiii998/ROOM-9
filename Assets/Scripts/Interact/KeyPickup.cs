using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public enum KeyType { A, B, C }
    public KeyType keyType;

    public float pickupRadius = 0.8f;

    Transform player;

    void Start()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
            else return;
        }

        if (!Input.GetKeyDown(KeyCode.E)) return;

        var d = Vector2.Distance(player.position, transform.position);
        if (d > pickupRadius) return;

        var s = GameState.I;
        if (s == null) return;

        if (keyType == KeyType.A) s.keyA = true;
        if (keyType == KeyType.B) s.keyB = true;
        if (keyType == KeyType.C) s.keyC = true;

        if (transform.parent != null) transform.parent.gameObject.SetActive(false);
        else gameObject.SetActive(false);
    }
}
