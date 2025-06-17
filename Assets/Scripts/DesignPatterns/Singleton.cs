using UnityEngine;

namespace DesignPattern
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null || _instance.Equals(null))
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance != null && _instance.transform.parent == null)
                    {
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }
                return _instance;
            }
        }

        protected void SingletonInit()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this as T;

                if (transform.parent == null) // ��Ʈ�� ���
                {
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Debug.LogWarning($"[Singleton<{typeof(T).Name}>] ��Ʈ GameObject�� �ƴϹǷ� DontDestroyOnLoad�� ������� �ʽ��ϴ�.");
                }
            }
        }
    }
}
