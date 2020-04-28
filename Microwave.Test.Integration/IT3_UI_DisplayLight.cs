using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestClass]
    public class IT3_UI_DisplayLight
    {
        private IOutput _output;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ILight _light;
        private ICookController _cookController;
        private ITimer _timer;
        
        private IUserInterface _uut;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private IDoor _door;

        [SetUp]
        public void SetUp()
        {
            //Fake dependencies
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _output = Substitute.For<IOutput>();

            //Real modules included 
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _timer = new Timer();
            _cookController = new CookController(_timer, _display, _powerTube, _uut);
            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }

        /*----------   UI/Light     ----------------*/

        [Test]
        public void UI_DoorOpenedEvent_TurnOnLight_whenOpened()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void UI_DoorOpenedEvent_TurnOnLight_()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");
            //_output.Received().OutputLine($"Display cleared");
        }

        [Test]
        public void UI_DoorOpenedEvent_TurnOnLight_But_Cleared_display23213()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");
            //_output.Received().OutputLine($"Display cleared");
        }




        /*----------   UI/Display     ----------------*/

        [Test]
        public void UI_DoorOpenedEvent_SetPower_ThenCleared_when_Opened()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine($"Display cleared");
        }


    }
}

