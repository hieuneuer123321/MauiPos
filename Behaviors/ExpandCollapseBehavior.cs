using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Behaviors
{
    public class ExpandCollapseBehavior : Behavior<VisualElement>
    {
        public static readonly BindableProperty IsExpandedProperty =
            BindableProperty.Create(nameof(IsExpanded), typeof(bool), typeof(ExpandCollapseBehavior),
                false, propertyChanged: OnIsExpandedChanged);

        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        private VisualElement AssociatedObject;

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            // Ẩn mặc định
            AssociatedObject.ScaleY = 0;
            AssociatedObject.IsVisible = false;
            AssociatedObject.AnchorY = 0;
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            base.OnDetachingFrom(bindable);
            AssociatedObject = null;
        }

        private static async void OnIsExpandedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (ExpandCollapseBehavior)bindable;
            if (behavior.AssociatedObject == null)
                return;

            if ((bool)newValue)
            {
                behavior.AssociatedObject.IsVisible = true;
                behavior.AssociatedObject.AnchorY = 0;

                // Reset scaleY để animation mượt
                behavior.AssociatedObject.ScaleY = 0;

                await behavior.AssociatedObject.ScaleYTo(1, 300, Easing.CubicInOut);
            }
            else
            {
                behavior.AssociatedObject.AnchorY = 0;
                await behavior.AssociatedObject.ScaleYTo(0, 300, Easing.CubicInOut);
                behavior.AssociatedObject.IsVisible = false;
            }
        }
    }
}
