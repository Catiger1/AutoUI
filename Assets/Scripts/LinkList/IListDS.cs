
public interface IListDS<T>
{
    int GetLength();
    void Clear();
    bool IsEmpty();
    void Add(T item);
    Node<T> Delete(Node<T> node);
}
