using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
//using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Assert = NUnit.Framework.Assert;

namespace Microwave.Test.Integration
{
    [TestClass]
    public class IT1_CookController_PowertubeDisplay
    {
        
        private IOutput _output;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IUserInterface _userInterface; 
        private CookController _uut; 


        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _userInterface = Substitute.For<IUserInterface>();  // nødtil eller kan jeg ikke oprette en instans af cookController når vi deler dependencies op i flere steps? 
            _timer = Substitute.For<ITimer>();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);

            _uut = new CookController(_timer,_display, _powerTube, _userInterface);
        }

        [TestCase(1, 60)]
        [TestCase(50, 60)]
        [TestCase(99, 60)]
        [TestCase(100, 60)]
        //[TestCase(700, 60)]       //kig på opgaven - der står 700
        public void CookCtrTurnOnPowerTube_TimeSixty(int power,int time)
        {
            _uut.StartCooking(power, time);
            _output.Received().OutputLine($"PowerTube works with {power}");
            _timer.Received().Start(time);
        }


        [TestCase(0, 0)]
        [TestCase(-1, 0)]
        [TestCase(101, 0)]
        public void CookCtrTurnOnPowerTube_WrongPowerValue(int power, int time)
        {
            Assert.That(() => _uut.StartCooking(power, time), Throws.TypeOf<ArgumentOutOfRangeException>());
        }


        [TestCase(1, 60)]
        [TestCase(50, 60)]
        [TestCase(99, 60)]
        [TestCase(100, 60)]
        public void StartCooking_Twice(int power, int time)
        {
            _uut.StartCooking(power, time);
            
            Assert.That(()=> _uut.StartCooking(power, time), Throws.TypeOf<ApplicationException>());
        }

    }
}
