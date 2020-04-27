using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestClass]
    public class IT2_CookController_Timer
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
            _userInterface = Substitute.For<IUserInterface>();  
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _uut = new CookController(_timer, _display, _powerTube, _userInterface);
        }

        [TestCase(50, 60, 1)]
        [TestCase(350,120,2)]
        [TestCase(350, 120, 3)]
        public void CookController_StartTime(int power, int time, int sleep)
        {
            _uut.StartCooking(power, time);
            Thread.Sleep(sleep*1000+1000);
            _output.Received().OutputLine($"Display shows: {((time - sleep)/60 ):D2}:{((time - sleep) % 60):D2}");
        }

        [TestCase(50, 1)]
        [TestCase(100, 2)]
        [TestCase(250, 3)]
        [TestCase(350, 4)]
        [TestCase(700,5)]
        public void CookController_OnTimerExpiredEvent(int power, int time)
        {
            _uut.StartCooking(power, time);
            Thread.Sleep(time*1000+1000);   //wait for X of ticks to time expired
            _output.Received().OutputLine($"PowerTube turned off");
        }

        [TestCase(50, 10, 1)]
        [TestCase(50, 10, 2)]
        [TestCase(50, 10, 3)]
        public void TimerStopped_display_ShowsDisplayValue_AtStoppedTime(int power, int time, int sleep)
        {
            _uut.StartCooking(power,time);
            Thread.Sleep(sleep* 1000 + 1000);
            _uut.Stop();
            _output.Received().OutputLine($"Display shows: {((time - sleep) / 60):D2}:{((time - sleep) % 60):D2}");
        }

    }



}
