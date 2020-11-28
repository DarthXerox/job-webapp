using System;
using Infrastructure;

namespace Business.QueryObjects
{
    public abstract class QueryObject
    {
        protected QueryObject(UnitOfWork unit)
        {
            UnitOfWork = unit;
        }

        public UnitOfWork UnitOfWork { get; }
    }
}
