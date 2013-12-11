using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGENT.PackageViewer.Packaging
{
    public class metadata
    {
        public string id { get; set; }
        public string type { get; set; }
        public string version { get; set; }
        public string title { get; set; }
        public string authors { get; set; }
        public string owners { get; set; }
        public string licenseUrl { get; set; }
        public string projectUrl { get; set; }
        public string requireLicenseAcceptance { get; set; }
        public string description { get; set; }
        public string language { get; set; }
        public string tags { get; set; }
        public string icon { get; set; }
    }

}
