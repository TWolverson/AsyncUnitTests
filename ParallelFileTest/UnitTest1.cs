using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reactive.Linq;
using System.Reactive;
using System.Xml.Schema;
using System.Xml;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reactive.Subjects;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace ParallelFileTest
{
    [TestClass]
    public class UnitTest1
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var args = string.Format("{0} {1}", inFolder, outFolder);
            stubProcess = Process.Start("TransformationEngine.exe", args);
            Thread.Sleep(100); //make sure the stub application is ready and listening before continuing to run the tests, otherwise the first will hang
        }

        [ClassCleanup]
        public static void TearDown()
        {
            stubProcess.Kill();
        }

        private static string inFolder = Path.GetTempPath() + "Demo\\In";

        private static string outFolder = Path.GetTempPath() + "Demo\\Out";

        public UnitTest1()
        {

            var watcher = new FileSystemWatcher(outFolder);
            watcher.EnableRaisingEvents = true;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            fileObservable = Observable.FromEventPattern<FileSystemEventArgs>(watcher, "Changed");
        }

        private readonly IObservable<EventPattern<FileSystemEventArgs>> fileObservable;
        private static Process stubProcess;


        private AsyncSubject<string> GetSubject()
        {
            var fileName = Path.GetRandomFileName();
            var filePath = Path.Combine(inFolder, fileName);

            var sequence = fileObservable.Where(f => f.EventArgs.Name.Contains(fileName))
                 .Take(1)
                 .Select(fs => fs.EventArgs.FullPath)
                 .GetAwaiter();

            using (var copyStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
            {
                using (var xmlstream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ParallelFileTest.XMLFile1.xml"))
                {
                    using (var streamreader = new StreamReader(xmlstream))
                    {
                        using (var streamwriter = new StreamWriter(copyStream))
                        {
                            streamwriter.Write(streamreader.ReadToEnd());
                        }

                    }
                }
            }

            return sequence;
        }

        private IList<string> Validate(string path)
        {
            var schema = XmlSchema.Read(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream(@"ParallelFileTest.schema.xsd")), (o, e) => {/*nobody cares*/});
            var schemaSet = new XmlSchemaSet();
            schemaSet.Add(schema);

            var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema, Schemas = schemaSet };
            var validationErrors = new List<string>();
            settings.ValidationEventHandler += (o, e) =>
            {
                validationErrors.Add(e.Message);
            };
            using (var documentReader = XmlReader.Create(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), settings))
            {
                while (documentReader.Read()) { };
                documentReader.Close();
            }

            return validationErrors;
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);

        }

        [TestMethod]
        public async Task TestMethod2()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);
        }

        [TestMethod]
        public async Task TestMethod3()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);
        }

        [TestMethod]
        public async Task TestMethod14()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);
        }

        [TestMethod]
        public async Task TestMethod4()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);
        }

        [TestMethod]
        public async Task TestMethod5()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);
        }

        [TestMethod]
        public async Task TestMethod6()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);
        }

        [TestMethod]
        public async Task TestMethod7()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);
        }

        [TestMethod]
        public async Task TestMethod8()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);
        }

        [TestMethod]
        public async Task TestMethod9()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);
        }

        [TestMethod]
        public async Task TestMethod10()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);
        }

        [TestMethod]
        public async Task TestMethod11()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);
        }

        [TestMethod]
        public async Task TestMethod12()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);
        }

        [TestMethod]
        public async Task TestMethod13()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);
        }

        [TestMethod]
        public async Task TestMethod15()
        {
            var result = await GetSubject();

            var validationErrors = Validate(result);

            Assert.AreEqual(0, validationErrors.Count);
        }
    }
}
