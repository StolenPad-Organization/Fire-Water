namespace Stolenpad.Utils
{
    using UnityEngine;

    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}
