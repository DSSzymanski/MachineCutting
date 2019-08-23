using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MachinCuttingApp;

namespace MachineCuttingUnitTests
{
    [TestClass]
    public class ValidatorUnitTests
    {
        [TestMethod]
        //Test for correct split
        public void TestValidatorParserBase()
        {
            string testString = "Hello my name is dan.";
            string[] intendedResult = { "Hello", "my", "name", "is", "dan" };

            string[] parsedTest = Validator.Parser(testString);

            CollectionAssert.AreEqual(parsedTest, intendedResult);
        }
        [TestMethod]
        //Test to ensure escapes and special characters are removed
        public void TestValidatorParserEscapes()
        {
            string testString = "Hello  \' \" / \\ . \r \n ; hello";
            string[] intendedResult = { "Hello", "hello"};

            string[] parsedTest = Validator.Parser(testString);

            CollectionAssert.AreEqual(parsedTest, intendedResult);
        }

        [TestMethod]
        //Tests answer is an int
        public void TestInputValidation()
        {
            string numAnswer = "1";
            string stringAnswer = "one";
            string emptyAnswer = "";
            string[] inputs = { numAnswer, stringAnswer, emptyAnswer };
            int[] outputs = new int[inputs.Length];
            int[] intendedOutputs = { 1, -1, -1 };

            for (int i = 0; i < inputs.Length; i++) { outputs[i] = Validator.ValidateInput(inputs[i]); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
        }
        [TestMethod]
        //Test val greater or equal to lower bounds
        public void TestLowerBoundCheckPass()
        {
            int val = 0;
            int lowerBoundPass = -1;
            int equal = 0;
            int[] tests = {lowerBoundPass, equal};
            bool[] outputs = new bool[tests.Length];
            bool[] intendedOutputs = { true, true };

            for (int i = 0; i < tests.Length; i++) { outputs[i] = Validator.LowerBoundCheck(tests[i], val); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
        }
        [TestMethod]
        //Test val greater or equal to lower bounds
        public void TestLowerBoundCheckFail()
        {
            int val = 0;
            int lowerBoundFail = 1;
            int[] tests = { lowerBoundFail };
            bool[] outputs = new bool[tests.Length];
            bool[] intendedOutputs = { false };

            for (int i = 0; i < tests.Length; i++) { outputs[i] = Validator.LowerBoundCheck(tests[i], val); }

            CollectionAssert.AreEqual(outputs, intendedOutputs);
        }

        [TestMethod]
        //Test val greater or equal to lower bound and less or equal to upper bound
        public void TestBoundsCheck()
        {
            int val = 0;
            int lowerPass = -1;
            int lowerFail = 1;
            int upperPass = 1;
            int upperFail = -1;
            int equal = 0;
            bool[] outputs = new bool[8];
            bool[] intendedOutputs = { true, true, true, false, false, false, false, false };

            outputs[0] = Validator.BoundsCheck(lowerPass, upperPass, val); //pass
            outputs[1] = Validator.BoundsCheck(lowerPass, equal, val); //pass
            outputs[2] = Validator.BoundsCheck(equal, upperPass, val); //pass
            outputs[3] = Validator.BoundsCheck(lowerFail, upperPass, val); //fail
            outputs[4] = Validator.BoundsCheck(lowerFail, equal, val); //fail
            outputs[5] = Validator.BoundsCheck(lowerPass, upperFail, val); //fail
            outputs[6] = Validator.BoundsCheck(equal, upperFail, val); //fail
            outputs[7] = Validator.BoundsCheck(lowerFail, upperFail, val); //fail

            CollectionAssert.AreEqual(outputs, intendedOutputs);
        }
    }
}
