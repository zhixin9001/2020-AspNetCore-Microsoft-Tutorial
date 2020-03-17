using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2_DI
{
    public class ScopeAndTransientTester
    {
        public ISingletonTest singleton { get; }
        public ITransientTest transient { get; }
        public IScopedTest scoped { get; }

        public ScopeAndTransientTester(ISingletonTest singleton, ITransientTest transient, IScopedTest scoped)
        {
            this.singleton = singleton;
            this.transient = transient;
            this.scoped = scoped;
        }

        public Guid SingletonID()
        {
            return singleton.guid;
        }
        public Guid TransientID()
        {
            return transient.guid;
        }
        public Guid ScopedID()
        {
            return scoped.guid;
        }

    }
}
