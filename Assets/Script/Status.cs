public class Health
{
    public float Maximum { get => m_maximum; private set { } }
    public float Current { get => m_current; private set { } }

    private float m_current;
    private float m_maximum;

    public Health(float maximum)
    {
        if (maximum > 0.0f)
        {
            m_maximum = maximum;
        }
        else
        {
            m_maximum = 0.0f;
        }

        m_current = m_maximum;
    }

    public void HealCapped(float value)
    {
        if (m_current + value > m_maximum)
        {
            m_current = m_maximum;
        }
        else
        {
            m_current += value;
        }
    }

    public void DamageFloored(float value)
    {
        if (m_current - value < 0.0f)
        {
            m_current = 0.0f;
        }
        else
        {
            m_current -= value;
        }
    }

    public float PercentRemaining()
    {
        var percent = (float)m_current / (float)m_maximum;

        return percent;
    }

    public bool IsZero()
    {
        return m_current < float.Epsilon;
    }
}

public class Status
{
    public Health Composure { get => m_composure; private set { } }
    public Health Health { get => m_health; private set { } }
    private Health m_composure;
    private Health m_health;

    public Status(float composureMaximum, float healthMaximum)
    {
        m_composure = new Health(composureMaximum);
        m_health = new Health(healthMaximum);
    }

    public void HealComposureCapped(float value)
    {
        m_composure.HealCapped(value);
    }

    public void HealHealthCapped(float value)
    {
        m_health.HealCapped(value);
    }

    public void DamageComposureArmor(float value)
    {
        if (value > float.Epsilon)
        {
            var composureRemaining = m_composure.PercentRemaining();
            var healthRemaining = m_health.PercentRemaining();

            if (composureRemaining > healthRemaining)
            {
                m_composure.DamageFloored(value);
            }
            else
            {
                var healthComposureDifference
                    = healthRemaining - composureRemaining;

                var healthDamage
                    = m_health.Maximum * healthComposureDifference;

                if (healthDamage > value)
                {
                    m_health.DamageFloored(value);
                }
                else
                {
                    var composureDamage = value - healthDamage;

                    m_health.DamageFloored(healthDamage);
                    m_composure.DamageFloored(composureDamage);
                }
            }
        }
    }
}
