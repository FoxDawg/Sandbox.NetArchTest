using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using NetArchTest.Rules.Policies;
using Xunit.Abstractions;

namespace Architecture.Tests;

public static class PolicyExtensions
{
    public static void Report(this PolicyResults results, ITestOutputHelper output)
    {
        if (results.HasViolations)
        {
            output.WriteLine($"Policy violations found for: {results.Name}");

            foreach (var rule in results.Results)
            {
                if (!rule.IsSuccessful)
                {
                    output.WriteLine("-----------------------------------------------------------");
                    output.WriteLine($"Rule failed: {rule.Name}");

                    foreach (var type in rule.FailingTypes)
                    {
                        output.WriteLine($"\t {type.FullName}");
                    }
                }
            }

            output.WriteLine("-----------------------------------------------------------");
        }
        else
        {
            output.WriteLine($"No policy violations found for: {results.Name}");
        }
        
        using var scope = new AssertionScope();
        results.HasViolations.Should().BeFalse();
    }
}