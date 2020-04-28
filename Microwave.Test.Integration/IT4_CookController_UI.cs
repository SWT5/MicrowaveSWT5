using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    class IT4_CookController_UI
    {
        private IOutput _output;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ILight _light;
        private ICookController _uut;
        private ITimer _timer;

        private IUserInterface _userInterface;
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
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _uut);
            _uut = new CookController(_timer, _display, _powerTube, _userInterface);
        }

        /*___________________________________________________*/

        [TestCase(5)]
        public void CookControlUIOnTimeExpired(int sleep)
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(sleep, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            Thread.Sleep( sleep*1000 + 1000);  
            _output.Received().OutputLine($"Display cleared");
        }

    }
}
