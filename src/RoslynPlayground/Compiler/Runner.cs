using System;
using System.Reflection;
using System.Threading.Tasks;

namespace RoslynPlayground.Compiler
{
    public class Runner
    {
        public static async Task CompileAndRun(Analyser analyser, string type, string methodName, object instance = null, params object[] parameters)
        {
            await CompileAndRun<object>(analyser, type, methodName, BindingFlags.Default, instance, parameters);
        }

        public static Task<T> CompileAndRun<T>(Analyser analyser, string type, string methodName, object instance = null, params object[] parameters)
        {
            return CompileAndRun<T>(analyser, type, methodName, BindingFlags.Default, instance, parameters);
        }

        public static async Task CompileAndRun(Analyser analyser, string type, string methodName, BindingFlags methodFlags, object instance = null, params object[] parameters)
        {
            await CompileAndRun<object>(analyser, type, methodName, methodFlags, instance, parameters);
        }

        public static async Task<T> CompileAndRun<T>(Analyser analyser, string type, string methodName, BindingFlags methodFlags, object instance = null, params object[] parameters)
        {
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

        public static Task CompileAndRunScript(Analyser analyser)
        {
            return CompileAndRun(analyser, "Script", "<Main>", BindingFlags.NonPublic | BindingFlags.Static);
        }

        public static Task<T> CompileAndRunScript<T>(Analyser analyser)
        {
            return CompileAndRun<T>(analyser, "Script", "<Main>", BindingFlags.NonPublic | BindingFlags.Static);
        }
    }
}
