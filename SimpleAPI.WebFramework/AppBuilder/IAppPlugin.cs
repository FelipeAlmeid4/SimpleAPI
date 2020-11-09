namespace SimpleAPI.WebFramework.AppBuilder
{
    public interface IAppPlugin
    {
        void Configure(IAppBuilder builder);
    }
}
