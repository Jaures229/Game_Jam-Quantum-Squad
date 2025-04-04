using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private Joystick joystick; // Assigner le joystick dans l'inspector
    [SerializeField] private float speed = 5f;
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Récupérer la direction du joystick
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        // Normaliser pour garder la vitesse constante
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // Gérer les animations en fonction du mouvement
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        // Déplacer le joueur
        rb.velocity = movement * speed;
    }

    private void UpdateAnimation()
    {
        if (movement.magnitude > 0.1f) // Si on bouge
        {
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y)) // Mouvement horizontal
            {
                if (movement.x > 0)
                    animator.Play("Right");
                else
                    animator.Play("Left");
            }
            else // Mouvement vertical
            {
                if (movement.y > 0)
                    animator.Play("Up");
                else
                    animator.Play("Down");
            }
        }
        else // Si on ne bouge pas → animations Idle
        {
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
            if (state.IsName("Right"))
                animator.Play("Idle4");
            else if (state.IsName("Left"))
                animator.Play("Idle3");
            else if (state.IsName("Up"))
                animator.Play("Idle1");
            else if (state.IsName("Down"))
                animator.Play("Idle2");
        }
    }
}
