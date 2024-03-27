using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;
    public Interactable focus;

    private Animator animator;
    PlayerMotor motor;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                // move player to the mouse click

                motor.MoveToPoint(hit.point);

                RemoveFocus();
            }
            

        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    Debug.Log("We working");
                    SetFocus(interactable);
                }
            }
        }

        void SetFocus (Interactable newFocus)
        {
            if (newFocus != focus)
            {
                if (focus != null)
                {
                    focus.OnDeFocused();
                }
                
                focus = newFocus;
                motor.FollowTarget(newFocus);
            }

            newFocus.OnFocused(transform);
        }

        void RemoveFocus()
        {
            if (focus != null)
            {
                focus.OnDeFocused();
            }

            focus = null;
            motor.StopFollowingTarget();
        }
    }
}
