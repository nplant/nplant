namespace NPlant.UI
{
    public interface IResultScreen<T> : IScreen
    {
        T GetResult();
    }
}