using System.Collections.Generic;

namespace RoslynPlayground.Code
{
    public class CodeImports
    {
        //public static readonly IReadOnlyCollection<string> DefaultUsings = new[]
        //{
        //    "System",
        //    "System.Linq",
        //    "System.Collections.Generic",
        //    "System.Globalization",
        //    "System.IO",
        //    "System.Text",
        //    "System.Threading.Tasks"
        //};

        public static IEnumerable<string> GetFrameworkReferences()
        {
            yield return "mscorlib";
            yield return "netstandard";
            yield return "System.AppContext";
            yield return "System.Collections.Concurrent";
            yield return "System.Collections";
            yield return "System.Collections.NonGeneric";
            yield return "System.Collections.Specialized";
            yield return "System.ComponentModel";
            yield return "System.ComponentModel.EventBasedAsync";
            yield return "System.ComponentModel.Primitives";
            yield return "System.ComponentModel.TypeConverter";
            yield return "System.Console";
            yield return "System.Core";
            yield return "System.Data.Common";
            yield return "System.Data";
            yield return "System.Diagnostics.Contracts";
            yield return "System.Diagnostics.Debug";
            yield return "System.Diagnostics.FileVersionInfo";
            yield return "System.Diagnostics.Process";
            yield return "System.Diagnostics.StackTrace";
            yield return "System.Diagnostics.TextWriterTraceListener";
            yield return "System.Diagnostics.Tools";
            yield return "System.Diagnostics.TraceSource";
            yield return "System.Diagnostics.Tracing";
            yield return "System";
            yield return "System.Drawing";
            yield return "System.Drawing.Primitives";
            yield return "System.Dynamic.Runtime";
            yield return "System.Globalization.Calendars";
            yield return "System.Globalization";
            yield return "System.Globalization.Extensions";
            yield return "System.IO.Compression";
            yield return "System.IO.Compression.FileSystem";
            yield return "System.IO.Compression.ZipFile";
            yield return "System.IO";
            yield return "System.IO.FileSystem";
            yield return "System.IO.FileSystem.DriveInfo";
            yield return "System.IO.FileSystem.Primitives";
            yield return "System.IO.FileSystem.Watcher";
            yield return "System.IO.IsolatedStorage";
            yield return "System.IO.MemoryMappedFiles";
            yield return "System.IO.Pipes";
            yield return "System.IO.UnmanagedMemoryStream";
            yield return "System.Linq";
            yield return "System.Linq.Expressions";
            yield return "System.Linq.Parallel";
            yield return "System.Linq.Queryable";
            yield return "System.Net";
            yield return "System.Net.Http";
            yield return "System.Net.NameResolution";
            yield return "System.Net.NetworkInformation";
            yield return "System.Net.Ping";
            yield return "System.Net.Primitives";
            yield return "System.Net.Requests";
            yield return "System.Net.Security";
            yield return "System.Net.Sockets";
            yield return "System.Net.WebHeaderCollection";
            yield return "System.Net.WebSockets.Client";
            yield return "System.Net.WebSockets";
            yield return "System.Numerics";
            yield return "System.ObjectModel";
            yield return "System.Reflection";
            yield return "System.Reflection.Extensions";
            yield return "System.Reflection.Primitives";
            yield return "System.Resources.Reader";
            yield return "System.Resources.ResourceManager";
            yield return "System.Resources.Writer";
            yield return "System.Runtime.CompilerServices.VisualC";
            yield return "System.Runtime";
            yield return "System.Runtime.Extensions";
            yield return "System.Runtime.Handles";
            yield return "System.Runtime.InteropServices";
            yield return "System.Runtime.InteropServices.RuntimeInformation";
            yield return "System.Runtime.Numerics";
            yield return "System.Runtime.Serialization";
            yield return "System.Runtime.Serialization.Formatters";
            yield return "System.Runtime.Serialization.Json";
            yield return "System.Runtime.Serialization.Primitives";
            yield return "System.Runtime.Serialization.Xml";
            yield return "System.Security.Claims";
            yield return "System.Security.Cryptography.Algorithms";
            yield return "System.Security.Cryptography.Csp";
            yield return "System.Security.Cryptography.Encoding";
            yield return "System.Security.Cryptography.Primitives";
            yield return "System.Security.Cryptography.X509Certificates";
            yield return "System.Security.Principal";
            yield return "System.Security.SecureString";
            yield return "System.ServiceModel.Web";
            yield return "System.Text.Encoding";
            yield return "System.Text.Encoding.Extensions";
            yield return "System.Text.RegularExpressions";
            yield return "System.Threading";
            yield return "System.Threading.Overlapped";
            yield return "System.Threading.Tasks";
            yield return "System.Threading.Tasks.Parallel";
            yield return "System.Threading.Thread";
            yield return "System.Threading.ThreadPool";
            yield return "System.Threading.Timer";
            yield return "System.Transactions";
            yield return "System.ValueTuple";
            yield return "System.Web";
            yield return "System.Windows";
            yield return "System.Xml";
            yield return "System.Xml.Linq";
            yield return "System.Xml.ReaderWriter";
            yield return "System.Xml.Serialization";
            yield return "System.Xml.XDocument";
            yield return "System.Xml.XmlDocument";
            yield return "System.Xml.XmlSerializer";
            yield return "System.Xml.XPath";
            yield return "System.Xml.XPath.XDocument";
        }
    }
}
