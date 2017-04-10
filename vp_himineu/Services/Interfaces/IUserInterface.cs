namespace Vp_himineu.Services.Interfaces
{
    public interface IUserInterface
    {
        string ReadLine();

        void WriteLine(string format, params string[] args);
    }
}
