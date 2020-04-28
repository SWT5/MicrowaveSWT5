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
            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light,
                _cookController);
        }

        /*----------   UI/Light     ----------------*/

        [Test]
        public void UI_DoorOpenedEvent_TurnOnLight_READY_state()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void UI_DoorOpenedEvent_TurnOnLight_SETPOWER_state()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");
            //_output.Received().OutputLine($"Display cleared");
        }

        [Test]
        public void UI_DoorOpenedEvent_TurnOnLight_SETTIME_state()
        {
            _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void UI_DoorClosedEvent_TurnOffLight_READY_state()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned off");
        }


        [Test]
        public void UI_OnStartCancelPressedEvent_SETPOWER_STATE()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned off");
        }

        [Test]
        public void UI_OnStartCancelPressedEvent_SETTIME_STATE()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned on");
        }


        [Test]
        public void UI_OnStartCancelPressedEvent_COOKING_STATE()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine("Light is turned off");
        }




        /*----------   UI/Display     ----------------*/

        [Test]
        public void UI_DoorOpenedEvent_SetPower_ThenCleared_when_Opened()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine($"Display cleared");
        }

        [Test]
        public void UI_OnPowerPressedEvent_ShowPower_OnDisplay()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine($"Display shows: {50} W");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void UI_OnPowerPressedEvent_ShowPower_OnDisplay(int timespressed)
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            for (int i = 0; i < timespressed; i++)
            {
                _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            }
            _output.Received().OutputLine($"Display shows: {(50+50*timespressed)} W");
        }


        [Test]
        public void UI_OnTimePressedEvent_ShowTime_OnDisplay()
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received().OutputLine($"Display shows: {(60 / 60):D2}:{(60 % 60):D2}");

        }

        
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void UI_OnTimePressedEvent_ShowTime_OnDisplay(int timespressed)
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            for (int i = 0; i < timespressed; i++)
            {
                _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            }
            _output.Received().OutputLine($"Display shows: {(60*timespressed /60):D2}:{(60 * timespressed % 60):D2}");

        }


    }
}

