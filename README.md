# Tween Creator

[![openupm](https://img.shields.io/npm/v/com.thejoun.tween-creator?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.thejoun.tween-creator/)

An add-on to DOTween that allows creating and previewing complex tweens in the editor.

Great for animating UIs.

Note: meant for Unity versions 2021.2 or later, 
[explained here](#user-content-instant-preview-in-editor-mode).

## Installation

See "Installation" on [OpenUPM](https://openupm.com/packages/com.thejoun.tween-creator).
Add these scopes:
- `com.thejoun`
- `com.solidalloy`
- `org.nuget`

#### DOTween integration

Make sure you've got DOTween installed (either the free version or Pro).

Additionally, you'll nee to create an `.asmdef` file for DoTween modules. 
Go to `Tools > Demigiant > DoTween Utility Panel` and then select `Create ASMDEF...`.

Tween Creator references the `.asmdef` file by GUID, 
and the created one has a random GUID assigned. 
To fix the reference, find the meta file 
(`Assets\Plugins\Demigiant\DOTween\Modules\DOTween.Modules.asmdef.meta`)
and replace the `guid` value with 
`4a27d636c21081747a8c6b481cccc046`. Then reload your project.

That's the only way I know to make the plugin work with DOTween. 
As of february 2023 there are ongoing efforts to release DOTween as a package, 
which would make the whole thing a lot easier
(see [issue on github](https://github.com/Demigiant/dotween/issues/251)).

## Features

#### Tweens defined in components

![Component](img/component.png)

#### Intuitive composition of groups and sequences in hierarchy

![Hierarchy](img/hierarchy.png)

#### Instant preview in editor mode

There are no side effects, 
which means no changes will be left on any objects when you press RESET, 
compared to the state before starting PREVIEW.

Some changes may be left on objects if you're using a version of Unity before 2021.2. 
That's because a certain method is unavailable.

![Preview](https://i.gyazo.com/be8b2b92ef24787c91ba7d5a0cba9a78.gif)

#### Quick type switch from context menu

To switch the type of a Tween component, right-click the component's header 
and choose 'Switch Type'.

![Switch](img/switch.png)

#### Easy usage

```csharp
// can assign any type of tween, group or sequence
[SerializeField] private TweenPlayable tween;

private void Test()
{
    // use any of these to play:
    tween.PlayForward();
    tween.PlayBackwards();
    tween.RewindAndPlayForward();
    tween.Rewind();
}
```

#### Custom tweens

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

The `TypeCategory` attribute is used to group certain tweens together 
in the type switch context menu.

#### Custom eases as animation curves

![Curve](img/curve.png)

### Dependencies

- [TypeSwitcher](https://github.com/thejoun/type-switcher)
- [DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)