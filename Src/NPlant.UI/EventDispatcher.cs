using System;
using System.Collections.Generic;
using System.Linq;

namespace NPlant.UI
{
    public static class EventDispatcher
    {
        [ThreadStatic]
        private static List<Delegate> _actions;

        public static IDisposable Register<T>(Action<T> action)
        {
            action.CheckForNullArg("action");

            if (_actions == null)
                _actions = new List<Delegate>();

            _actions.Add(action);

            return new EventDispatcherRegistration<T>(action);
        }

        private static void UnRegister<T>(Action<T> action)
        {
            if (_actions == null)
                return;

            _actions.Remove(action);
        }

        internal class EventDispatcherRegistration<T> : IDisposable
        {
            private readonly Action<T> _action;

            public EventDispatcherRegistration(Action<T> action)
            {
                _action = action;
            }

            public void Dispose()
            {
                UnRegister(_action);
            }
        }

        public static int Raise<T>(T @event)
        {
            int count = 0;

            if (_actions != null)
            {
                foreach (var action in _actions.OfType<Action<T>>())
                {
                    action(@event);
                    count++;
                }
            }

            return count;
        }
    }
}
