using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MachinCuttingApp;

namespace MachineCuttingUnitTests
{
    [TestClass]
    public class ControlerUnitTests
    {
        [TestMethod]
        public void SetMaterialBlockTests()
        {
            //set up controller
            Controler C = new Controler();

            //set up test strings
            string stringFailBoundsCheck1 = $"{Controler.DimensionString} 20 -30";
            string stringFailBoundsCheck2 = $"{Controler.DimensionString} -30 1";
            string stringFailParamsLength = $"{Controler.DimensionString}";
            string stringFailNotFoundNoSpace = $"{Controler.DimensionString}20 20";
            string stringFailNotFound = "I will return instruction not found";
            string stringPass = $"{Controler.DimensionString} 20 20";
            string properAmount = "(20,20)";
            
            //set up arrays
            string[] inputs = { stringFailBoundsCheck1, stringFailBoundsCheck2, stringFailParamsLength,
                stringFailNotFoundNoSpace, stringFailNotFound, stringPass};
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { Controler.FAILED_BOUNDS_CHECK, Controler.FAILED_BOUNDS_CHECK,
                Controler.FAILED_PARAM_LENGTH, Controler.FAILED_INSTRUCTION_NOT_FOUND,
                Controler.FAILED_INSTRUCTION_NOT_FOUND, Controler.PASS};
            
            //run tests
            for (int i = 0; i < inputs.Length; i++) { outputs[i] = C.runInstruction(inputs[i]); }
            string testAmount = C.materialString();

            CollectionAssert.AreEqual(outputs, intendedOutputs);
            Assert.AreEqual(testAmount, properAmount);
        }

        [TestMethod]
        public void SetCutLocationTests()
        {
            //set up controller
            Controler C = new Controler();
            C.runInstruction($"{Controler.DimensionString} 20 20");

            //test setup
            string stringFailBoundsCheck1 = $"{Controler.LocationString} 20 -30";
            string stringFailBoundsCheck2 = $"{Controler.LocationString} -30 1";
            string stringFailBoundsCheck3 = $"{Controler.LocationString} 30 10";
            string stringFailBoundsCheck4 = $"{Controler.LocationString} 10 30";
            string stringFailParamsLength = $"{Controler.LocationString}";
            string stringFailNotFoundNoSpace = $"{Controler.LocationString}20 20";
            string stringFailNotFound = "I will return instruction not found";
            string stringPass = $"{Controler.LocationString} 10 10";
            string properAmount = "(10,10)";
            
            //set up arrays for tests
            string[] inputs = { stringFailBoundsCheck1, stringFailBoundsCheck2,
                stringFailBoundsCheck3, stringFailBoundsCheck4, stringFailParamsLength,
                stringFailNotFoundNoSpace, stringFailNotFound, stringPass};
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { Controler.FAILED_BOUNDS_CHECK, Controler.FAILED_BOUNDS_CHECK,
                Controler.FAILED_BOUNDS_CHECK, Controler.FAILED_BOUNDS_CHECK, Controler.FAILED_PARAM_LENGTH,
                Controler.FAILED_INSTRUCTION_NOT_FOUND, Controler.FAILED_INSTRUCTION_NOT_FOUND, Controler.PASS};

            //run tests
            for (int i = 0; i < inputs.Length; i++) { outputs[i] = C.runInstruction(inputs[i]); }
            string testAmount = C.locationString();

            CollectionAssert.AreEqual(outputs, intendedOutputs);
            Assert.AreEqual(testAmount, properAmount);
        }
    }
}
