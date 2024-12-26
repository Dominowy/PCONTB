namespace PCONTB.UI
{
    public class ServerSettings
    {
        public string BaseUrl { get; set; }
        public bool ProxyIgnoreCertificateCheck { get; set; }
        public Dictionary<string, string> ProxyEndpoints { get; set; } = new Dictionary<string, string>();
    }
}
