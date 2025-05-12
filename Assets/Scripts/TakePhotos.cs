using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using NativeGalleryNamespace;

public class TakePhotos : MonoBehaviour
{
    public void TakePhoto()
    {
        StartCoroutine(TakeAPhoto());
    }

    IEnumerator TakeAPhoto()
    {
        yield return new WaitForEndOfFrame();
        Camera camera = Camera.main;
        int width = Screen.width;
        int height = Screen.height;
        RenderTexture rt = new RenderTexture(width, height, 24);
        camera.targetTexture = rt;
        var currentRT = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        camera.Render();

        Texture2D image = new Texture2D(width, height, TextureFormat.RGB24, false);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();

        camera.targetTexture = null;
        RenderTexture.active = currentRT;

        byte[] bytes = image.EncodeToPNG();
        string fileName = "Cloudy_" + DateTime.Now.ToString("MMdd_HHmmss") + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Saved to internal path: " + filePath);

        //  추가: NativeGallery로 갤러리에 등록
        NativeGallery.SaveImageToGallery(bytes, "CloudyPhotos", fileName);
        Debug.Log("Saved to gallery: " + fileName);

        Destroy(rt);
        Destroy(image);
    }
}
