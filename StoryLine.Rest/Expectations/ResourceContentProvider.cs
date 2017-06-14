using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StoryLine.Rest.Expectations
{
    public class ResourceContentProvider : IResourceContentProvider
    {
        private static readonly string[] AttributeNames =
        {
            "TestAttribute",
            "TestCaseAttribute"
        };

        private static readonly string[] Suffixes =
        {
            ".approved.json",
            ".approved.xml",
            ".json",
            ".xml",
            ""
        };

        private readonly IResourceAccessmblyProvider _resourceAssemblyProvider;

        public ResourceContentProvider(IResourceAccessmblyProvider resourceAssemblyProvider)
        {
            _resourceAssemblyProvider = resourceAssemblyProvider ?? throw new ArgumentNullException(nameof(resourceAssemblyProvider));
        }

        public string GetContent(string resourceName)
        {
            var stackTrace = Environment.StackTrace;
            var methods =
                (from line in stackTrace.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                    let methodIndex = line.IndexOf("(", StringComparison.OrdinalIgnoreCase)
                    where methodIndex != -1
                    select line.Substring(0, methodIndex))
                .ToArray();

            methods =
                (from method in methods
                    where method.StartsWith("   at ", StringComparison.OrdinalIgnoreCase)
                    select method.Substring("   at ".Length))
                .ToArray();

            var info =
                (from item in methods
                    let dotIndex = item.LastIndexOf(".", StringComparison.OrdinalIgnoreCase)
                    select new MethodDetails
                    {
                        TypeName = item.Substring(0, dotIndex),
                        MethodName = item.Substring(dotIndex + 1)
                    })
                .ToArray();

            var testMethod = info.FirstOrDefault(IsTestCase);
            if (testMethod == null)
                return string.Empty;

            resourceName = resourceName ?? testMethod.MethodName;

            foreach (var assembly in _resourceAssemblyProvider.GetAssemblies())
            {
                var resources = new HashSet<string>(assembly.GetManifestResourceNames(), StringComparer.OrdinalIgnoreCase);

                foreach (var suffix in Suffixes)
                {
                    var possibleResourceName = testMethod.TypeName + "." + resourceName + suffix;

                    if (resources.Contains(possibleResourceName))
                        return GetResourceData(assembly, possibleResourceName);
                }
            }

            return string.Empty;
        }

        private static string GetResourceData(Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    return null;

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }


        private bool IsTestCase(MethodDetails details)
        {
            foreach (var assembly in _resourceAssemblyProvider.GetAssemblies())
            {
                var type = assembly.GetType(details.TypeName);

                var method = type?.GetTypeInfo().GetDeclaredMethod(details.MethodName);
                if (method == null)
                    continue;

                var attributes = method.GetCustomAttributes();

                return attributes.Any(x => AttributeNames.Any(p => p == x.GetType().Name));
            }

            return false;
        }

        private class MethodDetails
        {
            public string TypeName { get; set; }
            public string MethodName { get; set; }
        }
    }
}