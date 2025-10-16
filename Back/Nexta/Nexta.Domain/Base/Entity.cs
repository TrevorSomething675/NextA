namespace Nexta.Domain.Base
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        protected Entity(Guid id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if(obj == null || GetType() != obj.GetType())
                return false;

            if(ReferenceEquals(this, obj))
                return true;

            if(GetType() != obj.GetType())
                return false;

            var otherObj = (Entity)obj;
            return Id == otherObj.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity? a, Entity? b)
        {
            if (a is null && b is null)
                return false;
            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity? a, Entity? b)
        {
            return !(a == b);
        }
    }
}