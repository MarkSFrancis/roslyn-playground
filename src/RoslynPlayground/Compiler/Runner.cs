using System;
using System.Reflection;
using System.Threading.Tasks;

namespace RoslynPlayground.Compiler
{
    public class Runner
    {
        private static async Task CompileAndRun(Analyser analyser, string type, string methodName, object instance = null, params object[] parameters)
        {
            await CompileAndRun<object>(analyser, type, methodName, instance, parameters);
        }

        private static async Task<T> CompileAndRun<T>(Analyser analyser, string type, string methodName, object instance = null, params object[] parameters)
        {
            CompilerResult compiled = await analyser.CompileAsync();

            if (!compiled.Success)
            {
                return default;
            }

            var loaded = Assembly.Load(compiled.Assembly);
            Type typeToExecute = loaded.GetType(type);

            var result = typeToExecute.InvokeMember(methodName ?? "<Main>", BindingFlags.InvokeMethod, null, instance, parameters);
            return (T)result;
        }

        public static async Task<T> CompileAndRunScript<T>()
        {


            MethodInfo[] allMethods = typeToExecute.GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            MethodInfo[] allMethods2 = typeToExecute.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}
