using System.IO;

namespace Editor.TypescriptAst
{
    public class Identifier : IExpression
    {
        public string Name { get; set; }

        public void Render(RenderState renderState, StringWriter writer)
        {
            writer.Write(Name);
        }

        public static implicit operator Identifier(string name)
        {
            return new Identifier(name);
        }
        
        public Identifier(string name)
        {
            Name = name;
        }
    }
}