using UnityEngine;

public class Markbox : Hitbox
{
    void Awake()
    {
        IsActive = false;
    }
    void Update()
    {
        if (IsActive)
        {
            HitboxProcess(Mark);
        }
    }

    void Mark(Collider2D collision)
    {
        if (collision)
        {
            if (collision.TryGetComponent<Hurtbox>(out var hurtbox))
            {
                CheckMark(hurtbox);
            }
        }
    }

    void CheckMark(Hurtbox hurtbox)
    {
        if (hurtbox.StatusDisplay.Status.Composure.IsZero())
        {
            hurtbox.IsMarked = true;
        }
    }
}