using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomapperDemo;
using FluentAssertions;
using FluentAssertionsDemo.Models;
using NUnit.Framework;

namespace FluentAssertionsDemo
{
    [TestFixture]
    public class FluentAssertionsTests
    {
        [Test]
        public void FluentAssertions_Can_Check_Property_Equality()
        {
            string a = "Hallo";
            string b = "Hallo";

            a.Should().Be(b);
        }

        [Test]
        public void FluentAssertions_Can_Chain_EqualityChecks()
        {
            string a = "Hallo";

            a.Should().StartWith("Ha").And.EndWith("lo").And.Contain("al").And.HaveLength(5);
        }

        [Test]
        public void FluentAssertions_Can_Check_For_Exceptions()
        {
            //Act
            Action act = () => new ExceptionThrower();

            //Assert
            act.ShouldThrow<FormatException>().WithInnerException<NoNullAllowedException>().WithMessage("Fehlerhaftes Format!").WithInnerMessage("Null ist nicht erlaubt!");
        }

        [Test]
        public void FluentAssertions_Can_Check_For_Exceptions_WithWildCards()
        {
            //Act
            Action act = () => new ExceptionThrower();

            //Assert
            act.ShouldThrow<FormatException>().WithMessage("?ehlerhaftes*");
        }

        [Test]
        public void FluentAssertions_Can_Check_Equality_With_Wildcards()
        {
            string a = "Hallo";

            a.Should().MatchEquivalentOf("?allo");
        }

        [Test]
        public void FluentAssertions_Can_Check_Assembly_References()
        {
            typeof(AutoMapperTests).Assembly.Should().Reference(typeof(AutoMapper.Mapper).Assembly);
            typeof(AutoMapperTests).Assembly.Should().NotReference(typeof(FluentAssertionsTests).Assembly);
        }

        [Test]
        public void FluentAssertions_Can_Check_Execution_Time_Of_Methods()
        {
            var sut = new SomePotentiallyVerySlowClass();

            sut.ExecutionTimeOf(s => s.ExpensiveMethod()).ShouldNotExceed(500.Milliseconds());
        }

        [Test]
        public void FluentAssertions_Can_Check_DateTimes()
        {
            var theDatetime = 1.March(2010).At(22, 15);

            theDatetime.Should().BeAfter(1.February(2010));
            theDatetime.Should().BeBefore(2.March(2010));
            theDatetime.Should().BeOnOrAfter(1.March(2010));

            theDatetime.Should().Be(1.March(2010).At(22, 15));
            theDatetime.Should().NotBe(1.March(2010).At(22, 16));

            theDatetime.Should().HaveDay(1);
            theDatetime.Should().HaveMonth(3);
            theDatetime.Should().HaveYear(2010);
            theDatetime.Should().HaveHour(22);
            theDatetime.Should().HaveMinute(15);
            theDatetime.Should().HaveSecond(0);

            theDatetime.Should().BeSameDateAs(1.March(2010));
        }


        /*
         * 
         * Sehr ausführliche FluentAssertions Doku:
         * https://github.com/dennisdoomen/fluentassertions/wiki
         * 
         */
    }
}
