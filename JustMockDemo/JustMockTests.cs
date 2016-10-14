using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using JustMockDemo.Interfaces;
using JustMockDemo.Models;
using NUnit.Framework;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace JustMockDemo
{
    [TestFixture]
    public class JustMockTests
    {
        private Calculator _sut;

        [SetUp]
        public void InitializeSut()
        {
            this._sut = new Calculator();
        }

        /// <summary>
        /// Testing ohne Mock
        /// Dieser Test testet sowohl die Calculator-Klasse, als auch die AddFunction.
        /// </summary>
        [Test]
        public void Calculator_Works()
        {
            //Arrange
            AddFunction addFunction = new AddFunction();
            addFunction.ValueA = 50;
            addFunction.ValueB = 40;

            //Act
            var result = this._sut.Calculate(addFunction);

            //Assert
            result.Should().Be(90);
        }

        /// <summary>
        /// Für einen wirklichen Unit-Test der Klasse Calculator möchte ich die externen Abhängigkeiten (hier AddFunction) mocken,
        /// damit tatsächlich nur die Funktionalität der Calculator-Klasse getestet wird.
        /// </summary>
        [Test]
        public void JustMock_Can_Create_Mock_Of_Class()
        {
            //Arrange
            var functionMock = Mock.Create<AddFunction>();
            functionMock.ValueA = 10;
            functionMock.ValueB = 20;
            Mock.Arrange(() => functionMock.Calc()).CallOriginal();

            //Act
            var result = this._sut.Calculate(functionMock);

            //Assert
            result.Should().Be(30);
        }

        /// <summary>
        /// Mit dem vorherigen Test wurde noch nicht viel erreicht, da JustMock aus der Klasse AddFunction zwar ein Mock-Objekt erzeugt hat,
        /// das Objekt aber weiterhin die Funktionalität der ursprünglichen Klasse implementiert.
        /// Für das Mocken ist es nun also wichtig, diese ursprüngliche Funktionalität zu entfernen.
        /// Vorsicht: Funktioniert in der Gratis-Version von Just Mock nur auf virtuelle Methoden.
        /// </summary>
        [Test]
        public void JustMock_Can_Create_Mock_Of_Class_And_Method_Can_Be_Mocked()
        {
            //Arrange
            var functionMock = Mock.Create<AddFunction>();

            Mock.Arrange(() => functionMock.Calc()).Returns((t) => 99);

            //Act
            var result = this._sut.Calculate(functionMock);

            //Assert
            result.Should().Be(99);
        }

        /// <summary>
        /// JustMock kann auch Mocks aus Interfaces erstellen.
        /// </summary>
        [Test]
        public void JustMock_Can_Create_Mock_Of_Interface_And_Method_Can_Be_Mocked()
        {
            //Arrange
            var functionMock = Mock.Create<IAdd>();
            Mock.Arrange(() => functionMock.Calc()).Returns((t) => 99);

            //Act
            var result = this._sut.Calculate(functionMock);

            //Assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Um auch nicht virtuelle Methoden überschreiben zu können wird die Vollversion von JustMock benötigt.
        /// Zudem muss in Visual Studio in der Menüleiste JustMock/Enable Profiler der Profiler aktiviert werden.
        /// Dabei ist zu beachten, dass Telerik offiziell Build vNext des TFS beim Ausführen der Tests mit Profiler (noch) nicht unterstützt.
        /// Das ganze lässt sich mit erheblichen Konfigurationsaufwand aber dennoch lösen.
        /// Daher die Empfehlung: Code so refaktorieren, so dass es Interfaces gibt und keine nicht virtuelle Methoden überschrieben werden müssen.
        /// </summary>
        [Test]
        public void JustMock_Can_Create_Mock_Of_Class_With_Non_Virtual_Method()
        {
            Mock.IsProfilerEnabled.Should().BeTrue();

            //Arrange
            var functionMock = Mock.Create<AddFunctionWithNonVirtualMethod>();
            Mock.Arrange(() => functionMock.Calc()).Returns((t) => 99);

            //Act
            var result = this._sut.Calculate(functionMock);

            //Assert
            result.Should().Be(99);
        }

        [Test]
        public void JustMock_Can_Test_If_Method_Was_Called()
        {
            Mock.IsProfilerEnabled.Should().BeTrue();

            //Arrange
            var functionMock = Mock.Create<AddFunctionWithNonVirtualMethod>();
            Mock.Arrange(() => functionMock.Calc()).Returns((t) => 99).MustBeCalled();

            //Act
            //Methode wird nicht aufgerufen!
            //var result = this._sut.Calculate(functionMock);

            //Assert
            Action act = () => Mock.Assert(functionMock);
            act.ShouldThrow<AssertionException>().WithMessage("Occurrence expectation failed. Expected at least 1 call. Calls so far: 0*");
        }

        [Test]
        public void JustMock_Can_Mock_Properties()
        {
            Mock.IsProfilerEnabled.Should().BeTrue();

            //Arrange
            var functionMock = Mock.Create<AddFunctionWithNonVirtualMethod>();
            Mock.Arrange(() => functionMock.ValueA).Returns(7);
            Mock.Arrange(() => functionMock.ValueB).Returns(8);
            Mock.Arrange(() => functionMock.Calc()).CallOriginal();

            //Act
            var result = this._sut.Calculate(functionMock);

            //Assert
            result.Should().Be(15);
        }

        [Test]
        public void JustMock_Can_Mock_Methods_With_Parameters_And_Filter()
        {
            Mock.IsProfilerEnabled.Should().BeTrue();

            //Arrange
            var functionMock = Mock.Create<ObjectFunction>();

            //Hier ist die Reihenfolge wichtig! Es muss erst der allgemeinere Filter (Arg.AnyObject) und dann die spezielleren
            //Filter (Arg.AnyInt und Arg.AnyBool) angegeben werden.
            Mock.Arrange<object>(() => functionMock.Calc(Arg.AnyObject)).CallOriginal();
            Mock.Arrange<object>(() => functionMock.Calc(Arg.AnyInt)).Returns("Das war ein Integer");
            Mock.Arrange<object>(() => functionMock.Calc(Arg.AnyBool)).Returns("Das war ein Bool");

            //Act
            var resultWithIntParameter = this._sut.Calculate(functionMock, 16);
            var resultWithBoolParameter = this._sut.Calculate(functionMock, true);
            var resultWithDecimalParameter = this._sut.Calculate(functionMock, 16.3m);

            //Assert
            resultWithIntParameter.Should().Be("Das war ein Integer");
            resultWithBoolParameter.Should().Be("Das war ein Bool");
            resultWithDecimalParameter.Should().Be(16.3m);
        }

        [Test]
        public void JustMock_Has_Reflection_Helper_Methods_To_Set_Fields_Of_Static_Classes()
        {
            var configurationHolderAccessor = PrivateAccessor.ForType(typeof(StaticClassWithGetterOnly));
            configurationHolderAccessor.SetField("_value", "Geänderter Wert");

            StaticClassWithGetterOnly.Value.Should().Be("Geänderter Wert");
        }

        [Test]
        public void JustMock_Can_Mock_Static_Constructors()
        {
            Mock.IsProfilerEnabled.Should().BeTrue();

            Mock.SetupStatic(typeof(StaticClassWhichThrowsExceptionInConstructor), StaticConstructor.Mocked);

            Mock.Arrange(() => StaticClassWhichThrowsExceptionInConstructor.DoSomTingWong()).CallOriginal();

            StaticClassWhichThrowsExceptionInConstructor.DoSomTingWong();
        }

        /*
         * 
         *     Alternativen zu JustMock:
         *        • NSubstitute
         *        • Rhino Mocks
         *        • Moq
         *        • FakeItEasy
         *        • NMock3
         * 
         */
    }
}
