using System;
using System.Collections.Generic;
using System.Reflection;

namespace WeakReferences
{
    public class WeakDelegate<TDelegate> where TDelegate : Delegate
    {
        class MethodTarget
        {
            public readonly WeakReference? Reference;
            public readonly MethodInfo Method;

            public MethodTarget(Delegate d)
            {
                if (d.Target != null)
                    Reference = new WeakReference(d.Target);

                Method = d.Method;
            }
        }

        List<MethodTarget> _targets = new();

        public void Combine(TDelegate target)
        {
            if (target == null) return;

            foreach (Delegate d in target.GetInvocationList())
                _targets.Add(new MethodTarget(d));
        }

        public void Remove(TDelegate target)
        {
            if (target == null) return;

            foreach (Delegate d in target.GetInvocationList())
            {
                MethodTarget? mt = _targets.Find(w =>
                    Equals(d.Target, w.Reference?.Target) &&
                    Equals(d.Method.MethodHandle, w.Method.MethodHandle));

                if (mt != null)
                    _targets.Remove(mt);
            }
        }

        public TDelegate? Target
        {
            get
            {
                Delegate? combined = null;

                foreach (var mt in _targets.ToArray())
                {
                    var wr = mt.Reference;

                    if (wr == null || wr.Target != null)
                    {
                        var del = Delegate.CreateDelegate(
                            typeof(TDelegate), wr?.Target, mt.Method);
                        combined = Delegate.Combine(combined, del);
                    }
                    else
                    {
                        _targets.Remove(mt);
                    }
                }

                return combined as TDelegate;
            }
            set
            {
                _targets.Clear();
                if (value != null)
                    Combine(value);
            }
        }
    }
}
