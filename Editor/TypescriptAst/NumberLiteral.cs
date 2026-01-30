using System.IO;

namespace Editor.TypescriptAst
{
    public class NumberLiteral : IExpression
    {
        public float Value { get; set; }

        public void Render(RenderState renderState, StringWriter writer)
        {
            writer.Write(Value);
        }

        public NumberLiteral(float value)
        {
            Value = value;
        }
    }
}