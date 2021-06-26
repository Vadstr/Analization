
namespace Analization
{
    public interface INode<T>
    {
        T Data { get; }
        char Operation { get; }
        void ChangeData(T newDate);
    }
}
