using System.Linq;
using System.Threading.Tasks;
using Spectre.Console.Testing;
using Spectre.Verify.Extensions;
using VerifyXunit;
using Xunit;

namespace Spectre.Console.Tests.Unit
{
    [UsesVerify]
    [ExpectationPath("Widgets/Tree")]
    public class TreeTests
    {
        [Fact]
        [Expectation("Render")]
        public Task Should_Render_Tree_Correctly()
        {
            // Given
            var console = new FakeConsole(width: 80);

            var tree = new Tree(new Text("Root node")).Guide(TreeGuide.DoubleLine);

            var nestedChildren = Enumerable.Range(0, 10).Select(x => new Text($"multiple\nline {x}"));
            var child2 = new TreeNode(new Text("child2"));
            var child2Child = new TreeNode(new Text("child2-1"));
            child2.AddNode(child2Child);
            child2Child.AddNode(new TreeNode(new Text("Child2-1-1\nchild")));
            var child3 = new TreeNode(new Text("child3"));
            var child3Child = new TreeNode(new Text("single leaf\nmultiline"));
            child3Child.AddNode(new TreeNode(new Calendar(2021, 01)));
            child3.AddNode(child3Child);

            tree.AddNode("child1").AddNodes(nestedChildren);
            tree.AddNode(child2);
            tree.AddNode(child3);
            tree.AddNode("child4");

            // When
            console.Render(tree);

            // Then
            return Verifier.Verify(console.Output);
        }

        [Fact]
        [Expectation("Render_NoChildren")]
        public Task Should_Render_Tree_With_No_Child_Nodes_Correctly()
        {
            // Given
            var console = new FakeConsole(width: 80);
            var tree = new Tree(new Text("Root node"));

            // When
            console.Render(tree);

            // Then
            return Verifier.Verify(console.Output);
        }
    }
}