using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RoslynPlayground.Workspace;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoslynPlayground.Tokens
{
    public static class TokensExtensions
    {
        public static async Task<IEnumerable<Token>> GetTokensAsync(this PlaygroundWorkspace workspace)
        {
            SemanticModel semantics = await workspace.EditingDocument.GetSemanticModelAsync();

            SyntaxNode root = await semantics.SyntaxTree.GetRootAsync();

            var allNodes = root.ChildNodesAndTokens();

            var tokens = new List<Token>();

            for (int index = 0; index < allNodes.Count; index++)
            {
                if (allNodes[index].IsToken)
                {
                    var converted = ConvertToken(allNodes[index].AsToken());
                    tokens.Add(converted);
                }
                else if (allNodes[index].IsNode)
                {
                    var converted = allNodes[index].AsNode();

                    var processedChildren = ProcessNodeChildren(converted);

                    tokens.AddRange(processedChildren);
                }
            }

            return tokens;
        }

        private static IEnumerable<Token> ProcessNodeChildren(SyntaxNode node)
        {
            foreach(var token in node.ChildTokens())
            {
                yield return ConvertToken(token);
            }

            foreach(var childNode in node.ChildNodes())
            {
                var nodeString = childNode.ToString();
                var kind = childNode.Kind();

                foreach(var childToken in ProcessNodeChildren(childNode))
                {
                    yield return childToken;
                }
            }
        }

        private static Token ConvertToken(SyntaxToken token)
        {
            var kind = token.Kind();

            var newToken = new Token
            {
                Start = token.Span.Start,
                End = token.Span.End,
                Type = ToToken(kind),
                Text = token.ToFullString()
            };

            return newToken;
        }

        private static TokenType ToToken(SyntaxKind kind)
        {
            if (KindCategoryIs(kind, "keyword"))
            {
                return TokenType.Keyword;
            }
            else if (KindCategoryIs(kind, "literaltoken"))
            {
                return TokenType.Literal;
            }
            else
            {
                return TokenType.None;
            }
        }

        private static bool KindCategoryIs(SyntaxKind syntax, string compare)
        {
            return syntax.ToString().EndsWith(compare, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
