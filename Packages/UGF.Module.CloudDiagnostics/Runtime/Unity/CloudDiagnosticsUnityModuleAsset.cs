using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UnityEngine;

namespace UGF.Module.CloudDiagnostics.Runtime.Unity
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Cloud Diagnostics/Cloud Diagnostics Unity Module", order = 2000)]
    public class CloudDiagnosticsUnityModuleAsset : ApplicationModuleAsset<CloudDiagnosticsUnityModule, CloudDiagnosticsUnityModuleDescription>
    {
        [SerializeField] private bool m_enableCaptureExceptions = true;
        [Range(0F, 50F)]
        [SerializeField] private int m_logBufferSize = 10;
        [SerializeField] private List<MetadataData> m_metadata = new List<MetadataData>();

        public bool EnableCaptureExceptions { get { return m_enableCaptureExceptions; } set { m_enableCaptureExceptions = value; } }
        public int LogBufferSize { get { return m_logBufferSize; } set { m_logBufferSize = value; } }
        public List<MetadataData> Metadata { get { return m_metadata; } }

        [Serializable]
        public struct MetadataData
        {
            [SerializeField] private string m_key;
            [SerializeField] private string m_value;

            public string Key { get { return m_key; } set { m_key = value; } }
            public string Value { get { return m_value; } set { m_value = value; } }
        }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var metadata = new Dictionary<string, string>();

            for (int i = 0; i < m_metadata.Count; i++)
            {
                MetadataData data = m_metadata[i];

                if (string.IsNullOrEmpty(data.Key)) throw new ArgumentException("Value cannot be null or empty.", nameof(data.Key));
                if (string.IsNullOrEmpty(data.Value)) throw new ArgumentException("Value cannot be null or empty.", nameof(data.Value));

                metadata.Add(data.Key, data.Value);
            }

            return new CloudDiagnosticsUnityModuleDescription(
                typeof(ICloudDiagnosticsModule),
                m_enableCaptureExceptions,
                (uint)m_logBufferSize,
                metadata
            );
        }

        protected override CloudDiagnosticsUnityModule OnBuild(CloudDiagnosticsUnityModuleDescription description, IApplication application)
        {
            return new CloudDiagnosticsUnityModule(description, application);
        }
    }
}
