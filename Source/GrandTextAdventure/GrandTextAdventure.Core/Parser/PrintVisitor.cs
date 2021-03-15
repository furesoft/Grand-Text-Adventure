using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandTextAdventure.Core.Parser.Syntax;

namespace GrandTextAdventure.Core.Parser
{
    public class PrintVisitor : IScriptVisitor
    {
        private readonly StringBuilder _stringBuilder = new();

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }

        public void Visit(BlockNode block)
        {
            _stringBuilder.Append('(');

            foreach (var item in block.Children)
            {
                if (item is EntityModelDefinitionNode emd)
                {
                    Visit(emd);
                }
                else if (item is EntityDefinitionNode ed)
                {
                    Visit(ed);
                }
                else if (item is PropertyDefinitionNode pd)
                {
                    Visit(pd);
                }
                else if (item is ApplyModelDefinition amd)
                {
                    Visit(amd);
                }
            }

            _stringBuilder.Append(')');
        }

        public void Visit(EntityDefinitionNode definitionNode)
        {
            _stringBuilder.AppendLine($"(entity {definitionNode.TypeToken.Value} {definitionNode.NameToken.Value} (");
            Visit(definitionNode.Children);
            _stringBuilder.AppendLine("))");
        }

        public void Visit(EntityModelDefinitionNode definitionNode)
        {
            _stringBuilder.AppendLine($"(model {definitionNode.NameToken.Value} (");
            Visit(definitionNode.Properties);
            _stringBuilder.AppendLine("))");
        }

        public void Visit(PropertyDefinitionNode definitionNode)
        {
            _stringBuilder.AppendLine($"(property ({definitionNode.NameToken.Value} {definitionNode.Value.Text}))");
        }

        public void Visit(ApplyModelDefinition applyModelDefinition)
        {
            _stringBuilder.AppendLine($"(applymodel {applyModelDefinition.NameToken.Text})");
        }
    }
}
