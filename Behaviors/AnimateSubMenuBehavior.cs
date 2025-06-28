using Microsoft.Maui.Controls;

namespace MauiAppUIDemo.Behaviors
{
    public class AnimateSubMenuBehavior : Behavior<ContentView>
    {
        protected override void OnAttachedTo(ContentView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.PropertyChanged += OnIsVisibleChanged;
        }

        protected override void OnDetachingFrom(ContentView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.PropertyChanged -= OnIsVisibleChanged;
        }

        private async void OnIsVisibleChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ContentView.IsVisible)) return;

            if (sender is ContentView view)
            {
                if (view.IsVisible)
                {
                    view.Opacity = 0;
                    view.TranslationY = -20;
                    await Task.WhenAll(
                        view.FadeTo(1, 200, Easing.SinIn),
                        view.TranslateTo(0, 0, 200, Easing.SinIn));
                }
            }
        }
    }
}
