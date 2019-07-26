namespace ServiceManagement.Core.Interfaces
{
    public interface IBuilder<TOut>
    {
        TOut Build();
    }
}