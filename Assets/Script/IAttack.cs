public interface IAttack
{
    public DamageInfo DamageInfo { get; }
    public void Begin();
    public void End();
}