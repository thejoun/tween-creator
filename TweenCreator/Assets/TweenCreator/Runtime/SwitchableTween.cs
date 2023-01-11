using TypeSwitcher;

namespace TweenCreator
{
    public abstract class SwitchableTween : SwitchableMonoBehaviour<SwitchableTween>
    {
        protected override TypeSwitchSettings TypeSwitchSettings => new TypeSwitchSettings()
        {
            BaseType = typeof(SwitchableTween),
            HideStrings = new []{"Tween"}
        };
    }
}