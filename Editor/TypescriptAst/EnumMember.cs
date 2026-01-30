using System.IO;

namespace Editor.TypescriptAst
{
    public class EnumMember : IRenderableNode
    {
        PropertyName Name { get; set; }
        IExpression Initializer { get; set; }
        
        public void Render(RenderState renderState, StringWriter writer)
        {
            writer.Write(renderState.IndentString);

            if (Initializer != null) {
                Name.Render(renderState, writer);
                writer.Write(" = ");
                Initializer.Render(renderState, writer);
            }
            else {
                Name.Render(renderState, writer);
            }
        }
        
        
        public EnumMember(PropertyName propertyName)
        {
            Name = propertyName;
            Initializer = null;
            //Comment = null;
        }
        
        public EnumMember(PropertyName propertyName, IExpression expression)
        {
            Name = propertyName;
            Initializer = expression;
            //Comment = null;
        }
    }
}