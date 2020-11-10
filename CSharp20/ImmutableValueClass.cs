using System;

namespace CSharp20
{
    public class ImmutableValueClass : IEquatable<ImmutableValueClass>, IComparable, IComparable<ImmutableValueClass>
    {
        private readonly int _id;
        private readonly string _name;
        private readonly string _remarks;
        private readonly int? _parentId;
        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public string Remarks { get { return _remarks; } }
        public int? ParentId { get { return _parentId; } }

        // Note: This constractor's API is exactly different from C# 4 and later,
        // because overload and default arguments are incompatible.
        public ImmutableValueClass(int id, string name) : this(id, name, null, default(int?))
        {
        }

        public ImmutableValueClass(int id, string name, string remarks) : this(id, name, remarks, default(int?))
        {
        }

        public ImmutableValueClass(int id, string name, int? parentId) : this(id, name, null, parentId)
        {
        }

        public ImmutableValueClass(int id, string name, string remarks, int? parentId)
        {
            _id = id;
            if (ReferenceEquals(name, null))
                throw new ArgumentNullException("name");
            _name = name;
            _remarks = remarks;
            _parentId = parentId;
        }

        public bool Equals(ImmutableValueClass other)
        {
            return !ReferenceEquals(other, null)
                && Id == other.Id
                && Name == other.Name
                && Remarks == other.Remarks
                && ParentId == other.ParentId;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ImmutableValueClass);
        }

        public override int GetHashCode()
        {
            // Note: HashCode struct is only provided in .NET Core 2.1 or later or .NET Framework 4.6.1 or later.
            return HashCode.Combine(Id, Name, Remarks, ParentId);
        }

        public override string ToString()
        {
            return string.Format("ImmutableValueClass: {{ Id: {0}, Name: {1}, Remarks: {2}, ParentId: {3} }}", Id, Name, Remarks, ParentId);
        }

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

            ImmutableValueClass other = obj as ImmutableValueClass;
            if (ReferenceEquals(other, null))
                throw new ArgumentException("obj is not a ImmutableValueClass", "obj");
            
            return CompareTo(other);
        }

        public int CompareTo(ImmutableValueClass other)
        {
            return ReferenceEquals(other, null) ? 1 : Id - other.Id;
        }

        public static bool operator > (ImmutableValueClass left, ImmutableValueClass right)
        {
            return (!ReferenceEquals(left, null) && !ReferenceEquals(right, null) && left.Id > right.Id)
                || (!ReferenceEquals(left, null) && ReferenceEquals(right, null));
        }

        public static bool operator < (ImmutableValueClass left, ImmutableValueClass right)
        {
            return (!ReferenceEquals(left, null) && !ReferenceEquals(right, null) && left.Id < right.Id)
                || (ReferenceEquals(left, null) && !ReferenceEquals(right, null));
        }

        public static bool operator >= (ImmutableValueClass left, ImmutableValueClass right)
        {
            return (!ReferenceEquals(left, null) && !ReferenceEquals(right, null) && left.Id >= right.Id)
                || ReferenceEquals(right, null);
        }

        public static bool operator <= (ImmutableValueClass left, ImmutableValueClass right)
        {
            return (!ReferenceEquals(left, null) && !ReferenceEquals(right, null) && left.Id <= right.Id)
                || ReferenceEquals(left, null);
        }
    }
}
