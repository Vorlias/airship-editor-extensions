using System.IO;

namespace Editor.TypescriptAst
{
    public class PropertyName : IRenderableNode
    {
        private IExpression Expression { get; set; }
        
        public void Render(RenderState renderState, StringWriter writer)
        {
            if (Expression is StringLiteral stringLiteral) {
                if (stringLiteral.Text.Contains(" ") || stringLiteral.Text == "") {
                    // writer.Write("[");
                    stringLiteral.Render(renderState, writer);
                    // writer.Write("]");
                }
                else {
                    writer.Write(stringLiteral.Text);
                }
            }
            else {
                Expression.Render(renderState, writer);
            }
        }

        public static implicit operator PropertyName(Identifier identifier)
        {
            return new PropertyName()
            {
                Expression = identifier,
            };
        }
        
        public static implicit operator PropertyName(StringLiteral stringLiteral)
        {
            return new PropertyName()
            {
                Expression = stringLiteral,
            };
        }
    }
}