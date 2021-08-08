namespace Drizzle.Logic
{
    public enum RenderStage
    {
        Start,
        CameraSetup,
        RenderLayers,
        RenderPropsPreEffects,
        RenderEffects,
        RenderPropsPostEffects,
        RenderLight,
        Finalize,
        RenderColors,
        Finished,
        SaveFile,
    }
}
