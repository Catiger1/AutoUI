
public interface IListDS<T>
{
    int GetLength();
    void Clear();
    bool IsEmpty();
    Node<T> Add(T item);
    Node<T> Delete(Node<T> node);
}
