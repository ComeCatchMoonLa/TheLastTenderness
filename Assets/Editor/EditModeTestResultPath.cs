using System.IO;
using UnityEditor;
using UnityEditor.TestTools.TestRunner.Api;
using UnityEngine;

namespace CatchMoon.Editor
{
    [InitializeOnLoad]
    static class EditModeTestResultPath
    {
        static readonly Saver saver;

        static EditModeTestResultPath()
        {
            saver = ScriptableObject.CreateInstance<Saver>();
            saver.hideFlags = HideFlags.HideAndDontSave;
            TestRunnerApi.RegisterTestCallback(saver);
        }

        static void MoveRootResultsIntoLogs()
        {
            var root = Directory.GetCurrentDirectory();
            var logs = Path.Combine(root, "Logs");
            Directory.CreateDirectory(logs);
            foreach (var file in Directory.GetFiles(root, "TestResults-*.xml"))
            {
                var dest = Path.Combine(logs, Path.GetFileName(file));
                if (File.Exists(dest))
                    File.Delete(dest);
                File.Move(file, dest);
            }
        }

        class Saver : ScriptableObject, ICallbacks
        {
            public void RunStarted(ITestAdaptor testsToRun) { }

            public void TestStarted(ITestAdaptor test) { }

            public void TestFinished(ITestResultAdaptor result) { }

            public void RunFinished(ITestResultAdaptor result)
            {
                var logs = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
                Directory.CreateDirectory(logs);
                TestRunnerApi.SaveResultToFile(result, Path.Combine("Logs", "TestResults.xml"));
                EditorApplication.delayCall += MoveRootResultsIntoLogs;
            }
        }
    }
}
