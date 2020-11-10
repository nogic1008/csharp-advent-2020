using System;

namespace CSharp40
{
    public class ImmutableValueClass : IEquatable<ImmutableValueClass>, IComparable, IComparable<ImmutableValueClass>
    {
        // Do not use automatic property implementation
        private readonly int _id;
        private readonly string _name;
        public int Id { get { return _id; } }
        public string Name { get { return _name; } }

        // Another way: "private set" property
        // However, you need to be careful because you can change its value in this class.
        public string Remarks { get; private set; }
        public int? ParentId { get; private set; }

        public ImmutableValueClass(int id, string name, string remarks = null, int? parentId = default(int?))
        {
            _id = id;
            if (ReferenceEquals(name, null))
                throw new ArgumentNullException("name");
            _name = name;
            Remarks = remarks;
            ParentId = parentId;
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

            var other = obj as ImmutableValueClass;
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
