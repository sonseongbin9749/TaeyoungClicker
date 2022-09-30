using UnityEngine;

public class Monosingleton<T> : MonoBehaviour where T : MonoBehaviour //t는 아무거나 넣을 수 있다
{
    private static bool shuttingDown = false;
    private static object locker = new object();//중복 생성 x
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if(shuttingDown)
            {
                Debug.LogWarning("[instance] Instance" + typeof(T) + " is already destroyed. Returning");
            }

            lock (locker)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();//씬에 있는지 찾는것
                    if (instance == null)//없으면 만들잣
                    {
                        instance = new GameObject(typeof(T).ToString()).AddComponent<T>();//T에 넣은 클레스 이름을 가져옴
                    }
                }
                return instance;
            }
        }
            
    }

    private void OnDestroy()
    {
        shuttingDown = true;
    }
    private void OnApplicationQuit()
    {
        shuttingDown = true;
    }

}
