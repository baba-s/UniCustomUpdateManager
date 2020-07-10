# UniCustomUpdateManager

MonoBehaviour の Update 関数の呼び出しを高速にするための機能

## 使用例

```cs
using Kogane;
using UnityEngine;

public class Example : MonoBehaviour, IUpdatable
{
    private void OnEnable()
    {
        // コンポーネントが有効になったらマネージャに登録
        CustomUpdateManager.Add( this );
    }

    private void OnDisable()
    {
        // コンポーネントが無効になったらマネージャから解除
        CustomUpdateManager.Remove( this );
    }

    // MonoBehaviour.Update の代わりに定義
    void IUpdatable.OnUpdate()
    {
        Debug.Log( "ピカチュウ" );
    }
}
```

## 参考

* https://www.packtpub.com/game-development/unity-2017-game-optimization-second-edition
* https://blogs.unity3d.com/jp/2015/12/23/1k-update-calls/
