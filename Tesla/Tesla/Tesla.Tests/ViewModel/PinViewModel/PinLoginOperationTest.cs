using System.Threading.Tasks;
using Exrin.Abstraction;
using TeslaDefinition.Interfaces.Model;
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Tesla.ViewModelOperation;
using Xunit;
using Tesla.Model;
using Exrin.Framework;
using Xunit.Extensions;
using Tesla.Tests;
using Tesla.Control;

namespace Tesla.ViewModelOperation.Tests
{

    public partial class PinLoginOperationTest
    {

        public PinLoginOperationTest()
        {

        }

        public IPinModel PinModel
        {
            get
            {
                return new PinModel(CommonService.DisplayService, CommonService.ErrorHandlingService);
            }
        }

        public Func<IResult, Task> GetOperation()
        {
            return new PinLoginOperation(PinModel, Keypad.BackCharacter).Function;
        }
      
        public static TheoryData<string[]> SimplePin { get { return new TheoryData<string[]>() { new string[] { "1", "2", "3", "4" } }; } }
        public static TheoryData<string[]> SimplePinWithBackspace { get { return new TheoryData<string[]>() { new string[] { "1", "2", Keypad.BackCharacter, "4", "5" } }; } }
        public static TheoryData<string[]> StartingBackspacePin { get { return new TheoryData<string[]>() { new string[] { Keypad.BackCharacter, "2", "1", "4", "5" } }; } }

        [Theory]
        [MemberData(nameof(SimplePin))]
        [MemberData(nameof(SimplePinWithBackspace))]
        [MemberData(nameof(StartingBackspacePin))]
        public void OperationTest(string[] characters)
        {
            var function = GetOperation();

            int count = 0;
            foreach (var character in characters)
            {
                IResult result = new Result(character);

                function(result);

                Assert.NotNull(result);

                if (count == BusinessRules.PinLength -1)
                    Assert.Equal(result.ResultAction, ResultType.Navigation);
                else
                    Assert.Equal(result.ResultAction, ResultType.None);

            }
        }

      
    }
}
