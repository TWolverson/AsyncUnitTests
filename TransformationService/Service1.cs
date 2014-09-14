using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace TransformationService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var watcher = new FileSystemWatcher(args[0], "*xml");
            watcher.EnableRaisingEvents = true;
            var random = new Random();
            watcher.Created += (sender, eArgs) =>
            {
                Thread.Sleep(random.Next(2000));
                using (var reader = XmlReader.Create(eArgs.FullPath))
                {
                    var transform = new XslCompiledTransform();
                    transform.Load("XSLTFile1.xslt");
                    using (var writer = XmlWriter.Create(Path.Combine(args[1], eArgs.Name)))
                    {
                        transform.Transform(reader, writer);
                    }
                }
            };
        }

        protected override void OnStop()
        {
        }
    }
}
