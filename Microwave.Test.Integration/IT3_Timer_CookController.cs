using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Microwave.Test.Integration
{
    [TestClass]
    public class IT3_Timer_CookController
    {
        private IOutput _output;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IUserInterface _userInterface;
        private CookController _cookController;
        private Timer _uut; 


        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _userInterface = Substitute.For<IUserInterface>();   // nødtil eller kan jeg ikke oprette en instans af cookController når vi deler dependencies op i flere steps? 
            _uut = new Timer();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _cookController = new CookController(_timer, _display, _powerTube, _userInterface);
        }

    //    [TestCase(120)]
    //    public void Start_CookController_receive_event(int newtime)
    //    {
    //        _uut.Start(newtime);
    //        Assert.That(_uut.Received());
    //    }
    }
}
