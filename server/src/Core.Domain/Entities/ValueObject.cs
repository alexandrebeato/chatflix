namespace Core.Domain.Entities
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public override bool Equals(object? obj)
        {
            var valueObject = obj as T;
            return !ReferenceEquals(valueObject, null);
        }

        public override int GetHashCode() =>
            base.GetHashCode();

        public override string? ToString() =>
            base.ToString();

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b) =>
            !(a == b);
    }
}