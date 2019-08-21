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
            string stringFailParamsLength2 = $"{Controler.DimensionString} 20 20 20";
            string stringFailNotFoundNoSpace = $"{Controler.DimensionString}20 20";
            string stringFailNotFound = "I will return instruction not found";
            string stringPass = $"{Controler.DimensionString} 20 20";
            
            //set up arrays
            string[] inputs = { stringFailBoundsCheck1, stringFailBoundsCheck2, stringFailParamsLength,
                stringFailParamsLength2, stringFailNotFoundNoSpace, stringFailNotFound, stringPass};
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { Controler.FAILED_BOUNDS_CHECK, Controler.FAILED_BOUNDS_CHECK,
                Controler.FAILED_PARAM_LENGTH, Controler.FAILED_PARAM_LENGTH, Controler.FAILED_INSTRUCTION_NOT_FOUND,
                Controler.FAILED_INSTRUCTION_NOT_FOUND, Controler.PASS};
            int[] commands = new int[inputs.Length];
            int[] intendedCommands = { 0, 0, 0, 0, 0, 0, 1 };

            //run tests
            for (int i = 0; i < inputs.Length; i++) { outputs[i] = C.testInstruction(inputs[i]); commands[i] = C.instructionLength(); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
            Assert.AreEqual(commands[6], intendedCommands[6]);
            CollectionAssert.AreEqual(commands, intendedCommands);
        }

        [TestMethod]
        public void SetCutLocationTests()
        {
            //set up controller
            Controler C = new Controler();
            C.SetMaterialBlockDimensions("20", "20");

            //test setup
            string stringFailBoundsCheck1 = $"{Controler.LocationString} 20 -30";
            string stringFailBoundsCheck2 = $"{Controler.LocationString} -30 1";
            string stringFailBoundsCheck3 = $"{Controler.LocationString} 30 10";
            string stringFailBoundsCheck4 = $"{Controler.LocationString} 10 30";
            string stringFailParamsLength = $"{Controler.LocationString}";
            string stringFailParamsLength2 = $"{Controler.LocationString} 20 20 20";
            string stringFailNotFoundNoSpace = $"{Controler.LocationString}20 20";
            string stringFailNotFound = "I will return instruction not found";
            string stringPass = $"{Controler.LocationString} 10 10";
            
            //set up arrays for tests
            string[] inputs = { stringFailBoundsCheck1, stringFailBoundsCheck2,
                stringFailBoundsCheck3, stringFailBoundsCheck4, stringFailParamsLength,
                stringFailParamsLength2, stringFailNotFoundNoSpace, stringFailNotFound, stringPass};
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { Controler.FAILED_BOUNDS_CHECK, Controler.FAILED_BOUNDS_CHECK,
                Controler.FAILED_BOUNDS_CHECK, Controler.FAILED_BOUNDS_CHECK, Controler.FAILED_PARAM_LENGTH,
                Controler.FAILED_PARAM_LENGTH, Controler.FAILED_INSTRUCTION_NOT_FOUND,
                Controler.FAILED_INSTRUCTION_NOT_FOUND, Controler.PASS};
            int[] commands = new int[inputs.Length];
            int[] intendedCommands = { 0, 0, 0, 0, 0, 0, 0, 0, 1 };

            //run tests
            for (int i = 0; i < inputs.Length; i++) { outputs[i] = C.testInstruction(inputs[i]); commands[i] = C.instructionLength(); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
            CollectionAssert.AreEqual(commands, intendedCommands);
        }
        
        [TestMethod]
        public void TestCutNorthTests()
        {
            //set up controler
            Controler C = new Controler();
            C.SetMaterialBlockDimensions("10", "10");

            //test setup
            string stringFailBoundsCheck1 = $"{Controler.CutNorth} 20";
            string stringFailBoundsCheck2 = $"{Controler.CutNorth} -30";
            string stringFailParamsLength = $"{Controler.CutNorth}";
            string stringFailParamsLength2 = $"{Controler.CutNorth} 20 20";
            string stringPass = $"{Controler.CutNorth} 5";

            //set up arrays for tests
            string[] inputs = {stringFailBoundsCheck1, stringFailBoundsCheck2,
                stringFailParamsLength, stringFailParamsLength2, stringPass};
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { Controler.FAILED_BOUNDS_CHECK, Controler.FAILED_BOUNDS_CHECK,
                Controler.FAILED_PARAM_LENGTH, Controler.FAILED_PARAM_LENGTH, Controler.PASS};
            int[] commands = new int[inputs.Length];
            int[] intendedCommands = { 0, 0, 0, 0, 1 };

            //run tests
            for (int i = 0; i < inputs.Length; i++) { outputs[i] = C.testInstruction(inputs[i]); commands[i] = C.instructionLength(); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
            CollectionAssert.AreEqual(commands, intendedCommands);
        }

        [TestMethod]
        public void TestCutSouthTests()
        {
            //set up controler
            Controler C = new Controler();
            C.SetMaterialBlockDimensions("10", "10");
            C.SetCutLocation("5", "5");

            //test setup
            string stringFailBoundsCheck1 = $"{Controler.CutSouth} 20";
            string stringFailBoundsCheck2 = $"{Controler.CutSouth} -30";
            string stringFailParamsLength = $"{Controler.CutSouth}";
            string stringFailParamsLength2 = $"{Controler.CutSouth} 20 20";
            string stringPass = $"{Controler.CutSouth} 5";

            //set up arrays for tests
            string[] inputs = {stringFailBoundsCheck1, stringFailBoundsCheck2,
                stringFailParamsLength, stringFailParamsLength2, stringPass};
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { Controler.FAILED_BOUNDS_CHECK, Controler.FAILED_BOUNDS_CHECK,
                Controler.FAILED_PARAM_LENGTH, Controler.FAILED_PARAM_LENGTH, Controler.PASS};
            int[] commands = new int[inputs.Length];
            int[] intendedCommands = { 0, 0, 0, 0, 1 };

            //run tests
            for (int i = 0; i < inputs.Length; i++) { outputs[i] = C.testInstruction(inputs[i]); commands[i] = C.instructionLength(); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
            CollectionAssert.AreEqual(commands, intendedCommands);
        }
        [TestMethod]
        public void TestCutEastTests()
        {
            //set up controler
            Controler C = new Controler();
            C.SetMaterialBlockDimensions("10", "10");
            C.SetCutLocation("5", "5");

            //test setup
            string stringFailBoundsCheck1 = $"{Controler.CutEast} 20";
            string stringFailBoundsCheck2 = $"{Controler.CutEast} -30";
            string stringFailParamsLength = $"{Controler.CutEast}";
            string stringFailParamsLength2 = $"{Controler.CutEast} 20 20";
            string stringPass = $"{Controler.CutEast} 5";

            //set up arrays for tests
            string[] inputs = {stringFailBoundsCheck1, stringFailBoundsCheck2,
                stringFailParamsLength, stringFailParamsLength2, stringPass};
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { Controler.FAILED_BOUNDS_CHECK, Controler.FAILED_BOUNDS_CHECK,
                Controler.FAILED_PARAM_LENGTH, Controler.FAILED_PARAM_LENGTH, Controler.PASS};
            int[] commands = new int[inputs.Length];
            int[] intendedCommands = { 0, 0, 0, 0, 1 };

            //run tests
            for (int i = 0; i < inputs.Length; i++) { outputs[i] = C.testInstruction(inputs[i]); commands[i] = C.instructionLength(); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
            CollectionAssert.AreEqual(commands, intendedCommands);
        }
        [TestMethod]
        public void TestCutWestTests()
        {
            //set up controler
            Controler C = new Controler();
            C.SetMaterialBlockDimensions("10", "10");
            C.SetCutLocation("5", "5");

            //test setup
            string stringFailBoundsCheck1 = $"{Controler.CutWest} 20";
            string stringFailBoundsCheck2 = $"{Controler.CutWest} -30";
            string stringFailParamsLength = $"{Controler.CutWest}";
            string stringFailParamsLength2 = $"{Controler.CutWest} 20 20";
            string stringPass = $"{Controler.CutWest} 5";

            //set up arrays for tests
            string[] inputs = {stringFailBoundsCheck1, stringFailBoundsCheck2,
                stringFailParamsLength, stringFailParamsLength2, stringPass};
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { Controler.FAILED_BOUNDS_CHECK, Controler.FAILED_BOUNDS_CHECK,
                Controler.FAILED_PARAM_LENGTH, Controler.FAILED_PARAM_LENGTH, Controler.PASS};
            int[] commands = new int[inputs.Length];
            int[] intendedCommands = { 0, 0, 0, 0, 1 };

            //run tests
            for (int i = 0; i < inputs.Length; i++) { outputs[i] = C.testInstruction(inputs[i]); commands[i] = C.instructionLength(); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
            CollectionAssert.AreEqual(commands, intendedCommands);
        }
    }
}
