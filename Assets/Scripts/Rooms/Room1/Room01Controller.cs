using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room01Controller : MonoBehaviour
{
    public float enterDelay = 2f;
    public int flashCount = 6;
    public float flashOn = 0.45f;
    public float flashOff = 0.35f;
    public float retryDelay = 2f;

    public SpriteRenderer ceilingLight;
    public ColorPad[] pads;

    public GameObject keyToReveal;

    public GameObject backDoorLockVisual;
    public Collider2D backDoorLockBlocker;
    public Collider2D backDoorTrigger;

    public StoneGateWideMask gate;

    public Color red = new Color(1f, 0.2f, 0.2f, 1f);
    public Color yellow = new Color(1f, 0.9f, 0.2f, 1f);
    public Color blue = new Color(0.25f, 0.55f, 1f, 1f);

    List<int> sequence = new List<int>();
    int inputIndex;
    bool accepting;
    bool success;

    void Start()
    {
        if (keyToReveal != null) keyToReveal.SetActive(false);

        if (backDoorLockVisual != null) backDoorLockVisual.SetActive(true);
        if (backDoorLockBlocker != null) backDoorLockBlocker.enabled = true;
        if (backDoorTrigger != null) backDoorTrigger.enabled = false;

        if (gate != null) gate.CloseInstant();

        SetLight(Color.white);
        SetPadsInactive();

        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        yield return new WaitForSeconds(enterDelay);

        while (!success)
        {
            SetPadsInactive();
            SetLight(Color.white);

            GenerateSequence();
            yield return PlaySequence();

            SetPadsActive();
            accepting = true;
            inputIndex = 0;

            while (accepting)
                yield return null;

            if (!success)
            {
                SetPadsInactive();
                SetLight(Color.white);
                yield return new WaitForSeconds(retryDelay);
            }
        }

        SetPadsActive();
        SetLight(Color.white);

        if (gate != null)
            gate.Open();

        if (backDoorLockVisual != null)
            backDoorLockVisual.SetActive(false);

        if (backDoorLockBlocker != null)
            backDoorLockBlocker.enabled = false;

        if (backDoorTrigger != null)
            backDoorTrigger.enabled = true;

        if (keyToReveal != null)
            keyToReveal.SetActive(true);

        DoorToScene door = FindObjectOfType<DoorToScene>();
        if (door != null)
            door.canUse = true;
    }

    void GenerateSequence()
    {
        sequence.Clear();

        for (int i = 0; i < flashCount; i++)
        {
            sequence.Add(Random.Range(0, 3));
        }
    }

    IEnumerator PlaySequence()
    {
        for (int i = 0; i < sequence.Count; i++)
        {
            SetLight(IndexToColor(sequence[i]));
            yield return new WaitForSeconds(flashOn);

            SetLight(Color.white);
            yield return new WaitForSeconds(flashOff);
        }
    }

    public void Press(int colorIndex, ColorPad pad)
    {
        if (!accepting) return;
        if (inputIndex >= sequence.Count) return;

        if (sequence[inputIndex] != colorIndex)
        {
            if (pad != null)
                pad.Flash(new Color(1f, 0.15f, 0.15f, 1f), 0.12f);

            accepting = false;
            return;
        }

        if (pad != null)
            pad.Flash(new Color(1f, 1f, 1f, 1f), 0.06f);

        inputIndex++;

        if (inputIndex >= sequence.Count)
        {
            success = true;
            accepting = false;
        }
    }

    void SetPadsInactive()
    {
        if (pads == null) return;

        for (int i = 0; i < pads.Length; i++)
        {
            if (pads[i] != null)
                pads[i].SetPad(false, Color.black);
        }
    }

    void SetPadsActive()
    {
        if (pads == null) return;

        for (int i = 0; i < pads.Length; i++)
        {
            if (pads[i] == null) continue;

            pads[i].SetPad(true, IndexToColor(pads[i].colorIndex));
        }
    }

    void SetLight(Color c)
    {
        if (ceilingLight != null)
            ceilingLight.color = c;
    }

    Color IndexToColor(int idx)
    {
        if (idx == 0) return red;
        if (idx == 1) return yellow;
        return blue;
    }
}