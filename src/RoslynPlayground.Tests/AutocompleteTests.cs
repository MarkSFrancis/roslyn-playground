using Microsoft.CodeAnalysis;
using NUnit.Framework;
using RoslynPlayground.Analysis;
using RoslynPlayground.Samples;
using RoslynPlayground.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoslynPlayground.Tests
{
    public class AutocompleteTests
    {
        [Test]
        public void Autocomplete_NullWorkspace_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new AutocompleteService(null));
        }

        [Test]
        public async Task Autocomplete_WithCollectionAndPrefix_GetsFilteredAutocomplete()
        {
            var playground = PlaygroundWorkspace.FromSource(SourceCodeKind.Regular, SampleCode.CompilerTest, 174);

            IEnumerable<string> results;
            using (var autoComplete = new AutocompleteService(playground))
            {
                results = await GetAutocomplete(autoComplete);
            }

            var expected = new[]
            {
                "Add",
                "AddRange"
            };

            CollectionAssert.AreEqual(expected, results);
        }

        private async Task<IEnumerable<string>> GetAutocomplete(AutocompleteService autocomplete)
        {
            var selection = await autocomplete.GetAutoComplete();

            return selection.Select(r => r.DisplayText);
        }
    }
}
