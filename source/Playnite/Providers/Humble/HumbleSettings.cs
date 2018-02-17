using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playnite.Providers.Humble
{
    public class HumbleSettings
    {

        public bool LibraryDownloadEnabled
        {
            get; set;
        } = true;

        public bool IntegrationEnabled
        {
            get; set;
        } = false;

        
    }
}
