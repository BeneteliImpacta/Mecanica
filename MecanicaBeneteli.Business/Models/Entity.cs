using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecanicaBeneteli.Business.Models
{
    public abstract class Entity
    {
        public string Codigo { get; set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Codigo.Equals(compareTo.Codigo);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 192) + Codigo.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Codigo={Codigo}]";
        }
    }
}
