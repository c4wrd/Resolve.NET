using System;
namespace Resolve.Internal
{
    public class BeanContext
    {

        public Type SealedType { get; private set; }

        public String Name { get; private set; }

        public BindingType Type { get; private set; }

        private Object Instance = null;

        private Type Implementation = null;

        private Boolean Singleton = false;

        public BeanContext(Type type)
        {
            this.SealedType = type;
        }

        public BeanContext Named(String name)
        {
            this.Name = name;
            return this;
        }

        public void ToInstance(Object instance)
        {

            if (instance == null)
            {
                throw new ArgumentException("Cannot bind to null");
            }

            if (this.SealedType.IsAssignableFrom(instance.GetType()))
            {
                this.Type = BindingType.INSTANCE;
                this.Instance = instance;
            }
            else
            {
                throw new ArgumentException($"Cannot bind the instance of type {instance.GetType().Name} to {this.SealedType.Name}");
            }
        }

		public void AsSingleton()
		{
			this.Singleton = true;
		}

        public BeanContext ToType(Type inheritedType)
        {

            if (this.SealedType.IsAssignableFrom(inheritedType))
            {
                this.Type = BindingType.IMPLEMENTATION;
                this.Implementation = inheritedType;
                return this;
            }
            else
            {
                throw new ArgumentException($"The type {SealedType.Name} is not assignable from {inheritedType}");
            }
        }

        public Object GetInstance()
        {
            return this.Instance;
        }

        public Type GetImplementation()
        {
            return this.Implementation;
        }
    }
}
