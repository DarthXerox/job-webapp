using System;
using DAL;

namespace BL
{
    public abstract class QueryObject : IDisposable
    {
        public UnitOfWork UnitOfWork { get; private set; }
        protected QueryObject(UnitOfWork unit)
        {
            UnitOfWork = unit;
        }
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
