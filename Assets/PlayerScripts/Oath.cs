namespace PlayerScripts
{
    public interface Oath
    {
        string name();
        bool didBreak();
        bool hasBeenBroken();
        God god();
    }
}
