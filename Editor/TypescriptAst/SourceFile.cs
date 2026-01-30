using System.Collections.Generic;
using System.IO;

namespace Editor.TypescriptAst
{
    public class SourceFile : IRenderableNode
    {
        public List<IStatement> Statements { get; set; } = new();
        public void Render(RenderState renderState, StringWriter writer)
        {
            if (Statements != null) {
                foreach (var statement in Statements) {
                    statement.Render(renderState, writer);
                    writer.WriteLine();
                }
            }
        }
        
        public override string ToString() {
            var renderState = new RenderState();
            var writer = new StringWriter();
            
            Render(renderState, writer);
            return writer.ToString();
        }
    }
}