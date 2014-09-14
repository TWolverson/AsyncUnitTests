using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace TransformationEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            inFolder = args[0];
            outFolder = args[1];
            transform = new XslCompiledTransform();
            transform.Load(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("TransformationEngine.XSLTFile1.xslt")));
            var watcher = new FileSystemWatcher(inFolder);
            watcher.EnableRaisingEvents = true;
            var random = new Random();
            watcher.Changed += watcher_Changed;
            Console.Read();
        }

        private static XslCompiledTransform transform;

        private static string inFolder;

        private static string outFolder;

        private static async void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            // I swear all this overload nonsense is necessary to stop the file from being created in a state where the test methods can't read it.
            // This app can't release the file fast enough after any listening FileSystemWatcher raises events for it, before any handler starts trying to access the file and fails
            await Task.Run(() =>
            {
                using (var readStream = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = XmlReader.Create(readStream))
                    {
                        using (var stream = new FileStream(Path.Combine(outFolder, e.Name), FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                        {
                            using (var writer = XmlWriter.Create(stream))
                            {
                                transform.Transform(reader, writer);
                            }
                        }
                    }
                }
            });
        }
    }
}
