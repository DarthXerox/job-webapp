using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DAL;

namespace BL
{
    public abstract class QueryObject : IDisposable
    {
        protected readonly IMapper mapper;
        public UnitOfWork UnitOfWork { get; private set; }
        protected QueryObject(UnitOfWork unit, IMapper mapper)
        {
            UnitOfWork = unit;
            this.mapper = mapper;
        }
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
