using UGF.Application.Runtime;
using UGF.Logs.Runtime;
using Unity.Services.CloudDiagnostics;
using UnityEngine.CrashReportHandler;

namespace UGF.Module.CloudDiagnostics.Runtime.Unity
{
    public class CloudDiagnosticsUnityModule : ApplicationModule<CloudDiagnosticsUnityModuleDescription>
    {
        public ICloudDiagnosticsService Service { get { return CloudDiagnosticsService.Instance; } }

        public CloudDiagnosticsUnityModule(CloudDiagnosticsUnityModuleDescription description, IApplication application) : base(description, application)
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            CrashReportHandler.enableCaptureExceptions = Description.EnableCaptureExceptions;
            CrashReportHandler.logBufferSize = Description.LogBufferSize;

            foreach ((string key, string value) in Description.Metadata)
            {
                CrashReportHandler.SetUserMetadata(key, value);
            }

            Log.Debug("Cloud Diagnostics initialize", new
            {
                Description.EnableCaptureExceptions,
                Description.LogBufferSize,
                metadataCount = Description.Metadata.Count
            });
        }
    }
}
