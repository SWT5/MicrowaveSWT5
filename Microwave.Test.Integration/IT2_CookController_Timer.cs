using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

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
    }


    [SetUp]
    public void SetUp()
    {
    _output = Substitute.For<IOutput>();
    _userInterface = Substitute.For<IUserInterface>();  // nødtil eller kan jeg ikke oprette en instans af cookController når vi deler dependencies op i flere steps? 
    _timer = Substitute.For<ITimer>();
    _powerTube = new PowerTube(_output);
    _display = new Display(_output);

    _uut = new CookController(_timer, _display, _powerTube, _userInterface);
    }

}
