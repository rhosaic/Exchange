using UnityEngine;

public class MarkBox : HitBox
{
    void Awake()
    {
        m_filter.SetLayerMask(m_searchLayer);

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
            if (collision.TryGetComponent<HurtBox>(out var hurtbox))
            {
                CheckMark(hurtbox);
            }
        }
    }

    void CheckMark(HurtBox hurtbox)
    {
        if (hurtbox.StatusDisplay.Status.Composure.IsZero())
        {
            hurtbox.IsMarked = true;
            IsActive = false;
        }
    }
}