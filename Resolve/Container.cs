using System;
using Resolve.Internal;

namespace Resolve
{
    public abstract class Container : IOCContainer
    {
        public abstract void Configure();

        public BeanContext Bind(Type type)
        {
            var context = new BeanContext(type);
            this.Contexts.Add(context);
            return context;
        }

        void test()
        {
            Bind(typeof(String)).ToInstance("test");
        }
    }
}
