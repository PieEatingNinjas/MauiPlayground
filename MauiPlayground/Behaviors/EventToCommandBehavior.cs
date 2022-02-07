using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;

namespace MauiPlayground.Behaviors
{
    public class EventToCommandBehavior : BindableBehavior<View>
    {
        public static BindableProperty EventNameProperty =
            BindableProperty.CreateAttached("EventName", typeof(string), typeof(EventToCommandBehavior), null,
                BindingMode.OneWay);

        public static BindableProperty CommandProperty =
            BindableProperty.CreateAttached("Command", typeof(ICommand), typeof(EventToCommandBehavior), null,
                BindingMode.OneWay);

        public static BindableProperty CommandParameterProperty =
            BindableProperty.CreateAttached("CommandParameter", typeof(object), typeof(EventToCommandBehavior), null,
                BindingMode.OneWay);

        public static BindableProperty EventArgsConverterProperty =
            BindableProperty.CreateAttached("EventArgsConverter", typeof(IValueConverter), typeof(EventToCommandBehavior), null,
                BindingMode.OneWay);

        public static BindableProperty EventArgsConverterParameterProperty =
            BindableProperty.CreateAttached("EventArgsConverterParameter", typeof(object), typeof(EventToCommandBehavior), null,
                BindingMode.OneWay);

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        protected Delegate? Handler;
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        private EventInfo? _eventInfo;
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        public string EventName
        {
            get => (string)GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public IValueConverter EventArgsConverter
        {
            get => (IValueConverter)GetValue(EventArgsConverterProperty);
            set => SetValue(EventArgsConverterProperty, value);
        }

        public object EventArgsConverterParameter
        {
            get => GetValue(EventArgsConverterParameterProperty);
            set => SetValue(EventArgsConverterParameterProperty, value);
        }

        protected override void OnAttachedTo(View visualElement)
        {
            base.OnAttachedTo(visualElement);
#pragma warning disable CS8601 // Possible null reference assignment.

            if (AssociatedObject != null)
            {
                var events = AssociatedObject.GetType().GetRuntimeEvents().ToArray();
                if (!events.Any()) return;
                _eventInfo = events.FirstOrDefault(e => e.Name == EventName);
#pragma warning restore CS8601 // Possible null reference assignment.
                if (_eventInfo == null)
                    throw new ArgumentException(
                        $"EventToCommand: Can't find any event named '{EventName}' on attached type");

                AddEventHandler(_eventInfo, AssociatedObject, OnFired);
            }

        }

        protected override void OnDetachingFrom(View view)
        {
            if (Handler != null && _eventInfo != null)
                _eventInfo.RemoveEventHandler(AssociatedObject, Handler);

            base.OnDetachingFrom(view);
        }

        private void AddEventHandler(EventInfo eventInfo, object item, Action<object, EventArgs> action)
        {
            if (eventInfo != null && eventInfo.EventHandlerType != null)
            {
                var eventParameters = eventInfo.EventHandlerType
                    .GetRuntimeMethods().First(m => m.Name == "Invoke")
                    .GetParameters()
                    .Select(p => Expression.Parameter(p.ParameterType))
                    .ToArray();

                var actionInvoke = action.GetType()
                    .GetRuntimeMethods().First(m => m.Name == "Invoke");

                Handler = Expression.Lambda(
                    eventInfo.EventHandlerType,
                    Expression.Call(Expression.Constant(action), actionInvoke, eventParameters[0], eventParameters[1]),
                    eventParameters
                )
                .Compile();

                eventInfo.AddEventHandler(item, Handler);

            }

        }

        private void OnFired(object sender, EventArgs eventArgs)
        {
            if (Command == null)
                return;

            var parameter = CommandParameter;

            if (eventArgs != null && eventArgs != EventArgs.Empty)
            {
                parameter = eventArgs;

                if (EventArgsConverter != null)
                {
                    parameter = EventArgsConverter.Convert(eventArgs, typeof(object), EventArgsConverterParameter, CultureInfo.CurrentUICulture);
                }
            }

            if (Command.CanExecute(parameter))
            {
                Command.Execute(parameter);
            }
        }
    }
}
