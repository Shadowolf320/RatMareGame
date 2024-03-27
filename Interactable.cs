using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocused = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        // This will be overwritten 
        Debug.Log("Interacting with " + transform.name);
    }

    void Update()
    {
        if (isFocused && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);

            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }

    }
    public void OnFocused(Transform playertransform)
    {
        isFocused = true;
        player = playertransform;
        hasInteracted = false;
    }

    public void OnDeFocused()
    {
        isFocused = false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);

    }
}
