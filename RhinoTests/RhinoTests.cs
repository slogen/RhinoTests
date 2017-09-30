using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Rhino.Mocks;

namespace RhinoTests
{
    public class RhinoTests
    {
        public class Foo
        {
            public virtual string Bar { get; set; }
        }
        [Test]
        public static void TestMockPropertyInsideRepo()
        {
            var mockRepo = new MockRepository();
            var foo = mockRepo.DynamicMock<Foo>();
            long i = 0;
            Func<string> cb = () => Interlocked.Increment(ref i).ToString();
            foo.Stub(x => x.Bar)
                .WhenCalled(mi => mi.ReturnValue = cb())
                .Return(default(string));
            mockRepo.ReplayAll();
            foo.Bar.Should().Be("1");
            foo.Bar.Should().Be("2");
        }
        [Test]
        public static void TestMockPropertyOnGlobal()
        {
            var foo = MockRepository.GenerateMock<Foo>();
            long i = 0;
            Func<string> cb = () => Interlocked.Increment(ref i).ToString();
            foo.Stub(x => x.Bar)
                .WhenCalled(mi => mi.ReturnValue = cb())
                .Return(default(string));
            foo.Bar.Should().Be("1");
            foo.Bar.Should().Be("2");
        }
    }
}
