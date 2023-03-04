using System.Collections;

namespace Sample.FinanceSystem.Domain.Types.Common;

public record ReadOnlyListRecord<T> : IReadOnlyList<T>
{
    private readonly IReadOnlyList<T> _baseList;

    /// <summary>
    /// Gets or initializes the items in this collection.
    /// </summary>
    public IReadOnlyList<T> Items
    {
        get { return _baseList; }
        //init { _baseList = value.ToList().AsReadOnly(); }
    }

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public int Count
    {
        get { return _baseList.Count; }
    }

    /// <inheritdoc />
    public T this[int index] => _baseList[index];

    public ReadOnlyListRecord(IEnumerable<T> list)
    {
        _baseList = list.ToList().AsReadOnly();
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        int someHashValue = -234897289;
        foreach (var item in _baseList)
        {
            someHashValue = someHashValue ^ item.GetHashCode();
        }

        return someHashValue;
    }

    /// <inheritdoc />
    public virtual bool Equals(ReadOnlyListRecord<T> other)
    {
        // create a proper equality method...
        if (other == null || other.Count != Count)
        {
            return false;
        }

        for (int i = 0; i < Count; i++)
        {
            if (!other[i].Equals(this[i]))
            {
                return false;
            }
        }

        return true;
    }
}
