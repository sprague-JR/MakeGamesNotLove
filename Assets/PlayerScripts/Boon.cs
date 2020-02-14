namespace PlayerScripts
{
    public interface Boon
    {
        God god();
        uint damage();
        DamageType damageType();
    }
}