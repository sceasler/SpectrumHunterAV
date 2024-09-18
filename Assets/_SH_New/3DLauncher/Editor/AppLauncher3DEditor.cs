using System.IO;
using System.Xml;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class AppLauncher3DEditor
{
    [PostProcessBuild(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target == BuildTarget.WSAPlayer)
        {
            // Find App Launcher Asset, if not we don't need to do anything
            string[] applauncher3DAssets = AssetDatabase.FindAssets("t:AppLauncher3D");
            if (applauncher3DAssets.Length > 0)
            {
                // Load in asset
                string assetPath = AssetDatabase.GUIDToAssetPath(applauncher3DAssets[0]);
                AppLauncher3D appLauncher = AssetDatabase.LoadAssetAtPath<AppLauncher3D>(assetPath);

                // Add app launcher to project
                Add3DAppLauncher(pathToBuiltProject, appLauncher);
            }
        }
    }

    private static void Add3DAppLauncher(string buildPath, AppLauncher3D settings)
    {
        string pathToProjectFiles = Path.Combine(buildPath, Application.productName);

        AddToPackageManifest(pathToProjectFiles, settings);
        AddToProject(pathToProjectFiles);
        CopyModel(pathToProjectFiles, settings);
    }

    private static void CopyModel(string buildPath, AppLauncher3D settings)
    {
        string launcherFileSourcePath = Path.Combine(Application.dataPath, settings.Model);
        string launcherFileTargetPath = Path.Combine(buildPath, "Assets\\AppLauncher_3D.glb");

        FileUtil.ReplaceFile(launcherFileSourcePath, launcherFileTargetPath);
    }

    private static void AddToProject(string buildPath)
    {
        ScriptingImplementation scriptingImplementation = PlayerSettings.GetScriptingBackend(EditorUserBuildSettings.selectedBuildTargetGroup);

        // Load project file xml
        string projFilename = Path.Combine(buildPath, PlayerSettings.productName + (scriptingImplementation == ScriptingImplementation.IL2CPP ? ".vcxproj" : ".csproj"));
        XmlDocument document = new XmlDocument();
        document.Load(projFilename);

        // Check if we've already added model to the project
        if (scriptingImplementation == ScriptingImplementation.IL2CPP)
        {
            bool alreadyAdded = false;
            XmlNodeList nones = document.GetElementsByTagName("None");
            foreach (var none in nones)
            {
                XmlElement element = none as XmlElement;
                if (element.GetAttribute("Include") == "Assets\\AppLauncher_3D.glb")
                {
                    alreadyAdded = true;
                }
            }

            // If not add the content object
            if (!alreadyAdded)
            {
                XmlElement newItemGroup = document.CreateElement("ItemGroup", document.DocumentElement.NamespaceURI);
                XmlElement newNoneElement = document.CreateElement("None", document.DocumentElement.NamespaceURI);
                XmlNode deploymentContentNode = document.CreateElement("DeploymentContent", document.DocumentElement.NamespaceURI);
                newNoneElement.AppendChild(deploymentContentNode);
                deploymentContentNode.AppendChild(document.CreateTextNode("true"));
                newNoneElement.SetAttribute("Include", "Assets\\AppLauncher_3D.glb");
                newItemGroup.AppendChild(newNoneElement);
                document.DocumentElement.AppendChild(newItemGroup);
            }
        }
        else
        {
            bool alreadyAdded = false;
            XmlNodeList contents = document.GetElementsByTagName("Content");
            foreach (var content in contents)
            {
                XmlElement element = content as XmlElement;
                if (element.GetAttribute("Include") == "Assets\\AppLauncher_3D.glb")
                {
                    alreadyAdded = true;
                }
            }

            // If not add the content object
            if (!alreadyAdded)
            {
                XmlElement itemGroup = document.CreateElement("ItemGroup", document.DocumentElement.NamespaceURI);
                XmlElement content = document.CreateElement("Content", document.DocumentElement.NamespaceURI);
                content.SetAttribute("Include", "Assets\\AppLauncher_3D.glb");
                itemGroup.AppendChild(content);
                document.DocumentElement.AppendChild(itemGroup);
            }
        }

        // Save project xml file
        document.Save(projFilename);
    }

    private static void AddToPackageManifest(string buildPath, AppLauncher3D settings)
    {
        // Load package appxmanifest xml
        string packageManifestPath = Path.Combine(buildPath, "Package.appxmanifest");
        XmlDocument document = new XmlDocument();
        document.Load(packageManifestPath);

        // Find the package node
        XmlNodeList packages = document.GetElementsByTagName("Package");
        XmlElement package = packages.Item(0) as XmlElement;

        // Set the require attributes
        package.SetAttribute("xmlns:uap5", "http://schemas.microsoft.com/appx/manifest/uap/windows10/5");
        package.SetAttribute("xmlns:uap6", "http://schemas.microsoft.com/appx/manifest/uap/windows10/6");
        package.SetAttribute("IgnorableNamespaces", "uap uap2 uap5 uap6 mp");

        // Check if we've already added the mixedl reality model node
        XmlNodeList mixedRealityModels = document.GetElementsByTagName("uap5:MixedRealityModel");
        XmlElement mixedRealityModel = null;
        if (mixedRealityModels.Count == 0)
        {
            // Add mixed reality model node
            XmlNodeList defaultTiles = document.GetElementsByTagName("uap:DefaultTile");
            XmlNode defaultTile = defaultTiles.Item(0);
            mixedRealityModel = document.CreateElement("uap5", "MixedRealityModel", "http://schemas.microsoft.com/appx/manifest/uap/windows10/5");
            defaultTile.AppendChild(mixedRealityModel);
        }
        else
        {
            mixedRealityModel = mixedRealityModels.Item(0) as XmlElement;
        }

        // Set the path of the mixed reality model
        mixedRealityModel.SetAttribute("Path", "Assets\\AppLauncher_3D.glb");

        // Check if we've already got a bounding box and remove it
        XmlNodeList boundingBoxes = document.GetElementsByTagName("uap6:SpatialBoundingBox");
        if (boundingBoxes.Count == 1)
        {
            mixedRealityModel.RemoveChild(boundingBoxes.Item(0));
        }

        // Add it back in if we want to override bounding box
        if (settings.OverrideBoundingBox)
        {
            // Add mixed reality model node
            XmlElement boundingBox = document.CreateElement("uap6", "SpatialBoundingBox", "http://schemas.microsoft.com/appx/manifest/uap/windows10/6");
            string center = settings.Center.x + "," + settings.Center.y + "," + settings.Center.z;
            string extents = settings.Extents.x + "," + settings.Extents.y + "," + settings.Extents.z;
            boundingBox.SetAttribute("Center", center);
            boundingBox.SetAttribute("Extents", extents);
            mixedRealityModel.AppendChild(boundingBox);
        }

        // Save xml
        document.Save(packageManifestPath);
    }
}