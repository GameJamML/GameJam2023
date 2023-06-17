
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelizePass : ScriptableRenderPass
{
    private PixelizeFeature.CustomPassSettings settings;

    private RenderTargetIdentifier colorBuffer, pixelBuffer;
    private int pixelBufferID = Shader.PropertyToID("_PixelBuffer");

    public PixelizePass(PixelizeFeature.CustomPassSettings settings)
    {
        this.settings = settings;
        this.renderPassEvent = settings.renderPassEvent;
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;


        descriptor.height = settings.cameraResolution.y;
        descriptor.width = settings.cameraResolution.x;

        cmd.GetTemporaryRT(pixelBufferID, descriptor, FilterMode.Point);
        //cmd.GetTemporaryRT(pixelBufferID, settings.cameraResolution.x, settings.cameraResolution.y, 24, FilterMode.Point);

        pixelBuffer = new RenderTargetIdentifier(pixelBufferID);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get();
        using (new ProfilingScope(cmd, new ProfilingSampler("Pixelize Pass")))
        {
            if (renderingData.cameraData.camera.name == "Main Camera")
            {
                cmd.Blit(colorBuffer, pixelBuffer);
            }
            else if (renderingData.cameraData.camera.name == "ViewportCamera")
            {
                cmd.Blit(pixelBuffer, colorBuffer, settings.scale, settings.margin);
            }

        }

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        if (cmd == null) throw new System.ArgumentNullException("cmd");
        cmd.ReleaseTemporaryRT(pixelBufferID);
    }

}
