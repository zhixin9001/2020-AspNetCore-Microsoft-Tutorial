using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2_DI
{
    public class Operation : IOperationTransient,
        IOperationScoped,
        IOperationSingleton,
        IOperationSingletonInstance
    {
        public Operation() : this(Guid.NewGuid())
        {
        }

        public Operation(Guid id)
        {
            OperationId = id;
        }

        public Guid OperationId { get; private set; }
    }
}
