using UnityEngine;

public class Monosingleton<T> : MonoBehaviour where T : MonoBehaviour //t�� �ƹ��ų� ���� �� �ִ�
{
    private static bool shuttingDown = false;
    private static object locker = new object();//�ߺ� ���� x
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
                    instance = FindObjectOfType<T>();//���� �ִ��� ã�°�
                    if (instance == null)//������ ������
                    {
                        instance = new GameObject(typeof(T).ToString()).AddComponent<T>();//T�� ���� Ŭ���� �̸��� ������
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
