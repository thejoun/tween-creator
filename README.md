# Tween Creator

Easy and intuitive tweening tool for Unity, based on DOTween.

Package not available yet.

#### Tweens defined in components
![Component](img/component.png)

#### Intuitive composition of groups and sequences in hierarchy
![Hierarchy](img/hierarchy.png)

#### Instant preview in editor mode (no side effects!)
![Preview](https://i.gyazo.com/be8b2b92ef24787c91ba7d5a0cba9a78.gif)

#### Quick type switch from context menu
![Switch](img/switch.png)

#### Your own custom tweens in a few lines of code
```csharp
[TypeCategory(TweenCategory.Basic)]
public class TweenScale : TweenCustomPlayable
{
    [Header("Scale")]
    [SerializeField] private Transform tr;
    [SerializeField] private Vector3 target = Vector3.one;
    [SerializeField] private Vector3 origin = Vector3.one;
        
    private Vector3 m_savedState;

    public override void PlayForward() => PlaySingleTween(tr.DOScale(target, duration));
    public override void PlayBackwards() => PlaySingleTween(tr.DOScale(origin, duration));
     
    public override void SavePreviewState() => m_savedState = tr.localScale;
    public override void RestorePreviewState() => tr.localScale = m_savedState;
        
    public override void Rewind()
    {
        base.Rewind();
            
        tr.localScale = origin;
    }
}
```

### Dependencies

- https://github.com/thejoun/type-switcher