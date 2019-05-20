﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoslynPlayground.Code
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
            foreach(var file in files)
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
        }

        public PlaygroundWorkspace(
            SourceCodeKind workspaceType,
            params SourceFile[] files) : this(workspaceType, (IReadOnlyCollection<SourceFile>)files)
        {
        }

        public IReadOnlyCollection<SourceFile> Files { get; }

        public SourceCodeKind WorkspaceType { get; }

        public SourceFile EditingFile { get; }

        public Project ToProject()
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

            foreach (var fileToLoad in Files)
            {
                newWorkspace.AddDocument(projectInfo.Id, fileToLoad.Filename, fileToLoad.Code);
            }

            return newWorkspace.CurrentSolution.GetProject(projectInfo.Id);
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
