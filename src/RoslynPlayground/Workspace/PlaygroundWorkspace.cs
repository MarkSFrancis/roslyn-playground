using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoslynPlayground.Workspace
{
    public class PlaygroundWorkspace
    {
        public PlaygroundWorkspace(
            SourceCodeKind workspaceType,
            IReadOnlyCollection<SourceFile> files)
        {
            WorkspaceType = workspaceType;
            files ??= Array.Empty<SourceFile>();

            var myFiles = new List<SourceFile>(files.Count);
            foreach (var file in files)
            {
                if (file.EditorPosition.HasValue)
                {
                    if (EditingFile != null)
                    {
                        throw new ArgumentException("Cannot edit two files simultaneously");
                    }

                    EditingFile = file;
                }

                if (myFiles.Any(f => f.Filename == file.Filename))
                {
                    throw new ArgumentException($"Duplicate filename:{Environment.NewLine}{string.Join(", ", files.Select(f => f.Filename))}");
                }

                myFiles.Add(file);
            }

            Files = myFiles;

            (ActiveProject, EditingDocument) = LoadProject();
        }

        public PlaygroundWorkspace(
            SourceCodeKind workspaceType,
            params SourceFile[] files) : this(workspaceType, (IReadOnlyCollection<SourceFile>)files)
        {
        }

        public IReadOnlyCollection<SourceFile> Files { get; }

        public SourceCodeKind WorkspaceType { get; }

        public SourceFile EditingFile { get; }

        public Document EditingDocument { get; }

        public Project ActiveProject { get; }

        private (Project project, Document editingDocument) LoadProject()
        {
            var newWorkspace = new AdhocWorkspace();
            var solution = SolutionInfo.Create(SolutionId.CreateNewId("sandbox"), VersionStamp.Default);
            newWorkspace.AddSolution(solution);

            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);

            var projectInfo = ProjectInfo.Create(
                ProjectId.CreateNewId("sandbox"),
                VersionStamp.Default,
                "sandbox",
                "sandbox",
                LanguageNames.CSharp,
                metadataReferences: new[] { mscorlib },
                compilationOptions: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
                parseOptions: new CSharpParseOptions(LanguageVersion.Default, DocumentationMode.Parse, WorkspaceType)
            );

            newWorkspace.AddProject(projectInfo);

            Document editingDocument = null;

            foreach (var fileToLoad in Files)
            {
                var documentToLoad = DocumentInfo.Create(
                    DocumentId.CreateNewId(projectInfo.Id), 
                    fileToLoad.Filename, 
                    loader: fileToLoad.GetSourceLoader()
                );

                var document = newWorkspace.AddDocument(documentToLoad);

                if (fileToLoad.EditorPosition.HasValue)
                {
                    editingDocument = document;
                }
            }

            var project = newWorkspace.CurrentSolution.GetProject(projectInfo.Id);

            return (project, editingDocument);
        }

        public static PlaygroundWorkspace FromSource(
            SourceCodeKind workspaceType,
            string source,
            int? editorPosition = null,
            string filename = "Program.cs")
        {
            return new PlaygroundWorkspace(workspaceType, new SourceFile(filename, source, editorPosition));
        }
    }
}
