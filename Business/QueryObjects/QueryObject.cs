using System;
using Infrastructure;

namespace Business.QueryObjects
{
    public abstract class QueryObject : IDisposable
    {
        protected QueryObject(UnitOfWork unit)
        {
            UnitOfWork = unit;
        }

        public UnitOfWork UnitOfWork { get; }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
