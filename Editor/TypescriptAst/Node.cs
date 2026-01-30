using System.IO;

namespace Editor.TypescriptAst
{
    public class RenderState {
        public int Indent { get; set; }

        public string IndentString => new ('\t', Indent);
    }
    
    public interface IRenderableNode
    {
        public void Render(RenderState renderState, StringWriter writer);
    }

    public interface IExpression : IRenderableNode {}
    public interface IStatement : IRenderableNode {}

    public interface IModifiers
    {
        public bool Export { get; set; }
        public bool Const { get; set; }
        public bool Declare { get; set; }
    }
}