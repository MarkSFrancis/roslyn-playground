using System;
using System.Reflection;
using System.Threading.Tasks;

namespace RoslynPlayground.Compiler
{
    public static class Runner
    {
        public const string ScriptTypeName = "Script";
        public const string ScriptEntryPointName = "<Main>";
        public const BindingFlags ScriptEntryPointFlags = BindingFlags.NonPublic | BindingFlags.Static;

        public static async Task CompileAndRun(this IAnalyser analyser, string type, string methodName, object instance = null, params object[] parameters)
        {
            await CompileAndRun<object>(analyser, type, methodName, BindingFlags.Default, instance, parameters);
        }

        public static Task<T> CompileAndRun<T>(this IAnalyser analyser, string type, string methodName, object instance = null, params object[] parameters)
        {
            return CompileAndRun<T>(analyser, type, methodName, BindingFlags.Default, instance, parameters);
        }

        public static async Task CompileAndRun(this IAnalyser analyser, string type, string methodName, BindingFlags methodFlags, object instance = null, params object[] parameters)
        {
            await CompileAndRun<object>(analyser, type, methodName, methodFlags, instance, parameters);
        }

        public static async Task<T> CompileAndRun<T>(this IAnalyser analyser, string type, string methodName, BindingFlags methodFlags, object instance = null, params object[] parameters)
        {
            if (analyser is null)
            {
                throw new ArgumentNullException(nameof(analyser));
            }
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (methodName is null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            CompilerResult compiled = await analyser.CompileAsync();

            if (!compiled.Success)
            {
                return default;
            }

            var loaded = Assembly.Load(compiled.Assembly);
            Type typeToExecute = loaded.GetType(type);

            var result = typeToExecute.InvokeMember(methodName, BindingFlags.InvokeMethod | methodFlags, null, instance, parameters);
            return (T)result;
        }

        public static Task CompileAndRunScript(this Analyser analyser)
        {
            return CompileAndRunScript<object>(analyser);
        }

        public static Task<T> CompileAndRunScript<T>(this Analyser analyser)
        {
            return CompileAndRun<T>(analyser, ScriptTypeName, ScriptEntryPointName, ScriptEntryPointFlags);
        }
    }
}
