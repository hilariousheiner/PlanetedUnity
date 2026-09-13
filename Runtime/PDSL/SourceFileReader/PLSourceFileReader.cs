using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.PackageManager;
#endif

using System.IO;

namespace Planeted
{
    public class PLSourceFileReader : ISourceFileReader
    {
        public bool TryReadSourceFile(string path, out string code)
        {
            string resolvedPath = this.resolvePath(path);
            
            if (resolvedPath != null)
            {
                code = File.ReadAllText(resolvedPath);
                return true;
            }

            code = null;
            return false;
        }

        private string resolvePath(string path)
        {
            string streamingPath = Path.Combine(Application.streamingAssetsPath, "pdsl", path);

            if (File.Exists(streamingPath))
            {
                return streamingPath;
            }
#if UNITY_EDITOR

            string packagePath = Path.Combine(this.getPackageRoot(), "StandardLibrary", path);
            Debug.Log(packagePath);
            if (File.Exists(packagePath))
            {
                Debug.Log("Got here!");

                return packagePath;
            }
#endif
            return null;
        }

        private string getPackageRoot()
        {
            PackageInfo packageInfo = PackageInfo.FindForPackageName("com.hilariousheiner.planeted");
            return packageInfo.resolvedPath;
        }
    }
}
