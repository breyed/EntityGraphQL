using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using EntityGraphQL.Compiler;

namespace EntityGraphQL.Directives
{
    public class IncludeDirectiveProcessor : DirectiveProcessor<IncludeArguments>
    {
        public override string Name { get => "include"; }
        public override string Description { get => "Directs the executor to include this field or fragment only when the `if` argument is true."; }

        public override List<ExecutableDirectiveLocation> On => new() { ExecutableDirectiveLocation.FIELD, ExecutableDirectiveLocation.FRAGMENT_SPREAD, ExecutableDirectiveLocation.INLINE_FRAGMENT };

        public override Expression? ProcessExpression(Expression expression, object? arguments)
        {
            if (arguments is null)
                throw new ArgumentNullException("Argument 'if' is requred for @include directive");
            if (((IncludeArguments)arguments).@if)
                return expression;
            return null;
        }
        public override BaseGraphQLField? ProcessField(BaseGraphQLField field, object? arguments)
        {
            if (arguments is null)
                throw new ArgumentNullException("Argument 'if' is requred for @include directive");
            if (((IncludeArguments)arguments).@if)
                return field;
            return null;
        }
    }

    public class IncludeArguments
    {
        [Description("Included when true.")]
        public bool @if { get; set; }
    }
}