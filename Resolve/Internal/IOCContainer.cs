using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Resolve.Internal
{
    public class IOCContainer
    {

        protected IEnumerable<BeanContext> Contexts = new List<BeanContext>();

        private Dictionary<Type, BeanContext> ResolvedBeans = new Dictionary<Type, BeanContext>();

        private Dictionary<Tuple<Type, String>, BeanContext> ResolvedNamedBeans = new Dictionary<Tuple<Type, string>, BeanContext>();

        private IEnumerable<BeanContext> GetBeansForType(Type t)
        {
            return from context in Contexts
                   where context.SealedType == t
                   select context;
        }

        public BeanContext ResolveDefaultBean(Type t)
        {
            Contract.Ensures(Contract.Result<BeanContext>() != null);
            if (ResolvedBeans.ContainsKey(t))
            {
                return ResolvedBeans[t];
            }
            else
            {
                var matchingContexts = from context in GetBeansForType(t)
                                       where context.Name == null
                                       select context;

                if (matchingContexts.Any())
                {
                    var context = matchingContexts.First();
                    ResolvedBeans[t] = context;
                    return context;
                }
                else
                {
                    throw new NotImplementedException($"No context was registered for †he type {t.Name}");
                }
            }
        }

        public BeanContext ResolvedNamedBean(Type t, String name)
        {
            Contract.Ensures(Contract.Result<BeanContext>() != null);
            var tupleContext = Tuple.Create(t, name);
            if (ResolvedNamedBeans.ContainsKey(tupleContext))
            {
                return ResolvedNamedBeans[tupleContext];
            }
            else
            {
                var matchingContexts = GetBeansForType(t).Where(ctx => ctx.Name == name);
                if (matchingContexts.Any())
                {
                    var context = matchingContexts.First();
                    ResolvedNamedBeans[tupleContext] = context;
                    return context;
                }
                else
                {
                    throw new NotImplementedException($"No context was registered for the type {t.Name} with name {name}");
                }
            }
        }

        public Object Resolve(Type t)
        {
            // TODO move resolver to external IBeanResolver interface and implementation class(es)
        }

        public Object ResolveNamed(Type t, String name)
        {
			// TODO move resolver to external IBeanResolver interface and implementation class(es)
		}



    }

}
