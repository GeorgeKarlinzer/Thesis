using CryptLearn.Modules.ModuleSolving.Core.Commands;
using CryptLearn.Modules.ModuleSolving.Core.DTOs;
using CryptLearn.Modules.ModuleSolving.Core.Entities;
using CryptLearn.Modules.ModuleSolving.Core.Interfaces;
using CryptLearn.Modules.ModuleSolving.Core.Services;
using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;

namespace CryptLearn.Modules.ModuleSolving.Unit.Handlers;

internal class CommandsHandler_Tests
{
    // submit solution

    [TestCase(false, TestName = "Test ivalid solution")]
    [TestCase(true, TestName = "Sumbit ivalid solution")]
    public async Task SubmitInvalidSolution(bool isSubmit)
    {
        // Arrange
        var repository = Substitute.For<ISolutionsRepository>();
        var codeExecutor = Substitute.For<ICodeExecutor>();
        codeExecutor.ExecuteAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(new SolutionResultDto(false)));
        var handler = new CommandsHandler(repository, codeExecutor);
        var command = new TestSolution(Guid.NewGuid(), Guid.NewGuid(), "", "", isSubmit);

        // Act
        var result = await handler.Handle(command, new());

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [TestCase(false, TestName = "Test solution with existing solution")]
    [TestCase(true, TestName = "Sumbit solution with existing solution")]
    public async Task SubmitWithExistingSolution(bool isSubmit)
    {
        // Arrange
        var moduleId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var lang = "C# 11";
        var existingSolution = new Solution();

        var repository = Substitute.For<ISolutionsRepository>();
        repository.GetAll().Returns(new (moduleId, userId, lang, Arg.Any<CancellationToken>()).Returns(Task.FromResult(existingSolution)));
        var codeExecutor = Substitute.For<ICodeExecutor>();
        codeExecutor.ExecuteAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(new SolutionResultDto(true)));

        var handler = new CommandsHandler(repository, codeExecutor);
        var command = new TestSolution(moduleId, userId, lang, "aaaa", isSubmit);

        // Act
        var result = await handler.Handle(command, new());

        // Assert
        using var scope = new AssertionScope();
        result.IsSuccess.Should().BeTrue();
        if (!isSubmit)
        {
            repository.DidNotReceive().Delete(existingSolution);
            repository.DidNotReceive().AddAsync(Arg.Any<Solution>(), Arg.Any<CancellationToken>());
            repository.DidNotReceive().SaveAsync();
        }
        else
        {
            repository.Received().Delete(existingSolution);
            repository.Received().AddAsync(Arg.Any<Solution>(), Arg.Any<CancellationToken>());
            repository.Received().SaveAsync();
        }
    }

    [TestCase(false, TestName = "Test solution")]
    [TestCase(true, TestName = "Sumbit solution")]
    public async Task SubmitSolution(bool isSubmit)
    {
        // Arrange
        var moduleId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var lang = "C# 11";

        var repository = Substitute.For<ISolutionsRepository>();
        var codeExecutor = Substitute.For<ICodeExecutor>();
        codeExecutor.ExecuteAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(new SolutionResultDto(true)));

        var handler = new CommandsHandler(repository, codeExecutor);
        var command = new TestSolution(moduleId, userId, lang, "aaaa", isSubmit);

        // Act
        var result = await handler.Handle(command, new());

        // Assert
        using var scope = new AssertionScope();
        result.IsSuccess.Should().BeTrue();
        repository.DidNotReceive().Delete(Arg.Any<Solution>());
        if (!isSubmit)
        {
            repository.DidNotReceive().AddAsync(Arg.Any<Solution>(), Arg.Any<CancellationToken>());
            repository.DidNotReceive().SaveAsync();
        }
        else
        {
            repository.Received().AddAsync(Arg.Any<Solution>(), Arg.Any<CancellationToken>());
            repository.Received().SaveAsync();
        }
    }
}
