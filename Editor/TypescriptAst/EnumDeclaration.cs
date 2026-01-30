using System.Collections.Generic;
using System.IO;

namespace Editor.TypescriptAst
{
    public class EnumDeclaration : IStatement, IModifiers
    {
        public bool Export { get; set; }
        public bool Const { get; set; }
        public bool Declare { get; set; }
        
        public Identifier Identifier { get; set; }
        public List<EnumMember> Members { get; set; } = new();
        
        public void Render(RenderState renderState, StringWriter writer)
        {
            writer.Write(renderState.IndentString);

            if (Export) {
                writer.Write("export ");
            }
            
            if (Declare) {
                writer.Write("declare ");
            }
            
            if (Const) {
                writer.Write("const ");
            }
            
            writer.Write($"enum ");
            if (Identifier != null) Identifier.Render(renderState, writer);
            writer.WriteLine(" {");

            if (Members != null) {
                renderState.Indent += 1;
                foreach (var member in Members) {
                    member.Render(renderState, writer);
                    writer.WriteLine(",");
                }
                renderState.Indent -= 1;
            }
            
            writer.Write(renderState.IndentString + "}");
        }

        public EnumDeclaration(Identifier identifier)
        {
            Identifier = identifier;
        }
    }
}