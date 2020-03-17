using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2_DI
{
    public interface ITransientTest
    {
        Guid guid { get; set; }
    }
    public interface IScopedTest
    {
        Guid guid { get; set; }
    }
    public interface ISingletonTest
    {
        Guid guid { get; set; }
    }

    public class ScopedTest : IScopedTest
    {
        public Guid guid { get; set; }
        public ScopedTest()
        {
            guid = Guid.NewGuid();
        }
    }

    public class TransientTest : ITransientTest
    {
        public Guid guid { get; set; }
        public TransientTest()
        {
            guid = Guid.NewGuid();
        }
    }

    public class SingletonTest : ISingletonTest
    {
        public Guid guid { get; set; }
        public SingletonTest()
        {
            guid = Guid.NewGuid();
        }
    }
}
