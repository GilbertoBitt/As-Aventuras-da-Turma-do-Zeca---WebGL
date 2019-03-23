using System;

namespace UnityEngine.Recorder.Input
{
    public class RenderTextureInput : BaseRenderTextureInput
    {
        RenderTextureInputSettings cbSettings
        {
            get { return (RenderTextureInputSettings)settings; }
        }

        public override void BeginRecording(RecordingSession session)
        {
            if (cbSettings.m_SourceRTxtr == null)
                throw new Exception("No Render Texture object provided as source");

            outputHeight = cbSettings.m_SourceRTxtr.height;
            outputWidth = cbSettings.m_SourceRTxtr.width;
            outputRT = cbSettings.m_SourceRTxtr;
        }
    }
}