# UniCustomUpdateManager

## 使用例

```cs
using Kogane;
using UnityEngine;

public class Example : MonoBehaviour, IUpdatable
{
    private void OnEnable()
    {
        CustomUpdateManager.Add( this );
    }

    private void OnDisable()
    {
        CustomUpdateManager.Remove( this );
    }

    void IUpdatable.OnUpdate()
    {
        Debug.Log( "ピカチュウ" );
    }
}
```