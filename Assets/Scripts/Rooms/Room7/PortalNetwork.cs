using System.Collections.Generic;
using UnityEngine;

public class PortalNetwork : MonoBehaviour
{
    private Dictionary<string, Transform> idToExit = new Dictionary<string, Transform>();

    void Awake()
    {
        idToExit.Clear();
        PortalDoor[] doors = FindObjectsByType<PortalDoor>(FindObjectsSortMode.None);

        for (int i = 0; i < doors.Length; i++)
        {
            PortalDoor d = doors[i];
            if (d == null) continue;
            if (string.IsNullOrWhiteSpace(d.doorId)) continue;
            if (d.exitPoint == null) continue;
            idToExit[d.doorId] = d.exitPoint;
        }
    }

    public Transform GetExitPoint(string doorId)
    {
        if (string.IsNullOrWhiteSpace(doorId)) return null;
        if (idToExit.TryGetValue(doorId, out Transform t)) return t;
        return null;
    }
}