using System;
using System.Collections.Generic;
using UGF.Application.Runtime;

namespace UGF.Module.CloudDiagnostics.Runtime.Unity
{
    public class CloudDiagnosticsUnityModuleDescription : ApplicationModuleDescription
    {
        public bool EnableCaptureExceptions { get; }
        public uint LogBufferSize { get; }
        public IReadOnlyDictionary<string, string> Metadata { get; }

        public CloudDiagnosticsUnityModuleDescription(Type registerType, bool enableCaptureExceptions, uint logBufferSize, IReadOnlyDictionary<string, string> metadata)
        {
            RegisterType = registerType ?? throw new ArgumentNullException(nameof(registerType));
            EnableCaptureExceptions = enableCaptureExceptions;
            LogBufferSize = logBufferSize;
            Metadata = metadata;
        }
    }
}
