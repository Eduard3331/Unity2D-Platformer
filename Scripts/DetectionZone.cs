using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedColliders = new List<Collider2D>();

    private bool _isFacingPlayer;
    public bool IsFacingPlayer
    {
        get
        {
            return _isFacingPlayer;
        }
        set
        {
            _isFacingPlayer = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);
    }

}
