using CryptLearn.Modules.AccessControl.Core.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;

namespace CryptLearn.Modules.AccessControl.Unit.Services
{
    public class PasswordValidator_Test
    {
        private static IEnumerable<TestCaseData> GetData()
        {
            var describer = new IdentityErrorDescriber();

            yield return new TestCaseData(MakeOptions(x => x.RequireDigit = true), "aaafasdj@#!(", Errors(describer.PasswordRequiresDigit()))
                .SetName(GetName("a digit", false));
            yield return new TestCaseData(MakeOptions(x => x.RequireDigit = true), "aaafasdj@#!(1", null)
                .SetName(GetName("a digit", true));


            yield return new TestCaseData(MakeOptions(x => x.RequiredLength = 9), "12345678", Errors(describer.PasswordTooShort(9)))
                .SetName(GetName("length 9", false));
            yield return new TestCaseData(MakeOptions(x => x.RequiredLength = 9), "123456789", null)
                .SetName(GetName("length 9", true));


            yield return new TestCaseData(MakeOptions(x => x.RequireLowercase = true), "ASD1232*@!*(*", Errors(describer.PasswordRequiresLower()))
                .SetName(GetName("lowercase", false));
            yield return new TestCaseData(MakeOptions(x => x.RequireLowercase = true), "ASbD1232*@!*(*", null)
                .SetName(GetName("lowercase", true));


            yield return new TestCaseData(MakeOptions(x => x.RequiredUniqueChars = 5), "111222333444", Errors(describer.PasswordRequiresUniqueChars(5)))
                .SetName(GetName("5 unique chars", false));
            yield return new TestCaseData(MakeOptions(x => x.RequiredUniqueChars = 5), "11111222233344455", null)
                .SetName(GetName("5 unique chars", true));


            yield return new TestCaseData(MakeOptions(x => x.RequireNonAlphanumeric = true), "DAsasdASdsd12321", Errors(describer.PasswordRequiresNonAlphanumeric()))
                .SetName(GetName("non alphanumeric", false));
            yield return new TestCaseData(MakeOptions(x => x.RequireNonAlphanumeric = true), "asdasASD2132(", null)
                .SetName(GetName("non alphanumeric", true));


            yield return new TestCaseData(MakeOptions(x => x.RequireUppercase = true), "asdsadsas123121@#!@#", Errors(describer.PasswordRequiresUpper()))
                .SetName(GetName("uppercase", false));
            yield return new TestCaseData(MakeOptions(x => x.RequireUppercase = true), "dasjd@#@*(2A(", null)
                .SetName(GetName("uppercase", true));


            static PasswordOptions MakeOptions(Action<PasswordOptions> makeOptions)
            {
                var options = new PasswordOptions
                {
                    RequireDigit = false,
                    RequiredLength = 0,
                    RequiredUniqueChars = 0,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false
                };
                makeOptions(options);
                return options;
            }

            static IEnumerable<IdentityError> Errors(params IdentityError[] errors) 
                => errors;

            static string GetName(string condition, bool isSatisfy)
            {
                var end = isSatisfy ? " and gets it" : ", but doesn't get it";
                return $"Requires {condition}{end}";
            }
        }

        [Test, TestCaseSource(nameof(GetData))]
        public void TestPasswords(PasswordOptions options, string password, IEnumerable<IdentityError> expectedErrors)
        {
            // arrange
            var validator = new PasswordValidator(x =>
            {
                x.RequireDigit = options.RequireDigit;
                x.RequiredLength = options.RequiredLength;
                x.RequiredUniqueChars = options.RequiredUniqueChars;
                x.RequireLowercase = options.RequireLowercase;
                x.RequireNonAlphanumeric = options.RequireNonAlphanumeric;
                x.RequireUppercase = options.RequireUppercase;
            });

            // act
            var result = validator.Validate(password);

            // assert
            result.Succeeded.Should().Be(expectedErrors?.Any() is not true);
            GetCodesInOrder(result.Errors).Should().BeEquivalentTo(GetCodesInOrder(expectedErrors));

            static IEnumerable<string> GetCodesInOrder(IEnumerable<IdentityError> errors)
                 => errors?.Select(x => x.Code).OrderBy(x => x) ?? Enumerable.Empty<string>();
        }
    }
}
