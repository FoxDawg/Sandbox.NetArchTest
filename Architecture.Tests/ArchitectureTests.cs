using System;
using System.Threading.Tasks;
using FluentAssertions;
using NetArchTest.Rules;
using NetArchTest.Rules.Policies;
using Xunit;
using Xunit.Abstractions;

namespace Architecture.Tests;

public class ArchitectureTests
{
    private readonly ITestOutputHelper output;

    public ArchitectureTests(ITestOutputHelper output)
    {
        this.output = output;
    }
    
    [Fact]
    public void ArchitecturalRules_InCore_AreMet()
    {
        var policy = Policy.Define("Core policies", "Enforces different checks on core types")
            .For(Types.InAssembly(typeof(Core.Core).Assembly))
            .Add(types => types
                    .That().AreClasses()
                    .And().DoNotHaveNameEndingWith("Query")
                    .Should().NotBePublic(),
                "Use correct modifiers",
                "Classes other than '...Query' should not be public"
            );
      
        policy.Evaluate().Report(output);
    }
    
    [Fact]
    public void ArchitecturalRules_InInfrastructure_AreMet()
    {
        var policy = Policy.Define("Infrastructure policies", "Enforces different checks on infrastructural types")
            .For(Types.InAssembly(typeof(Infrastructure.Infrastructure).Assembly))
            .Add(types => types
                    .That().AreClasses()
                    .Should().NotBePublic(),
                "Use correct modifiers",
                "Classes in Infrastructure should not be public"
                )
            .Add(types => types
                    .That().AreClasses()
                    .Should().NotHaveDependencyOn("Newtonsoft.Json"),
                "Use only System.Text.Json",
            "Classes in Infrastructure should not use Newtonsoft");
      
        policy.Evaluate().Report(output);
    }
}