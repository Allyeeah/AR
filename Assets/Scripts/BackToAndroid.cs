using UnityEngine;

public class BackToAndroid : MonoBehaviour
{
    public void OnBackButtonClick()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("finish"); // ���� Unity Activity ����
        }
#endif
    }
}
