using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecanicaBeneteli.Business.Util
{
    public abstract class ValueObject
    {
        public Guid Id { get; set; }

        protected ValueObject()
        {
            Id = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as ValueObject;
            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;
            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() * 192 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}
