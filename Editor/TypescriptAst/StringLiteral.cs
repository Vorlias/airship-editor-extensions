using System.IO;

namespace Editor.TypescriptAst
{
    public class StringLiteral : IExpression
    {
        public string Text { get; set; }
        
        public static implicit operator StringLiteral(string value)
        {
            return new StringLiteral(value);
        }

        public StringLiteral(string text) {
            Text = text;
        }
        
        public void Render(RenderState renderState, StringWriter writer) {
            writer.Write($"\"{Text}\"");
        }
    }
}