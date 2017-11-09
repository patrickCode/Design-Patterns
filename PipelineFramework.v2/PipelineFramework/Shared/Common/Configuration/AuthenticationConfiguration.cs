namespace PipelineFramework.Common.Configuration
{
    public class AuthenticationConfiguration: BaseConfiguration
    {
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string CertificateThumbprint { get; set; }
        public string CertificationLocation { get; set; }

        public AuthenticationConfiguration():base("Authentication Configuration") { }
    }
}