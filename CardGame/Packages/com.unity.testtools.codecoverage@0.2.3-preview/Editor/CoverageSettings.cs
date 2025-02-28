using UnityEngine;

namespace UnityEditor.TestTools.CodeCoverage
{
    internal class CoverageSettings
    {
        public const string ReportFolderName = "Report";
        public const string ReportHistoryFolderName = "Report-history";
        public const string PackageName = "Code Coverage";

        public string projectPath = Application.dataPath;
        public string rootFolderPath;
        public string rootFolderName = "CodeCoverage";
        public string resultsPathFromCommandLine;
        public string resultsFileName = "TestCoverageResults";
        public string resultsFileExtension;
        public string resultsFolderSuffix;
        public string resultsFolderPath;
        public string resultsRootFolderPath;
        public string resultsFilePath;      
        public bool hasPersistentRunData = true;
        public bool resetCoverageData = true;
    }
}