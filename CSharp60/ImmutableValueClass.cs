using System;

namespace CSharp60
{
    public class ImmutableValueClass : IEquatable<ImmutableValueClass>, IComparable, IComparable<ImmutableValueClass>
    {
        public int Id { get; }
        public string Name { get; }
        public string Remarks { get; }
        public int? ParentId { get; }

        public ImmutableValueClass(int id, string name, string remarks = null, int? parentId = default(int?))
        {
            Id = id;
            if (ReferenceEquals(name, null))
                throw new ArgumentNullException(nameof(name));
            Name = name;
            Remarks = remarks;
            ParentId = parentId;
        }

        public bool Equals(ImmutableValueClass other)
            => !ReferenceEquals(other, null)
            && Id == other.Id
            && Name == other.Name
            && Remarks == other.Remarks
            && ParentId == other.ParentId;

        public override bool Equals(object obj)
            => Equals(obj as ImmutableValueClass);

        public override int GetHashCode()
            // Note: HashCode struct is only provided in .NET Core 2.1 or later or .NET Framework 4.6.1 or later.
            => HashCode.Combine(Id, Name, Remarks, ParentId);

        public override string ToString()
            => $"{nameof(ImmutableValueClass)}: {{ {nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Remarks)}: {Remarks}, {nameof(ParentId)}: {ParentId} }}";

        // Note: This method is not useful in C# 6 and below.
        public void Deconstract(out int id, out string name, out string remarks, out int? parentId)
        {
            id = Id;
            name = Name;
            remarks = Remarks;
            parentId = ParentId;
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
                return 1;

            var other = obj as ImmutableValueClass;
            if (ReferenceEquals(other, null))
                throw new ArgumentException(nameof(obj) + " is not a " + nameof(ImmutableValueClass), nameof(obj));
            
            return CompareTo(other);
        }

        public int CompareTo(ImmutableValueClass other)
            => ReferenceEquals(other, null) ? 1 : Id - other.Id;

        public static bool operator > (ImmutableValueClass left, ImmutableValueClass right)
            => (!ReferenceEquals(left, null) && !ReferenceEquals(right, null) && left.Id > right.Id)
            || (!ReferenceEquals(left, null) && ReferenceEquals(right, null));

        public static bool operator < (ImmutableValueClass left, ImmutableValueClass right)
            => (!ReferenceEquals(left, null) && !ReferenceEquals(right, null) && left.Id < right.Id)
            || (ReferenceEquals(left, null) && !ReferenceEquals(right, null));

        public static bool operator >= (ImmutableValueClass left, ImmutableValueClass right)
            => (!ReferenceEquals(left, null) && !ReferenceEquals(right, null) && left.Id >= right.Id)
            || ReferenceEquals(right, null);

        public static bool operator <= (ImmutableValueClass left, ImmutableValueClass right)
            => (!ReferenceEquals(left, null) && !ReferenceEquals(right, null) && left.Id <= right.Id)
            || ReferenceEquals(left, null);
    }
}
