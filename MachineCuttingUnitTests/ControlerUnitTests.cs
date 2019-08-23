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
            Controller C = new Controller();

            //set up test strings
            string stringFailBoundsCheck1 = $"{Controller.DimensionString} 20 -30";
            string stringFailBoundsCheck2 = $"{Controller.DimensionString} -30 1";
            string stringFailParamsLength = $"{Controller.DimensionString}";
            string stringFailParamsLength2 = $"{Controller.DimensionString} 20 20 20";
            string stringFailNotFoundNoSpace = $"{Controller.DimensionString}20 20";
            string stringFailNotFound = "I will return instruction not found";
            string stringPass = $"{Controller.DimensionString} 20 20";
            
            //set up arrays
            string[] inputs = { stringFailBoundsCheck1, stringFailBoundsCheck2, stringFailParamsLength,
                stringFailParamsLength2, stringFailNotFoundNoSpace, stringFailNotFound, stringPass};
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { Controller.FAILED_BOUNDS_CHECK, Controller.FAILED_BOUNDS_CHECK,
                Controller.FAILED_PARAM_LENGTH, Controller.FAILED_PARAM_LENGTH, Controller.FAILED_INSTRUCTION_NOT_FOUND,
                Controller.FAILED_INSTRUCTION_NOT_FOUND, Controller.PASS};
            int[] commands = new int[inputs.Length];
            int[] intendedCommands = { 0, 0, 0, 0, 0, 0, 1 };

            //run tests
            for (int i = 0; i < inputs.Length; i++) { outputs[i] = C.TestInstruction(inputs[i]); commands[i] = C.InstructionLength(); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
            Assert.AreEqual(commands[6], intendedCommands[6]);
            CollectionAssert.AreEqual(commands, intendedCommands);
        }

        [TestMethod]
        public void SetCutLocationTests()
        {
            //set up controller
            Controller C = new Controller();
            C.SetMaterialBlockDimensions("20", "20");

            //test setup
            string stringFailBoundsCheck1 = $"{Controller.LocationString} 20 -30";
            string stringFailBoundsCheck2 = $"{Controller.LocationString} -30 1";
            string stringFailBoundsCheck3 = $"{Controller.LocationString} 30 10";
            string stringFailBoundsCheck4 = $"{Controller.LocationString} 10 30";
            string stringFailParamsLength = $"{Controller.LocationString}";
            string stringFailParamsLength2 = $"{Controller.LocationString} 20 20 20";
            string stringFailNotFoundNoSpace = $"{Controller.LocationString}20 20";
            string stringFailNotFound = "I will return instruction not found";
            string stringPass = $"{Controller.LocationString} 10 10";
            
            //set up arrays for tests
            string[] inputs = { stringFailBoundsCheck1, stringFailBoundsCheck2,
                stringFailBoundsCheck3, stringFailBoundsCheck4, stringFailParamsLength,
                stringFailParamsLength2, stringFailNotFoundNoSpace, stringFailNotFound, stringPass};
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { Controller.FAILED_BOUNDS_CHECK, Controller.FAILED_BOUNDS_CHECK,
                Controller.FAILED_BOUNDS_CHECK, Controller.FAILED_BOUNDS_CHECK, Controller.FAILED_PARAM_LENGTH,
                Controller.FAILED_PARAM_LENGTH, Controller.FAILED_INSTRUCTION_NOT_FOUND,
                Controller.FAILED_INSTRUCTION_NOT_FOUND, Controller.PASS};
            int[] commands = new int[inputs.Length];
            int[] intendedCommands = { 0, 0, 0, 0, 0, 0, 0, 0, 1 };

            //run tests
            for (int i = 0; i < inputs.Length; i++) { outputs[i] = C.TestInstruction(inputs[i]); commands[i] = C.InstructionLength(); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
            CollectionAssert.AreEqual(commands, intendedCommands);
        }
        
        [TestMethod]
        public void TestCutNorthTests()
        {
            //set up controler
            Controller C = new Controller();
            C.SetMaterialBlockDimensions("10", "10");

            //test setup
            string stringFailBoundsCheck1 = $"{Controller.CutNorth} 20";
            string stringFailBoundsCheck2 = $"{Controller.CutNorth} -30";
            string stringFailParamsLength = $"{Controller.CutNorth}";
            string stringFailParamsLength2 = $"{Controller.CutNorth} 20 20";
            string stringPass = $"{Controller.CutNorth} 5";

            //set up arrays for tests
            string[] inputs = {stringFailBoundsCheck1, stringFailBoundsCheck2,
                stringFailParamsLength, stringFailParamsLength2, stringPass};
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { Controller.FAILED_BOUNDS_CHECK, Controller.FAILED_BOUNDS_CHECK,
                Controller.FAILED_PARAM_LENGTH, Controller.FAILED_PARAM_LENGTH, Controller.PASS};
            int[] commands = new int[inputs.Length];
            int[] intendedCommands = { 0, 0, 0, 0, 1 };

            //run tests
            for (int i = 0; i < inputs.Length; i++) { outputs[i] = C.TestInstruction(inputs[i]); commands[i] = C.InstructionLength(); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
            CollectionAssert.AreEqual(commands, intendedCommands);
        }

        [TestMethod]
        public void TestCutSouthTests()
        {
            //set up controler
            Controller C = new Controller();
            C.SetMaterialBlockDimensions("10", "10");
            C.SetCutLocation("5", "5");

            //test setup
            string stringFailBoundsCheck1 = $"{Controller.CutSouth} 20";
            string stringFailBoundsCheck2 = $"{Controller.CutSouth} -30";
            string stringFailParamsLength = $"{Controller.CutSouth}";
            string stringFailParamsLength2 = $"{Controller.CutSouth} 20 20";
            string stringPass = $"{Controller.CutSouth} 5";

            //set up arrays for tests
            string[] inputs = {stringFailBoundsCheck1, stringFailBoundsCheck2,
                stringFailParamsLength, stringFailParamsLength2, stringPass};
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { Controller.FAILED_BOUNDS_CHECK, Controller.FAILED_BOUNDS_CHECK,
                Controller.FAILED_PARAM_LENGTH, Controller.FAILED_PARAM_LENGTH, Controller.PASS};
            int[] commands = new int[inputs.Length];
            int[] intendedCommands = { 0, 0, 0, 0, 1 };

            //run tests
            for (int i = 0; i < inputs.Length; i++) { outputs[i] = C.TestInstruction(inputs[i]); commands[i] = C.InstructionLength(); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
            CollectionAssert.AreEqual(commands, intendedCommands);
        }
        [TestMethod]
        public void TestCutEastTests()
        {
            //set up controler
            Controller C = new Controller();
            C.SetMaterialBlockDimensions("10", "10");
            C.SetCutLocation("5", "5");

            //test setup
            string stringFailBoundsCheck1 = $"{Controller.CutEast} 20";
            string stringFailBoundsCheck2 = $"{Controller.CutEast} -30";
            string stringFailParamsLength = $"{Controller.CutEast}";
            string stringFailParamsLength2 = $"{Controller.CutEast} 20 20";
            string stringPass = $"{Controller.CutEast} 5";

            //set up arrays for tests
            string[] inputs = {stringFailBoundsCheck1, stringFailBoundsCheck2,
                stringFailParamsLength, stringFailParamsLength2, stringPass};
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { Controller.FAILED_BOUNDS_CHECK, Controller.FAILED_BOUNDS_CHECK,
                Controller.FAILED_PARAM_LENGTH, Controller.FAILED_PARAM_LENGTH, Controller.PASS};
            int[] commands = new int[inputs.Length];
            int[] intendedCommands = { 0, 0, 0, 0, 1 };

            //run tests
            for (int i = 0; i < inputs.Length; i++) { outputs[i] = C.TestInstruction(inputs[i]); commands[i] = C.InstructionLength(); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
            CollectionAssert.AreEqual(commands, intendedCommands);
        }
        [TestMethod]
        public void TestCutWestTests()
        {
            //set up controler
            Controller C = new Controller();
            C.SetMaterialBlockDimensions("10", "10");
            C.SetCutLocation("5", "5");

            //test setup
            string stringFailBoundsCheck1 = $"{Controller.CutWest} 20";
            string stringFailBoundsCheck2 = $"{Controller.CutWest} -30";
            string stringFailParamsLength = $"{Controller.CutWest}";
            string stringFailParamsLength2 = $"{Controller.CutWest} 20 20";
            string stringPass = $"{Controller.CutWest} 5";

            //set up arrays for tests
            string[] inputs = {stringFailBoundsCheck1, stringFailBoundsCheck2,
                stringFailParamsLength, stringFailParamsLength2, stringPass};
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { Controller.FAILED_BOUNDS_CHECK, Controller.FAILED_BOUNDS_CHECK,
                Controller.FAILED_PARAM_LENGTH, Controller.FAILED_PARAM_LENGTH, Controller.PASS};
            int[] commands = new int[inputs.Length];
            int[] intendedCommands = { 0, 0, 0, 0, 1 };

            //run tests
            for (int i = 0; i < inputs.Length; i++) { outputs[i] = C.TestInstruction(inputs[i]); commands[i] = C.InstructionLength(); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
            CollectionAssert.AreEqual(commands, intendedCommands);
        }
    }
}
