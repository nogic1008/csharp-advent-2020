using System;

namespace CSharp73
{
    public class ImmutableValueClass : IEquatable<ImmutableValueClass>, IComparable, IComparable<ImmutableValueClass>
    {
        public int Id { get; }
        public string Name { get; }
        public string Remarks { get; }
        public int? ParentId { get; }

        public ImmutableValueClass(int id, string name, string remarks = null, int? parentId = default)
            => (Id, Name, Remarks, ParentId) = (id, name ?? throw new ArgumentNullException(nameof(name)), remarks, parentId);

        public bool Equals(ImmutableValueClass other)
            => !(other is null)
            && (Id, Name, Remarks, ParentId) == (other.Id, other.Name, other.Remarks, other.ParentId);

        public override bool Equals(object obj)
            => obj is ImmutableValueClass other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(Id, Name, Remarks, ParentId);

        public override string ToString()
            => $"{nameof(ImmutableValueClass)}: {{ {nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Remarks)}: {Remarks}, {nameof(ParentId)}: {ParentId} }}";

        public void Deconstract(out int id, out string name, out string remarks, out int? parentId)
            => (id, name, remarks, parentId) = (Id, Name, Remarks, ParentId);

        public int CompareTo(object obj)
            => obj is null ? 1
            : obj is ImmutableValueClass other ? CompareTo(other)
            : throw new ArgumentException(nameof(obj) + " is not a " + nameof(ImmutableValueClass), nameof(obj));

        public int CompareTo(ImmutableValueClass other)
            => other is null ? 1 : Id - other.Id;

        public static bool operator > (ImmutableValueClass left, ImmutableValueClass right)
            => (!(left is null) && !(right is null) && left.Id > right.Id)
            || (!(left is null) && right is null);

        public static bool operator < (ImmutableValueClass left, ImmutableValueClass right)
            => (!(left is null) && !(right is null) && left.Id < right.Id)
            || (left is null && !(right is null));

        public static bool operator >= (ImmutableValueClass left, ImmutableValueClass right)
            => (!(left is null) && !(right is null) && left.Id >= right.Id)
            || right is null;

        public static bool operator <= (ImmutableValueClass left, ImmutableValueClass right)
            => (!(left is null) && !(right is null) && left.Id <= right.Id)
            || left is null;
    }
}
