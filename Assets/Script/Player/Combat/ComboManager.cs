using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private const int MARK_INDEX = 2;
    private const float DAMAGE_DELAY_STRIKE1 = 0.24f;
    private const float DAMAGE_DELAY_STRIKE2 = 0.24f;
    private const float DAMAGE_DELAY_MARK = 0.8f;
    private const float DAMAGE_HOLD_STRIKE1 = 0.1f;
    private const float DAMAGE_HOLD_STRIKE2 = 0.1f;
    private const float DAMAGE_HOLD_MARK = 0.3f;

    private readonly float[] m_damageDelays =
        { DAMAGE_DELAY_STRIKE1, DAMAGE_DELAY_STRIKE2, DAMAGE_DELAY_MARK };
    private readonly float[] m_damageHolds =
        { DAMAGE_HOLD_STRIKE1, DAMAGE_HOLD_STRIKE2, DAMAGE_HOLD_MARK };

    [SerializeField] List<GameObject> m_attackObjects;
    [SerializeField] List<Transform> m_attackRightTransforms;
    [SerializeField] List<Transform> m_attackLeftTransforms;
    [SerializeField] GameObject m_moveHorizontalObject;
    [SerializeField] GameObject m_formManagerObject;
    [SerializeField] GameObject m_comboObject;
    [SerializeField] GameObject m_moveMarkObject;
    [SerializeField] SpriteRenderer m_sprite;

    List<Vector3> m_attackDefaultPositions;
    List<HitBox> m_hitboxes;
    MoveHorizontal m_moveHorizontal;
    FormManager m_formManager;
    Combo m_combo;
    MoveMark m_moveMark;
    float m_delay;
    int m_attackCount;
    int m_currentComboIndex;
    bool m_isSpawn;
    bool m_isSent;
    bool m_isSpriteFacingRight;

    void Awake()
    {
        m_moveHorizontal = m_moveHorizontalObject.GetComponent<MoveHorizontal>();
        m_formManager = m_formManagerObject.GetComponent<FormManager>();
        m_combo = m_comboObject.GetComponent<Combo>();
        m_moveMark = m_moveMarkObject.GetComponent<MoveMark>();

        m_attackDefaultPositions = new List<Vector3>();
        m_hitboxes = new List<HitBox>();

        foreach (GameObject attack in m_attackObjects)
        {
            var defaultPosition = attack.transform.position;

            m_attackDefaultPositions.Add(defaultPosition);
            m_hitboxes.Add(attack.GetComponentInChildren<HitBox>());
        }

        m_attackCount = m_attackObjects.Count;
        m_isSpawn = false;
        m_isSent = false;
        m_currentComboIndex = 0;
        m_isSpriteFacingRight = true;
    }

    void Update()
    {
        if (m_formManager.CurrentForm == Form.Two)
        {
            if (!m_combo.IsIdle)
            {
                Combo();
            }
            else
            {
                End();

                m_isSpawn = false;
                m_isSent = false;
                m_currentComboIndex = 0;
            }
        }
        else
        {
            End();

            m_isSpawn = false;
            m_isSent = false;
            m_currentComboIndex = 0;
        }
    }

    private void Combo()
    {
        m_delay += Time.deltaTime;

        if (m_currentComboIndex == m_combo.ComboIndex)
        {
            if (m_delay > m_damageDelays[m_currentComboIndex])
            {
                SpawnAndSend();
            }

            if (m_isSent && m_delay > m_damageHolds[m_currentComboIndex])
            {
                End();
            }
        }
        else
        {
            End();

            m_isSpawn = false;
            m_isSent = false;
            m_currentComboIndex = m_combo.ComboIndex;
        }
    }

    void SpawnAndSend()
    {
        if (!m_isSpawn)
        {
            if (m_delay > m_damageDelays[m_currentComboIndex])
            {
                SpawnAttack();

                m_delay = 0.0f;
                m_isSpawn = true;
                m_isSent = true;

                SendAttack();
            }
        }
    }

    void SpawnAttack()
    {
        m_currentComboIndex = m_combo.ComboIndex;

        if (m_currentComboIndex >= 0 && m_currentComboIndex < m_attackCount)
        {
            var position = m_attackLeftTransforms[m_currentComboIndex].transform.position;

            if (m_isSpriteFacingRight)
            {
                m_sprite.flipX = true;
                m_isSpriteFacingRight = false;
            }

            if (m_moveHorizontal.Direction == MoveDirection.Right)
            {
                if (!m_isSpriteFacingRight)
                {
                    m_sprite.flipX = false;
                    m_isSpriteFacingRight = true;
                }

                position = m_attackRightTransforms[m_currentComboIndex].transform.position;
            }

            m_attackObjects[m_currentComboIndex].transform.position = position;
        }
    }

    void SendAttack()
    {
        m_hitboxes[m_currentComboIndex].IsActive = true;

        if (m_currentComboIndex == MARK_INDEX)
        {
            m_moveMark.IsMove = true;
        }
    }

    void End()
    {
        m_attackObjects[m_currentComboIndex].transform.position =
            m_attackDefaultPositions[m_currentComboIndex];

        m_delay = 0.0f;
        m_hitboxes[m_currentComboIndex].IsActive = false;
        m_moveMark.IsMove = false;
    }
}