using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace WebAPILab.Infrastructure.Attributes
{
    public class CustomFilterInfo : IComparable
    {
        public IFilter Instance { get; set; }
        public FilterScope Scope { get; set; }

        public CustomFilterInfo(IFilter instance, FilterScope scope)
        {
            this.Instance = instance;
            this.Scope = scope;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is CustomFilterInfo))
            {
                throw new ArgumentException("Object is of wrong type");
            }

            var item = obj as CustomFilterInfo;

            if (!(item.Instance is IBaseAttribute))
            {
                throw new ArgumentException("Object is of wrong type");
            }

            var attr = item.Instance as IBaseAttribute;
            return (this.Instance as IBaseAttribute).Order.CompareTo(attr.Order);
        }

        public FilterInfo ConvertToFilterInfo()
        {
            return new FilterInfo(this.Instance, this.Scope);
        }
    }
}