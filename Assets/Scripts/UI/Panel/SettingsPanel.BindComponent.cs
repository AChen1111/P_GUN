using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public partial class SettingsPanel
    {
        private Toggle m_Tog_Display;
        private Toggle m_Tog_Audio;
        private RectTransform m_Rect_DisplayPage;
        private RectTransform m_Rect_AudioPage;
        private TMP_Dropdown m_Drop_Resolution;
        private Toggle m_Tog_FullScreen;
        private Toggle m_Tog_VSync;
        private TMP_Dropdown m_Drop_FrameRate;
        private Slider m_Slider_MasterVolume;
        private Slider m_Slider_MusicVolume;
        private Slider m_Slider_SfxVolume;
        private Button m_Btn_Apply;
        private Button m_Btn_Back;

        private void GetBindComponents(GameObject go)
        {
            ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

            m_Tog_Display = autoBindTool.GetBindComponent<Toggle>(0);
            m_Tog_Audio = autoBindTool.GetBindComponent<Toggle>(1);

            m_Rect_DisplayPage = go.transform.Find("Window/Pages/Rect_DisplayPage").GetComponent<RectTransform>();
            m_Rect_AudioPage = go.transform.Find("Window/Pages/Rect_AudioPage").GetComponent<RectTransform>();

            m_Drop_Resolution = autoBindTool.GetBindComponent<TMP_Dropdown>(2);
            m_Tog_FullScreen = autoBindTool.GetBindComponent<Toggle>(3);
            m_Tog_VSync = autoBindTool.GetBindComponent<Toggle>(4);
            m_Drop_FrameRate = autoBindTool.GetBindComponent<TMP_Dropdown>(5);
            m_Slider_MasterVolume = autoBindTool.GetBindComponent<Slider>(6);
            m_Slider_MusicVolume = autoBindTool.GetBindComponent<Slider>(7);
            m_Slider_SfxVolume = autoBindTool.GetBindComponent<Slider>(8);
            m_Btn_Apply = autoBindTool.GetBindComponent<Button>(9);
            m_Btn_Back = autoBindTool.GetBindComponent<Button>(10);
        }
    }
}
