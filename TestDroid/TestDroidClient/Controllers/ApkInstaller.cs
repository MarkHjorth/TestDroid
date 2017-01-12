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
        ADBhandler adb;

        public ApkInstaller()
        {
            adb = new ADBhandler();
        }

        public bool InstallApk()
        {
            bool worked = false;
            string apkName = "\"C:/Users/David/AppData/Local/Xamarin/Mono for Android/Archives/2016-12-15/TestDroid 12-15-16 1.19 PM.apkarchive/signed-apks/com.rohde_schwarz.testdroid.apk\"";

            try
            {
                string parameters = string.Format("-d install {0}", apkName);
                Console.WriteLine(adb.StartProcess(parameters, true));
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
