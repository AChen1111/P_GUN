using System.IO;
using UnityEngine;

namespace Game.Gameplay.Save
{
    public static class SaveSlotSnapshotCapture
    {
        private const int SnapshotWidth = 320;
        private const int SnapshotHeight = 180;

        public static void Capture(int slotIndex)
        {
            var camera = Camera.main;
            if (camera == null)
            {
                Debug.LogWarning("存档快照失败, 场景中没有 MainCamera.");
                return;
            }

            Directory.CreateDirectory(SaveSlotStorage.SaveFolderPath);
            var previousTarget = camera.targetTexture;
            var previousActive = RenderTexture.active;
            var renderTexture = RenderTexture.GetTemporary(SnapshotWidth, SnapshotHeight, 24);
            Texture2D texture = null;
            try
            {
                // 使用相机直接渲染缩略图, 避免把 Screen Space Overlay 的存档面板截进去.
                camera.targetTexture = renderTexture;
                RenderTexture.active = renderTexture;
                camera.Render();
                texture = new Texture2D(SnapshotWidth, SnapshotHeight, TextureFormat.RGB24, false);
                texture.ReadPixels(new Rect(0f, 0f, SnapshotWidth, SnapshotHeight), 0, 0);
                texture.Apply();
                File.WriteAllBytes(SaveSlotStorage.GetSnapshotPath(slotIndex), texture.EncodeToPNG());
            }
            finally
            {
                camera.targetTexture = previousTarget;
                RenderTexture.active = previousActive;
                RenderTexture.ReleaseTemporary(renderTexture);
                if (texture != null)
                {
                    Object.Destroy(texture);
                }
            }
        }
    }
}
