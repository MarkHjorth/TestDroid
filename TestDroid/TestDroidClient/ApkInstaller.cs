using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDroidClient
{
    class ApkInstaller
    {
        public ApkInstaller()
        {

        }

        public bool InstallApk(string adbPath, string apkName)
        {
            bool worked = false;

            try
            {
                string parameters = string.Format("-d install {0}", apkName);
                Process.Start(adbPath, parameters);
                worked = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return worked;
        }
    }
}
